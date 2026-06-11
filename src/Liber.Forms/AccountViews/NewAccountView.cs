// NewAccountView.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using Liber.Forms.Properties;

namespace Liber.Forms.AccountViews;

internal sealed class NewAccountView : IAccountView
{
    private static NewAccountView? s_instance;

    public static NewAccountView Value
    {
        get
        {
            s_instance ??= new NewAccountView();

            return s_instance;
        }
    }

    public Guid Id
    {
        get
        {
            return typeof(NewAccountView).GUID;
        }
    }

    public string DisplayName
    {
        get
        {
            return Resources.NewAccount;
        }
    }

    private NewAccountView() { }

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
