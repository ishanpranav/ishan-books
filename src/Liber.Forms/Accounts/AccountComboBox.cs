// AccountComboBox.cs
// Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Liber.Forms.Accounts;

internal sealed class AccountComboBox : ComboBox
{
    private Company? _company;
    private Func<Guid, bool>? _validator;

    public AccountComboBox()
    {
        DropDownStyle = ComboBoxStyle.DropDownList;
    }

    [Browsable(false)]
    public void Initialize(Company? company, Func<Guid, bool>? validator)
    {
        FreeCompany();

        _company = company;
        _validator = validator;

        if (company == null)
        {
            return;
        }

        foreach (KeyValuePair<Guid, Account> account in company.OrderedAccounts)
        {
            InitializeAccount(account.Key, account.Value);
        }

        company.AccountAdded += OnCompanyAccountAdded;
        company.AccountUpdated += OnCompanyAccountUpdated;
        company.AccountRemoved += OnCompanyAccountRemoved;

        if (Items.Contains(Guid.Empty))
        {
            SelectedItem = Guid.Empty;
        }
    }

    public new Guid SelectedItem
    {
        get
        {
            return ((IAccountView)base.SelectedItem).Id;
        }
        set
        {
            base.SelectedItem = value;
        }
    }

    private void InitializeAccount(Guid id, Account value)
    {
        if (_validator == null || _validator(id))
        {
            Items.Add(new AccountView(id, value));
        }
    }

    private void OnCompanyAccountAdded(object? sender, GuidEventArgs e)
    {
        InitializeAccount(e.Id, _company!.Accounts[e.Id]);
    }

    private void OnCompanyAccountUpdated(object? sender, GuidEventArgs e)
    {
        if (_validator != null && !_validator(e.Id))
        {
            return;
        }

        Items.Remove(e.Id);

        if (!_company!.Accounts[e.Id].Placeholder)
        {
            Items.Add(e.Id);
        }

        Refresh();
    }

    private void OnCompanyAccountRemoved(object? sender, GuidEventArgs e)
    {
        Items.Remove(e.Id);
    }

    private void FreeCompany()
    {
        if (_company == null)
        {
            return;
        }

        _company.AccountAdded -= OnCompanyAccountAdded;
        _company.AccountUpdated -= OnCompanyAccountUpdated;
        _company.AccountRemoved -= OnCompanyAccountRemoved;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            FreeCompany();
        }

        base.Dispose(disposing);
    }
}
