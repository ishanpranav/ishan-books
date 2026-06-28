// DescriptionFilter.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.ComponentModel;
using Liber.Properties;

namespace Liber.Filters;

public class DescriptionFilter : Filter
{
    [Browsable(false)]
    public override string Name
    {
        get
        {
            if (string.IsNullOrWhiteSpace(Value))
            {
                return Resources.Description;
            }

            return $"{Resources.Description} = '{Value}'";
        }
    }

    [LocalizedCategory(nameof(Value))]
    [LocalizedDescription(nameof(Value))]
    [LocalizedDisplayName(nameof(Value))]
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
