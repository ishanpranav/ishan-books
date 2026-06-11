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
    private readonly Func<Account, bool>? _validator;

    public AccountViewBindingList(Company company, Func<Account, bool>? validator)
    {
        _company = company;
        _validator = validator;

        foreach (Account account in company.Accounts)
        {
            InitializeAccount(account);
        }

        company.AccountAdded += OnCompanyAccountAdded;
        company.AccountUpdated += OnCompanyAccountUpdated;
        company.AccountRemoved += OnCompanyAccountRemoved;
    }

    public void AddNullAccount()
    {
        InsertSorted(NullAccountView.Default);
    }

    private void InitializeAccount(Account value)
    {
        if (_validator == null || _validator(value))
        {
            InsertSorted(new ReadOnlyAccountView(value));
        }
    }

    private void OnCompanyAccountAdded(object? sender, GuidEventArgs e)
    {
        InitializeAccount(_company.GetAccount(e.Id));
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
        Account account = _company.GetAccount(e.Id);
        bool validated = _validator == null || _validator(account);

        if (index == -1)
        {
            if (validated)
            {
                InitializeAccount(account);
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
