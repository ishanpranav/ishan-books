// Position.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Liber;

public class Position
{
    public Guid SecurityId { get; set; }
    public DateTime Opened { get; set; }
    public decimal Quantity { get; set; }
    public decimal Cost { get; set; }
    public decimal Commission { get; set; }
    public decimal Price { get; set; }
    public decimal RealizedGain { get; set; }
    public decimal Dividend { get; set; }

    public decimal CostBasis
    {
        get
        {
            return Cost * Quantity;
        }
    }
    public decimal MarketValue
    {
        get
        {
            return Price * Quantity;
        }
    }

    public decimal UnrealizedGain
    {
        get
        {
            return MarketValue - CostBasis;
        }
    }

    public decimal TotalGain
    {
        get
        {
            return RealizedGain + UnrealizedGain + Dividend;
        }
    }
}
