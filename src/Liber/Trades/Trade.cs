// Trade.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text.Json.Serialization;

namespace Liber.Trades;

[JsonDerivedType(typeof(DepositTrade), "deposit")]
[JsonDerivedType(typeof(WithdrawalTrade), "withdrawal")]
public abstract class Trade
{
    protected Trade(DateTime posted)
    {
        Posted = posted;
    }

    public Guid AccountId { get; }
    public DateTime Posted { get; }
    public string? Memo { get; set; }

    public abstract Transaction ToTransaction();
}
