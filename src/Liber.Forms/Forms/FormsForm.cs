// FormsForm.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Liber.Forms.Forms;

internal partial class FormsForm : Form
{
    private readonly FormFactory _factory;

    public FormsForm(FormFactory factory)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        _factory = factory;
        _factory.Invalidated += OnFactoryInvalidated;

        InitializeForms();
    }

    private void OnFactoryInvalidated(object? sender, EventArgs e)
    {
        InitializeForms();
    }

    private void InitializeForms()
    {
        _listView.BeginUpdate();

        try
        {
            _listView.Items.Clear();

            foreach (KeyValuePair<Guid, Form> entry in _factory.Forms)
            {
                if (entry.Value != this)
                {
                    _listView.Items.Add(entry.Key.ToString(), entry.Value.Text, imageIndex: 0).Tag = entry.Key;
                }
            }

            _listView.AutoResizeColumns();
            _listView.Sort();
        }
        finally
        {
            _listView.EndUpdate();
        }
    }

    private void OnListViewItemActivate(object sender, EventArgs e)
    {
        if (_listView.SelectedItems.Count > 0)
        {
            Guid key = (Guid)_listView.SelectedItems[0].Tag!;

            _factory.Forms[key].Activate();
        }
    }

    private void OnCloseFormButtonClick(object sender, EventArgs e)
    {
        foreach (ListViewItem item in _listView.SelectedItems)
        {
            Guid key = (Guid)item.Tag!;

            _factory.Forms[key].Close();
        }
    }

    private void OnAcceptButtonClick(object sender, EventArgs e)
    {
        Close();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _factory.Invalidated -= OnFactoryInvalidated;

            if (components != null)
            {
                components.Dispose();

                components = null;
            }
        }

        base.Dispose(disposing);
    }
}
