// MathEngineException.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Liber.MathEngine.Exceptions;

public class MathEngineException : Exception
{
    public int Offset { get; }
    public int Length { get; }

    public MathEngineException(int offset, int length)
    {
        Offset = offset;
        Length = length;
    }
}
