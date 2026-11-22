// TradesForm.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Liber.Forms.Properties;

namespace Liber.Forms.Trades;

internal partial class TradesForm : Form
{
    private readonly Company _company;
    private readonly BindingList<YahooFinanceTrade> _items;

    public TradesForm(Company company)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        _company = company;
        _items = new BindingList<YahooFinanceTrade>(
            _company.Trades
                .Select(trade => new YahooFinanceTrade()
                {
                    Value = trade,
                    Symbol = _company.GetSecurity(trade.SecurityId).Symbol
                })
                .ToList())
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
        e.NewObject = new YahooFinanceTrade()
        {
            Value = new Trade(
                Guid.Empty,
                DateTime.Today,
                TradeType.Purchase)
        };
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

        foreach (YahooFinanceTrade item in _items)
        {
            item.Value.SecurityId = _company.SecuritiesBySymbol[item.Symbol];
        }

        _company.SetTrades(_items.Select(x => x.Value));
    }
}
