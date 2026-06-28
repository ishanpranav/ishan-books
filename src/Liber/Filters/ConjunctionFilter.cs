// ConjunctionFilter.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Liber.Properties;

namespace Liber.Filters;

public class ConjunctionFilter : Filter
{
    [Browsable(false)]
    public override string Name
    {
        get
        {
            switch (Conjunction)
            {
                case Conjunction.And: return Resources.And;
                case Conjunction.Or: return Resources.Or;
            }

            return Resources.None;
        }
    }

    [DefaultValue(Conjunction.And)]
    [LocalizedCategory(nameof(Conjunction))]
    [LocalizedDescription(nameof(Conjunction))]
    [LocalizedDisplayName(nameof(Conjunction))]
    public Conjunction Conjunction { get; set; } = Conjunction.And;

    [Browsable(false)]
    public ICollection<Filter> Children { get; set; } = new List<Filter>();

    public override bool IsMatch(Line value)
    {
        switch (Conjunction)
        {
            case Conjunction.And: return Children.All(x => x.IsMatch(value));
            case Conjunction.Or: return Children.Any(x => x.IsMatch(value));
        }

        return false;
    }

    public override Filter Clone()
    {
        return new ConjunctionFilter()
        {
            Children = Children
                .Select(x => x.Clone())
                .ToList(),
            Conjunction = Conjunction
        };
    }

    public override string ToString()
    {
        if (Children.Count == 0)
        {
            return Resources.All;
        }

        switch (Conjunction)
        {
            case Conjunction.And: return $"({string.Join(separator: $" {Resources.AndSeparator} ", Children)})";
            case Conjunction.Or: return $"({string.Join(separator: $" {Resources.OrSeparator} ", Children)})";
        }

        return Resources.None;
    }
}
