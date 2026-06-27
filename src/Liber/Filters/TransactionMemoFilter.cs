// TransactionMemoFilter.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.Filters;

public class TransactionMemoFilter : Filter
{
    public override string Name
    {
        get
        {
            if (string.IsNullOrWhiteSpace(Value))
            {
                return "Memo";
            }

            return $"Memo '{Value}'";
        }
    }

    public string? Value { get; set; }

    public override bool IsMatch(Line value)
    {
        return value.Transaction.Memo == Value;
    }

    public override Filter Clone()
    {
        return new TransactionMemoFilter()
        {
            Value = Value
        };
    }
}
