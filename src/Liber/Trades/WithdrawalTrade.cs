// WithdrawalTrade.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text.Json.Serialization;

namespace Liber.Trades;

public class WithdrawalTrade : Trade
{
    public WithdrawalTrade(DateTime posted, decimal amount, Guid targetAccountId) : base(posted)
    {
        Amount = amount;
        TargetAccountId = targetAccountId;
    }

    public decimal Amount { get; }
    public Guid TargetAccountId { get; }

    public override Transaction ToTransaction()
    {
        return new Transaction(Guid.Empty, number: 0, string.Empty, Posted, new Line[]
        {
            new Line(AccountId, -Amount, description: null, reconciled: null),
            new Line(TargetAccountId, Amount, description: null, reconciled: null)
        })
        {
            Memo = Memo
        };
    }
}
