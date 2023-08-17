// AlphaVantageRequest.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using RestSharp;

namespace Liber.AlphaVantage;

public class AlphaVantageRequest
{
    [RequestProperty(Name = "apikey")]
    public string? ApiKey { get; set; }

    [RequestProperty(Name = "symbol")]
    public string? Symbol { get; set; }

    [RequestProperty(Name = "function")]
    public string? Function { get; set; }

    [RequestProperty(Name = "outputsize")]
    public AlphaVantageOutputSize OutputSize { get; set; }

    [RequestProperty(Name = "datatype")]
    public AlphaVantageDataType DataType { get; set; }
}
