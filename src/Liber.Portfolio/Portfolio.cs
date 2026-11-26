// Portfolio.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text.Json.Serialization;

namespace Liber;

/// <summary>
/// Represents a portfolio of securities.
/// </summary>
public class Portfolio : ICloneable
{
    private readonly Dictionary<Guid, Security> _securities;
    private readonly SortedDictionary<string, Security> _securitiesBySymbol =
        new SortedDictionary<string, Security>(StringComparer.InvariantCultureIgnoreCase);

    private string? _name;

    /// <summary>
    /// Initializes a new instance of the <see cref="Portfolio"/> class.
    /// </summary>
    public Portfolio()
    {
        _securities = new Dictionary<Guid, Security>();
    }

    [JsonConstructor]
    public Portfolio(IReadOnlyCollection<Security>? securities)
    {
        securities ??= Array.Empty<Security>();
        _securities = securities.ToDictionary(x => x.Id);

        foreach (Security security in securities)
        {
            _securitiesBySymbol[security.Symbol] = security;

            AddTradeReferences(security, security.Trades);
            AddPriceReferences(security, security.Prices);
            AddDividendReferences(security, security.Dividends);
        }
    }

    public string? Name
    {
        get
        {
            return _name;
        }
        set
        {
            if (_name != value)
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    value = null;
                }

                _name = value;

                NameChanged?.Invoke(sender: this, EventArgs.Empty);
            }
        }
    }
    public Color Color { get; set; } = Colors.BlueBell;

    [JsonIgnore]
    public string? Password { get; set; }

    [JsonIgnore]
    public string DisplayName
    {
        get
        {
            return Name ?? PortfolioResources.DefaultPortfolioName;
        }
    }

    public IReadOnlyCollection<Security> Securities
    {
        get
        {
            return _securitiesBySymbol.Values;
        }
    }

    public event EventHandler? NameChanged;
    public event EventHandler<GuidEventArgs>? SecuritiesChanged;

    private void AddTradeReferences(Security value, IReadOnlyCollection<Trade> trades)
    {
        foreach (Trade trade in trades)
        {
            if (trade.Quantity == 0)
            {
                continue;
            }

            trade.security = value;
        }
    }

    private void RemoveTradeReferences(Security value)
    {
        foreach (Trade trade in value.trades)
        {
            trade.security = null;
        }
    }

    private void AddPriceReferences(Security value, IReadOnlyCollection<Price> prices)
    {
        foreach (Price price in prices)
        {
            if (price.Close == 0)
            {
                continue;
            }

            price.security = value;
        }
    }

    private void RemovePriceReferences(Security value)
    {
        foreach (Price price in value.prices)
        {
            price.security = null;
        }
    }

    private void AddDividendReferences(Security value, IReadOnlyCollection<Dividend> dividends)
    {
        foreach (Dividend dividend in dividends)
        {
            if (dividend.Value == 0)
            {
                continue;
            }

            dividend.security = value;
        }
    }

    private void RemoveDividendReferences(Security value)
    {
        foreach (Dividend dividend in value.dividends)
        {
            dividend.security = null;
        }
    }

    public Security GetSecurity(Guid id)
    {
        return _securities[id];
    }

    public bool TryGetSecurity(string symbol, [NotNullWhen(true)] out Security? value)
    {
        return _securitiesBySymbol.TryGetValue(symbol, out value);
    }

    public void ClearSecurities()
    {
        if (_securities.Count > 0)
        {
            _securities.Clear();
            _securitiesBySymbol.Clear();
            SecuritiesChanged?.Invoke(sender: this, new GuidEventArgs(Guid.Empty));
        }
    }

    public void AddSecurity(Security value)
    {
        if (value.Id != Guid.Empty)
        {
            throw new InvalidOperationException();
        }

        Guid id = Guid.NewGuid();

        value.Id = id;

        AddTradeReferences(value, value.Trades);
        AddPriceReferences(value, value.Prices);
        AddDividendReferences(value, value.Dividends);
        _securities.Add(value.Id, value);

        _securitiesBySymbol[value.Symbol] = value;

        SecuritiesChanged?.Invoke(sender: this, new GuidEventArgs(value.Id));
    }

    public void UpdateSecurity(
        Security value,
        string symbol,
        IReadOnlyCollection<Trade> trades,
        IReadOnlyCollection<Price> prices,
        IReadOnlyCollection<Dividend> dividends)
    {
        if (!_securities.TryGetValue(value.Id, out Security? other))
        {
            throw new InvalidOperationException();
        }

        RemoveTradeReferences(value);
        AddTradeReferences(value, trades);
        RemovePriceReferences(value);
        AddPriceReferences(value, prices);
        RemoveDividendReferences(value);
        AddDividendReferences(value, dividends);

        if (trades != value.trades)
        {
            value.trades.Clear();

            foreach (Trade trade in trades)
            {
                if (trade.Quantity == 0)
                {
                    continue;
                }

                value.trades.Add(trade);
            }
        }

        if (prices != value.prices)
        {
            value.prices.Clear();

            foreach (Price price in prices)
            {
                if (price.Close == 0)
                {
                    continue;
                }

                value.prices.Add(price);
            }
        }

        if (dividends != value.dividends)
        {
            value.dividends.Clear();

            foreach (Dividend dividend in dividends)
            {
                if (dividend.Value == 0)
                {
                    continue;
                }

                value.dividends.Add(dividend);
            }
        }

        _securitiesBySymbol.Remove(value.Symbol);

        value.Symbol = symbol;

        _securitiesBySymbol.Add(symbol, value);
        SecuritiesChanged?.Invoke(sender: this, new GuidEventArgs(value.Id));
    }

    public override string ToString()
    {
        return DisplayName;
    }

    public Portfolio Clone()
    {
        return new Portfolio(_securities
            .Select(x => x.Value.Clone())
            .ToList())
        {
            Name = Name,
            Password = Password,
            Color = Color
        };
    }

    object ICloneable.Clone()
    {
        return Clone();
    }
}
