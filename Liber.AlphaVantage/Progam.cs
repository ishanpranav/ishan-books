// Progam.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RestSharp.Serializers.CsvHelper;

namespace Liber.AlphaVantage;

internal static class Progam
{
    private static async Task Main()
    {
        const string apiKey = "";
        const string symbol = "SWPPX";

        using HttpClient httpClient = new HttpClient();
        using AlphaVantageClient alphaVantageClient = new AlphaVantageClient(httpClient);

        IReadOnlyCollection<AlphaVantageDailyTimeSeriesResponse>? response = await alphaVantageClient.GetDailyTimeSeriesAsync(apiKey, symbol);

        Console.WriteLine(response);

        CsvHelperSerializer s = new CsvHelperSerializer();

        Console.WriteLine(s.Serialize(response));
    }
}
