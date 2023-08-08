// IifExtra.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using CsvHelper.Configuration.Attributes;

namespace Liber;

public enum IifExtra
{
    [Name("")]
    None,

    [Name("INVENTORYASSET")]
    Inventory,

    [Name("RETEARNINGS")]
    Equity,

    [Name("OPENBAL")]
    OpeningBalanceEquity,

    [Name("COGS")]
    Cost
}
