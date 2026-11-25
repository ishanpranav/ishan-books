// SecuritiesForm.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Liber.AlphaVantage;
using Liber.AlphaVantage.AlphaVantageResponses;
using Liber.Forms.Properties;
using Liber.Forms.Saving;

namespace Liber.Forms.Securities;

internal partial class SecuritiesForm : Form
{
    private const int AssumedSize = 100;

    private readonly Company _company;
    private readonly AlphaVantageClient _client;

    public SecuritiesForm(Company company, AlphaVantageClient client)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        typeColumn.ValueType = typeof(TradeType);
        typeColumn.DataSource = Enum.GetValues<TradeType>();
        costColumn.ValueType = typeof(decimal);
        quantityColumn.ValueType = typeof(decimal);
        commissionColumn.ValueType = typeof(decimal);
        closeColumn.ValueType = typeof(decimal);
        adjustedCloseColumn.ValueType = typeof(decimal);
        valueColumn.ValueType = typeof(decimal);
        costColumn.DefaultCellStyle.Format = DecimalExtensions.Format;
        quantityColumn.DefaultCellStyle.Format = DecimalExtensions.Format;
        commissionColumn.DefaultCellStyle.Format = DecimalExtensions.Format;
        closeColumn.DefaultCellStyle.Format = DecimalExtensions.Format;
        adjustedCloseColumn.DefaultCellStyle.Format = DecimalExtensions.Format;
        valueColumn.DefaultCellStyle.Format = DecimalExtensions.Format;

        _company = company;
        _client = client;

        // TODO: custom data grid view columns for enums: replace enum columns with drop downs
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public SecuritiesFormTabPage TabPage
    {
        get
        {
            return (SecuritiesFormTabPage)_tabControl.SelectedIndex;
        }
        set
        {
            _tabControl.SelectedIndex = (int)value;
        }
    }

    private void OnLoad(object sender, EventArgs e)
    {
        foreach (Security security in _company.Securities)
        {
            securityDataGridView.Rows.Add(
                security.Symbol,
                security.Name ?? string.Empty,
                security.Color);

            foreach (Trade trade in security.Trades)
            {
                tradeDataGridView.Rows.Add(
                    security.Symbol,
                    trade.Posted,
                    trade.Type,
                    trade.Cost,
                    trade.Quantity,
                    trade.Commission);
            }

            foreach (Price price in security.Prices)
            {
                priceDataGridView.Rows.Add(
                    security.Symbol,
                    price.Timestamp, price.Close, price.AdjustedClose ?? 0);
            }

            foreach (Dividend dividend in security.Dividends)
            {
                dividendDataGridView.Rows.Add(
                    security.Symbol,
                    dividend.Timestamp, dividend.Value);
            }
        }

        securityDataGridView.AutoResizeColumns();
        tradeDataGridView.AutoResizeColumns();
        priceDataGridView.AutoResizeColumns();
        dividendDataGridView.AutoResizeColumns();
    }

