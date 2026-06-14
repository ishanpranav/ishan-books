// NameDialog.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Windows.Forms;

namespace Liber.Forms.Transactions;

internal partial class NameDialog : Form
{
    public string Value
    {
        get
        {
            return nameComboBox.Text;
        }
    }

    public NameDialog(Company company)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        DialogResult = DialogResult.Cancel;
        nameComboBox.DataSource = company.GetNames();
    }

    private void OnAcceptButtonClick(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;

        Close();
    }
}
