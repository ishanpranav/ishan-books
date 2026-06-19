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
        SmallImageList = AccountImageListManager.ImageList;

        ColumnHeader nameColumn = Columns.Add(Properties.Resources.Name);
        ColumnHeader numberColumn = Columns.Add(Properties.Resources.Number);

        numberColumn.DisplayIndex = 0;
        nameColumn.DisplayIndex = 1;

        foreach (AccountType type in AccountTypeExtensions.GetSortedValues())
        {
            Groups.Add(type.ToString(), type.Humanize());
        }
    }

    public void Initialize(Company company, IReadOnlySet<Account> checkedAccounts, Func<Account, bool>? validator)
    {
        BeginUpdate();

        try
        {
            foreach (Account account in company.Accounts)
            {
                if (validator != null && !validator(account))
                {
                    continue;
                }

                ListViewItem item = Items.Add(
                    account.Id.ToString(),
                    account.Name,
                    AccountImageListManager.GetImageIndex(company.GetColorOrDefault(account)));
                AccountType type = account.Type;
                string key = type.ToString();

                item.Tag = account;
                item.Group = Groups[key];
                item.SubItems.Add(account.Number.ToStringOrEmpty());

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

        object? tag = SelectedItems[0].Tag;

        if (tag == null)
        {
            return Guid.Empty;
        }

        return ((Account)tag).Id;
    }

    public IReadOnlySet<Account> GetCheckedAccounts()
    {
        HashSet<Account> accounts = new HashSet<Account>();

        foreach (ListViewItem item in CheckedItems)
        {
            if (item.Tag != null)
            {
                accounts.Add(((Account)item.Tag));
            }
        }

        return accounts;
    }

    public void AddNullAccount()
    {
        ListViewItem nullAccount = Items.Add(Properties.Resources.NoAccount);

        nullAccount.Tag = null;
        nullAccount.Selected = true;
    }
}
