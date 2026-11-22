// Position.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Liber.Forms.Reports.Html;

internal class Position
{
    public string? Symbol { get; set; }
    public DateTime Posted { get; set; }
    public TradeType Type { get; set; }
    public decimal? Price { get; set; }
    public decimal? PreviousPrice { get; set; }
    public decimal Cost { get; set; }
    public decimal Quantity { get; set; }
    public decimal Commission { get; set; }
}
