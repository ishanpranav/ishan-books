// CheckDialog.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Windows.Forms;

namespace Liber.Forms.Lines;

internal partial class CheckDialog : Form
{
    public CheckDialog(CheckView value)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        DialogResult = DialogResult.Cancel;
        Value = value;

        _listView.Initialize(
            value.Company.GetChecks(),
            AccountTypeExtensions.Credit,
            x => (x.AccountId.ToString(), value.Company.GetAccount(x.AccountId).Name));
    }

    public CheckView Value { get; private set; }

    private void OnAcceptButtonClick(object sender, EventArgs e)
    {
        if (!_listView.TryGetSelection(out Line? line))
        {
            return;
        }

        Value = new CheckView(Value.Company, line);
        DialogResult = DialogResult.OK;

        Close();
    }

    private void OnCancelButtonClick(object sender, EventArgs e)
    {
        Close();
    }
}
