// ImportTradesForm.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using Liber.AlphaVantage;
using Liber.Forms.Accounts;
using Liber.Forms.Forms;

namespace Liber.Forms.Securities;

internal partial class ImportTradesForm : ImportForm
{
    private readonly FormFactory _factory;
    private readonly Dictionary<string, List<Trade>> _trades = new Dictionary<string, List<Trade>>(StringComparer.InvariantCultureIgnoreCase);
    private readonly AlphaVantageClient _client;

    public ImportTradesForm(
        Company company,
        FormFactory factory,
        IReadOnlyCollection<YahooFinanceTrade> trades,
        AlphaVantageClient client) : base(company)
    {
        foreach (YahooFinanceTrade trade in trades)
        {
            if (trade.Quantity == 0)
            {
                continue;
            }

            if (!_trades.TryGetValue(trade.Symbol, out List<Trade>? values))
            {
                values = new List<Trade>();
                _trades[trade.Symbol] = values;
            }

            values.Add(trade.Value);
        }

        SetDataSource(trades.ToList());
        _factory = factory;
        _client = client;
    }

    protected override void CommitChanges()
    {
        _factory.Kill(typeof(SecuritiesForm).GUID);
        
        foreach (KeyValuePair<string, List<Trade>> entry in _trades)
        {
            if (Company.TryGetSecurity(entry.Key, out Security? security))
            {
                Company.UpdateSecurity(security, entry.Key, entry.Value, security.Prices, security.Dividends);
            }
            else
            {
                security = new Security(entry.Key, entry.Value);

                Company.AddSecurity(security);
            }
        }

        _factory.Register(typeof(SecuritiesForm).GUID, new SecuritiesForm(Company, _client)
        {
            TabPage = SecuritiesFormTabPage.Trades
        });
    }
}
