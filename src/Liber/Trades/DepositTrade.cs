// DepositTrade.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text.Json.Serialization;

namespace Liber.Trades;

public class DepositTrade : Trade
{
    public DepositTrade(DateTime posted, decimal amount, Guid sourceAccountId) : base(posted)
    {
        Amount = amount;
        SourceAccountId = sourceAccountId;
    }

    public decimal Amount { get; }
    public Guid SourceAccountId { get; }

    public override Transaction ToTransaction()
    {
        return new Transaction(Guid.Empty, number: 0, string.Empty, Posted, new Line[]
        {
            new Line(AccountId, Amount, description: null, reconciled: null),
            new Line(SourceAccountId, -Amount, description: null, reconciled: null),
        })
        {
            Memo = Memo
        };
    }
}
