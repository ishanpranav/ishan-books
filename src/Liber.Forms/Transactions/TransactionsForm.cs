// TransactionsForm.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Humanizer;
using Liber.Forms.AccountViews;
using Liber.Forms.Forms;
using Liber.Forms.Lines;
using Liber.Forms.LineSources;
using Liber.Forms.Properties;
using Liber.MathEngine.Expressions;

namespace Liber.Forms.Transactions;

internal partial class TransactionsForm : Form
{
    private readonly Company _company;
    private readonly FormFactory _factory;
    private readonly List<Line> _lines = new List<Line>();
    private readonly ILineSource _source;

    private bool _pendingInitialization;
    private int _selectedLineIndex = -1;
    private int? _editingIndex;

    public TransactionsForm(Company company, ILineSource source, FormFactory factory, Line line) :
        this(company, source, factory)
    {
        SelectIndex(_lines.IndexOf(line));
    }

    public TransactionsForm(Company company, ILineSource source, FormFactory factory)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);
        Design.ApplyStyles(_contextMenu);

        AccountViewBindingList bindingList = new AccountViewBindingList(company, x => !x.ReadOnly);

        bindingList.AddNullAccount();

        accountColumn.DataSource = bindingList;
        accountColumn.ValueMember = nameof(AccountView.Id);
        accountColumn.DisplayMember = nameof(AccountView.DisplayName);

        _dataGridView.SetCompanyColor(source.Color);

        _dataGridView.DebitColumnIndex = debitColumn.Index;
        _dataGridView.CreditColumnIndex = creditColumn.Index;
        company.AccountUpdated += OnCompanyAccountUpdated;
        company.AccountRemoved += OnCompanyAccountRemoved;
        company.TransactionAdded += OnCompanyTransactionAdded;
        company.TransactionUpdated += OnCompanyTransactionUpdated;
        company.TransactionReconciled += OnCompanyTransactionReconciled;
        company.TransactionRemoved += OnCompanyTransactionRemoved;
        _company = company;
        postedColumn.ValueType = typeof(DateTime);
        balanceColumn.ValueType = typeof(decimal);
        balanceColumn.DefaultCellStyle.Font = _dataGridView.ColumnHeadersDefaultCellStyle.Font;
        balanceColumn.DefaultCellStyle.Format = DecimalExtensions.Format;
        Text = source.Name;
        _source = source;
        _factory = factory;

        InitializeLines();

        if (_dataGridView.Rows.Count > 0)
        {
            _dataGridView.CurrentCell = _dataGridView[0, _dataGridView.Rows.Count - 1];
        }
    }

    private void OnCompanyAccountUpdated(object? sender, GuidEventArgs e)
    {
        if (_source.IsInvalidatedByAccountUpdated(e.Id))
        {
            InvalidateTransactions();
        }
    }

    private void OnCompanyAccountRemoved(object? sender, GuidEventArgs e)
    {
        if (_source.IsInvalidatedByAccountRemoved(e.Id))
        {
            Close();
        }
    }

    private void OnCompanyTransactionAdded(object? sender, GuidEventArgs e)
    {
        if (_source.IsInvalidatedByTransactionAdded(e.Id))
        {
            InvalidateTransactions();
        }
    }

    private void OnCompanyTransactionUpdated(object? sender, GuidEventArgs e)
    {
        InvalidateTransactions();
    }

    private void OnCompanyTransactionReconciled(object? sender, GuidEventArgs e)
    {
        if (_source.IsInvalidatedByTransactionReconciled(e.Id))
        {
            InvalidateTransactions();
        }
    }

    private void OnCompanyTransactionRemoved(object? sender, GuidEventArgs e)
    {
        if (_source.IsInvalidatedByAccountRemoved(e.Id))
        {
            InvalidateTransactions();
        }
    }

    private void InvalidateTransactions()
    {
        int previousSelectedLineIndex = _selectedLineIndex;

        InitializeLines();

        if (previousSelectedLineIndex != -1 && previousSelectedLineIndex < _lines.Count)
        {
            SelectIndex(previousSelectedLineIndex);

            return;
        }

        if (_dataGridView.Rows.Count > 0)
        {
            _dataGridView.CurrentCell = _dataGridView[0, _dataGridView.Rows.Count - 1];
        }
    }

    private void InitializeLines()
    {
        if (_pendingInitialization)
        {
            return;
        }

        _selectedLineIndex = -1;
        _pendingInitialization = true;

        _lines.Clear();
        _lines.AddRange(_source.GetOrderedLines());
        _dataGridView.SuspendLayout();

        AccountType type = _source.GetRepresentativeType();

        try
        {
            _dataGridView.Rows.Clear();

            decimal runningBalance = 0;

            for (int i = 0; i < _lines.Count; i++)
            {
                Line line = _lines[i];
                Transaction transaction = line.Transaction;
                DataGridViewRow top = new DataGridViewRow()
                {
                    Tag = new RegisterRow(i)
                };

                runningBalance += line.Balance;

                top.CreateCells(
                    _dataGridView,
                    transaction.Posted,
                    transaction.Number.ToStringOrEmpty(),
                    transaction.Name ?? string.Empty,
                    Guid.Empty,
                    line.Reconciled != null,
                    new DecimalExpression(line.Debit),
                    new DecimalExpression(line.Credit),
                    type.Toggle(runningBalance));

                DataGridViewRow bottom = new DataGridViewRow()
                {
                    Tag = new RegisterRow(i)
                };

                bottom.CreateCells(
                    _dataGridView,
                    string.Empty,
                    _company.GetLineType(line, type).Humanize(),
                    transaction.Memo ?? string.Empty,
                    _source.GetRepresentativeAccountId(line),
                    false,
                    string.Empty,
                    string.Empty,
                    string.Empty);

                if (!_source.CanEditAccount(line))
                {
                    top.Cells[debitColumn.Index].ReadOnly = true;
                    top.Cells[creditColumn.Index].ReadOnly = true;
                    bottom.Cells[accountColumn.Index].ReadOnly = true;
                }

                AddDoubleRow(top, bottom);
            }

            DataGridViewRow newTop = new DataGridViewRow()
            {
                Tag = new RegisterRow(-1)
            };

            newTop.CreateCells(
                _dataGridView,
                Settings.Default.LastPosted,
                string.Empty,
                string.Empty,
                Guid.Empty,
                false,
                new DecimalExpression(0),
                new DecimalExpression(0),
                type.Toggle(runningBalance));

            DataGridViewRow newBottom = new DataGridViewRow()
            {
                Tag = new RegisterRow(-1)
            };

            newBottom.CreateCells(
                _dataGridView,
                string.Empty,
                string.Empty,
                string.Empty,
                Guid.Empty,
                false,
                string.Empty,
                string.Empty,
                string.Empty);
            AddDoubleRow(newTop, newBottom);
        }
        finally
        {
            _dataGridView.ResumeLayout();

            _pendingInitialization = false;
        }
    }

    private void AddDoubleRow(DataGridViewRow top, DataGridViewRow bottom)
    {
        top.Cells[accountColumn.Index].ReadOnly = true;
        bottom.Cells[postedColumn.Index].ReadOnly = true;
        bottom.Cells[debitColumn.Index].ReadOnly = true;
        bottom.Cells[creditColumn.Index].ReadOnly = true;

        _dataGridView.Rows.Add(top);
        _dataGridView.Rows.Add(bottom);
    }

    private Line? GetValue(int index)
    {
        if (index >= _lines.Count)
        {
            return null;
        }

        return _lines[index];
    }

    private bool Save(int index, out DateTime posted)
    {
        int topIndex = index * 2;
        int bottomIndex = topIndex + 1;

        if (topIndex < 0 || bottomIndex >= _dataGridView.Rows.Count)
        {
            posted = default;

            return false;
        }

        DataGridViewRow top = _dataGridView.Rows[topIndex];

        top.ErrorText = string.Empty;

        if (!decimal.TryParse(_dataGridView[numberTypeColumn.Index, topIndex].Value?.ToString(), out decimal number))
        {
            number = 0;
        }

        if (!_dataGridView.TryGetBalance(top, out decimal balance))
        {
            posted = default;

            return false;
        }

        Line? currentLine = GetValue(index);

        if (_dataGridView.Rows[bottomIndex].Cells[accountColumn.Index].Value is not Guid siblingId)
        {
            top.ErrorText = Properties.Resources.InvalidAccountError;
            posted = default;

            return false;
        }

        if (siblingId == Guid.Empty)
        {
            if (currentLine == null && !_source.CanGetNewLines(siblingId))
            {
                top.ErrorText = Properties.Resources.InvalidAccountError;
                posted = default;

                return false;
            }

            if (currentLine != null && _source.CanEditAccount(currentLine))
            {
                top.ErrorText = Properties.Resources.InvalidAccountError;
                posted = default;

                return false;
            }
        }

        if (_dataGridView[postedColumn.Index, topIndex].Value is not DateTime postedValue)
        {
            top.ErrorText = Properties.Resources.InvalidAccountError;
            posted = default;

            return false;
        }

        posted = postedValue;

        bool addingNew;
        Transaction transaction;

        if (currentLine == null)
        {
            transaction = new Transaction();
            addingNew = true;
        }
        else
        {
            transaction = currentLine.Transaction;
            addingNew = false;
        }

        transaction.Memo = _dataGridView[nameMemoColumn.Index, bottomIndex].Value?.ToString();

        string? name = _dataGridView[nameMemoColumn.Index, topIndex].Value?.ToString();
        IReadOnlyCollection<Line> lines = currentLine != null && !_source.CanEditAccount(currentLine)
            ? transaction.Lines
            : _source.GetNewLines(siblingId, balance);

        if (addingNew)
        {
            _company.AddTransaction(transaction, name, lines);
        }

        _company.UpdateTransaction(transaction.Id, number, name, posted, lines);

        return true;
    }

    private bool RowHasContent(int index)
    {
        int topIndex = index * 2;
        int bottomIndex = topIndex + 1;

        if (topIndex < 0 || bottomIndex >= _dataGridView.Rows.Count)
        {
            return false;
        }

        object? name = _dataGridView[nameMemoColumn.Index, topIndex].Value;
        object? memo = _dataGridView[nameMemoColumn.Index, bottomIndex].Value;
        object? number = _dataGridView[numberTypeColumn.Index, topIndex].Value;
        object? account = _dataGridView[accountColumn.Index, bottomIndex].Value;

        if (!string.IsNullOrEmpty(name?.ToString()) ||
            !string.IsNullOrEmpty(memo?.ToString()) ||
            !string.IsNullOrEmpty(number?.ToString()))
        {
            return true;
        }

        if (account is Guid accountId && accountId != Guid.Empty)
        {
            return true;
        }

        if (_dataGridView.TryGetBalance(_dataGridView.Rows[topIndex], out decimal balance) && balance != 0)
        {
            return true;
        }

        return false;
    }

    private void CommitChanges(int index)
    {
        if (index >= _lines.Count && !RowHasContent(index))
        {
            return;
        }

        int topIndex = index * 2;

        if (topIndex < 0 || topIndex >= _dataGridView.Rows.Count)
        {
            return;
        }

        BeginInvoke(() =>
        {
            if (Save(index, out DateTime posted))
            {
                TransactionHelpers.Post(posted);
            }
        });
    }

    private void RevertChanges()
    {
        if (_editingIndex == null)
        {
            return;
        }

        _editingIndex = null;
        _dataGridView.CancelEdit();
    }

    public void SelectLine(Line value)
    {
        if (_source.Contains(value))
        {
            SelectIndex(_lines.IndexOf(value));
        }
    }

    private void SelectIndex(int value)
    {
        _selectedLineIndex = (value < 0 || value >= _lines.Count) ? -1 : value;

        int rowIndex = value * 2;

        if (rowIndex < _dataGridView.Rows.Count)
        {
            _dataGridView.FirstDisplayedScrollingRowIndex = rowIndex;
            _dataGridView.CurrentCell = _dataGridView.Rows[rowIndex].Cells[nameMemoColumn.Index];

            _dataGridView.Invalidate();
        }
    }

    private void OnDataGridViewCellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex == -1)
        {
            return;
        }

        RegisterRow row = (RegisterRow)_dataGridView.Rows[e.RowIndex].Tag!;

        _selectedLineIndex = row.LineIndex;

        _dataGridView.Invalidate();
    }

    private void Open(int index)
    {
        using TransactionForm form = new TransactionForm(_company)
        {
            ShowApplyButton = false
        };

        form.InitializeTransaction(_lines[index].Transaction);
        form.ShowDialog();
    }

    private void OnDataGridViewCellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.ColumnIndex == -1 || e.RowIndex == -1)
        {
            return;
        }

        int index = e.RowIndex / 2;

        if (index >= _lines.Count)
        {
            return;
        }

        SelectIndex(index);

        if (_source.CanEditAccount(_lines[index]))
        {
            return;
        }

        Open(index);
    }

    private void OnDataGridViewCellPainting(object sender, DataGridViewCellPaintingEventArgs e)
    {
        if (e.Graphics == null)
        {
            return;
        }

        if (e.RowIndex == -1 || e.ColumnIndex == -1)
        {
            return;
        }

        RegisterRow tag = (RegisterRow)_dataGridView.Rows[e.RowIndex].Tag!;
        int lineIndex = tag.LineIndex;
        bool isTop = e.RowIndex % 2 == 0;
        DataGridViewCellStyle cellStyle = isTop
            ? _dataGridView.DefaultCellStyle
            : _dataGridView.AlternatingRowsDefaultCellStyle;
        Color backColor;

        if (lineIndex == _selectedLineIndex)
        {
            backColor = cellStyle.SelectionBackColor;
        }
        else
        {
            backColor = cellStyle.BackColor;
        }

        using (SolidBrush backColorBrush = new SolidBrush(backColor))
        {
            e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
        }

        using (Pen pen = new Pen(_dataGridView.GridColor))
        {
            if (isTop)
            {
                pen.DashStyle = DashStyle.Dot;
            }

            e.Graphics.DrawLine(
                pen,
                e.CellBounds.Left,
                e.CellBounds.Bottom - 1,
                e.CellBounds.Right,
                e.CellBounds.Bottom - 1);
        }

        if (e.ColumnIndex == accountColumn.Index)
        {
            if (isTop || lineIndex < 0 || lineIndex > _lines.Count)
            {
                e.Handled = true;

                return;
            }

            Line line = _lines[lineIndex];

            if (!_source.CanEditAccount(line))
            {
                using SolidBrush foreColorBrush = new SolidBrush(backColor.GetForeColor());

                e.Graphics.DrawString(
                    _source.GetRepresentativeAccountName(line),
                    e.CellStyle?.Font ?? _dataGridView.Font,
                    foreColorBrush,
                    e.CellBounds,
                    new StringFormat(StringFormatFlags.NoWrap)
                    {
                        Trimming = StringTrimming.EllipsisWord
                    });

                e.Handled = true;

                return;
            }
        }

        if (!isTop && (e.ColumnIndex == postedColumn.Index || e.ColumnIndex == balanceColumn.Index || e.ColumnIndex == reconciledColumn.Index))
        {
            e.Handled = true;

            return;
        }

        e.PaintContent(e.CellBounds);

        e.Handled = true;
    }

    private void OnDataGridViewEditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
    {
        DataGridViewCell? current = _dataGridView.CurrentCell;

        if (e.Control is not TextBox textBox || current == null)
        {
            return;
        }

        if (current.ColumnIndex != nameMemoColumn.Index || current.RowIndex % 2 != 0)
        {
            textBox.AutoCompleteCustomSource = null;

            return;
        }

        textBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
        textBox.AutoCompleteCustomSource = new AutoCompleteStringCollection();

        textBox.AutoCompleteCustomSource.AddRange(_company.GetNames());
    }

    private void OnDataGridViewSelectionChanged(object sender, EventArgs e)
    {
        _dataGridView.Invalidate();
    }

    private void OnDataGridViewCellEndEdit(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex == -1)
        {
            return;
        }

        _editingIndex = e.RowIndex / 2;
    }

    private void OnDataGridViewCurrentCellChanged(object sender, EventArgs e)
    {
        if (_pendingInitialization || _editingIndex == null)
        {
            return;
        }

        DataGridViewCell? current = _dataGridView.CurrentCell;
        int newIndex = current == null ? -1 : current.RowIndex / 2;

        if (newIndex == _editingIndex.Value)
        {
            return;
        }

        int index = _editingIndex.Value;

        _editingIndex = null;

        CommitChanges(index);
    }

    private void OnDataGridViewKeyDown(object sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.Tab:
                DataGridViewCell? cell = _dataGridView.CurrentCell;

                if (cell == null)
                {
                    break;
                }

                int columnIndex = cell.ColumnIndex;
                int rowIndex = cell.RowIndex;
                bool forward = !e.Shift;
                int nextColumnIndex = forward ? columnIndex + 1 : columnIndex - 1;

                if (nextColumnIndex < _dataGridView.ColumnCount &&
                    _dataGridView[nextColumnIndex, rowIndex].ReadOnly)
                {
                    e.SuppressKeyPress = true;

                    nextColumnIndex += forward ? 1 : -1;

                    if (nextColumnIndex >= _dataGridView.ColumnCount)
                    {
                        int nextRowIndex = rowIndex + 1;

                        if (nextRowIndex < _dataGridView.Rows.Count)
                        {
                            _dataGridView.CurrentCell = _dataGridView.Rows[nextRowIndex].Cells[0];
                        }
                    }
                    else if (nextColumnIndex < 0)
                    {
                        int previousRowIndex = rowIndex - 1;

                        if (previousRowIndex >= 0)
                        {
                            _dataGridView.CurrentCell = _dataGridView.Rows[previousRowIndex]
                                .Cells[_dataGridView.ColumnCount - 1];
                        }
                    }
                    else
                    {
                        _dataGridView.CurrentCell = _dataGridView.Rows[rowIndex].Cells[nextColumnIndex];
                    }
                }
                break;

            case Keys.Enter:
                e.SuppressKeyPress = true;

                if (_editingIndex == null)
                {
                    break;
                }

                _dataGridView.EndEdit();

                int index = _editingIndex.Value;

                _editingIndex = null;

                CommitChanges(index);
                SelectIndex(index + 1);
                break;

            case Keys.Escape:
                RevertChanges();
                break;
        }
    }

    private void OnRevertToolStripMenuItem(object sender, EventArgs e)
    {
        RevertChanges();
    }

    private void OnDataGridViewLeave(object sender, EventArgs e)
    {
        if (_editingIndex == null)
        {
            return;
        }

        _dataGridView.EndEdit();

        int index = _editingIndex.Value;

        _editingIndex = null;

        CommitChanges(index);
    }

    private void OnCloseToolStripButtonClick(object sender, EventArgs e)
    {
        Close();
    }

    private void OnNewToolStripButtonClick(object sender, EventArgs e)
    {
        SelectIndex(_lines.Count);
    }

    private void OnSaveToolStripButtonClick(object sender, EventArgs e)
    {
        int index = _editingIndex ?? _selectedLineIndex;

        if (index == -1)
        {
            return;
        }

        _editingIndex = null;

        if (Save(index, out DateTime posted))
        {
            TransactionHelpers.Post(posted);
        }
    }

    private void OnSaveCloseToolStripButtonClick(object sender, EventArgs e)
    {
        int index = _editingIndex ?? _selectedLineIndex;

        if (index == -1)
        {
            Close();

            return;
        }

        _editingIndex = null;

        if (Save(index, out DateTime posted))
        {
            TransactionHelpers.Post(posted);
            Close();
        }
    }

    private void OnCopyToolStripButtonClick(object sender, EventArgs e)
    {
        int index = _editingIndex ?? _selectedLineIndex;

        if (index == -1)
        {
            return;
        }

        Line? line = GetValue(index);

        if (line == null)
        {
            return;
        }

        _editingIndex = null;

        if (!Save(index, out DateTime posted))
        {
            return;
        }

        line = GetValue(index);

        if (line == null)
        {
            return;
        }

        Transaction? clone = line.Transaction.Clone();

        _company.AddTransaction(clone, clone.Name, clone.Lines);
        _company.UpdateTransaction(clone.Id, _company.NextTransactionNumber, clone.Name, clone.Posted, clone.Lines);
        TransactionHelpers.Post(posted);
    }

    private void OnRemoveToolStripButtonClick(object sender, EventArgs e)
    {
        int index = _editingIndex ?? _selectedLineIndex;

        if (index == -1)
        {
            return;
        }

        Line? line = GetValue(index);

        if (line == null)
        {
            return;
        }

        if (FormattedStrings.ShowDeleteTransactionMessage() != DialogResult.OK)
        {
            return;
        }

        Transaction remove = line.Transaction;

        SelectIndex(_selectedLineIndex);
        _company.RemoveTransaction(remove.Id);
    }

    private void OnFirstToolStripButtonClick(object sender, EventArgs e)
    {
        SelectIndex(value: 0);
    }

    private void OnPreviousToolStripButtonClick(object sender, EventArgs e)
    {
        SelectIndex(_selectedLineIndex - 1);
    }

    private void OnNextToolStripButtonClick(object sender, EventArgs e)
    {
        SelectIndex(_selectedLineIndex + 1);
    }

    private void OnLastToolStripButtonClick(object sender, EventArgs e)
    {
        SelectIndex(_lines.Count - 1);
    }

    private void OnGoToSelectedToolStripButtonClick(object sender, EventArgs e)
    {
        SelectIndex(_selectedLineIndex);
    }

    private void OnGoToSiblingToolStripButtonClick(object sender, EventArgs e)
    {
        int index = _editingIndex ?? _selectedLineIndex;

        if (index == -1)
        {
            return;
        }

        Line? line = GetValue(index);

        if (line == null)
        {
            return;
        }

        Line? sibling = line.Sibling;

        if (sibling == null)
        {
            return;
        }

        LineHelpers.BeginTransactions(_company, _factory, sibling);
    }

    private void OnTransactionToolStripButtonClick(object sender, EventArgs e)
    {
        int index = _editingIndex ?? _selectedLineIndex;

        if (index == -1 || index >= _lines.Count)
        {
            return;
        }

        Open(index);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _company.AccountUpdated -= OnCompanyAccountUpdated;
            _company.AccountRemoved -= OnCompanyAccountRemoved;
            _company.TransactionAdded -= OnCompanyTransactionAdded;
            _company.TransactionUpdated -= OnCompanyTransactionUpdated;
            _company.TransactionReconciled -= OnCompanyTransactionReconciled;
            _company.TransactionRemoved -= OnCompanyTransactionRemoved;

            if (components != null)
            {
                components.Dispose();
                components = null;
            }
        }

        base.Dispose(disposing);
    }
}
