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
    Bank = 1100,

    [Name("CREDIT")]
    CreditCard = -2100,

    [Name("EQUITY")]
    Equity = -3000,

    [Name("EXPENSE")]
    Expense = 6000,

    [Name("INCOME")]
    Income = -4000,

    [Name("ASSET", "A/RECEIVABLE")]
    OtherCurrentAsset = 1300,

    [Name("LIABILITY", "A/PAYABLE")]
    OtherCurrentLiability = -2300,

    Cost = 5000,
    FixedAsset = 1600,
    OtherAsset = 1800,
    LongTermLiability = -2600,
    OtherComprehensiveIncome = -3900,
    IncomeTaxExpense = 7000,
    OtherIncomeExpense = -9000
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
