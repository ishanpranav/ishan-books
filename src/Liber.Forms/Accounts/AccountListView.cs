// AccountListView.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Humanizer;

namespace Liber.Forms.Accounts;

internal class AccountListView : ListViewEx
{
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new ListViewItemCollection Items
    {
        get
        {
            return base.Items;
        }
    }

    public AccountListView()
    {
        View = View.Details;
        FullRowSelect = true;

        ColumnHeader nameColumn = Columns.Add(Properties.Resources.Name);

        Columns.Add(Properties.Resources.Number);

        nameColumn.DisplayIndex = 1;

        foreach (AccountType type in AccountTypeExtensions.GetSortedValues())
        {
            Groups.Add(type.ToString(), type.Humanize());
        }
    }

    public void Initialize(Company company, IReadOnlySet<Account> checkedAccounts)
    {
        BeginUpdate();

        foreach (KeyValuePair<Guid, Account> account in company.OrderedAccounts)
        {
            if (account.Value.Inactive)
            {
                continue;
            }

            ListViewItem item = Items.Add(account.Key.ToString(), account.Value.Name, imageIndex: 0);
            AccountType type = account.Value.Type;
            string key = type.ToString();

            item.Tag = account;
            item.Group = Groups[key];
            item.SubItems.Add(account.Value.Number.ToString());

            if (checkedAccounts.Contains(account.Value))
            {
                item.Checked = true;
            }
        }

        AutoResizeColumns();
        Sort();
        EndUpdate();
    }

    public Guid GetSelectedAccountId()
    {
        if (SelectedItems.Count < 1)
        {
            return Guid.Empty;
        }

        return ((KeyValuePair<Guid, Account>)SelectedItems[0].Tag!).Key;
    }

    public IReadOnlySet<Account> GetCheckedAccounts()
    {
        HashSet<Account> accounts = new HashSet<Account>();

        foreach (ListViewItem item in CheckedItems)
        {
            accounts.Add(((KeyValuePair<Guid, Account>)item.Tag!).Value);
        }

        return accounts;
    }

    public void AddNullAccount()
    {
        ListViewItem nullAccount = Items.Add(Properties.Resources.NoAccount);

        nullAccount.Tag = KeyValuePair.Create(Guid.Empty, default(Account));
        nullAccount.Selected = true;
    }
}
