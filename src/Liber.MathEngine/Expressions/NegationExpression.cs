// NegationExpression.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Liber.MathEngine.Expressions;

internal class NegationExpression : IExpression
{
    public IExpression InnerExpression { get; }

    public NegationExpression(IExpression innerExpression)
    {
        InnerExpression = innerExpression;
    }

    public decimal Evaluate()
    {
        return -InnerExpression.Evaluate();
    }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return $"-({InnerExpression.ToString(format, formatProvider)})";
    }

    public override string ToString()
    {
        return ToString(format: null, formatProvider: null);
    }
}
