// TransactionsForm.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Media;
using System.Windows.Forms;
using Liber.Forms.AccountViews;
using Liber.Forms.Properties;
using Liber.MathEngine.Expressions;

namespace Liber.Forms.Transactions;

internal sealed partial class TransactionsForm : Form
{
    private readonly Company _company;
    private readonly Guid _id;
    private readonly List<Line> _lines = new List<Line>();
    private int _selectedLineIndex = -1;

    private bool OnLastCell
    {
        get
        {
            DataGridViewCell? currentCell = _dataGridView.CurrentCell;

            if (currentCell == null)
            {
                return false;
            }

            return currentCell.RowIndex == _dataGridView.Rows.Count - 1 &&
                   currentCell.ColumnIndex == nameMemoColumn.Index;
        }
    }

    public TransactionsForm(Company company, Guid id)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        AccountViewBindingList bindingList = new AccountViewBindingList(company, x => !company.GetAccount(x).ReadOnly);

        bindingList.AddNullAccount();

        accountColumn.DataSource = bindingList;
        accountColumn.ValueMember = nameof(AccountView.Id);
        accountColumn.DisplayMember = nameof(AccountView.DisplayName);

        Account account = company.GetAccount(id);

        _dataGridView.CompanyColor = company.GetColorOrDefault(account);
        _dataGridView.DebitColumnIndex = debitColumn.Index;
        _dataGridView.CreditColumnIndex = creditColumn.Index;
        _company = company;
        _id = id;
        postedColumn.ValueType = typeof(DateTime);
        balanceColumn.ValueType = typeof(decimal);
        balanceColumn.DefaultCellStyle.Font = _dataGridView.ColumnHeadersDefaultCellStyle.Font;
        balanceColumn.DefaultCellStyle.Format = DecimalExtensions.Format;
        transactionColumn.ValueType = typeof(Transaction);
        Text = account.Name;

        InitializeLines();

