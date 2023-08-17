// AlphaVantageDataType.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Liber.AlphaVantage;

public readonly struct AlphaVantageDataType : IEquatable<AlphaVantageDataType>
{
    private readonly string _value;

    public static readonly AlphaVantageDataType Json;
    public static readonly AlphaVantageDataType Csv = new AlphaVantageDataType("csv");

    public AlphaVantageDataType()
    {
        _value = "json";
    }

    private AlphaVantageDataType(string value)
    {
        _value = value;
    }

    public override bool Equals(object? obj)
    {
        return obj is AlphaVantageDataType other && Equals(other);
    }

    public bool Equals(AlphaVantageDataType other)
    {
        return _value == other._value;
    }

    public static bool operator ==(AlphaVantageDataType left, AlphaVantageDataType right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(AlphaVantageDataType left, AlphaVantageDataType right)
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
