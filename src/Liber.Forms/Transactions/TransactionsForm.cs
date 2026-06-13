// TransactionsForm.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using Liber.Forms.AccountViews;
using Liber.Forms.Components;
using Liber.Forms.Properties;
using Liber.MathEngine.Expressions;

namespace Liber.Forms.Transactions;

internal sealed partial class TransactionsForm : Form
{
    private readonly Company _company;
    private readonly Account _account;
    private readonly FormFactory _factory;
    private readonly List<Line> _lines = new List<Line>();

    private int _selectedLineIndex = -1;
    private bool _pendingInitialization;
    private int? _editingIndex;

    public TransactionsForm(Company company, Account account, FormFactory factory, Line line) :
        this(company, account, factory)
    {
        SelectLine(_lines.IndexOf(line));
    }

    public TransactionsForm(Company company, Account account, FormFactory factory)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        AccountViewBindingList bindingList = new AccountViewBindingList(company, x => !x.ReadOnly);

        bindingList.AddNullAccount();

        accountColumn.DataSource = bindingList;
        accountColumn.ValueMember = nameof(AccountView.Id);
        accountColumn.DisplayMember = nameof(AccountView.DisplayName);

        _dataGridView.SetCompanyColor(company.GetColorOrDefault(account));

        _dataGridView.DebitColumnIndex = debitColumn.Index;
        _dataGridView.CreditColumnIndex = creditColumn.Index;
        company.AccountUpdated += OnCompanyAccountUpdated;
        company.AccountRemoved += OnCompanyAccountRemoved;
        company.TransactionAdded += OnCompanyTransactionAdded;
        company.TransactionUpdated += OnCompanyTransactionInvalidated;
        company.TransactionRemoved += OnCompanyTransactionInvalidated;
        _company = company;
        postedColumn.ValueType = typeof(DateTime);
        balanceColumn.ValueType = typeof(decimal);
        balanceColumn.DefaultCellStyle.Font = _dataGridView.ColumnHeadersDefaultCellStyle.Font;
        balanceColumn.DefaultCellStyle.Format = DecimalExtensions.Format;
        Text = account.Name;
        _account = account;
        _factory = factory;

        InitializeLines();

