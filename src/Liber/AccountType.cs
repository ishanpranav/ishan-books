// AccountType.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Resources;
using CsvHelper.Configuration.Attributes;

namespace Liber;

public enum AccountType : short
{
    [Name("", "STOCK", "MUTUAL", "TRADING")]
    None = 0,

    [Name("BANK", "CASH")]
    Bank = 11,

    [Name("CREDIT")]
    CreditCard = -21,

    [Name("EQUITY")]
    Equity = -30,

    [Name("EXPENSE")]
    Expense = 60,

    [Name("INCOME")]
    Income = -40,

    [Name("ASSET", "A/RECEIVABLE")]
    OtherCurrentAsset = 13,

    [Name("LIABILITY", "A/PAYABLE")]
    OtherCurrentLiability = -23,

    [Name("EXPENSE")]
    Cost = 5000,

    [Name("ASSET")]
    FixedAsset = 1600,

    [Name("ASSET")]
    OtherAsset = 1800,

    [Name("LIABILITY")]
    LongTermLiability = -2600,

    [Name("EQUITY")]
    OtherComprehensiveIncome = -3900,

    [Name("EXPENSE")]
    IncomeTaxExpense = 7000,

    [Name("EXPENSE")]
    OtherIncomeExpense = 9000
}

public static class AccountTypeExtensions
{
    private static readonly ResourceManager s_resourceManager = new ResourceManager(typeof(AccountTypeExtensions));

    public static decimal ToBalance(this AccountType value, decimal debit)
    {
        return value.Sign() * debit;
    }

    public static int Sign(this AccountType value)
    {
        return Math.Sign((int)value);
    }

    public static bool IsDebit(this AccountType value)
    {
        return value >= 0;
    }

    public static string ToLocalizedString(this AccountType value)
    {
        string key = value.ToString();

        return s_resourceManager.GetString(key) ?? key;
    }
}
