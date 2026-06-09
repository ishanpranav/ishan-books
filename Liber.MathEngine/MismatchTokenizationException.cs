// MismatchTokenizationException.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Liber.MathEngine;

public class MismatchTokenizationException : TokenizationException
{
    public char Expected { get; }
    public int ExpectedPosition { get; }

    public MismatchTokenizationException() { }
    public MismatchTokenizationException(string? message, char current, int position, char expected, int expectedPosition) :
        base(message, current, position)
    {
        Expected = expected;
        ExpectedPosition = expectedPosition;
    }

    public MismatchTokenizationException(char current, int position, char expected, int expectedPosition) :
        this($"Unexpected '{current}' at position {position} does not match '{expected}' at position {expectedPosition}.",
            current, position, expected, expectedPosition)
    { }

    public MismatchTokenizationException(string? message) : base(message) { }
    public MismatchTokenizationException(string? message, Exception? innerException) : base(message, innerException) { }
}
