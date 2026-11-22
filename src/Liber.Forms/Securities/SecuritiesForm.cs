// SecuritiesForm.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.RightsManagement;
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
    private readonly BindingList<SecurityView> _items;

    public SecuritiesForm(Company company, AlphaVantageClient client)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        List<SecurityView> items = new List<SecurityView>();

        foreach (Security security in company.Securities)
        {
            SecurityView item = new SecurityView(security.Id)
            {
                Symbol = security.Symbol,
                Name = security.Name
            };

            Price? price = company.GetPrice(security.Id);

            if (price != null)
            {
                item.Price = price.AdjustedClose;
            }

            items.Add(item);
        }

        _company = company;
        _client = client;
        _items = new BindingList<SecurityView>(items)
        {
            AllowNew = true,
            AllowEdit = true,
            AllowRemove = true
        };
        _items.AddingNew += OnItemsAddingNew;
        _dataGridView.DataSource = _items;
    }

    private void OnItemsAddingNew(object? sender, AddingNewEventArgs e)
    {
        e.NewObject = new SecurityView(Guid.Empty);
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
        _dataGridView.EndEdit();

        if (BindingContext != null)
        {
            BindingContext[_items].EndCurrentEdit();
        }

        foreach (SecurityView item in _items)
        {
            Security security = _company.GetSecurity(item.Id);

            security.Name = item.Name;

            _company.UpdateSecurity(security, item.Symbol);
        }
    }

    private void OnCloseButtonClick(object sender, EventArgs e)
    {
        Close();
    }

    private async void OnBasicRefreshButtonClick(object sender, EventArgs e)
    {
        basicRefreshButton.Enabled = false;
        premiumRefreshButton.Enabled = false;

        SavingForm form = new SavingForm()
        {
            MaxProgress = AssumedSize * _items.Count
        };

        form.Show();

        try
        {
            await Program.AbortRetryIgnoreAsync(async () =>
            {
                form.Progress = 0;

                for (int i = 0; i < _items.Count; i++)
                {
                    IReadOnlyList<AlphaVantageTimeSeriesResponse> timeSeries = await _client.GetTimeSeriesAsync(
                        Settings.Default.AlphaVantageApiKey,
                        AlphaVantageTimeSeries.Monthly,
                        new AlphaVantageTimeSeriesRequest()
                        {
                            OutputSize = AlphaVantageOutputSize.Compact,
                            Symbol = _items[i].Symbol
                        });

                    _company.UpdatePrices(timeSeries.Select(x => new Price(_items[i].Id, x.Timestamp)
                    {
                        Close = x.Close,
                    }));

                    Price? price = _company.GetPrice(_items[i].Id);

                    if (price != null)
                    {
                        _items[i].Price = price.AdjustedClose;
                    }

                    form.Progress += Math.Min(timeSeries.Count, AssumedSize);

                    form.Refresh();
                    _dataGridView.Refresh();

                    await Task.Yield();

                    if (i < _items.Count - 1)
                    {
                        await Task.Delay(millisecondsDelay: 15000);
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

    private async void OnPremiumRefreshButtonClick(object sender, EventArgs e)
    {
        basicRefreshButton.Enabled = false;
        premiumRefreshButton.Enabled = false;

        SavingForm form = new SavingForm()
        {
            MaxProgress = AssumedSize * _items.Count
        };

        form.Show();

        try
        {
            await Program.AbortRetryIgnoreAsync(async () =>
            {
                form.Progress = 0;

                for (int i = 0; i < _items.Count; i++)
                {
                    IReadOnlyList<AlphaVantageAdjustedTimeSeriesResponse> timeSeries = await _client.GetTimeSeriesAsync(
                        Settings.Default.AlphaVantageApiKey,
                        AlphaVantageAdjustedTimeSeries.Monthly,
                        new AlphaVantageTimeSeriesRequest()
                        {
                            OutputSize = AlphaVantageOutputSize.Compact,
                            Symbol = _items[i].Symbol
                        });

                    _company.UpdatePrices(timeSeries.Select(x => new Price(_items[i].Id, x.Timestamp)
                    {
                        Close = x.Close,
                        AdjustedClose = x.AdjustedClose
                    }));

                    Price? price = _company.GetPrice(_items[i].Id);

                    if (price != null)
                    {
                        _items[i].Price = price.AdjustedClose;
                    }

                    form.Progress += Math.Min(timeSeries.Count, AssumedSize);

                    form.Refresh();
                    _dataGridView.Refresh();

                    await Task.Yield();

                    if (i < _items.Count - 1)
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
