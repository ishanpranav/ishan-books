﻿// AccountsForm.cs
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

    private void InitializeAccount(Guid key, Account value)
    {
        ListViewItem item = _listView.Items.Add(new ListViewItem()
        {
            Tag = key,
            Selected = true
        });

        AddSubItems(item, value);
        _items.Add(key, item);
    }

    private static void AddSubItems(ListViewItem item, Account value)
    {
        item.Text = value.Name;

        item.SubItems.AddRange(new string[]
        {
            value.Number.ToString(),
            value.Type.ToLocalizedString(),
            value.Type.ToBalance(value.Balance).ToString("c")
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

    private void OnCompanyAccountAdded(object? sender, KeyEventArgs e)
    {
        InitializeAccount(e.Key, _company.Accounts[e.Key]);
        _listView.AutoResizeColumns();
    }

    private void OnCompanyAccountUpdated(object? sender, KeyEventArgs e)
    {
        ListViewItem item = _items[e.Key];

        item.SubItems.Clear();
        AddSubItems(item, _company.Accounts[e.Key]);
    }

    private void OnCompanyAccountRemoved(object? sender, KeyEventArgs e)
    {
        ListViewItem item = _items[e.Key];

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
        if (!_listView.TryGetSelection(out Guid key))
        {
            return;
        }

        Account value = _company.Accounts[key];

        if (value.Children.Count > 0 || value.Lines.Count > 0)
        {
            return;
        }

        _company.RemoveAccount(key);
    }

    private void OnTransactionToolStripMenuItemClick(object sender, EventArgs e)
    {
        Guid formKey = typeof(TransactionForm).GUID;

        if (!_listView.TryGetSelection(out Guid accountKey) || _factory.TryKill(formKey))
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
            AccountKey = accountKey
        });
        form.InitializeTransaction(transaction);
        _factory.Register(formKey, form);
    }

    private void OnListViewAfterLabelEdit(object sender, LabelEditEventArgs e)
    {
        Guid key = (Guid)_listView.Items[e.Item].Tag;
        Account value = _company.Accounts[key];

        value.Name = e.Label!;

        _company.UpdateAccount(key, value.ParentKey);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _company.AccountAdded -= OnCompanyAccountAdded;
            _company.AccountUpdated -= OnCompanyAccountUpdated;
            _company.AccountRemoved -= OnCompanyAccountRemoved;

            components?.Dispose();
        }

        base.Dispose(disposing);
    }
}