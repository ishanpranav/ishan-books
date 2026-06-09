// ParsingException.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Liber.MathEngine.Exceptions;

public class ParsingException : MathEngineException
{
    public TokenType Type { get; }
    public TokenType ExpectedType { get; }

    public ParsingException(TokenType type, TokenType expected, int offset, int length) :
        base(offset, length)
    {
        Type = type;
        ExpectedType = expected;
    }
}