        if (_dataGridView.Rows.Count > 0)
        {
            _dataGridView.CurrentCell = _dataGridView[0, _dataGridView.Rows.Count - 1];
        }
    }

    private string GetLineType(Line value)
    {
        if (value.Sibling == null)
        {
            return "GENJRN";
        }

        string? memo = _company.GetSuggestedMemo(value.Transaction!);

        if (memo == Liber.Properties.Resources.TransferMemo)
        {
            return "TNF";
        }

        if (memo == Liber.Properties.Resources.DepositMemo)
        {
            return "DEP";
        }

        if (memo == Liber.Properties.Resources.CheckMemo)
        {
            return "CHK";
        }

        return memo ?? string.Empty;
    }

    private void InitializeLines()
    {
        _selectedLineIndex = -1;

        _lines.Clear();
        _lines.AddRange(_company.GetAccount(_id).OrderedLines);
        _dataGridView.SuspendLayout();

        try
        {
            _dataGridView.Rows.Clear();

            decimal balance = 0;

            for (int i = 0; i < _lines.Count; i++)
            {
                Line line = _lines[i];
                Transaction transaction = line.Transaction!;
                DataGridViewRow top = new DataGridViewRow()
                {
                    Tag = new RegisterRow(i)
                };

                balance += line.Balance;

                top.CreateCells(
                    _dataGridView,
                    transaction.Posted,
                    transaction.Number,
                    transaction.Name ?? string.Empty,
                    Guid.Empty,
                    string.Empty,
                    new DecimalExpression(line.Debit),
                    new DecimalExpression(line.Credit),
                    balance);

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
                    string.Empty,
                    string.Empty);

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
                string.Empty,
                new DecimalExpression(0),
                new DecimalExpression(0),
                balance);

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
                string.Empty,
                string.Empty);
            AddDoubleRow(newTop, newBottom);
        }
        finally
        {
            _dataGridView.ResumeLayout();
        }
    }

    private void AddDoubleRow(DataGridViewRow top, DataGridViewRow bottom)
    {
        top.Cells[accountColumn.Index].ReadOnly = true;
        bottom.Cells[postedColumn.Index].ReadOnly = true;

        Guid accountId = (Guid?)bottom.Cells[accountColumn.Index].Value ?? Guid.Empty;

        if (accountId == Guid.Empty)
        {
            top.Cells[debitColumn.Index].ReadOnly = true;
            top.Cells[creditColumn.Index].ReadOnly = true;
            bottom.Cells[accountColumn.Index].ReadOnly = true;
        }

        bottom.Cells[debitColumn.Index].ReadOnly = true;
        bottom.Cells[creditColumn.Index].ReadOnly = true;

        _dataGridView.Rows.Add(top);
        _dataGridView.Rows.Add(bottom);
    }

    private bool Save()
    {
        int count = _dataGridView.Rows.Count;
        int topIndex = count - 2;
        int bottomIndex = count - 1;

        DataGridViewRow top = _dataGridView.Rows[topIndex];

        top.ErrorText = string.Empty;

        if (!decimal.TryParse(_dataGridView[numberTypeColumn.Index, topIndex].Value?.ToString(), out decimal number))
        {
            number = 0;
        }

        if (!_dataGridView.TryGetBalance(top, out decimal balance))
        {
            return false;
        }

        DateTime posted = (DateTime)_dataGridView[postedColumn.Index, topIndex].Value!;
        Transaction transaction = new Transaction()
        {
            Id = Guid.NewGuid(),
            Number = number,
            Posted = posted,
            Name = _dataGridView[nameMemoColumn.Index, topIndex].Value?.ToString(),
            Memo = _dataGridView[nameMemoColumn.Index, bottomIndex].Value?.ToString()
        };

        if (top.Cells[accountColumn.Index].Value is not Guid accountId ||
            accountId == Guid.Empty ||
            accountId == _id)
        {
            top.ErrorText = Resources.InvalidAccountError;

            return false;
        }

        transaction.Lines.Add(new Line()
        {
            AccountId = accountId,
            Balance = 0
        });

        if (transaction.Balance != 0)
        {
            top.ErrorText = Resources.ImbalanceError;

            return false;
        }

        _company.AddTransaction(transaction);
        SystemSounds.Asterisk.Play();

        Settings.Default.LastPosted = posted;

        Settings.Default.Save();
        InitializeLines();
        SelectLine(_lines.Count - 1);

        return true;
    }

    private void SelectLine(int index)
    {
        _selectedLineIndex = index;

        int rowIndex = index * 2;

        if (rowIndex < _dataGridView.Rows.Count)
        {
            _dataGridView.FirstDisplayedScrollingRowIndex = rowIndex;
            _dataGridView.CurrentCell = _dataGridView.Rows[rowIndex].Cells[nameMemoColumn.Index];

            _dataGridView.Invalidate();
        }
    }

    private void RemoveTransaction()
    {
        if (_selectedLineIndex == -1 || _selectedLineIndex >= _lines.Count)
        {
            return;
        }

        // TODO: use resource

        DialogResult result = MessageBox.Show(
            "Delete this transaction?", "Confirm Delete",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

        if (result != DialogResult.Yes)
        {
            return;
        }

        Transaction transaction = _lines[_selectedLineIndex].Transaction!;

        _company.RemoveTransaction(transaction);
        InitializeLines();
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

    private void OnDataGridViewCellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex == -1)
        {
            return;
        }

        int index = e.RowIndex / 2;

        if (index >= _lines.Count)
        {
            return;
        }

        using TransactionForm form = new TransactionForm(_company)
        {
            ShowApplyButton = false
        };

        form.InitializeTransaction(_lines[index].Transaction!);

        if (form.ShowDialog() == DialogResult.OK && form.Value != null)
        {
            //cell.Value = form.Value;

            // TODO: bind events for transaction added, removed, updated

            //InitializeTransactions();
            SelectLine(index);
        }
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

        using Brush backColorBrush = new SolidBrush(backColor);

        e.Graphics.FillRectangle(backColorBrush, e.CellBounds);

        using Pen pen = new Pen(_dataGridView.GridColor);

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

    private void OnDataGridViewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter || (e.KeyCode == Keys.Tab && OnLastCell))
        {
            Save();

            e.SuppressKeyPress = true;

            return;
        }

        if (e.KeyCode == Keys.Delete && _selectedLineIndex != -1)
        {
            RemoveTransaction();
        }
    }
}
