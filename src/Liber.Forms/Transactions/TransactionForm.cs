// TransactionForm.cs
// Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
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

    public TransactionForm(Company company)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        _company = company;
        _company.AccountAdded += OnCompanyAccountAdded;
        _company.AccountUpdated += OnCompanyAccountUpdated;
        _company.AccountRemoved += OnCompanyAccountRemoved;
        DialogResult = DialogResult.Cancel;
        accountColumn.ValueMember = nameof(IAccountView.Id);
        accountColumn.DisplayMember = nameof(IAccountView.DisplayName);
        numberNumericUpDown.Maximum = decimal.MaxValue;
        nameComboBox.DataSource = _company.GetNames();
        debitColumn.ValueType = typeof(decimal);
        debitColumn.DefaultCellStyle.Format = DecimalExtensions.Format;
        creditColumn.ValueType = typeof(decimal);
        creditColumn.DefaultCellStyle.Format = DecimalExtensions.Format;
        _dataGridView.AlternatingRowsDefaultCellStyle.BackColor = _company.Color;
        _dataGridView.AlternatingRowsDefaultCellStyle.ForeColor = Colors.GetForeColor(_company.Color);

        foreach (KeyValuePair<Guid, Account> account in _company.Accounts)
        {
            InitializeAccount(account.Key, account.Value);
        }

        _dataGridView.AutoResizeColumns();
        CreateNew();
    }

    public bool ShowApplyButton
    {
        get
        {
            return applyButton.Enabled;
        }
        set
        {
            applyButton.Enabled = value;
        }
    }

    public Transaction? Value { get; private set; }

    private void InitializeAccount(Guid key, Account value)
    {
        accountColumn.Items.Add(new AccountView(key, value));
    }

    [MemberNotNull(nameof(Value))]
    public void InitializeTransaction(Transaction transaction)
    {
        Value = transaction;
        numberNumericUpDown.Value = transaction.Number;
        postedDateTimePicker.Value = transaction.Posted;
        nameComboBox.Text = transaction.Name;
        memoTextBox.Text = transaction.Memo;

        _dataGridView.Rows.Clear();

        foreach (Line line in transaction.OrderedLines)
        {
            _dataGridView.Rows.Add(line.AccountId, line.Debit, line.Credit, line.Description);
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

    [MemberNotNullWhen(true, nameof(Value))]
    private bool Save()
    {
        if (Value != null)
        {
            _company.RemoveTransaction(Value);
        }

        Transaction transaction = new Transaction()
        {
            Id = Guid.NewGuid(),
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
        nameComboBox.SelectedItem = null;

        memoTextBox.Clear();
        _dataGridView.Rows.Clear();
    }

    private void CreateNew()
    {
        Clear();

        Value = null;
        numberNumericUpDown.Value = _company.NextTransactionNumber;
    }

    private void OnCompanyAccountAdded(object? sender, GuidEventArgs e)
    {
        InitializeAccount(e.Id, _company.Accounts[e.Id]);
    }

    private void OnCompanyAccountUpdated(object? sender, GuidEventArgs e)
    {
        accountColumn.Items.Remove(e.Id);

        if (!_company!.Accounts[e.Id].Placeholder)
        {
            accountColumn.Items.Add(e.Id);
        }

        Refresh();
    }

    private void OnCompanyAccountRemoved(object? sender, GuidEventArgs e)
    {
        accountColumn.Items.Remove(e.Id);
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
            Id = Guid.NewGuid(),
            Posted = DateTime.Today,
            Number = _company.NextTransactionNumber,
            Name = Value.Name,
            Memo = Value.Memo
        };

        foreach (Line line in Value.Lines)
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
        if (Value == null)
        {
            CreateNew();

            return;
        }

        Transaction? next = _company.GetTransactionAfter(Value);

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

        if (Value == null)
        {
            previous = _company.LastTransaction;
        }
        else
        {
            previous = _company.GetTransactionBefore(Value);
        }

        if (previous != null)
        {
            InitializeTransaction(previous);
        }
    }

    private void OnPrintToolStripButtonClick(object sender, EventArgs e)
    {
        // TODO: Print transaction
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _company.AccountAdded -= OnCompanyAccountAdded;
            _company.AccountUpdated -= OnCompanyAccountUpdated;
            _company.AccountRemoved -= OnCompanyAccountRemoved;

            if (components != null)
            {
                components.Dispose();
                components = null;
            }
        }

        base.Dispose(disposing);
    }
}
