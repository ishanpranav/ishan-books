// AlphaVantageDailyTimeSeriesResponse.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using CsvHelper.Configuration.Attributes;

namespace Liber.AlphaVantage;

public class AlphaVantageDailyTimeSeriesResponse
{
    [Name("timestamp")]
    public DateTime Timestamp { get; set; }

    [Name("open")]
    public decimal Open { get; set; }

    [Name("high")]
    public decimal High { get; set; }

    [Name("low")]
    public decimal Low { get; set; }

    [Name("close")]
    public decimal Close { get; set; }

    [Name("volume")]
    public decimal Volume { get; set; }
}
