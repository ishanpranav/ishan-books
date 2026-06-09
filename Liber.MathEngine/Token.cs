// Token.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.MathEngine;

public class Token
{
    public TokenType Type { get; }
    public string Value { get; }
    public int Offset { get; }

    public Token(TokenType type, string value, int offset)
    {
        Type = type;
        Value = value;
        Offset = offset;
    }

    public override string ToString()
    {
        if (string.IsNullOrEmpty(Value))
        {
            return Type.ToString();
        }

        return $"({Type} '{Value}')";
    }
}
