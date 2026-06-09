// Token.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.MathEngine;

internal sealed class Token
{
    public TokenType Type { get; }
    public string Value { get; }
    public int Position { get; }

    public Token(TokenType type, string value, int position)
    {
        Type = type;
        Value = value;
        Position = position;
    }

    public override string ToString()
    {
        if (string.IsNullOrEmpty(Value))
        {
            return $"({Type} @{Position})";
        }

        return $"({Type} '{Value}' @{Position})";
    }
}
