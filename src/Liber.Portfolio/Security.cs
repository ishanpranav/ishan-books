// Security.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.Json.Serialization;
using Liber.BasisMethods;

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

    public SortedSet<Trade> GetTradesBefore(DateTime posted)
    {
        Trade? min = trades.Min;

        if (min == null || posted < min.Posted)
        {
            return new SortedSet<Trade>();
        }

        return trades.GetViewBetween(min, new Trade(posted));
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

    public Position? GetPosition(DateTime posted, IBasisMethod basisMethod)
    {
        Price? price = GetPrice(posted);
        decimal commission = 0;
        decimal dividend = 0;
        SortedSet<Trade> subset = GetTradesBefore(posted);
        List<Dividend> list = dividends
            .Where(x => x.Paid.Date <= posted.Date)
            .ToList();
        int i = 0;

        void CollectDividendsThrough(DateTime date)
        {
            while (i < list.Count && list[i].Timestamp.Date <= date.Date)
            {
                dividend += list[i].Value * basisMethod.Quantity;
                i++;
            }
        }

        foreach (Trade trade in subset)
        {
            CollectDividendsThrough(trade.Posted);

            commission += trade.Commission;

            basisMethod.Apply(trade);
        }

        CollectDividendsThrough(posted);

        if (basisMethod.Quantity == 0 && commission == 0 && dividend == 0 && basisMethod.RealizedGain == 0)
        {
            return null;
        }

        return new Position(this)
        {
            Opened = subset.Min != null ? subset.Min.Posted : posted,
            Price = price?.Close ?? 0,
            Cost = basisMethod.Quantity == 0 ? 0 : basisMethod.CostBasis / basisMethod.Quantity,
            Quantity = basisMethod.Quantity,
            Commission = commission,
            Dividend = dividend,
            RealizedGain = basisMethod.RealizedGain,
        };
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
