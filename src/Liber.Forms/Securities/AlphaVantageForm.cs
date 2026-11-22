// AlphaVantageForm.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Windows.Forms;
using Liber.AlphaVantage;
using Liber.AlphaVantage.AlphaVantageResponses;
using Liber.Forms.Properties;

namespace Liber.Forms.Securities;

internal partial class AlphaVantageForm : Form
{
    private readonly AlphaVantageClient _client;

    public AlphaVantageForm(AlphaVantageClient client)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        DialogResult = DialogResult.Cancel;
        hostnameTextBox.Text = AlphaVantageClient.Hostname;
        apiKeyTextBox.Text = Settings.Default.AlphaVantageApiKey;
        symbolTextBox.Text = Settings.Default.LastSymbol;
        _client = client;
    }

    private void OnAcceptButtonClick(object sender, EventArgs e)
    {
        Settings.Default.AlphaVantageApiKey = apiKeyTextBox.Text;
        Settings.Default.LastSymbol = symbolTextBox.Text;
        Settings.Default.Save();

        DialogResult = DialogResult.OK;

        Close();
    }

    private void OnCancelButtonClick(object sender, EventArgs e)
    {
        Close();
    }

    private async void OnTestButtonClick(object sender, EventArgs e)
    {
        string apiStatus;

        try
        {
            AlphaVantageQuoteResponse quote = await _client.GetQuoteAsync(AlphaVantageClient.DemoApiKey, symbolTextBox.Text);

            apiStatus = quote.Content ?? string.Empty;
        }
        catch (AlphaVantageException)
        {
            apiStatus = Resources.OK;
        }
        catch (Exception exception)
        {
            apiStatus = exception.Message;
        }

        string basicStatus;

        try
        {
            AlphaVantageQuoteResponse quote = await _client.GetQuoteAsync(apiKeyTextBox.Text, symbolTextBox.Text);

            basicStatus = JsonSerializer.Serialize(quote, SerializationOptions.Json);
        }
        catch (Exception exception)
        {
            basicStatus = exception.Message;
        }

        string premiumStatus;

        try
        {
            IReadOnlyList<AlphaVantageAdjustedTimeSeriesResponse> timeSeries = await _client.GetTimeSeriesAsync(
                apiKeyTextBox.Text,
                AlphaVantageAdjustedTimeSeries.Daily,
                new AlphaVantageTimeSeriesRequest()
                {
                    OutputSize = AlphaVantageOutputSize.Compact,
                    Symbol = symbolTextBox.Text
                });

            premiumStatus = timeSeries.Count > 0 ?
                JsonSerializer.Serialize(timeSeries[0], SerializationOptions.Json) :
                Resources.ApiError;
        }
        catch (Exception exception)
        {
            premiumStatus = exception.Message;
        }

        string realTimeStatus = string.Empty;

        statusTextBox.Text = FormattedStrings.GetAlphaVantageStatus(
            apiStatus,
            basicStatus,
            premiumStatus,
            realTimeStatus);

    }
}
