// Security.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.Json.Serialization;

namespace Liber;

public class Security :
    ICloneable,
    IComparable<Security>,
    IComparable,
    IEquatable<Security>
{
    internal readonly SortedSet<Trade> trades;
    internal readonly SortedSet<Price> prices;
    internal readonly SortedSet<Dividend> dividends;

    public Security()
    {
        trades = new SortedSet<Trade>();
        prices = new SortedSet<Price>();
        dividends = new SortedSet<Dividend>();
    }

    public Security(string symbol, IReadOnlyCollection<Trade> trades)
    {
        Symbol = symbol;
        this.trades = new SortedSet<Trade>(trades);
        prices = new SortedSet<Price>();
        dividends = new SortedSet<Dividend>();
    }

    // new Security(entry.Key, entry.Value, security.Prices, security.Dividends);

    [JsonConstructor]
    public Security(
        Guid id,
        string symbol,
        IReadOnlyCollection<Trade> trades,
        IReadOnlyCollection<Price> prices,
        IReadOnlyCollection<Dividend> dividends)
    {
        Id = id;
        Symbol = symbol;
        this.trades = new SortedSet<Trade>(trades);
        this.prices = new SortedSet<Price>(prices);
        this.dividends = new SortedSet<Dividend>(dividends);

        foreach (Trade trade in trades)
        {
            trade.security = this;
        }
    }

    public Guid Id { get; internal set; }
    public string Symbol { get; internal set; } = string.Empty;
    public string? Name { get; set; }
    public Color Color { get; set; }

    public IReadOnlyCollection<Trade> Trades
    {
        get
        {
            return trades;
        }
    }

    public IReadOnlyCollection<Price> Prices
    {
        get
        {
            return prices;
        }
    }

    public IReadOnlyCollection<Dividend> Dividends
    {
        get
        {
            return dividends;
        }
    }

    public Price? GetPrice()
    {
        return prices.Max;
    }

    public Price? GetPrice(DateTime timestamp)
    {
        return GetPricesBefore(timestamp).Max;
    }

    public SortedSet<Price> GetPricesBefore(DateTime timestamp)
    {
        Price? min = prices.Min;

        if (min == null || timestamp < min.Timestamp)
        {
            return new SortedSet<Price>();
        }

        return prices.GetViewBetween(min, new Price(timestamp));
    }

    public SortedSet<Price> GetPricesAfter(DateTime timestamp)
    {
        Price? max = prices.Max;

        if (max == null || timestamp > max.Timestamp)
        {
            return new SortedSet<Price>();
        }

        return prices.GetViewBetween(new Price(timestamp), max);
    }

    public override string ToString()
    {
        if (Name != null)
        {
            return $"{Name} ({Symbol})";
        }

        return Symbol;
    }

    public Security Clone()
    {
        int tradeLength = prices.Count;
        Trade[] clonedTrades = new Trade[tradeLength];
        int i = 0;

        foreach (Trade trade in trades)
        {
            clonedTrades[i] = trade.Clone();
            i++;
        }

        int priceLength = prices.Count;
        Price[] clonedPrices = new Price[priceLength];
        int j = 0;

        foreach (Price price in prices)
        {
            clonedPrices[j] = price.Clone();
            j++;
        }

        int dividendLength = dividends.Count;
        Dividend[] clonedDividend = new Dividend[dividendLength];
        int k = 0;

        foreach (Dividend dividend in dividends)
        {
            clonedDividend[k] = dividend.Clone();
            k++;
        }

        return new Security(Guid.Empty, Symbol, clonedTrades, clonedPrices, clonedDividend)
        {
            Color = Color,
            Name = Name
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

        if (obj is not Security other)
        {
            throw new ArgumentException(message: null, nameof(obj));
        }

        return CompareTo(other);
    }

    public int CompareTo(Security? other)
    {
        if (other is null)
        {
            return 1;
        }

        int result = Symbol.CompareTo(other.Symbol);

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

        return obj is Security other && Equals(other);
    }

    public bool Equals(Security? other)
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

    public static bool operator ==(Security? left, Security? right)
    {
        if (left is null)
        {
            return right is null;
        }

        return left.Equals(right);
    }

    public static bool operator !=(Security? left, Security? right)
    {
        return !(left == right);
    }

    public static bool operator <(Security? left, Security? right)
    {
        return left is null ? right is not null : left.CompareTo(right) < 0;
    }

    public static bool operator <=(Security? left, Security? right)
    {
        return left is null || left.CompareTo(right) <= 0;
    }

    public static bool operator >(Security? left, Security? right)
    {
        return left is not null && left.CompareTo(right) > 0;
    }

    public static bool operator >=(Security? left, Security? right)
    {
        return left is null ? right is null : left.CompareTo(right) >= 0;
    }
}
