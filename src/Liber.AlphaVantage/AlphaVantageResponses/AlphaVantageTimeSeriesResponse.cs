// AlphaVantageTimeSeriesResponse.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using CsvHelper.Configuration.Attributes;

namespace Liber.AlphaVantage.AlphaVantageResponses;

public class AlphaVantageTimeSeriesResponse : AlphaVantageResponse
{
    [Name("timestamp")]
    public DateTime Timestamp { get; set; }

    [Name("close")]
    public decimal Close { get; set; }
}
