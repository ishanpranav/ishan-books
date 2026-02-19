// AccountOptions.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.Forms.Accounts;

public class AccountOptions
{
    public AccountOptions(Account value)
    {
        Value = value;
        Selected = true;
    }

    public Account Value { get; }

    public string Name
    {
        get
        {
            return Value.Name;
        }
    }

    public bool Selected { get; set; }
    public bool IsFixedCost { get; set; }
}
