// EditableAccountView.cs
// Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using System.Drawing.Design;
using Liber.Forms.Properties;

namespace Liber.Forms.Accounts;

[Editor(typeof(AccountEditor), typeof(UITypeEditor))]
public class EditableAccountView : IAccountView
{
    public EditableAccountView(Company company)
    {
        Company = company;
    }

    public EditableAccountView(Company company, Guid id) : this(company)
    {
        Id = id;
    }

    public Company Company { get; }
    public Guid Id { get; }

    public Account? Value
    {
        get
        {
            if (Id == Guid.Empty)
            {
                return null;
            }

            return Company.Accounts[Id];
        }
    }

    public string DisplayName
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
