// AlphaVantageClient.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using Liber.AlphaVantage.AlphaVantageResponses;
using RestSharp;
using RestSharp.Serializers.CsvHelper;

namespace Liber.AlphaVantage;

public class AlphaVantageClient : IDisposable
{
    public const string FunctionParameter = "function";
    public const string SymbolParameter = "symbol";
    public const string Hostname = "https://www.alphavantage.co";
    public const string DemoApiKey = "demo";

    private readonly CsvHelperSerializer _serializer = new CsvHelperSerializer();
    private RestClient? _restClient;

    public AlphaVantageClient()
    {
        _restClient = new RestClient(CreateRestClientOptions());
    }

    public AlphaVantageClient(HttpClient client)
    {
        _restClient = new RestClient(client, CreateRestClientOptions());
    }

    private static RestClientOptions CreateRestClientOptions()
    {
        return new RestClientOptions(Hostname);
    }

    public async Task<AlphaVantageQuoteResponse> GetQuoteAsync(string apiKey, string symbol)
    {
        RestRequest restRequest = CreateRequest(apiKey);

        restRequest.AddQueryParameter(FunctionParameter, "GLOBAL_QUOTE");
        restRequest.AddQueryParameter(SymbolParameter, symbol);

        IReadOnlyList<AlphaVantageQuoteResponse> response = await ExecuteRequestAsync<AlphaVantageQuoteResponse>(restRequest);

        return response[0];
    }

    public Task<IReadOnlyList<AlphaVantageDividendResponse>> GetDividendsAsync(string apiKey, string symbol)
    {
        RestRequest restRequest = CreateRequest(apiKey);

        restRequest.AddQueryParameter(FunctionParameter, "DIVIDENDS");
        restRequest.AddQueryParameter(SymbolParameter, symbol);

        return ExecuteRequestAsync<AlphaVantageDividendResponse>(restRequest);
    }

    public Task<IReadOnlyList<AlphaVantageTimeSeriesResponse>> GetTimeSeriesAsync(string apiKey, AlphaVantageTimeSeries timeSeries, AlphaVantageTimeSeriesRequest request)
    {
        RestRequest restRequest = CreateRequest(apiKey);

        restRequest.AddObject(request);
        restRequest.AddQueryParameter(FunctionParameter, timeSeries.ToString());

        return ExecuteRequestAsync<AlphaVantageTimeSeriesResponse>(restRequest);
    }

    public Task<IReadOnlyList<AlphaVantageAdjustedTimeSeriesResponse>> GetTimeSeriesAsync(string apiKey, AlphaVantageAdjustedTimeSeries timeSeries, AlphaVantageTimeSeriesRequest request)
    {
        RestRequest restRequest = CreateRequest(apiKey);

        restRequest.AddObject(request);
        restRequest.AddQueryParameter(FunctionParameter, timeSeries.ToString());

        return ExecuteRequestAsync<AlphaVantageAdjustedTimeSeriesResponse>(restRequest);
    }

    private RestRequest CreateRequest(string apiKey)
    {
        RestRequest restRequest = new RestRequest("query");

        restRequest.AddHeader(KnownHeaders.Accept, "*/*");
        restRequest.AddQueryParameter("apikey", apiKey);
        restRequest.AddQueryParameter("datatype", "csv");

        return restRequest;
    }

    private async Task<IReadOnlyList<TResponse>> ExecuteRequestAsync<TResponse>(RestRequest request)
        where TResponse : IAlphaVantageResponse
    {
        if (_restClient == null)
        {
            throw new InvalidOperationException();
        }

        RestResponse restResponse = await _restClient.GetAsync(request);

        if (restResponse.ErrorMessage != null)
        {
            throw new Exception(restResponse.ErrorMessage + ": " + restResponse.Content);
        }

        if (restResponse.ErrorException != null)
        {
            throw restResponse.ErrorException;
        }

        if (restResponse.ContentType == "application/json")
        {
            throw new AlphaVantageException(restResponse.Content);
        }

        List<TResponse>? result = _serializer.Deserialize<List<TResponse>>(restResponse);

        if (result == null)
        {
            throw new Exception();
        }

        string? content = restResponse.Content;

        foreach (TResponse response in result)
        {
            response.Content = content;
        }

        return result;
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
