using CsvHelper.Configuration.Attributes;
using System;
using System.Resources;

namespace Liber;

public enum AccountType : short
{
    [Name("", "STOCK", "MUTUAL", "TRADING")]
    None = 0,

    [Name("A/RECEIVABLE")]
    AccountReceivable = 12,

    [Name("A/PAYABLE")]
    AccountPayable = -22,

    [Name("CASH")]
    Cash = 1101,

    [Name("BANK")]
    Bank = 1102,

    [Name("CREDIT")]
    CreditCard = -21,

    [Name("EQUITY")]
    Equity = -3101,

    [Name("EXPENSE")]
    Expense = 6,

    [Name("INCOME")]
    Income = -4000,

    [Name("LIABILITY")]
    OtherCurrentLiability = -2300,

    [Name("ASSET")]
    OtherCurrentAsset = 13,

    [Name("EXPENSE")]
    Cost = 50,

    [Name("ASSET")]
    OtherAsset = 18,

    [Name("ASSET")]
    FixedAsset = 16,

    [Name("LIABILITY")]
    LongTermLiability = -26,

    [Name("INCOME")]
    OtherIncome = -49,

    [Name("EXPENSE")]
    OtherExpense = 69,

    [Name("EQUITY")]
    OtherComprehensiveIncome = -39,

    [Name("EXPENSE")]
    IncomeTaxExpense = 7
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
