using CsvHelper.Configuration.Attributes;
using System;
using System.Windows.Forms;

namespace Liber;

/// <summary>
/// 
/// </summary>
public enum AccountType
{
    /// <summary>
    /// 
    /// </summary>
    None = 0,

    /// <summary>
    /// 
    /// </summary>
    [Name("ASSET", "CASH", "BANK")]
    Asset = 1,

    /// <summary>
    /// 
    /// </summary>
    [Name("LIABILITY", "CREDIT")]
    Liability = -2,

    /// <summary>
    /// 
    /// </summary>
    [Name("EQUITY")]
    Equity = -3,

    /// <summary>
    /// 
    /// </summary>
    [Name("INCOME")]
    Income = -4,

    /// <summary>
    /// 
    /// </summary>
    [Name("EXPENSE")]
    Expense = 5
}

/// <summary>
/// 
/// </summary>
public static class AccountTypeExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="debit"></param>
    /// <returns></returns>
    public static decimal ToBalance(this AccountType value, decimal debit)
    {
        return value.Sign() * debit;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int Sign(this AccountType value)
    {
        return Math.Sign((int)value);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsDebit(this AccountType value)
    {
        return value >= 0;
    }
}
