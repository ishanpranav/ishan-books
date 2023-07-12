using System;

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
    Asset = 1,

    /// <summary>
    /// 
    /// </summary>
    Liability = -2,

    /// <summary>
    /// 
    /// </summary>
    Equity = -3,

    /// <summary>
    /// 
    /// </summary>
    Income = -4,

    /// <summary>
    /// 
    /// </summary>
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
