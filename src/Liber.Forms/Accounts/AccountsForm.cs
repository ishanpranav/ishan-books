// AccountsForm.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Liber.Forms.Components;
using Liber.Forms.Transactions;

namespace Liber.Forms.Accounts;

internal sealed partial class AccountsForm : Form
{
    private readonly Company _company;
    private readonly FormFactory _factory;
    private readonly Dictionary<Guid, ListViewItem> _items = new Dictionary<Guid, ListViewItem>();

    public AccountsForm(Company company, FormFactory factory)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        _company = company;
        company.AccountAdded += OnCompanyAccountAdded;
        company.AccountUpdated += OnCompanyAccountUpdated;
        company.AccountRemoved += OnCompanyAccountRemoved;
        _factory = factory;

        _listView.BeginUpdate();

        foreach (KeyValuePair<Guid, Account> account in company.OrderedAccounts)
        {
            InitializeAccount(account.Key, account.Value);
        }

        _listView.AutoResizeColumns();
        _listView.Sort();
        _listView.EndUpdate();
    }

    private void InitializeAccount(Guid id, Account value)
    {
        ListViewItem item = _listView.Items.Add(new ListViewItem()
        {
            Tag = id,
            Selected = true
        });

        AddSubItems(item, value);
        _items.Add(id, item);
    }

    private void InitializeTransaction(Guid id)
    {
        Guid key = typeof(TransactionForm).GUID;

        if (_factory.TryKill(key))
        {
            return;
        }

        TransactionForm form = new TransactionForm(_company);
        Transaction transaction = new Transaction()
        {
            Posted = DateTime.Today,
            Number = _company.NextTransactionNumber
        };

        transaction.Lines.Add(new Line()
        {
            AccountId = id
        });
        form.InitializeTransaction(transaction);
        _factory.Register(key, form);
    }

    private void InitializeTransactions(Guid id)
    {
        if (_factory.TryKill(id))
        {
            return;
        }

        TransactionsForm form = new TransactionsForm(_company, id);

        _factory.Register(id, form);
    }

    private void AddSubItems(ListViewItem item, Account value)
    {
        item.Text = value.Name;

        decimal balance;
        DateTime posted = DateTime.Today;
        DateTime started = new DateTime(posted.Year, 1, 1);

        if (value == _company.EquityAccount)
        {
            balance = _company.GetEquity(started);
        }
        else if (value == _company.OtherEquityAccount)
        {
            balance = _company.GetOtherEquity(started);
        }
        else if (value.Temporary)
        {
            balance = value.GetBalance(started, posted);
        }
        else
        {
            balance = value.GetBalance(posted);
        }

        balance = value.Type.ToBalance(balance);

        item.SubItems.Add(value.Number.ToString()).Tag = value.Number;
        item.SubItems.Add(value.Type.ToLocalizedString());
        item.SubItems.Add(balance.ToLocalizedString()).Tag = balance;

        if (value.Placeholder)
        {
            item.ForeColor = SystemColors.GrayText;
        }
        else
        {
            item.ForeColor = SystemColors.ControlText;
        }
    }

    private void OnCompanyAccountAdded(object? sender, GuidEventArgs e)
    {
        InitializeAccount(e.Id, _company.Accounts[e.Id]);
        _listView.AutoResizeColumns();
    }

    private void OnCompanyAccountUpdated(object? sender, GuidEventArgs e)
    {
        ListViewItem item = _items[e.Id];

        item.SubItems.Clear();
        AddSubItems(item, _company.Accounts[e.Id]);
    }

    private void OnCompanyAccountRemoved(object? sender, GuidEventArgs e)
    {
        ListViewItem item = _items[e.Id];

        _listView.Items.Remove(item);
        _listView.AutoResizeColumns();
    }

    private void OnNewToolStripMenuItemClick(object sender, EventArgs e)
    {
        _factory.AutoRegister(() => new NewAccountForm(_company));
    }

    private void OnEditToolStripMenuItemClick(object sender, EventArgs e)
    {
        if (!_listView.TryGetSelection(out Guid id) || _factory.TryKill(id))
        {
            return;
        }

        EditAccountForm form = new EditAccountForm(_company, id);

        _factory.Register(id, form);
    }

    private void OnRenameToolStripMenuItemClick(object sender, EventArgs e)
    {
        if (_listView.SelectedItems.Count == 0)
        {
            return;
        }

        _listView.SelectedItems[0].BeginEdit();
    }

    private void OnRemoveToolStripMenuItem(object sender, EventArgs e)
    {
        if (!_listView.TryGetSelection(out Guid id))
        {
            return;
        }

        _company.RemoveAccount(id);
    }

    private void OnTransactionToolStripMenuItemClick(object sender, EventArgs e)
    {
        if (_listView.TryGetSelection(out Guid id))
        {
            InitializeTransaction(id);
        }
    }

    private void OnTransactionsToolStripMenuItemClick(object sender, EventArgs e)
    {
        if (_listView.TryGetSelection(out Guid id))
        {
            InitializeTransactions(id);
        }
    }

    private void OnListViewAfterLabelEdit(object sender, LabelEditEventArgs e)
    {
        Guid id = (Guid)_listView.Items[e.Item].Tag;
        Account value = _company.Accounts[id];

        value.Name = e.Label!;

        _company.UpdateAccount(id, value.ParentId);
    }

    private void OnListViewItemActivate(object sender, EventArgs e)
    {
        if (!_listView.TryGetSelection(out Guid id))
        {
            return;
        }

        Account value = _company.Accounts[id];

        if (value.Placeholder)
        {
            // TODO: QuickReport
        }
        else if (value.Virtual)
        {
            InitializeTransaction(id);
        }
        else
        {
            InitializeTransactions(id);
        }
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
