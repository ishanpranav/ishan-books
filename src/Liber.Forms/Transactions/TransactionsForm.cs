// TransactionsForm.cs
// Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Liber.Forms.Accounts;

namespace Liber.Forms.Transactions;

internal sealed partial class TransactionsForm : Form
{
    private readonly Company _company;
    private readonly Account _account;

    public TransactionsForm(Company company, Guid id)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        company.AccountAdded += OnCompanyAccountAdded;
        company.AccountUpdated += OnCompanyAccountUpdated;
        company.AccountRemoved += OnCompanyAccountRemoved;
        _company = company;
        _account = company.Accounts[id];
        postedColumn.ValueType = typeof(DateTime);
        accountColumn.ValueMember = nameof(IAccountView.Id);
        accountColumn.DisplayMember = nameof(IAccountView.DisplayName);
        debitColumn.ValueType = typeof(decimal);
        debitColumn.DefaultCellStyle.Format = DecimalExtensions.Format;
        creditColumn.ValueType = typeof(decimal);
        creditColumn.DefaultCellStyle.Format = DecimalExtensions.Format;
        balanceColumn.ValueType = typeof(decimal);
        balanceColumn.DefaultCellStyle.Format = DecimalExtensions.Format;
        transactionColumn.ValueType = typeof(Transaction);
        _dataGridView.AlternatingRowsDefaultCellStyle.BackColor = _company.Color;
        _dataGridView.AlternatingRowsDefaultCellStyle.ForeColor = Colors.GetForeColor(_company.Color);
        Text = _account.Name;

        accountColumn.Items.Add(NullAccountView.Value);

        foreach (KeyValuePair<Guid, Account> account in _company.Accounts)
        {
            InitializeAccount(account.Key, account.Value);
        }

        InitializeTransactions();

        if (_dataGridView.Rows.Count > 0)
        {
            _dataGridView.CurrentCell = _dataGridView[0, _dataGridView.Rows.Count - 1];
        }
    }

    private void InitializeAccount(Guid key, Account value)
    {
        accountColumn.Items.Add(new AccountView(key, value));
    }

    private void InitializeTransactions()
    {
        _dataGridView.Rows.Clear();

        decimal balance = 0;

        foreach (Line line in _account.OrderedLines)
        {
            Transaction transaction = line.Transaction!;
            Guid accountId;
            Line? sibling = line.Sibling;
            bool readOnly;

            if (sibling == null)
            {
                accountId = Guid.Empty;
                readOnly = true;
            }
            else
            {
                accountId = sibling.AccountId;
                readOnly = false;
            }

            balance += line.Balance;

            int row = _dataGridView.Rows.Add(transaction.Posted, transaction.Number, accountId, transaction.Name, line.Debit, line.Credit, balance, transaction);

            for (int column = 0; column < _dataGridView.Columns.Count; column++)
            {
                _dataGridView[column, row].ReadOnly = readOnly;
            }

            _dataGridView[balanceColumn.Index, row].ReadOnly = true;
        }

        _dataGridView.AutoResizeColumns();
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

    private void OnDataGridViewCellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex == -1 || e.ColumnIndex == balanceColumn.Index || !_dataGridView[e.ColumnIndex, e.RowIndex].ReadOnly)
        {
            return;
        }

        using TransactionForm form = new TransactionForm(_company)
        {
            ShowApplyButton = false
        };

        DataGridViewCell cell = _dataGridView[transactionColumn.Index, e.RowIndex];

        form.InitializeTransaction((Transaction)cell.Value);

        if (form.ShowDialog() == DialogResult.OK && form.Value != null)
        {
            cell.Value = form.Value;

            InitializeTransactions();

            _dataGridView.CurrentCell = _dataGridView[e.ColumnIndex, e.RowIndex];
        }
    }

    private void OnDataGridViewEditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
    {
        if (e.Control is not TextBox textBox)
        {
            return;
        }

        if (_dataGridView.CurrentCell.ColumnIndex != nameColumn.Index)
        {
            textBox.AutoCompleteCustomSource = null;

            return;
        }

        textBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        textBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
        textBox.AutoCompleteCustomSource = new AutoCompleteStringCollection();
        textBox.AutoCompleteCustomSource.AddRange(_company.GetNames());
    }

    private void OnDataGridViewUserAddedRow(object sender, DataGridViewRowEventArgs e)
    {
        //Transaction transaction = new Transaction()
        //{
        //    Id = Guid.NewGuid(),
        //    Number = (decimal)e.Row.Cells[numberColumn.Index].Value,
        //    Posted = (DateTime)e.Row.Cells[postedColumn.Index].Value,
        //    Name = (string)e.Row.Cells[nameColumn.Index].Value
        //};

        //e.Row.Cells[transactionColumn.Index].Value = transaction;

        //transaction.Lines.Add(new Line()
        //{
        //    AccountId = ((IAccountView)e.Row.Cells[accountColumn.Index].Value).Id,
        //    Balance = (decimal)e.Row.Cells[balanceColumn.Index].Value
        //});
        //_company.AddTransaction(transaction);
    }

    private void OnDataGridViewUserDeletedRow(object sender, DataGridViewRowEventArgs e)
    {
        _company.RemoveTransaction((Transaction)e.Row.Cells[transactionColumn.Index].Value);
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
