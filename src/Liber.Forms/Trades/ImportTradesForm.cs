// ImportTradesForm.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using Liber.Forms.Accounts;

namespace Liber.Forms.Trades;

internal partial class ImportTradesForm : ImportForm
{
    private readonly List<Trade> _trades = new List<Trade>();
    private readonly List<Guid> _securityIds = new List<Guid>();
    private readonly Dictionary<string, Guid> _securities;

    public ImportTradesForm(Company company, IReadOnlyCollection<YahooFinanceTrade> trades) : base(company)
    {
        _securities = Company.Securities.ToDictionary(x => x.Symbol, x => x.Id);

        foreach (YahooFinanceTrade trade in trades)
        {
            if (trade.Quantity == 0)
            {
                continue;
            }

            if (!_securities.TryGetValue(trade.Symbol, out Guid securityId))
            {
                securityId = Guid.NewGuid();
                _securities[trade.Symbol] = securityId;
            }

            _trades.Add(trade.Value);
            _securityIds.Add(securityId);
        }

        SetDataSource(trades.ToList());
    }

    protected override void CommitChanges()
    {
        for (int i = 0; i < _trades.Count; i++)
        {
            Company.AddTrade(_trades[i], _securityIds[i]);
        }

        foreach (KeyValuePair<string, Guid> entry in _securities)
        {
            Security security = Company.GetSecurity(entry.Value);

            security.Symbol = entry.Key;
        }
    }
}
