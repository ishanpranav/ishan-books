// AlphaVantageClient.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Serializers.CsvHelper;

namespace Liber.AlphaVantage;

public class AlphaVantageClient : IDisposable
{
    private RestClient? _restClient;

    public AlphaVantageClient(HttpClient client)
    {
        _restClient = new RestClient(client, new RestClientOptions("https://www.alphavantage.co"), configureSerialization: x => x.UseCsvHelper());
    }

    public async Task<IReadOnlyCollection<AlphaVantageDailyTimeSeriesResponse>?> GetDailyTimeSeriesAsync(string apiKey, string symbol)
    {
        return await GetAsync<List<AlphaVantageDailyTimeSeriesResponse>>(new AlphaVantageRequest()
        {
            ApiKey = apiKey,
            DataType = AlphaVantageDataType.Csv,
            Function = "TIME_SERIES_DAILY",
            Symbol = symbol,
            OutputSize = AlphaVantageOutputSize.Compact
        });
    }

    public async Task<T?> GetAsync<T>(AlphaVantageRequest request)
    {
        if (_restClient == null)
        {
            return default;
        }

        CsvHelperSerializer serializer = new CsvHelperSerializer();
        RestRequest restRequest = new RestRequest("query");

        restRequest.AddObject(request);

        RestResponse? restResponse = await _restClient.GetAsync(restRequest);

        if (restResponse == null)
        {
            return default;
        }

        return serializer.Deserialize<T>(restResponse);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_restClient != null)
            {
                _restClient.Dispose();

                _restClient = null;
            }
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
