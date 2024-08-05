// AccountType.cs
// Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using CsvHelper.Configuration.Attributes;

namespace Liber;

/// <summary>
/// Represents the type of a financial account.
/// </summary>
public enum AccountType : short
{
    /// <summary>
    /// Specifies an uncategorized account.
    /// </summary>
    [LocalizedDescription(nameof(None))]
    None = 0,

    /// <summary>
    /// Specifies a bank account. This account type records cash and cash equivalents, checks, and deposits.
    /// </summary>
    [LocalizedDescription(nameof(Bank))]
    [Name("BANK")]
    Bank = 1100,

    /// <summary>
    /// Specifies a current asset (other than cash and accounts receivable). This account type records accrued revenue, prepayments, tax withholdings, short-term notes receivable, and the current portion of long-term receivables.
    /// </summary>
    [LocalizedDescription(nameof(OtherCurrentAsset))]
    [Name("OCASSET")]
    OtherCurrentAsset = 1300,

    /// <summary>
    /// Specifies a fixed asset. This account type records property, plant, and equipment.
    /// </summary>
    [LocalizedDescription(nameof(FixedAsset))]
    [Name("FIXASSET")]
    FixedAsset = 1600,

    /// <summary>
    /// Specifies an other asset. This account type records long-term notes and loans receivable, investments, intellectual property, and goodwill.
    /// </summary>
    [LocalizedDescription(nameof(OtherAsset))]
    [Name("OASSET")]
    OtherAsset = 1800,

    /// <summary>
    /// Specifies a credit card or line of credit. This account type records payments and charges.
    /// </summary>
    [LocalizedDescription(nameof(CreditCard))]
    [Name("CCARD")]
    CreditCard = -2100,

    /// <summary>
    /// Specifies a current liability (other than credit cards and accounts payable). This account type records unearned revenue, accrued expenses, tax liabilities, short-term notes payable, and the current portion of long-term payables.
    /// </summary>
    [LocalizedDescription(nameof(OtherCurrentLiability))]
    [Name("OCLIAB")]
    OtherCurrentLiability = -2300,

    /// <summary>
    /// Specifies a long-term liability. This account type records long-term notes and loans payable, warranties, and legal liabilities.
    /// </summary>
    [LocalizedDescription(nameof(LongTermLiability))]
    [Name("LTLIAB")]
    LongTermLiability = -2600,

    /// <summary>
    /// Specifies an equity account. This account type records opening balance equity, common stock, preferred stock, retained earnings, and accumulated other comprehensive income.
    /// </summary>
    [LocalizedDescription(nameof(Equity))]
    [Name("EQUITY")]
    Equity = -3000,

    /// <summary>
    /// Specifies an income account. This account type records income, revenue, sales, and sales returns and allowances.
    /// </summary>
    [LocalizedDescription(nameof(Income))]
    [Name("INC")]
    Income = -4000,

    /// <summary>
    /// Specifies a cost of goods sold, cost of revenue, or cost of sales account. This account type records inventory costs.
    /// </summary>
    [LocalizedDescription(nameof(Cost))]
    [Name("COGS")]
    Cost = 5000,

    /// <summary>
    /// Specifies an expense account. This account type records selling, general, and administrative expenses.
    /// </summary>
    [LocalizedDescription(nameof(Expense))]
    [Name("EXP")]
    Expense = 6000,

    /// <summary>
    /// Specifies an income tax expense. This account type records taxes.
    /// </summary>
    [LocalizedDescription(nameof(IncomeTaxExpense))]
    [Name("INC")]
    IncomeTaxExpense = 7000,

    /// <summary>
    /// Specifies an other income or expense account. This account is used for realized capital gains and losses and extraordinary income and expenses.
    /// </summary>
    [LocalizedDescription(nameof(OtherIncomeExpense))]
    [Name("EXEXP", "EXINC")]
    OtherIncomeExpense = -9000
}

public static class AccountTypeExtensions
{
    public static bool IsAsset(this AccountType value)
    {
        switch (value)
        {
            case AccountType.Bank:
            case AccountType.OtherCurrentAsset:
            case AccountType.OtherCurrentLiability:
            case AccountType.FixedAsset:
            case AccountType.OtherAsset:
                return true;

            default:
                return false;
        }
    }

    public static bool IsTemporary(this AccountType value)
    {
        switch (value)
        {
            case AccountType.Expense:
            case AccountType.Income:
            case AccountType.Cost:
            case AccountType.OtherIncomeExpense:
            case AccountType.IncomeTaxExpense:
                return true;

            default:
                return false;
        }
    }

    public static decimal ToBalance(this AccountType value, decimal debit)
    {
        return Math.Sign((short)value) * debit;
    }
}
