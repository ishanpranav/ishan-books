// HtmlPortfolioReport.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.Json;
using Liber.BasisMethods;

namespace Liber.Forms.Reports.Html;

[ClassInterface(ClassInterfaceType.AutoDual)]
[ComVisible(true)]
public class HtmlPortfolioReport
{
    private readonly Portfolio _portfolio;

    public HtmlPortfolioReport(Portfolio portfolio)
    {
        _portfolio = portfolio;
    }

    public string GetPositions()
    {
        List<Position> positions = new List<Position>();
        List<PositionView> items = new List<PositionView>();
        DateTime today = DateTime.Today;
        DateTime opened = today;

        foreach (Security security in _portfolio.Securities)
        {
            Position? position = security.GetPosition(today, new FifoBasisMethod());

            if (position == null)
            {
                continue;
            }

            positions.Add(position);

            if (position.Opened < opened)
            {
                opened = position.Opened;
            }
        }

        foreach (Position position in positions)
        {
            SortedSet<Price> prices = position.Security.GetPricesAfter(opened);
            decimal[] timeSeries;
            DateTime[] labels;
            int i = 0;

            timeSeries = new decimal[prices.Count];
            labels = new DateTime[prices.Count];

            foreach (Price price in prices)
            {
                timeSeries[i] = price.AdjustedClose ?? price.Close;
                labels[i] = price.Timestamp;
                i++;
            }

            PositionView item = new PositionView(position, timeSeries, labels)
            {
                Symbol = position.Security.Symbol,
                Name = position.Security.Name
            };

            if (position.Security.Color.IsEmpty)
            {
                item.Color = Djb2Color.GetColorFromHash(Colors.BlueBell, position.Security.Symbol);
            }
            else
            {
                item.Color = position.Security.Color;
            }

            items.Add(item);
        }

        return JsonSerializer.Serialize(items, SerializationOptions.HtmlJson);
    }
}
