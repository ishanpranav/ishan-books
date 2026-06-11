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

        try
        {
            foreach (Account account in company.Accounts)
            {
                if (account.Inactive)
                {
                    continue;
                }

                ListViewItem item = Items.Add(account.Id.ToString(), account.Name, imageIndex: 0);
                AccountType type = account.Type;
                string key = type.ToString();

                item.Tag = account;
                item.Group = Groups[key];
                item.SubItems.Add(account.Number.ToString());

                if (checkedAccounts.Contains(account))
                {
                    item.Checked = true;
                }
            }

            AutoResizeColumns();
            Sort();
        }
        finally
        {
            EndUpdate();
        }
    }

    public Guid GetSelectedAccountId()
    {
        if (SelectedItems.Count < 1)
        {
            return Guid.Empty;
        }

        return ((Account)SelectedItems[0].Tag!).Id;
    }

    public IReadOnlySet<Account> GetCheckedAccounts()
    {
        HashSet<Account> accounts = new HashSet<Account>();

        foreach (ListViewItem item in CheckedItems)
        {
            accounts.Add(((Account)item.Tag!));
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
