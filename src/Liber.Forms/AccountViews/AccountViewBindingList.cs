// AccountViewBindingList.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Liber.Forms.AccountViews;

internal sealed class AccountViewBindingList : SortedBindingList<AccountView>
{
    private readonly Company _company;
    private readonly Func<Guid, bool>? _validator;

    public AccountViewBindingList(Company company, Func<Guid, bool>? validator)
    {
        _company = company;
        _validator = validator;

        foreach (KeyValuePair<Guid, Account> account in company.OrderedAccounts)
        {
            InitializeAccount(account.Key, account.Value);
        }

        company.AccountAdded += OnCompanyAccountAdded;
        company.AccountUpdated += OnCompanyAccountUpdated;
        company.AccountRemoved += OnCompanyAccountRemoved;
    }

    public void AddNullAccount()
    {
        InsertSorted(NullAccountView.Default);
    }

    private void InitializeAccount(Guid id, Account value)
    {
        if (_validator == null || _validator(id))
        {
            InsertSorted(new ReadOnlyAccountView(id, value));
        }
    }

    private void OnCompanyAccountAdded(object? sender, GuidEventArgs e)
    {
        InitializeAccount(e.Id, _company.GetAccount(e.Id));
    }

    private void OnCompanyAccountRemoved(object? sender, GuidEventArgs e)
    {
        int index = FindByKey(e.Id, v => v.Id);

        if (index != -1)
        {
            RemoveSortedAt(index);
        }
    }

    private void OnCompanyAccountUpdated(object? sender, GuidEventArgs e)
    {
        int index = FindByKey(e.Id, v => v.Id);
        bool validated = _validator == null || _validator(e.Id);

        if (index == -1)
        {
            if (validated)
            {
                InitializeAccount(e.Id, _company.GetAccount(e.Id));
            }

            return;
        }

        if (!validated)
        {
            RemoveSortedAt(index);

            return;
        }

        NotifyItemChanged(index);
    }
}
