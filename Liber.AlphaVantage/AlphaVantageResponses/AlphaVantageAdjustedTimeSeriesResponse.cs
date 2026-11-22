// AlphaVantageAdjustedTimeSeriesResponse.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using CsvHelper.Configuration.Attributes;

namespace Liber.AlphaVantage.AlphaVantageResponses;

public sealed class AlphaVantageAdjustedTimeSeriesResponse : AlphaVantageTimeSeriesResponse
{
    [Name("adjusted close")]
    public decimal AdjustedClose { get; set; }

    [Name("dividend amount")]
    public decimal Dividend { get; set; }
}
