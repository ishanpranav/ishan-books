// TransactionNameFilter.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.Filters;

public class TransactionNameFilter : Filter
{
    public override string Name
    {
        get
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                return "Name";
            }

            return $"Name '{Value}'";
        }
    }

    public string? Value { get; set; }

    public override bool IsMatch(Line value)
    {
        return value.Transaction.Name == Value;
    }

    public override Filter Clone()
    {
        return new TransactionNameFilter()
        {
            Value = Value
        };
    }
}
