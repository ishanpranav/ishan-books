// ImportContext.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Drawing;

namespace Liber;

public class ImportContext
{
    public ImportContext(IReadOnlyCollection<Account> accounts)
    {
        Accounts = accounts;
    }

    public IReadOnlyCollection<Account> Accounts { get; }
    public Account? EquityAccount { get; set; }
    public Account? OtherEquityAccount { get; set; }
    public Color Color { get; set; }

    public void Apply(IEnumerable<ImportRule> rules)
    {
        if (rules != null)
        {
            foreach (ImportRule rule in rules)
            {
                rule.Apply(context: this);
            }
        }

        foreach (Account account in Accounts)
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

        foreach (Account value in Accounts)
        {
            if (value.CashFlow == CashFlow.None)
            {
                value.CashFlow = value.Type.ToCashFlow();
            }
        }
    }
}
