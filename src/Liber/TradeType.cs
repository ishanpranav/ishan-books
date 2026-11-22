// TradeType.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.ComponentModel;
using CsvHelper.Configuration.Attributes;

namespace Liber;

public enum TradeType
{
    [LocalizedDescription(nameof(None))]
    [Name("")]
    None,

    [LocalizedDescription(nameof(Purchase))]
    [Name("BUY")]
    Purchase,

    [LocalizedDescription(nameof(Sale))]
    [Name("SELL")]
    Sale
}
