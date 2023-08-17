// AlphaVantageOutputSize.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Liber.AlphaVantage;

public readonly struct AlphaVantageOutputSize : IEquatable<AlphaVantageOutputSize>
{
    private readonly string _value;

    public static readonly AlphaVantageOutputSize Full;
    public static readonly AlphaVantageOutputSize Compact = new AlphaVantageOutputSize("compact");

    public AlphaVantageOutputSize()
    {
        _value = "full";
    }

    private AlphaVantageOutputSize(string value)
    {
        _value = value;
    }

    public override bool Equals(object? obj)
    {
        return obj is AlphaVantageOutputSize other && Equals(other);
    }

    public bool Equals(AlphaVantageOutputSize other)
    {
        return _value == other._value;
    }

    public static bool operator ==(AlphaVantageOutputSize left, AlphaVantageOutputSize right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(AlphaVantageOutputSize left, AlphaVantageOutputSize right)
    {
        return !(left == right);
    }

    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }

    public override string ToString()
    {
        return _value;
    }
}
