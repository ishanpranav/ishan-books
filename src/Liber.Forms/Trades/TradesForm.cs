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
    private readonly BindingList<TradeView> _items;

    public TradesForm(Company company)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        _company = company;
        _items = new BindingList<TradeView>(
            _company.Trades
                .Select(x => new TradeView(x.Id, _company.GetSecurity(x.SecurityId).Symbol)
                {
                    Posted = x.Posted,
                    Type = x.Type,
                    Cost = x.Cost,
                    Quantity = x.Quantity,
                    Commission = x.Commission
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
        e.NewObject = new TradeView(Guid.Empty, string.Empty);
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

        List<Trade> trades = new List<Trade>();

        foreach (TradeView item in _items)
        {
            trades.Add(new Trade(Guid.Empty, item.Posted, item.Type)
            {
                SecurityId = _company.SecuritiesBySymbol[item.Symbol],
                Cost = item.Cost,
                Quantity = item.Quantity,
                Commission = item.Commission
            });
        }

        _company.SetTrades(trades);
    }

    private void OnCloseButtonClick(object sender, EventArgs e)
    {
        Close();
    }
}
