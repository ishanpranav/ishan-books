// DivisionExpression.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Liber.MathEngine.Expressions;

internal class DivisionExpression : IExpression
{
    public IExpression Left { get; }
    public IExpression Right { get; }

    public DivisionExpression(IExpression left, IExpression right)
    {
        Left = left;
        Right = right;
    }

    public decimal Evaluate()
    {
        decimal denominator = Right.Evaluate();

        if (denominator == 0)
        {
            throw new DivideByZeroException();
        }

        return Left.Evaluate() / denominator;
    }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return $"({Left.ToString(format, formatProvider)} / {Right.ToString(format, formatProvider)})";
    }

    public override string ToString()
    {
        return ToString(format: null, formatProvider: null);
    }
}