    private void OnDataGridViewDataError(object sender, DataGridViewDataErrorEventArgs e)
    {
        if (e.Exception != null)
        {
            MessageBox.Show(e.Exception.Message, Resources.ExceptionCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void OnSaveButtonClick(object sender, EventArgs e)
    {
        CommitChanges();
    }

    private void CommitChanges()
    {
        StringComparer stringComparer = StringComparer.InvariantCultureIgnoreCase;
        Dictionary<string, List<Trade>> trades = new Dictionary<string, List<Trade>>(stringComparer);

        foreach (DataGridViewRow row in tradeDataGridView.Rows)
        {
            if (row.IsNewRow)
            {
                continue;
            }

            string symbol = (string)row.Cells[tradeSymbolColumn.Index].Value!;

            if (!trades.TryGetValue(symbol, out List<Trade>? values))
            {
                values = new List<Trade>();
                trades[symbol] = values;
            }

            values.Add(new Trade(
                (DateTime)row.Cells[postedColumn.Index].Value!,
                (TradeType)row.Cells[typeColumn.Index].Value!,
                (decimal)row.Cells[costColumn.Index].Value!,
                (decimal)row.Cells[quantityColumn.Index].Value!,
                (decimal)row.Cells[commissionColumn.Index].Value!));
        }

        Dictionary<string, List<Price>> prices = new Dictionary<string, List<Price>>(stringComparer);

        foreach (DataGridViewRow row in priceDataGridView.Rows)
        {
            if (row.IsNewRow)
            {
                continue;
            }

            string symbol = (string)row.Cells[priceSymbolColumn.Index].Value!;

            if (!prices.TryGetValue(symbol, out List<Price>? values))
            {
                values = new List<Price>();
                prices[symbol] = values;
            }

            values.Add(new Price((DateTime)row.Cells[priceTimestampColumn.Index].Value!)
            {
                Close = (decimal)row.Cells[closeColumn.Index].Value!,
                AdjustedClose = (decimal)row.Cells[adjustedCloseColumn.Index].Value!
            });
        }

        Dictionary<string, List<Dividend>> dividends = new Dictionary<string, List<Dividend>>(stringComparer);

        foreach (DataGridViewRow row in dividendDataGridView.Rows)
        {
            if (row.IsNewRow)
            {
                continue;
            }

            string symbol = (string)row.Cells[dividendSymbolColumn.Index].Value!;

            if (!dividends.TryGetValue(symbol, out List<Dividend>? values))
            {
                values = new List<Dividend>();
                dividends[symbol] = values;
            }

            values.Add(new Dividend((DateTime)row.Cells[dividendTimestampColumn.Index].Value!)
            {
                Value = (decimal)row.Cells[valueColumn.Index].Value!
            });
        }

        _company.ClearSecurities();

        foreach (DataGridViewRow row in securityDataGridView.Rows)
        {
            if (row.IsNewRow)
            {
                continue;
            }

            string symbol = (string)row.Cells[securitySymbolColumn.Index].Value!;

            if (!trades.TryGetValue(symbol, out List<Trade>? securityTrades))
            {
                securityTrades = new List<Trade>();
            }

            if (!prices.TryGetValue(symbol, out List<Price>? securityPrices))
            {
                securityPrices = new List<Price>();
            }

            if (!dividends.TryGetValue(symbol, out List<Dividend>? securityDividends))
            {
                securityDividends = new List<Dividend>();
            }

            _company.AddSecurity(new Security(Guid.Empty, symbol, securityTrades, securityPrices, securityDividends)
            {
                Name = (string)row.Cells[nameColumn.Index].Value!,
                Color = (Color)row.Cells[colorColumn.Index].Value!
            });
        }
    }

    private void OnCloseButtonClick(object sender, EventArgs e)
    {
        Close();
    }

    private async void OnBasicRefreshButtonClick(object sender, EventArgs e)
    {
    }

    private async void OnPremiumRefreshButtonClick(object sender, EventArgs e)
    {
        basicRefreshButton.Enabled = false;
        premiumRefreshButton.Enabled = false;

        SavingForm form = new SavingForm()
        {
            MaxProgress = AssumedSize * securityDataGridView.Rows.Count
        };

        form.Show();

        try
        {
            await Program.AbortRetryIgnoreAsync(async () =>
            {
                form.Progress = 0;

                for (int i = 0; i < securityDataGridView.Rows.Count; i++)
                {
                    if (securityDataGridView.Rows[i].IsNewRow)
                    {
                        continue;
                    }

                    string symbol = (string)securityDataGridView[securitySymbolColumn.Index, i].Value!;
                    IReadOnlyList<AlphaVantageAdjustedTimeSeriesResponse> timeSeries = await _client.GetTimeSeriesAsync(
                        Settings.Default.AlphaVantageApiKey,
                        AlphaVantageAdjustedTimeSeries.Monthly,
                        new AlphaVantageTimeSeriesRequest()
                        {
                            OutputSize = AlphaVantageOutputSize.Compact,
                            Symbol = symbol
                        });

                    foreach (AlphaVantageAdjustedTimeSeriesResponse point in timeSeries)
                    {
                        priceDataGridView.Rows.Add(
                            symbol,
                            point.Timestamp, point.Close, point.AdjustedClose);
                    }

                    form.Progress += Math.Min(timeSeries.Count, AssumedSize);

                    form.Refresh();
                    priceDataGridView.Refresh();

                    await Task.Yield();

                    if (i < securityDataGridView.Rows.Count - 1)
                    {
                        await Task.Delay(millisecondsDelay: 1000);
                    }
                }
            });
        }
        finally
        {
            form.Close();

            basicRefreshButton.Enabled = true;
            premiumRefreshButton.Enabled = true;
        }
    }
}
