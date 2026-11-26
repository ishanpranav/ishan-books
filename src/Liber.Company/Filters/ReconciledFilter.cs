// ReconciledFilter.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Liber.Filters;

public class ReconciledFilter : Filter
{
    [Browsable(false)]
    [JsonIgnore]
    public override string Name
    {
        get
        {
            return Value ? CompanyResources.Reconciled : CompanyResources.Unreconciled;
        }
    }

    [DefaultValue(true)]
    [LocalizedCategory(nameof(Value))]
    [LocalizedDescription(nameof(Value))]
    [LocalizedDisplayName(nameof(Value))]
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
