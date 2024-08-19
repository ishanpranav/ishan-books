// ImportAccountsForm.cs
// Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Liber.Forms.Components;
using Liber.Forms.Import;
using Liber.Forms.Properties;

namespace Liber.Forms.Accounts;

internal sealed class ImportAccountsForm : ImportForm
{
    private readonly FormFactory _factory;
    private readonly IReadOnlyCollection<GnuCashAccount> _accounts;

    public ImportAccountsForm(Company company, FormFactory factory, IReadOnlyCollection<GnuCashAccount> accounts) : base(company)
    {
        _factory = factory;
        _accounts = accounts;

        List<Account> values = accounts.Select(x => x.Value).ToList();
        ImportRule[]? rules = JsonSerializer.Deserialize<ImportRule[]>(Settings.Default.ImportRules, FormattedStrings.JsonOptions);

        if (rules != null)
        {
            foreach (ImportRule rule in rules)
            {
                rule.Apply(values);
            }
        }

        foreach (Account account in values)
        {
            switch (account.Type)
            {
                case AccountTypeExtensions.Debit:
                    account.Type = AccountType.OtherCurrentAsset;
                    break;

                case AccountTypeExtensions.Credit:
                    account.Type = AccountType.OtherCurrentLiability;
                    break;
            }
        }

        SetDataSource(values);
    }

    private Guid GetParentId(Guid id, string value)
    {
        foreach (KeyValuePair<Guid, Account> account in Company.Accounts)
        {
            if (account.Key == id)
            {
                continue;
            }

            if (account.Value.ToString() == value || account.Value.Name == value)
            {
                return account.Key;
            }
        }

        return Guid.Empty;
    }

    protected override void CommitChanges()
    {
        _factory.Kill(typeof(AccountsForm).GUID);

        foreach (GnuCashAccount account in _accounts)
        {
            account.Id = Company.AddAccount(account.Value, Guid.Empty);
        }

        foreach (GnuCashAccount account in _accounts)
        {
            string[] segments = account.Path.Split(':');

            if (segments.Length < 2)
            {
                continue;
            }

            Guid parentId = GetParentId(account.Id, segments[segments.Length - 2]);

            if (parentId == Guid.Empty)
            {
                continue;
            }

            Company.UpdateAccount(account.Id, parentId);
        }

        _factory.Register(typeof(AccountsForm).GUID, new AccountsForm(Company, _factory));
    }
}
