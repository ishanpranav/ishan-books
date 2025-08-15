// TaxNode.cs
// Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text.Json.Serialization;

namespace Liber.TaxNodes;

[JsonDerivedType(typeof(AccountTaxNode), "account")]
[JsonDerivedType(typeof(NotImplementedTaxNode), "not-implemented")]
[JsonDerivedType(typeof(SumTaxNode), "sum")]
public abstract class TaxNode
{
    private decimal? _value;

    public string? Name { get; set; }
    public string? Description { get; set; }

    public event EventHandler<DecimalEventArgs>? Evaluated;

    public decimal Evaluate(DateTime started, DateTime posted)
    {
        if (_value is decimal result)
        {
            return result;
        }

        result = EvaluateCore(started, posted);
        _value = result;

        OnEvaluated(new DecimalEventArgs(result));

        return result;
    }

    protected abstract decimal EvaluateCore(DateTime started, DateTime posted);

    protected virtual void OnEvaluated(DecimalEventArgs e)
    {
        Evaluated?.Invoke(sender: this, e);
    }

    public void Clear()
    {
        _value = null;
    }
}
