// Price.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text.Json.Serialization;

namespace Liber;

public class Price :
    ICloneable,
    IComparable<Price>,
    IComparable,
    IEquatable<Price>
{
    private decimal? _adjustedClose;

    internal Security? security;

    public Price() { }

    [JsonConstructor]
    public Price(DateTime timestamp)
    {
        Timestamp = timestamp;
    }

    public DateTime Timestamp { get; private set; }
    public decimal Close { get; set; }

    public decimal? AdjustedClose
    {
        get
        {
            if (_adjustedClose == null || _adjustedClose == 0)
            {
                return Close;
            }

            return _adjustedClose;
        }
        set
        {
            if (value == 0)
            {
                value = null;
            }

            _adjustedClose = value;
        }
    }

    public override string ToString()
    {
        return $"{Timestamp} {security} {AdjustedClose}";
    }

    public Price Clone()
    {
        return new Price(Timestamp)
        {
            Close = Close,
            AdjustedClose = AdjustedClose,
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

        if (obj is not Price other)
        {
            throw new ArgumentException(message: null, nameof(obj));
        }

        return CompareTo(other);
    }

    public int CompareTo(Price? other)
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
            result = security.CompareTo(other.security);

            return result;
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

        return obj is Price other && Equals(other);
    }

    public bool Equals(Price? other)
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

    public static bool operator ==(Price? left, Price? right)
    {
        if (left is null)
        {
            return right is null;
        }

        return left.Equals(right);
    }

    public static bool operator !=(Price? left, Price? right)
    {
        return !(left == right);
    }

    public static bool operator <(Price? left, Price? right)
    {
        return left is null ? right is not null : left.CompareTo(right) < 0;
    }

    public static bool operator <=(Price? left, Price? right)
    {
        return left is null || left.CompareTo(right) <= 0;
    }

    public static bool operator >(Price? left, Price? right)
    {
        return left is not null && left.CompareTo(right) > 0;
    }

    public static bool operator >=(Price? left, Price? right)
    {
        return left is null ? right is null : left.CompareTo(right) >= 0;
    }
}
