// DescriptionFilter.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.Filters;

public class DescriptionFilter : Filter
{
    public override string Name
    {
        get
        {
            if (string.IsNullOrWhiteSpace(Value))
            {
                return "Description";
            }

            return $"Description '{Value}'";
        }
    }

    public string? Value { get; set; }

    public override bool IsMatch(Line value)
    {
        return value.Description == Value;
    }

    public override Filter Clone()
    {
        return new DescriptionFilter()
        {
            Value = Value
        };
    }
}
