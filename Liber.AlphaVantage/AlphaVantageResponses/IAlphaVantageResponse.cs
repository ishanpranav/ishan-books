// IAlphaVantageResponse.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.AlphaVantage.AlphaVantageResponses;

public interface IAlphaVantageResponse
{
    string? Content { get; set; }
}
