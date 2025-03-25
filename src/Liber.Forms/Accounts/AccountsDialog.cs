// AccountsDialog.cs
// Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Liber.Forms.Accounts;

internal sealed partial class AccountsDialog : Form
{
    public AccountsDialog(AccountsView value)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        DialogResult = DialogResult.Cancel;
        Value = value;
        _accountListView.Initialize(value.Company, value.Values);
    }

    public AccountsView Value { get; private set; }

    private void OnCheckedListBoxFormat(object sender, ListControlConvertEventArgs e)
    {
        if (e.ListItem is Account account)
        {
            e.Value = account.Name;
        }
    }

    private void OnSelectAllButtonClick(object sender, EventArgs e)
    {
        foreach (ListViewItem item in _accountListView.Items)
        {
            item.Checked = true;
        }
    }

    private void OnDeselectAllButtonClick(object sender, EventArgs e)
    {
        foreach (ListViewItem item in _accountListView.Items)
        {
            item.Checked = false;
        }
    }

    private void OnToggleAllButtonClick(object sender, EventArgs e)
    {
        foreach (ListViewItem item in _accountListView.Items)
        {
            item.Checked = !item.Checked;
        }
    }

    private void OnAcceptButtonClick(object sender, EventArgs e)
    {
        IReadOnlySet<Account> accounts = _accountListView.GetCheckedAccounts();

        if (accounts.Count == 0)
        {
            return;
        }

        Value = new AccountsView(Value.Company, accounts);
        DialogResult = DialogResult.OK;

        Close();
    }

    private void OnCancelButtonClick(object sender, EventArgs e)
    {
        Close();
    }
}
