// TokenType.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.MathEngine;

public enum TokenType
{
    End = 0,
    Number = 1,
    Plus = 2,
    Minus = 3,
    Star = 4,
    Slash = 5,
    Left = 6,
    Right = 7
}

internal static class TokenTypeExtensions
{
    public static Precedence ToPrecedence(this TokenType value)
    {
        switch (value)
        {
            case TokenType.Plus:
            case TokenType.Minus:
                return Precedence.AdditionSubtraction;

            case TokenType.Star:
            case TokenType.Slash:
                return Precedence.MultiplicationDivision;
        }

        return Precedence.None;
    }
}
