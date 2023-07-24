// AccountView.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Liber.Forms.Accounts;

internal sealed class AccountView : IEquatable<Guid>
{
    public AccountView(Guid id, Account value)
    {
        Key = id;
        Value = value;
    }

    public Guid Key { get; }
    public Account Value { get; }

    public string DisplayName
    {
        get
        {
            return Value.ToString();
        }
    }

    public bool Equals(Guid other)
    {
        return Key.Equals(other);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }

        if (this == obj)
        {
            return true;
        }

        return obj is Guid other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Key.GetHashCode();
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
