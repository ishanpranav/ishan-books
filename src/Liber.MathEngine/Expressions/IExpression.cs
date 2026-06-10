// IExpression.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;

namespace Liber.MathEngine.Expressions;

[TypeConverter(typeof(ExpressionConverter))]
public interface IExpression : IFormattable
{
    decimal Evaluate();
}
