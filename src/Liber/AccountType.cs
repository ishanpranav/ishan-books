// AccountType.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using CsvHelper.Configuration.Attributes;

namespace Liber;

public enum AccountType : short
{
    [LocalizedDescription(nameof(None))]
    None = 0,

    [LocalizedDescription(nameof(Bank))]
    [Name("BANK")]
    Bank = 1100,

    [LocalizedDescription(nameof(CreditCard))]
    [Name("CCARD")]
    CreditCard = -2100,

    [LocalizedDescription(nameof(Equity))]
    [Name("EQUITY")]
    Equity = -3000,

    [LocalizedDescription(nameof(Expense))]
    [Name("EXP")]
    Expense = 6000,

    [LocalizedDescription(nameof(Income))]
    [Name("INC")]
    Income = -4000,

    [LocalizedDescription(nameof(OtherCurrentAsset))]
    [Name("OCASSET")]
    OtherCurrentAsset = 1300,

    [LocalizedDescription(nameof(OtherCurrentLiability))]
    [Name("OCLIAB")]
    OtherCurrentLiability = -2300,

    [LocalizedDescription(nameof(Cost))]
    [Name("COGS")]
    Cost = 5000,

    [LocalizedDescription(nameof(FixedAsset))]
    [Name("FIXASSET")]
    FixedAsset = 1600,

    [LocalizedDescription(nameof(OtherAsset))]
    [Name("OASSET")]
    OtherAsset = 1800,

    [LocalizedDescription(nameof(LongTermLiability))]
    [Name("LTLIAB")]
    LongTermLiability = -2600,

    [LocalizedDescription(nameof(IncomeTaxExpense))]
    [Name("INC")]
    IncomeTaxExpense = 7000,

    [LocalizedDescription(nameof(OtherIncomeExpense))]
    [Name("EXEXP", "EXINC")]
    OtherIncomeExpense = -9000
}

public static class AccountTypeExtensions
{
    public static decimal ToBalance(this AccountType value, decimal debit)
    {
        return Math.Sign((short)value) * debit;
    }
}
