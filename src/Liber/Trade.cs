// Trade.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Liber;

public class Trade
{
    public Trade() { }
    public Trade(Guid id, Guid securityId, DateTime posted, TradeType type)
    {
        Id = id;
        SecurityId = securityId;
        Posted = posted;
        Type = type;
    }

    public Guid Id { get; internal set; }
    public Guid SecurityId { get; }
    public DateTime Posted { get; internal set; }
    public TradeType Type { get; internal set; }
    public decimal Cost { get; set; }
    public decimal Quantity { get; set; }
    public decimal Commission { get; set; }
}
