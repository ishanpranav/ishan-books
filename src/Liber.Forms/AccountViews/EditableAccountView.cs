// EditableAccountView.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using System.Drawing.Design;
using Liber.Forms.Accounts;
using Liber.Forms.Properties;

namespace Liber.Forms.AccountViews;

[Editor(typeof(AccountEditor), typeof(UITypeEditor))]
public class EditableAccountView : AccountView
{
    public Company Company { get; }
    public override Guid Id { get; }

    public override Account? Value
    {
        get
        {
            if (Id == Guid.Empty)
            {
                return null;
            }

            return Company.GetAccount(Id);
        }
    }

    public override string DisplayName
    {
        get
        {
            if (Value == null)
            {
                return Resources.NoAccount;
            }

            return Value.Name;
        }
    }

    public EditableAccountView(Company company)
    {
        Company = company;
    }

    public EditableAccountView(Company company, Guid id) : this(company)
    {
        Id = id;
    }
}
