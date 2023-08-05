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
    [Name("", "STOCK", "MUTUAL", "TRADING")]
    None = 0,

    [LocalizedDescription(nameof(Bank))]
    [Name("BANK", "CASH")]
    Bank = 1100,

    [LocalizedDescription(nameof(CreditCard))]
    [Name("CREDIT")]
    CreditCard = -2100,

    [LocalizedDescription(nameof(Equity))]
    [Name("EQUITY")]
    Equity = -3000,

    [LocalizedDescription(nameof(Expense))]
    [Name("EXPENSE")]
    Expense = 6000,

    [LocalizedDescription(nameof(Income))]
    [Name("INCOME")]
    Income = -4000,

    [LocalizedDescription(nameof(OtherCurrentAsset))]
    [Name("ASSET", "A/RECEIVABLE")]
    OtherCurrentAsset = 1300,

    [LocalizedDescription(nameof(OtherCurrentLiability))]
    [Name("LIABILITY", "A/PAYABLE")]
    OtherCurrentLiability = -2300,

    [LocalizedDescription(nameof(Cost))]
    Cost = 5000,

    [LocalizedDescription(nameof(FixedAsset))]
    FixedAsset = 1600,

    [LocalizedDescription(nameof(OtherAsset))]
    OtherAsset = 1800,

    [LocalizedDescription(nameof(LongTermLiability))]
    LongTermLiability = -2600,

    [LocalizedDescription(nameof(OtherComprehensiveIncome))]
    OtherComprehensiveIncome = -3900,

    [LocalizedDescription(nameof(IncomeTaxExpense))]
    IncomeTaxExpense = 7000,

    [LocalizedDescription(nameof(OtherIncomeExpense))]
    OtherIncomeExpense = -9000
}

public static class AccountTypeExtensions
{
    public static decimal ToBalance(this AccountType value, decimal debit)
    {
        return Math.Sign((short)value) * debit;
    }
}
