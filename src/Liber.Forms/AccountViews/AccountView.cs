// AccountView.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Liber.Forms.AccountViews;

public abstract class AccountView : IComparable<AccountView>, IComparable
{
    public abstract Guid Id { get; }
    public abstract string DisplayName { get; }
    public abstract Account? Value { get; }

    public int CompareTo(object? obj)
    {
        if (obj == null)
        {
            return 1;
        }

        if (obj is not AccountView account)
        {
            throw new ArgumentException(message: null, nameof(obj));
        }

        return CompareTo(account);
    }

    public int CompareTo(AccountView? other)
    {
        if (other == null)
        {
            return 1;
        }

        if (Value == null)
        {
            if (other.Value == null)
            {
                return 0;
            }

            return -1;
        }

        int result = Value.CompareTo(other.Value);

        if (result == 0)
        {
            return Id.CompareTo(other.Id);
        }

        return result;
    }

    public override string ToString()
    {
        return DisplayName;
    }
}
