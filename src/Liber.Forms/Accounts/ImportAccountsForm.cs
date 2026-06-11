// ImportAccountsForm.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Liber.Forms.Components;
using Liber.Forms.Properties;
using Liber.Forms.Reports;

namespace Liber.Forms.Accounts;

internal sealed class ImportAccountsForm : ImportForm
{
    private readonly FormFactory _factory;
    private readonly ReportEngine _engine;
    private readonly IReadOnlyCollection<GnuCashAccount> _accounts;
    private readonly ImportContext _context;

    public ImportAccountsForm(
        Company company,
        FormFactory factory,
        ReportEngine engine,
        IReadOnlyCollection<GnuCashAccount> accounts) : base(company)
    {
        _factory = factory;
        _accounts = accounts;
        _engine = engine;

        List<Account> values = accounts
            .Select(x => x.Value)
            .ToList();
        ImportRule[]? rules = JsonSerializer.Deserialize<ImportRule[]>(Settings.Default.ImportRules, FormattedStrings.JsonOptions);

        _context = new ImportContext(values)
        {
            EquityAccount = company.Accounts[company.EquityAccountId],
            OtherEquityAccount = company.Accounts[company.OtherEquityAccountId],
            Color = company.Color
        };

        _context.Apply(rules ?? Enumerable.Empty<ImportRule>());
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
            Guid id = Company.AddAccount(account.Value, Guid.Empty);

            if (_context.EquityAccount == account.Value)
            {
                Company.EquityAccountId = id;
            }

            if (_context.OtherEquityAccount == account.Value)
            {
                Company.OtherEquityAccountId = id;
            }

            account.Id = id;
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

        _factory.Register(typeof(AccountsForm).GUID, new AccountsForm(Company, _factory, _engine));
    }
}