        if (_dataGridView.Rows.Count > 0)
        {
            _dataGridView.CurrentCell = _dataGridView[0, _dataGridView.Rows.Count - 1];
        }
    }

    private void OnCompanyAccountUpdated(object? sender, GuidEventArgs e)
    {
        if (e.Id == _account.Id)
        {
            InvalidateTransactions();
        }
    }

    private void OnCompanyAccountRemoved(object? sender, GuidEventArgs e)
    {
        if (e.Id == _account.Id)
        {
            Close();
        }
    }

    private void OnCompanyTransactionAdded(object? sender, GuidEventArgs e)
    {
        Transaction transaction = _company.GetTransaction(e.Id);

        if (transaction.Lines.Any(x => x.AccountId == _account.Id))
        {
            InvalidateTransactions();
        }
    }

    private void OnCompanyTransactionInvalidated(object? sender, GuidEventArgs e)
    {
        InvalidateTransactions();
    }

    private void InvalidateTransactions()
    {
        int previousSelectedLineIndex = _selectedLineIndex;

        InitializeLines();

        if (previousSelectedLineIndex != -1 && previousSelectedLineIndex < _lines.Count)
        {
            SelectLine(previousSelectedLineIndex);

            return;
        }

        if (_dataGridView.Rows.Count > 0)
        {
            _dataGridView.CurrentCell = _dataGridView[0, _dataGridView.Rows.Count - 1];
        }
    }

    private string GetLineType(Line value)
    {
        Line? sibling = value.Sibling;

        if (sibling == null)
        {
            return "GENJRN";
        }

        AccountType left = _account.Type;
        AccountType right = _company.GetAccount(sibling.AccountId).Type;

        if (left == AccountType.Bank)
        {
            if (right == AccountType.Bank)
            {
                return "TNF";
            }

            if (value.Balance > 0)
            {
                return "DEP";
            }

            if (value.Balance < 0)
            {
                return "CHK";
            }
        }

        if (left == AccountType.CreditCard)
        {
            if (right == AccountType.CreditCard)
            {
                return "TNF";
            }

            if (value.Balance > 0)
            {
                return "PMT";
            }

            if (value.Balance < 0)
            {
                return "BILL";
            }
        }

        return string.Empty;
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
        _lines.AddRange(_account.Lines.Order());
        _dataGridView.SuspendLayout();

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
                    transaction.Number,
                    transaction.Name ?? string.Empty,
                    Guid.Empty,
                    new DecimalExpression(line.Debit),
                    new DecimalExpression(line.Credit),
                    _account.Type.ToBalance(runningBalance));

                DataGridViewRow bottom = new DataGridViewRow()
                {
                    Tag = new RegisterRow(i)
                };
                Line? sibling = line.Sibling;

                bottom.CreateCells(
                    _dataGridView,
                    string.Empty,
                    GetLineType(line),
                    transaction.Memo ?? string.Empty,
                    sibling == null ? Guid.Empty : sibling.AccountId,
                    string.Empty,
                    string.Empty,
                    string.Empty);

                if (sibling == null)
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
                new DecimalExpression(0),
                new DecimalExpression(0),
                runningBalance);

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
        if (index > _lines.Count)
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

        if (_dataGridView.Rows[bottomIndex].Cells[accountColumn.Index].Value is not Guid siblingId ||
            ((currentLine == null || currentLine.Sibling != null) && siblingId == Guid.Empty) || siblingId == _account.Id)
        {
            top.ErrorText = Resources.InvalidAccountError;
            posted = default;

            return false;
        }

        if (_dataGridView[postedColumn.Index, topIndex].Value is not DateTime postedValue)
        {
            posted = default;
            top.ErrorText = Resources.InvalidAccountError;

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

        transaction.Number = number;
        transaction.Posted = posted;
        transaction.Memo = _dataGridView[nameMemoColumn.Index, bottomIndex].Value?.ToString();

        string? name = _dataGridView[nameMemoColumn.Index, topIndex].Value?.ToString();
        IReadOnlyCollection<Line> lines = currentLine != null && currentLine.Sibling == null
            ? transaction.Lines
            : new Line[]
            {
                new Line()
                {
                    AccountId = _account.Id,
                    Balance = balance
                },
                new Line()
                {
                    AccountId = siblingId,
                    Balance = -balance
                }
            };

        if (addingNew)
        {
            _company.AddTransaction(transaction, name, lines);
        }
        else
        {
            _company.UpdateTransaction(transaction.Id, name, lines);
        }

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

    private void CommitRow(int index)
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

    private void SelectLine(int index)
    {
        _selectedLineIndex = (index < 0 || index >= _lines.Count) ? -1 : index;

        int rowIndex = index * 2;

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

        if (_lines[index].Sibling != null)
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

        using (Brush backColorBrush = new SolidBrush(backColor))
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

        if ((isTop || _dataGridView[e.ColumnIndex, e.RowIndex].ReadOnly) && e.ColumnIndex == accountColumn.Index)
        {
            e.Handled = true;

            return;
        }

        if (!isTop && (e.ColumnIndex == postedColumn.Index || e.ColumnIndex == balanceColumn.Index))
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

        CommitRow(index);
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

        CommitRow(index);
    }

    private void OnCloseToolStripButtonClick(object sender, EventArgs e)
    {
        Close();
    }

    private void OnNewToolStripButtonClick(object sender, EventArgs e)
    {
        SelectLine(_lines.Count);
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

        clone.Number = _company.NextTransactionNumber;

        _company.AddTransaction(clone, clone.Name, clone.Lines);
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

        SelectLine(_selectedLineIndex);
        _company.RemoveTransaction(remove.Id);
    }

    private void OnFirstToolStripButtonClick(object sender, EventArgs e)
    {
        SelectLine(index: 0);
    }

    private void OnPreviousToolStripButtonClick(object sender, EventArgs e)
    {
        SelectLine(_selectedLineIndex - 1);
    }

    private void OnNextToolStripButtonClick(object sender, EventArgs e)
    {
        SelectLine(_selectedLineIndex + 1);
    }

    private void OnLastToolStripButtonClick(object sender, EventArgs e)
    {
        SelectLine(_lines.Count - 1);
    }

    private void OnGoToSelectedToolStripButtonClick(object sender, EventArgs e)
    {
        SelectLine(_selectedLineIndex);
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

        Transaction transaction = sibling.Transaction;

        if (sibling.AccountId == _account.Id)
        {
            SelectLine(_lines.IndexOf(sibling));

            return;
        }

        Account siblingAccount = _company.GetAccount(sibling.AccountId);

        if (_factory.TryActivate(siblingAccount.Id) || siblingAccount.ReadOnly)
        {
            return;
        }

        TransactionsForm form = new TransactionsForm(_company, siblingAccount, _factory, sibling);

        _factory.Register(siblingAccount.Id, form);
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
            _company.TransactionUpdated -= OnCompanyTransactionInvalidated;
            _company.TransactionRemoved -= OnCompanyTransactionInvalidated;

            if (components != null)
            {
                components.Dispose();
                components = null;
            }
        }

        base.Dispose(disposing);
    }
}
