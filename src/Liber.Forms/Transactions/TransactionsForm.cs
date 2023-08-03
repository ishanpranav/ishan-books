// TransactionsForm.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
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
        creditColumn.ValueType = typeof(decimal);
        balanceColumn.ValueType = typeof(decimal);
        balanceColumn.ReadOnly = false;

        accountColumn.Items.Add(NullAccountView.Value);

        foreach (KeyValuePair<Guid, Account> account in _company.Accounts)
        {
            InitializeAccount(account.Key, account.Value);
        }

        decimal balance = 0;

        foreach (Line line in _account.OrderedLines)
        {
            if (line.Transaction == null)
            {
                continue;
            }

            balance += line.Balance;

            Guid accountId;
            Line? sibling = line.Sibling;

            if (sibling == null)
            {
                accountId = Guid.Empty;
            }
            else
            {
                accountId = sibling.AccountId;
            }

            _dataGridView.Rows.Add(line.Transaction.Posted, line.Transaction.Number, accountId, line.Debit, line.Credit, balance);
        }
    }

    private void InitializeAccount(Guid key, Account value)
    {
        accountColumn.Items.Add(new AccountView(key, value));
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
