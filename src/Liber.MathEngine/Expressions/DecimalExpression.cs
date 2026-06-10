// DecimalExpression.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Liber.MathEngine.Expressions;

public class DecimalExpression : IExpression
{
    public decimal Value { get; }

    public DecimalExpression(decimal value)
    {
        Value = value;
    }

    public decimal Evaluate()
    {
        return Value;
    }

    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        return Value.ToString(format, formatProvider);
    }

    public override string ToString()
    {
        return ToString(format: null, formatProvider: null);
    }
}
