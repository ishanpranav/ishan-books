// CheckView.cs
// Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text;

namespace Liber.Forms.Lines;

[Editor(typeof(CheckEditor), typeof(UITypeEditor))]
internal sealed class CheckView
{
    public CheckView(Company company)
    {
        Company = company;
    }

    public CheckView(Company company, Line value) : this(company)
    {
        Value = value;
    }

    public Company Company { get; }
    public Line? Value { get; }

    public override string ToString()
    {
        if (Value == null)
        {
            return string.Empty;
        }

        Transaction transaction = Value.Transaction!;
        StringBuilder result = new StringBuilder(transaction.Posted.ToShortDateString());

        if (transaction.Number != 0)
        {
            result.Append(' ').Append(transaction.Number);
        }

        if (transaction.Name != null)
        {
            result.Append(' ').Append(transaction.Name);
        }

        return result
            .Append(' ')
            .Append(Math.Abs(Value.Balance))
            .ToString();
    }
}
