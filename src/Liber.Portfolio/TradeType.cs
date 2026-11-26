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
    None = 0,

    [LocalizedDescription(nameof(Purchase))]
    [Name("BUY")]
    Purchase = 1,

    [LocalizedDescription(nameof(Sale))]
    [Name("SELL")]
    Sale = -1
}
