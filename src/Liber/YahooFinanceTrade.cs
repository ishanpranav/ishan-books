// YahooFinanceTrade.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using CsvHelper.Configuration.Attributes;

namespace Liber;

public class YahooFinanceTrade
{
    public YahooFinanceTrade()
    {
        Value = new Trade();
    }

    public YahooFinanceTrade(Trade value)
    {
        Value = value;
    }

    [Format("yyyyMMdd")]
    [LocalizedDisplayName("YahooFinanceTrade.Posted")]
    [Name("Trade Date")]
    [Optional]
    public DateTime? Posted
    {
        get
        {
            return Value.Posted;
        }
        set
        {
            Value.Posted = value.GetValueOrDefault();
        }
    }

    [LocalizedDisplayName(nameof(Symbol))]
    [Name("Symbol")]
    public string Symbol { get; set; } = string.Empty;

    [LocalizedDisplayName(nameof(Type))]
    [Name("Transaction Type")]
    [System.ComponentModel.TypeConverter(typeof(LocalizedEnumConverter<TradeType>))]
    public TradeType Type
    {
        get
        {
            return Value.Type;
        }
        set
        {
            Value.Type = value;
        }
    }

    [Default(0)]
    [LocalizedDisplayName(nameof(Quantity))]
    [Name("Quantity")]
    [Optional]
    public double Quantity
    {
        get
        {
            return (double)Value.Quantity;
        }
        set
        {
            Value.Quantity = (decimal)value;
        }
    }

    [Default(0)]
    [LocalizedDisplayName(nameof(Cost))]
    [Name("Purchase Price")]
    [Optional]
    public double Cost
    {
        get
        {
            return (double)Value.Cost;
        }
        set
        {
            Value.Cost = (decimal)value;
        }
    }

    [Default(0)]
    [LocalizedDisplayName(nameof(Commission))]
    [Name("Commission")]
    [Optional]
    public double Commission
    {
        get
        {
            return (double)Value.Commission;
        }
        set
        {
            Value.Commission = (decimal)value;
        }
    }

    [Browsable(false)]
    [Ignore]
    public Trade Value { get; set; }
}
