// TaxNode.cs
// Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;

namespace Liber.TaxNodes;

public abstract class TaxNode
{
    protected TaxNode(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; }
    public string Description { get; }
    public decimal? Value { get; private set; }
    public event EventHandler? Evaluated;

    public IReadOnlyCollection<TaxNode> Dependencies { get; } = new List<TaxNode>();

    public void Evaluate()
    {
        if (Value != null)
        {
            return;
        }

        foreach (TaxNode dependency in Dependencies)
        {
            dependency.Evaluate();
        }

        Value = EvaluateCore();

        OnEvaluated(EventArgs.Empty);
    }

    protected abstract decimal EvaluateCore();

    protected virtual void OnEvaluated(EventArgs e)
    {
        Evaluated?.Invoke(sender: this, e);
    }

    public void Clear()
    {
        Value = null;
    }
}
