// ChartJSChartData.cs
// Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace Liber.Forms.Reports.Html;

internal sealed class ChartJSChartData
{
    public ChartJSChartData(IReadOnlyList<string> labels, IReadOnlyList<ChartJSChartDataset> datasets)
    {
        Labels = labels;
        Datasets = datasets;
    }

    public IReadOnlyList<string> Labels { get; }
    public IReadOnlyList<ChartJSChartDataset> Datasets { get; }
}
