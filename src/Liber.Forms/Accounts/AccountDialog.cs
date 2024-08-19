// AccountDialog.cs
// Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Humanizer;

namespace Liber.Forms.Accounts;

internal partial class AccountDialog : Form
{
    public AccountDialog(EditableAccountView value)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        DialogResult = DialogResult.Cancel;
        Value = value;

        AccountListView.BeginUpdate();

        foreach (AccountType type in AccountTypeExtensions.GetSortedValues())
        {
            AccountListView.Groups.Add(type.ToString(), type.Humanize());
        }

        foreach (KeyValuePair<Guid, Account> account in value.Company.Accounts)
        {
            if (account.Value.Placeholder)
            {
                continue;
            }

            ListViewItem item = AccountListView.Items.Add(account.Value.Name);
            AccountType type = account.Value.Type;
            string key = type.ToString();

            item.Tag = account.Key;
            item.Group = AccountListView.Groups[key];
            item.Selected = account.Key == value.Id;
            item.SubItems.Add(account.Value.Number.ToString());
        }

        AccountListView.AutoResizeColumns();
        AccountListView.Sort();
        AccountListView.EndUpdate();
    }

    public EditableAccountView Value { get; private set; }

    private void OnAcceptButtonClick(object sender, EventArgs e)
    {
        if (AccountListView.SelectedItems.Count < 1)
        {
            return;
        }

        Guid id = (Guid)AccountListView.SelectedItems[0].Tag;

        Value = new EditableAccountView(Value.Company, id);
        DialogResult = DialogResult.OK;

        Close();
    }

    private void OnCancelButtonClick(object sender, EventArgs e)
    {
        Close();
    }
}
