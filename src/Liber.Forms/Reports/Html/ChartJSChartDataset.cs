// ChartJSChartDataset.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Drawing;
using System.Text.Json.Serialization;

namespace Liber.Forms.Reports.Html;

internal sealed class ChartJSChartDataset
{
    public string? Label { get; set; }
    public int BorderWidth { get; set; }
    public double LineTension { get; set; }
    public IReadOnlyList<double> Data { get; }

    [JsonConverter(typeof(ChartJSColorCollectionConverter))]
    [JsonPropertyName("backgroundColor")]
    public IEnumerable<Color>? BackgroundColors { get; set; }

    public ChartJSChartDataset(IReadOnlyList<double> data)
    {
        Data = data;
    }
}
