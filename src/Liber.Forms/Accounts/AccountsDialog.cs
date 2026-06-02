// AccountsDialog.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Humanizer;
using Liber.Forms.Properties;

namespace Liber.Forms.Accounts;

internal sealed partial class AccountsDialog : Form
{
    public AccountsView Value { get; private set; }

    public AccountsDialog(AccountsView value)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        DialogResult = DialogResult.Cancel;
        Value = value;
        _accountListView.Initialize(value.Company, value.Values);

        AccountGroups lastAccountGroup = (AccountGroups)Settings.Default.LastAccountGroup;

        _comboBox.DataSource = Enum
            .GetValues<AccountGroups>()
            .Where(x => !IsPowerOfTwo((int)x))
            .OrderByDescending(x => x)
            .ToList();

        try
        {
            _comboBox.SelectedItem = lastAccountGroup;
        }
        catch
        {
            _comboBox.SelectedItem = AccountGroups.All;
        }
    }

    private static bool IsPowerOfTwo(int value)
    {
        return value > 0 && (value & (value - 1)) == 0;
    }

    private void OnComboBoxFormat(object sender, ListControlConvertEventArgs e)
    {
        if (e.ListItem is AccountGroups group)
        {
            e.Value = group.Humanize();
        }
    }

    private void OnComboBoxSelectedIndexChanged(object sender, EventArgs e)
    {
        Settings.Default.LastAccountGroup = (int)((AccountGroups?)_comboBox.SelectedItem ?? AccountGroups.All);

        Settings.Default.Save();
    }

    private IEnumerable<ListViewItem> GetMatchingItems()
    {
        AccountGroups group = (AccountGroups?)_comboBox.SelectedItem ?? AccountGroups.None;

        foreach (ListViewItem item in _accountListView.Items)
        {
            if (item.Tag is not KeyValuePair<Guid, Account> account ||
                !group.HasFlag(AccountGroup.FromType(account.Value.Type)))
            {
                continue;
            }

            yield return item;
       }
    }

    private void OnSelectAllButtonClick(object sender, EventArgs e)
    {
        foreach (ListViewItem item in GetMatchingItems())
        {
            item.Checked = true;
        }
    }

    private void OnDeselectAllButtonClick(object sender, EventArgs e)
    {
        foreach (ListViewItem item in GetMatchingItems())
        {
            item.Checked = false;
        }
    }

    private void OnToggleAllButtonClick(object sender, EventArgs e)
    {
        foreach (ListViewItem item in GetMatchingItems())
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
