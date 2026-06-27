// ConjunctionFilter.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Liber.Filters;

public class ConjunctionFilter : Filter
{
    public override string Name
    {
        get
        {
            switch (Conjunction)
            {
                case Conjunction.And: return "All of the following";
                case Conjunction.Or: return "Any of the following";
            }

            return "None";
        }
    }

    [DefaultValue(Conjunction.And)]
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
            return "All";
        }

        switch (Conjunction)
        {
            case Conjunction.And: return $"({string.Join(separator: " and ", Children)})";
            case Conjunction.Or: return $"({string.Join(separator: " or ", Children)})";
        }

        return "None";
    }
}
