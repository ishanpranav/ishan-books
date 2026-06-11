// AccountBindingList.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Liber.Forms.Accounts;

namespace Liber.Forms.AccountViews;

internal class AccountViewBindingList : BindingList<IAccountView>
{
    private readonly Company _company;
    private readonly Func<Guid, bool>? _validator;

    public AccountViewBindingList(Company company, Func<Guid, bool>? validator)
    {
        Add(NullAccountView.Value);

        foreach (KeyValuePair<Guid, Account> account in company.OrderedAccounts)
        {
            InitializeAccount(account.Key, account.Value);
        }

        company.AccountAdded += OnCompanyAccountAdded;
        company.AccountUpdated += OnCompanyAccountUpdated;
        company.AccountRemoved += OnCompanyAccountRemoved;
        _company = company;
        _validator = validator;
    }

    private void InitializeAccount(Guid id, Account value)
    {
        if (_validator == null || _validator(id))
        {
            Add(new AccountView(id, value));
        }
    }

    private void OnCompanyAccountAdded(object? sender, GuidEventArgs e)
    {
        InitializeAccount(e.Id, _company.Accounts[e.Id]);
    }

    private void OnCompanyAccountRemoved(object? sender, GuidEventArgs e)
    {
        int index = Find(e.Id);

        if (index != -1)
        {
            RemoveAt(index);
        }
    }

    private void OnCompanyAccountUpdated(object? sender, GuidEventArgs e)
    {
        int index = Find(e.Id);
        bool validated = _validator == null || _validator(e.Id);


        if (index == -1)
        {
            if (validated)
            {
                InitializeAccount(e.Id, _company.Accounts[e.Id]);
            }

            return;
        }

        if (!validated)
        {
            RemoveAt(index);
        }

        ResetItem(index);
    }

    private int Find(Guid id)
    {
        for (int i = 0; i < Count; i++)
        {
            if (this[i].Id == id)
            {
                return i;
            }
        }

        return -1;
    }
}
