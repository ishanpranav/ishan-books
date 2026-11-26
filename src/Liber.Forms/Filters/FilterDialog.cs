// FilterDialog.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using System.Windows.Forms;
using Liber.Filters;

namespace Liber.Forms.Filters;

internal partial class FilterDialog : Form
{
    public FilterDialog()
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        DialogResult = DialogResult.Cancel;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [TypeConverter(typeof(FormattableConverter))]
    public Filter Value
    {
        get
        {
            return _filterControl.Value;
        }
        set
        {
            _filterControl.Value = value;
        }
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
