// NewAccountView.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using Liber.Forms.Properties;

namespace Liber.Forms.AccountViews;

internal sealed class NewAccountView : AccountView
{
    private static NewAccountView? s_instance;

    public static NewAccountView Default
    {
        get
        {
            s_instance ??= new NewAccountView();

            return s_instance;
        }
    }

    public override Guid Id
    {
        get
        {
            return typeof(NewAccountView).GUID;
        }
    }

    public override string DisplayName
    {
        get
        {
            return Resources.NewAccount;
        }
    }

    public override Account? Value
    {
        get
        {
            return null;        
        }
    }

    private NewAccountView() { }
}
