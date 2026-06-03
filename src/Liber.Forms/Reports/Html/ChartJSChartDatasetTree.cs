// ChartJSChartDatasetTree.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Drawing;
using System.Text.Json.Serialization;

namespace Liber.Forms.Reports.Html;

internal sealed class ChartJSChartDatasetTree
{
    public string Name { get; }
    public string? Parent { get; set; }
    public double Value { get; set; }

    [JsonConverter(typeof(ChartJSColorConverter))]
    public Color? Color { get; set; }

    [JsonConverter(typeof(ChartJSColorConverter))]
    public Color? BackgroundColor { get; set; }

    public ChartJSChartDatasetTree(string name)
    {
        Name = name;
    }
}
