// TokenizationException.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Liber.MathEngine;

public class TokenizationException : Exception
{
    public char Current { get; }
    public int Position { get; }

    public TokenizationException() { }

    public TokenizationException(string? message, char current, int position) : base(message)
    {
        Current = current;
        Position = position;
    }

    public TokenizationException(char current, int position) :
        this($"Unexpected '{current}' at position {position}.", current, position)
    { }

    public TokenizationException(string? message) : base(message) { }
    public TokenizationException(string? message, Exception? innerException) : base(message, innerException) { }
}
