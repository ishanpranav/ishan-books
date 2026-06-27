// ReconciledFilter.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.ComponentModel;

namespace Liber.Filters;

public class ReconciledFilter : Filter
{
    public override string Name
    {
        get
        {
            return Value ? "Reconciled" : "Not reconciled";
        }
    }

    [DefaultValue(true)]
    public bool Value { get; set; } = true;

    public override bool IsMatch(Line value)
    {
        return (value.Reconciled != null) == Value;
    }

    public override Filter Clone()
    {
        return new ReconciledFilter()
        {
            Value = Value
        };
    }
}
