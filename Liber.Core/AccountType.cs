using CsvHelper.Configuration.Attributes;
using System;
using System.Resources;

namespace Liber;

public enum AccountType : short
{
    [Name("", "STOCK", "MUTUAL", "TRADING")]
    None = 0,

    [Name("A/RECEIVABLE")]
    AccountReceivable = 1200,

    [Name("A/PAYABLE")]
    AccountPayable = -2200,

    [Name("CASH")]
    Cash = 1101,

    [Name("BANK")]
    Bank = 1102,

    [Name("CREDIT")]
    CreditCard = -2100,

    [Name("EQUITY")]
    Equity = -3000,

    [Name("EXPENSE")]
    Expense = 6000,

    [Name("INCOME")]
    Income = -4000,

    [Name("LIABILITY")]
    OtherCurrentLiability = -2300,

    [Name("ASSET")]
    OtherCurrentAsset = 1300,

    [Name("EXPENSE")]
    Cost = 5000,

    [Name("ASSET")]
    OtherAsset = 1800,

    [Name("ASSET")]
    FixedAsset = 1600,

    [Name("LIABILITY")]
    LongTermLiability = -2600,

    [Name("INCOME")]
    OtherIncome = -4900,

    [Name("EXPENSE")]
    OtherExpense = 6900,

    [Name("EQUITY")]
    OtherComprehensiveIncome = -3900,

    [Name("EXPENSE")]
    IncomeTaxExpense = 7000
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
