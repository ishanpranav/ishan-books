// Dividend.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text.Json.Serialization;

namespace Liber;

public class Dividend :
    ICloneable,
    IComparable<Dividend>,
    IComparable,
    IEquatable<Dividend>
{
    internal Security? security;

    public Dividend() { }

    [JsonConstructor]
    public Dividend(DateTime timestamp)
    {
        Timestamp = timestamp;
    }

    public DateTime Timestamp { get; private set; }
    public decimal Value { get; set; }

    public override string ToString()
    {
        return $"{Timestamp} {security} {Value}";
    }

    public Dividend Clone()
    {
        return new Dividend(Timestamp)
        {
            Value = Value
        };
    }

    object ICloneable.Clone()
    {
        return Clone();
    }

    public int CompareTo(object? obj)
    {
        if (obj == null)
        {
            return 1;
        }

        if (obj is not Dividend other)
        {
            throw new ArgumentException(message: null, nameof(obj));
        }

        return CompareTo(other);
    }

    public int CompareTo(Dividend? other)
    {
        if (other is null)
        {
            return 1;
        }

        int result = Timestamp.CompareTo(other.Timestamp);

        if (result != 0)
        {
            return result;
        }

        if (security != null)
        {
            return security.CompareTo(other.security);
        }

        return 0;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }

        if (ReferenceEquals(obj, this))
        {
            return true;
        }

        return obj is Dividend other && Equals(other);
    }

    public bool Equals(Dividend? other)
    {
        if (other is null)
        {
            return false;
        }

        return security == other.security && Timestamp == other.Timestamp;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(security, Timestamp);
    }

    public static bool operator ==(Dividend? left, Dividend? right)
    {
        if (left is null)
        {
            return right is null;
        }

        return left.Equals(right);
    }

    public static bool operator !=(Dividend? left, Dividend? right)
    {
        return !(left == right);
    }

    public static bool operator <(Dividend? left, Dividend? right)
    {
        return left is null ? right is not null : left.CompareTo(right) < 0;
    }

    public static bool operator <=(Dividend? left, Dividend? right)
    {
        return left is null || left.CompareTo(right) <= 0;
    }

    public static bool operator >(Dividend? left, Dividend? right)
    {
        return left is not null && left.CompareTo(right) > 0;
    }

    public static bool operator >=(Dividend? left, Dividend? right)
    {
        return left is null ? right is null : left.CompareTo(right) >= 0;
    }
}
