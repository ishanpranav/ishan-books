// SecurityTrade.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Liber.Trades;

public abstract class SecurityTrade : Trade
{
    public SecurityTrade(DateTime posted, Security security) : base(posted)
    {
        Security = security;
    }

    public Security Security { get; }
}
