// AccountsDialog.cs
// Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
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

        _checkedListBox.BeginUpdate();

        foreach (Account account in value.Company.Accounts.Values)
        {
            int index = _checkedListBox.Items.Add(account);

            if (value.Values.Contains(account))
            {
                _checkedListBox.SetItemChecked(index, value: true);
            }
        }

        _checkedListBox.EndUpdate();
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
        for (int i = 0; i < _checkedListBox.Items.Count; i++)
        {
            _checkedListBox.SetItemChecked(i, value: true);
        }
    }

    private void OnDeselectAllButtonClick(object sender, EventArgs e)
    {
        for (int i = 0; i < _checkedListBox.Items.Count; i++)
        {
            _checkedListBox.SetItemChecked(i, value: false);
        }
    }

    private void OnToggleAllButtonClick(object sender, EventArgs e)
    {
        for (int i = 0; i < _checkedListBox.Items.Count; i++)
        {
            _checkedListBox.SetItemChecked(i, !_checkedListBox.GetItemChecked(i));
        }
    }

    private void OnAcceptButtonClick(object sender, EventArgs e)
    {
        if (_checkedListBox.CheckedItems.Count < 1)
        {
            return;
        }

        HashSet<Account> accounts = new HashSet<Account>();

        foreach (Account item in _checkedListBox.CheckedItems)
        {
            accounts.Add(item);
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
