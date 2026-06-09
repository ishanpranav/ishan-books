// IExpression.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Liber.MathEngine.Expressions;

public interface IExpression : IFormattable
{
    decimal Evaluate();
}
