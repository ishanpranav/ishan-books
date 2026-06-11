// NullAccountView.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Liber.Forms.AccountViews;

internal sealed class NullAccountView : AccountView
{
    private static NullAccountView? s_instance;

    public static NullAccountView Default
    {
        get
        {
            s_instance ??= new NullAccountView();

            return s_instance;
        }
    }

    public override Guid Id
    {
        get
        {
            return Guid.Empty;
        }
    }

    public override string DisplayName
    {
        get
        {
            return string.Empty;
        }
    }

    public override Account? Value
    {
        get
        {
            return null;
        }
    }

    private NullAccountView() { }
}
