// AccountsView.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using Humanizer;
using Liber.Forms.Properties;

namespace Liber.Forms.Accounts;

[Editor(typeof(AccountsEditor), typeof(UITypeEditor))]
public class AccountsView
{
    public AccountsView(Company company)
    {
        Company = company;
        Values = company.Accounts.Values.ToHashSet();
    }

    public AccountsView(Company company, IReadOnlySet<Account> values)
    {
        Company = company;
        Values = values;
    }

    public Company Company { get; }
    public IReadOnlySet<Account> Values { get; }

    public override string ToString()
    {
        return Resources.Account.ToQuantity(Values.Count, "n0");
    }
}
