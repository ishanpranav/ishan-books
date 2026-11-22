// TradeView.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;

namespace Liber.Forms.Trades;

internal sealed class TradeView
{
    public TradeView(Guid id, string symbol)
    {
        Id = id;
        Symbol = symbol;
    }

    [Browsable(false)]
    public Guid Id { get; }

    [LocalizedDisplayName("TradeView.Posted")]
    public DateTime Posted { get; set; }

    [LocalizedDisplayName(nameof(Symbol))]
    public string Symbol { get; set; }

    [LocalizedDisplayName(nameof(Type))]
    [System.ComponentModel.TypeConverter(typeof(LocalizedEnumConverter<TradeType>))]
    public TradeType Type { get; set; }

    [LocalizedDisplayName(nameof(Quantity))]
    public decimal Quantity { get; set; }

    [LocalizedDisplayName(nameof(Cost))]
    public decimal Cost { get; set; }

    [LocalizedDisplayName(nameof(Commission))]
    public decimal Commission { get; set; }
}
