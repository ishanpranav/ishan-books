//
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Liber.MathEngine.Expressions;

internal class SubtractionExpression : IExpression
{
    public IExpression Left { get; }
    public IExpression Right { get; }

    public SubtractionExpression(IExpression left, IExpression right)
    {
        Left = left;
        Right = right;
    }

    public decimal Evaluate()
    {
        return Left.Evaluate() - Right.Evaluate();
    }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return $"({Left.ToString(format, formatProvider)} - {Right.ToString(format, formatProvider)})";
    }

    public override string ToString()
    {
        return ToString(format: null, formatProvider: null);
    }
}
