// AccountsView.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using Humanizer;

namespace Liber.Forms.Accounts;

[Editor(typeof(AccountsEditor), typeof(UITypeEditor))]
public class AccountsView
{
    public Company Company { get; }
    public IReadOnlySet<Account> Values { get; }

    public AccountsView(Company company)
    {
        Company = company;
        Values = company.Accounts.ToHashSet();
    }

    public AccountsView(Company company, IReadOnlySet<Account> values)
    {
        Company = company;
        Values = values;
    }

    public override string ToString()
    {
        return Properties.Resources.Account.ToQuantity(Values.Count, format: "n0");
    }
}
