// AccountGroups.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;

namespace Liber.Forms.Accounts;

[Flags]
internal enum AccountGroups
{
    [LocalizedDescription(nameof(None))]
    None = 0,

    [LocalizedDescription(nameof(All))]
    All = Temporary | Permanent,

    [LocalizedDescription(nameof(Temporary))]
    Temporary = Income | Cost | Expense | OtherIncomeExpense | IncomeTaxExpense,

    [LocalizedDescription(nameof(Permanent))]
    Permanent = Assets | Liabilities | Equity,

    [LocalizedDescription(nameof(Assets))]
    Assets = CurrentAssets | NonCurrentAssets,

    [LocalizedDescription(nameof(CurrentAssets))]
    CurrentAssets = Bank | OtherCurrentAsset,

    [LocalizedDescription(nameof(NonCurrentAssets))]
    NonCurrentAssets = FixedAsset | OtherAsset,

    [LocalizedDescription(nameof(Liabilities))]
    Liabilities = CurrentLiabilities | LongTermLiability,

    [LocalizedDescription(nameof(CurrentLiabilities))]
    CurrentLiabilities = CreditCard | OtherCurrentLiability,

    [LocalizedDescription(nameof(Bank))]
    Bank = 1,

    [LocalizedDescription(nameof(OtherCurrentAsset))]
    OtherCurrentAsset = 2,

    [LocalizedDescription(nameof(FixedAsset))]
    FixedAsset = 4,

    [LocalizedDescription(nameof(OtherAsset))]
    OtherAsset = 8,

    [LocalizedDescription(nameof(CreditCard))]
    CreditCard = 16,

    [LocalizedDescription(nameof(OtherCurrentLiability))]
    OtherCurrentLiability = 32,

    [LocalizedDescription(nameof(LongTermLiability))]
    LongTermLiability = 64,

    [LocalizedDescription(nameof(Equity))]
    Equity = 128,

    [LocalizedDescription(nameof(Income))]
    Income = 256,

    [LocalizedDescription(nameof(Cost))]
    Cost = 512,

    [LocalizedDescription(nameof(Expense))]
    Expense = 1024,

    [LocalizedDescription(nameof(OtherIncomeExpense))]
    OtherIncomeExpense = 2048,

    [LocalizedDescription(nameof(IncomeTaxExpense))]
    IncomeTaxExpense = 4096
}


internal static class AccountGroup
{
    public static AccountGroups FromType(AccountType value)
    {
        switch (value)
        {
            case AccountType.Bank: return AccountGroups.Bank;
            case AccountType.OtherCurrentAsset: return AccountGroups.OtherCurrentAsset;
            case AccountType.FixedAsset: return AccountGroups.FixedAsset;
            case AccountType.OtherAsset: return AccountGroups.OtherAsset;
            case AccountType.CreditCard: return AccountGroups.CreditCard;
            case AccountType.OtherCurrentLiability: return AccountGroups.OtherCurrentLiability;
            case AccountType.LongTermLiability: return AccountGroups.LongTermLiability;
            case AccountType.Equity: return AccountGroups.Equity;
            case AccountType.Income: return AccountGroups.Income;
            case AccountType.Cost: return AccountGroups.Cost;
            case AccountType.Expense: return AccountGroups.Expense;
            case AccountType.OtherIncomeExpense: return AccountGroups.OtherIncomeExpense;
            case AccountType.IncomeTaxExpense: return AccountGroups.IncomeTaxExpense;
        }

        return AccountGroups.None;
    }
}
