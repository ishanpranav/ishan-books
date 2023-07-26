// TransactionForm.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using Liber.Forms.Accounts;
using Liber.Forms.Properties;

namespace Liber.Forms.Transactions;

internal sealed partial class TransactionForm : Form
{
    private readonly Company _company;

    private Transaction? _current;

    public TransactionForm(Company company)
    {
        InitializeComponent();
        ClickOnce.Initialize(this);

        _company = company;
        _company.AccountAdded += OnCompanyAccountAdded;
        _company.AccountUpdated += OnCompanyAccountUpdated;
        _company.AccountRemoved += OnCompanyAccountRemoved;
        DialogResult = DialogResult.Cancel;
        accountColumn.ValueMember = nameof(AccountView.Id);
        accountColumn.DisplayMember = nameof(AccountView.DisplayName);
        numberNumericUpDown.Maximum = decimal.MaxValue;

        foreach (KeyValuePair<Guid, Account> account in _company.Accounts)
        {
            InitializeAccount(account.Key, account.Value);
        }

        SortedSet<string> names = new SortedSet<string>();

        foreach (Transaction transaction in _company.Transactions)
        {
            if (!string.IsNullOrWhiteSpace(transaction.Name))
            {
                names.Add(transaction.Name);
            }
        }

        string[] array = new string[names.Count];

        names.CopyTo(array);

        nameComboBox.DataSource = array;
        debitColumn.ValueType = typeof(decimal);
        creditColumn.ValueType = typeof(decimal);
        _dataGridView.AlternatingRowsDefaultCellStyle.BackColor = _company.Color;
        _dataGridView.AlternatingRowsDefaultCellStyle.ForeColor = Colors.GetForeColor(_company.Color);

        _dataGridView.AutoResizeColumns();
        CreateNew();
    }

    private void InitializeAccount(Guid key, Account value)
    {
        accountColumn.Items.Add(new AccountView(key, value));
    }

    [MemberNotNull(nameof(_current))]
    public void InitializeTransaction(Transaction transaction)
    {
        _current = transaction;
        numberNumericUpDown.Value = transaction.Number;
        postedDateTimePicker.Value = transaction.Posted;
        nameComboBox.Text = transaction.Name;
        memoTextBox.Text = transaction.Memo;

        _dataGridView.Rows.Clear();

        foreach (Line line in transaction.Lines)
        {
            object? debit = null;
            object? credit = null;

            if (line.Debit > 0)
            {
                debit = line.Debit;
            }

            if (line.Credit > 0)
            {
                credit = line.Credit;
            }

            _dataGridView.Rows.Add(line.AccountId, debit, credit, line.Description);
        }

        _dataGridView.AutoResizeColumns();
    }

    private static decimal ToDecimal(object cellValue)
    {
        if (cellValue == null || cellValue is DBNull)
        {
            return 0m;
        }

        return (decimal)cellValue;
    }

    [MemberNotNullWhen(true, nameof(_current))]
    private bool Save()
    {
        if (_current != null)
        {
            _company.RemoveTransaction(_current);
        }

        Transaction transaction = new Transaction()
        {
            Number = numberNumericUpDown.Value,
            Posted = postedDateTimePicker.Value,
            Name = nameComboBox.Text,
            Memo = memoTextBox.Text
        };

        foreach (DataGridViewRow row in _dataGridView.Rows)
        {
            row.ErrorText = null;

            if (row.IsNewRow)
            {
                continue;
            }

            if (row.Cells[accountColumn.Index].Value == null)
            {
                row.ErrorText = Resources.InvalidAccountError;

                return false;
            }

            Guid accountId = (Guid)row.Cells[accountColumn.Index].Value;
            decimal debit = ToDecimal(row.Cells[debitColumn.Index].Value);
            decimal credit = ToDecimal(row.Cells[creditColumn.Index].Value);

            transaction.Lines.Add(new Line()
            {
                AccountId = accountId,
                Balance = debit - credit,
                Description = (string?)row.Cells[descriptionColumn.Index].Value
            });
        }

        if (transaction.Balance != 0)
        {
            _dataGridView.Rows[_dataGridView.NewRowIndex].ErrorText = Resources.ImbalanceError;

            return false;
        }

        _company.AddTransaction(transaction);
        InitializeTransaction(transaction);

        return true;
    }

    private void Clear()
    {
        postedDateTimePicker.Value = DateTime.Today;
        nameComboBox.Text = string.Empty;

        memoTextBox.Clear();
        _dataGridView.Rows.Clear();
    }

    private void CreateNew()
    {
        Clear();

        _current = null;
        numberNumericUpDown.Value = _company.NextTransactionNumber;
    }

    private void OnCompanyAccountAdded(object? sender, GuidEventArgs e)
    {
        InitializeAccount(e.Id, _company.Accounts[e.Id]);
    }

    private void OnCompanyAccountUpdated(object? sender, GuidEventArgs e)
    {
        _dataGridView.Refresh();
    }

    private void OnCompanyAccountRemoved(object? sender, GuidEventArgs e)
    {
        accountColumn.Items.Remove(e.Id);
    }

    private void OnNumberNumericUpDownValueChanged(object sender, EventArgs e)
    {
    }

    private void OnNewToolStripButtonClick(object sender, EventArgs e)
    {
        CreateNew();
    }

    private void OnSaveToolStripButtonClick(object sender, EventArgs e)
    {
        Save();
    }

    private void OnCopyToolStripButtonClick(object sender, EventArgs e)
    {
        if (!Save())
        {
            return;
        }

        Transaction clone = new Transaction()
        {
            Posted = _current.Posted,
            Number = _current.Number + 1,
            Name = _current.Name,
            Memo = _current.Memo
        };

        foreach (Line line in _current.Lines)
        {
            clone.Lines.Add(new Line()
            {
                AccountId = line.AccountId,
                Balance = line.Balance,
                Description = line.Description
            });
        }

        _company.AddTransaction(clone);
        InitializeTransaction(clone);
    }

    private void OnAcceptButtonClick(object sender, EventArgs e)
    {
        if (!Save())
        {
            return;
        }

        DialogResult = DialogResult.OK;

        Close();
    }

    private void OnApplyButtonClick(object sender, EventArgs e)
    {
        if (Save())
        {
            CreateNew();
        }
    }

    private void OnCancelButtonClick(object sender, EventArgs e)
    {
        Clear();
    }

    private void OnNextButtonClick(object sender, EventArgs e)
    {
        if (_current == null)
        {
            CreateNew();

            return;
        }

        Transaction? next = _company.GetTransactionAfter(_current);

        if (next == null)
        {
            CreateNew();

            return;
        }

        InitializeTransaction(next);
    }

    private void OnPreviousButtonClick(object sender, EventArgs e)
    {
        Transaction? previous;

        if (_current == null)
        {
            previous = _company.LastTransaction;
        }
        else
        {
            previous = _company.GetTransactionBefore(_current);
        }

        if (previous != null)
        {
            InitializeTransaction(previous);
        }
    }

    private void OnPrintToolStripButtonClick(object sender, EventArgs e)
    {

    }

    private void OnDataGridViewCellMousDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {

    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            components?.Dispose();

            _company.AccountAdded -= OnCompanyAccountAdded;
            _company.AccountUpdated -= OnCompanyAccountUpdated;
            _company.AccountRemoved -= OnCompanyAccountRemoved;
        }

        base.Dispose(disposing);
    }
}
