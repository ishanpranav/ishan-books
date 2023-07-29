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
        ClickOnce.Initialize(this);

        _company = company;
        _factory = factory;

        company.AccountAdded += OnCompanyAccountAdded;
        company.AccountUpdated += OnCompanyAccountUpdated;
        company.AccountRemoved += OnCompanyAccountRemoved;

        foreach (KeyValuePair<Guid, Account> account in company.Accounts)
        {
            InitializeAccount(account.Key, account.Value);
        }

        _listView.AutoResizeColumns();
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

    private static void AddSubItems(ListViewItem item, Account value)
    {
        item.Text = value.Name;

        item.SubItems.AddRange(new string[]
        {
            value.Number.ToString(),
            value.Type.ToLocalizedString(),
            value.Type
                .ToBalance(value.GetBalance(
                    started: new DateTime(DateTime.Today.Year, 1, 1),
                    posted: DateTime.Today))
                .ToLocalizedString()
        });

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
        if (!_listView.TryGetSelection(out Guid key) || _factory.TryKill(key))
        {
            return;
        }

        EditAccountForm form = new EditAccountForm(_company, key);

        _factory.Register(key, form);
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

        Account value = _company.Accounts[id];

        if (value.Children.Count > 0 || value.Lines.Count > 0)
        {
            return;
        }

        _company.RemoveAccount(id);
    }

    private void OnTransactionToolStripMenuItemClick(object sender, EventArgs e)
    {
        Guid key = typeof(TransactionForm).GUID;

        if (!_listView.TryGetSelection(out Guid id) || _factory.TryKill(key))
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

    private void OnListViewAfterLabelEdit(object sender, LabelEditEventArgs e)
    {
        Guid id = (Guid)_listView.Items[e.Item].Tag;
        Account value = _company.Accounts[id];

        value.Name = e.Label!;

        _company.UpdateAccount(id, value.ParentId);
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
