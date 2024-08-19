// AccountDialog.cs
// Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Humanizer;

namespace Liber.Forms.Accounts;

internal sealed partial class AccountDialog : Form
{
    public AccountDialog(EditableAccountView value)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        DialogResult = DialogResult.Cancel;
        Value = value;

        _listView.BeginUpdate();

        foreach (AccountType type in AccountTypeExtensions.GetSortedValues())
        {
            _listView.Groups.Add(type.ToString(), type.Humanize());
        }

        foreach (KeyValuePair<Guid, Account> account in value.Company.Accounts)
        {
            ListViewItem item = _listView.Items.Add(account.Value.Name);
            AccountType type = account.Value.Type;
            string key = type.ToString();

            item.Tag = account.Key;
            item.Group = _listView.Groups[key];
            item.SubItems.Add(account.Value.Number.ToString());
        }

        _listView.AutoResizeColumns();
        _listView.Sort();
        _listView.EndUpdate();
    }

    public EditableAccountView Value { get; }

    private void OnAcceptButtonClick(object sender, EventArgs e)
    {
        Value.Id = (Guid)_listView.SelectedItems[0].Tag;
        DialogResult = DialogResult.OK;

        Close();
    }

    private void OnCancelButtonClick(object sender, EventArgs e)
    {
        Close();
    }
}
