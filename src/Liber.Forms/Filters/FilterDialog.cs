// FilterDialog.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Windows.Forms;
using Liber.Filters;

namespace Liber.Forms.Filters;

internal partial class FilterDialog : Form
{
    public Filter Value
    {
        get
        {
            return _filterControl.Value;
        }
        private set
        {
            _filterControl.Value = value;
        }
    }

    public FilterDialog(Filter value)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        DialogResult = DialogResult.Cancel;
        Value = value;
    }

    private void OnAcceptButtonClick(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;

        Close();
    }

    private void OnCancelButtonClick(object sender, EventArgs e)
    {
        Close();
    }
}
