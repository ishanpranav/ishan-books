// PositionView.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.Json.Serialization;

namespace Liber.Forms.Reports.Html;

internal class PositionView
{
    public PositionView(Position position, IReadOnlyCollection<decimal> timeSeries, IReadOnlyCollection<DateTime> labels)
    {
        Position = position;
        TimeSeries = timeSeries;
        Labels = labels;
    }

    public string? Symbol { get; set; }
    public string? Name { get; set; }

    [JsonConverter(typeof(ChartJSColorConverter))]
    public Color Color { get; set; }

    public Position Position { get; set; }
    public IReadOnlyCollection<decimal> TimeSeries { get; }
    public IReadOnlyCollection<DateTime> Labels { get; }
}
