using System;

namespace Liber.Forms.Accounts;

internal sealed class AccountView
{
    public AccountView(Guid id, Account value)
    {
        Key = id;
        Value = value;
    }

    public Guid Key { get; }
    public Account Value { get; }

    public override string ToString()
    {
        return Value.ToString();
    }
}
