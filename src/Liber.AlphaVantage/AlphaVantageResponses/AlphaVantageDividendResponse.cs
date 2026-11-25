// AlphaVantageDividendResponse.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using CsvHelper.Configuration.Attributes;

namespace Liber.AlphaVantage.AlphaVantageResponses;

public class AlphaVantageDividendResponse : IAlphaVantageResponse
{
    [Name("ex_dividend_date")]
    public DateOnly ExDividendDate { get; set; }

    [Name("declaration_date")]
    [NullValues("None")]
    public DateOnly? DeclarationDate { get; set; }

    [Name("record_date")]
    [NullValues("None")]
    public DateOnly? RecordDate { get; set; }

    [Name("payment_date")]
    [NullValues("None")]
    public DateOnly? PaymentDate { get; set; }

    [Name("amount")]
    public decimal Amount { get; set; }

    [Ignore]
    [JsonIgnore]
    public string? Content { get; set; }
}
