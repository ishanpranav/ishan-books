// ChartJSChartDatasetSankeyData.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Drawing;
using System.Text.Json.Serialization;

namespace Liber.Forms.Reports.Html;

internal sealed class ChartJSChartDatasetSankeyData
{
    public ChartJSChartDatasetSankeyData(string from, string to, double flow)
    {
        From = from;
        To = to;
        Flow = flow;
    }

    public string From { get; }
    public string To { get; }
    public double Flow { get; }

    [JsonConverter(typeof(ChartJSColorConverter))]
    public Color FromColor { get; set; } = Color.Orange;

    [JsonConverter(typeof(ChartJSColorConverter))]
    public Color ToColor { get; set; } = Color.Blue; 
}
