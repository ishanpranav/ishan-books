// ImportTradesForm.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using Liber.Forms.Accounts;
using Liber.Forms.Forms;

namespace Liber.Forms.Trades;

internal partial class ImportTradesForm : ImportForm
{
    private readonly FormFactory _factory;
    private readonly List<Trade> _trades = new List<Trade>();
    private readonly Dictionary<string, Guid> _securities;

    public ImportTradesForm(
        Company company,
        FormFactory factory,
        IReadOnlyCollection<YahooFinanceTrade> trades) : base(company)
    {
        _securities = new Dictionary<string, Guid>(Company.SecuritiesBySymbol);

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

            trade.Value.SecurityId = securityId;

            _trades.Add(trade.Value);
        }

        SetDataSource(trades.ToList());
        _factory = factory;
    }

    protected override void CommitChanges()
    {
        _factory.Kill(typeof(TradesForm).GUID);
        Company.SetTrades(_trades);

        foreach (KeyValuePair<string, Guid> entry in _securities)
        {
            Security security = Company.GetSecurity(entry.Value);

            Company.UpdateSecurity(security, entry.Key);
        }

        _factory.Register(typeof(TradesForm).GUID, new TradesForm(Company));
    }
}
