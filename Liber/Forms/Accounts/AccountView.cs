using System;

namespace Liber.Forms.Accounts;

internal sealed class AccountView : IEquatable<AccountView>, IEquatable<Guid>
{
    public AccountView(Guid id, Account value)
    {
        Id = id;
        Value = value;
    }

    public Guid Id { get; }
    public Account Value { get; }

    public string DisplayName
    {
        get
        {
            return Value.QualifiedName;
        }
    }

    public bool Equals(Guid other)
    {
        return Id == other;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Guid id)
        {
            return Equals(id);
        }

        if (obj is AccountView view)
        {
            return Equals(view);
        }

        return false;
    }

    public bool Equals(AccountView? other)
    {
        return other != null && Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
