using System;

namespace Liber;

/// <summary>
/// 
/// </summary>
public class AccountEventArgs : EventArgs
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="account"></param>
    public AccountEventArgs(Guid id, Account account)
    {
        Id = id;
        Account = account;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    public Guid Id { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    public Account Account { get; }
}
