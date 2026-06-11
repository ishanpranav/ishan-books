// ReadOnlyAccountView.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Liber.Forms.AccountViews;

internal class ReadOnlyAccountView : AccountView
{
    public override Guid Id { get; }
    public override Account Value { get; }

    public override string DisplayName
    {
        get
        {
            return Value.Name;
        }
    }

    public ReadOnlyAccountView(Guid id, Account value)
    {
        Id = id;
        Value = value;
    }
}
