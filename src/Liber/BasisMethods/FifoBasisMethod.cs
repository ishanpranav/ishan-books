// PositionBuilder.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;

namespace Liber.BasisMethods;

internal sealed class FifoBasisMethod : IBasisMethod
{
    private readonly Queue<Lot> _lots = new Queue<Lot>();

    public decimal Quantity { get; private set; }
    public decimal CostBasis { get; private set; }
    public decimal RealizedGain { get; private set; }
    
    public void Apply(Trade trade)
    {
        int sign = Math.Sign((int)trade.Type);

        if (sign == 0 || trade.Quantity == 0)
        {
            return;
        }

        if (sign > 0)
        {
            decimal costPerShare = trade.Cost + trade.Commission / trade.Quantity;

            _lots.Enqueue(new Lot { Quantity = trade.Quantity, CostPerShare = costPerShare });

            Quantity += trade.Quantity;
            CostBasis += trade.Quantity * costPerShare;

            return;
        }

        if (trade.Quantity > Quantity)
        {
            throw new InvalidOperationException();
        }

        decimal proceedsPerShare = trade.Cost - trade.Commission / trade.Quantity;
        decimal remaining = trade.Quantity;

        Quantity -= remaining;

        while (remaining > 0)
        {
            Lot lot = _lots.Peek();
            decimal used = Math.Min(lot.Quantity, remaining);

            RealizedGain += used * (proceedsPerShare - lot.CostPerShare);
            CostBasis -= used * lot.CostPerShare;

            lot.Quantity -= used;
            remaining -= used;

            if (lot.Quantity == 0)
            {
                _lots.Dequeue();
            }
        }
    }
}
