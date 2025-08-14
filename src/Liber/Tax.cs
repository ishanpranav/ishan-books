// Tax.cs
// Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using Liber.TaxNodes;

namespace Liber;

public class Tax
{
    internal readonly List<TaxComponent> components = new List<TaxComponent>();
    internal readonly List<TaxNode> nodes = new List<TaxNode>();

    public IReadOnlyCollection<TaxComponent> Components
    {
        get
        {
            return components;
        }
    }

    public IReadOnlyCollection<TaxNode> Nodes
    {
        get
        {
            return nodes;
        }
    }

    public void Evaluate()
    {
        foreach (TaxNode node in nodes)
        {
            node.Clear();
        }

        foreach (TaxNode node in nodes)
        {
            node.Evaluate();
        }
    }
}
