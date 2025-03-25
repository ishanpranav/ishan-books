// AccountView.cs
// Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Liber.Forms.Accounts;

internal sealed class AccountView : IAccountView
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
            return Value.Name;
        }
    }

    public bool Equals(Guid other)
    {
        return Id.Equals(other);
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
        return Id.GetHashCode();
    }

    public override string ToString()
    {
        return DisplayName;
    }
}
