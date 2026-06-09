// MismatchTokenizationException.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.MathEngine.Exceptions;

public class MismatchException : MathEngineException
{
    public int ExpectedOffset { get; }
    public int ExpectedLength { get; }

    public MismatchException(int offset, int length, int expectedOffset, int expectedLength) :
        base(offset, length)
    {
        ExpectedOffset = expectedOffset;
        ExpectedLength = expectedLength;
    }
}
