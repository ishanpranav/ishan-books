// Tax.cs
// Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Liber.TaxNodes;

namespace Liber;

public class Tax
{
    private readonly HashSet<TaxNode> _lines = new HashSet<TaxNode>();

    public IReadOnlyCollection<TaxComponent> Forms { get; set; } = new List<TaxComponent>();

    [JsonIgnore]
    public IReadOnlyCollection<TaxNode> Lines
    {
        get
        {
            return _lines;
        }
    }

    public void Evaluate(DateTime started, DateTime posted)
    {
        foreach (TaxComponent component in Forms)
        {
            foreach (TaxNode node in component.Lines)
            {
                _lines.Add(node);
            }
        }

        foreach (TaxNode line in _lines)
        {
            line.Clear();
        }

        foreach (TaxNode line in _lines)
        {
            line.Evaluate(started, posted);
        }
    }
}
