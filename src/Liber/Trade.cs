// Trade.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using Humanizer;

namespace Liber;

public class Trade :
    IComparable<Trade>,
    IComparable,
    IEquatable<Trade>
{
    public Trade() { }
    public Trade(Guid id, DateTime posted, TradeType type)
    {
        Id = id;
        Posted = posted;
        Type = type;
    }

    public Guid Id { get; internal set; }
    public Guid SecurityId { get; set; }
    public DateTime Posted { get; internal set; }
    public TradeType Type { get; internal set; }
    public decimal Cost { get; set; }
    public decimal Quantity { get; set; }
    public decimal Commission { get; set; }

    public override string ToString()
    {
        return $"{Posted}: {Type.Humanize()} {Quantity} {SecurityId} @ {Cost:c2} ${-Commission:c2}";
    }

    public int CompareTo(object? obj)
    {
        if (obj == null)
        {
            return 1;
        }

        if (obj is not Trade trade)
        {
            throw new ArgumentException(message: null, nameof(obj));
        }

        return CompareTo(trade);
    }

    public int CompareTo(Trade? other)
    {
        if (other is null)
        {
            return 1;
        }

        int result = Posted.CompareTo(other.Posted);

        if (result != 0)
        {
            return result;
        }

        result = other.Type.CompareTo(Type);

        if (result != 0)
        {
            return result;
        }

        return Id.CompareTo(other.Id);
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

        return obj is Trade other && Equals(other);
    }

    public bool Equals(Trade? other)
    {
        if (other is null)
        {
            return false;
        }

        return Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static bool operator ==(Trade? left, Trade? right)
    {
        if (left is null)
        {
            return right is null;
        }

        return left.Equals(right);
    }

    public static bool operator !=(Trade? left, Trade? right)
    {
        return !(left == right);
    }

    public static bool operator <(Trade? left, Trade? right)
    {
        return left is null ? right is not null : left.CompareTo(right) < 0;
    }

    public static bool operator <=(Trade? left, Trade? right)
    {
        return left is null || left.CompareTo(right) <= 0;
    }

    public static bool operator >(Trade? left, Trade? right)
    {
        return left is not null && left.CompareTo(right) > 0;
    }

    public static bool operator >=(Trade? left, Trade? right)
    {
        return left is null ? right is null : left.CompareTo(right) >= 0;
    }
}
