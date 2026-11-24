// Trade.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text.Json.Serialization;
using Humanizer;

namespace Liber;

public class Trade :
    IComparable<Trade>,
    IComparable,
    IEquatable<Trade>
{
    internal Security? security;

    public Trade() { }

    [JsonConstructor]
    public Trade(
        DateTime posted,
        TradeType type,
        decimal cost,
        decimal quantity,
        decimal commission)
    {
        Posted = posted;
        Type = type;
        Cost = cost;
        Quantity = quantity;
        Commission = commission;
    }

    public DateTime Posted { get; internal set; }
    public TradeType Type { get; internal set; }
    public decimal Cost { get; internal set; }
    public decimal Quantity { get; internal set; }
    public decimal Commission { get; internal set; }

    public override string ToString()
    {
        return $"{Posted}: {Type.Humanize()} {Quantity} {security} @ {Cost:c2} ${-Commission:c2}";
    }

    public int CompareTo(object? obj)
    {
        if (obj == null)
        {
            return 1;
        }

        if (obj is not Trade other)
        {
            throw new ArgumentException(message: null, nameof(obj));
        }

        return CompareTo(other);
    }

    public int CompareTo(Trade? other)
    {
        if (other is null)
        {
            return 1;
        }

        int result;

        if (security != null)
        {
            result = security.CompareTo(other.security);

            if (result != 0)
            {
                return result;
            }
        }

        result = Posted.CompareTo(other.Posted); ;

        if (result != 0)
        {
            return result;
        }

        result = other.Type.CompareTo(Type);

        if (result != 0)
        {
            return result;
        }

        result = Quantity.CompareTo(other.Quantity);

        if (result != 0)
        {
            return result;
        }

        result = Cost.CompareTo(other.Cost);

        if (result != 0)
        {
            return result;
        }

        return Commission.CompareTo(other.Commission);
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

        return security == other.security &&
            Posted == other.Posted &&
            Type == other.Type &&
            Quantity == other.Quantity &&
            Cost == other.Cost &&
            Commission == other.Commission;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(security, Posted, Type, Quantity, Cost, Commission);
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
