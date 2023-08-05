// NullAccountView.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Liber.Forms.Accounts;

internal sealed class NullAccountView : IAccountView
{
    private static NullAccountView? s_instance;

    public static NullAccountView Value
    {
        get
        {
            s_instance ??= new NullAccountView();

            return s_instance;
        }
    }

    private NullAccountView() { }

    public Guid Id
    {
        get
        {
            return Guid.Empty;
        }
    }

    public string DisplayName
    {
        get
        {
            return string.Empty;
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
