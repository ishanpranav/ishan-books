// Position.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.Json.Serialization;

namespace Liber.Forms.Reports.Html;

internal class Position
{
    public Position(IReadOnlyCollection<decimal> timeSeries, IReadOnlyCollection<DateTime> labels)
    {
        TimeSeries = timeSeries;
        Labels = labels;
    }

    public string? Symbol { get; set; }
    public string? Name { get; set; }

    [JsonConverter(typeof(ChartJSColorConverter))]
    public Color Color { get; set; }

    public DateTime Posted { get; set; }
    public TradeType Type { get; set; }
    public decimal? Price { get; set; }
    public decimal? PreviousPrice { get; set; }
    public decimal Cost { get; set; }
    public decimal Quantity { get; set; }
    public decimal Commission { get; set; }
    public IReadOnlyCollection<decimal> TimeSeries { get; }
    public IReadOnlyCollection<DateTime> Labels { get; }
}
