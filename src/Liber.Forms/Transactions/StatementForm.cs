// ReconciliationContextForm.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using System.Windows.Forms;
using Liber.Forms.AccountViews;

namespace Liber.Forms.Transactions;

internal partial class StatementForm : Form
{
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public decimal OpeningBalance
    {
        get
        {
            return openingBalanceNumericUpDown.Value;
        }
        set
        {
            openingBalanceNumericUpDown.Value = value;
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public decimal EndingBalance
    {
        get
        {
            return endingBalanceNumericUpDown.Value;
        }
        set
        {
            endingBalanceNumericUpDown.Value = value;
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public DateTime Reconciled
    {
        get
        {
            return reconciledDateTimePicker.Value;
        }
        set
        {
            reconciledDateTimePicker.Value = value;
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Guid Account
    {
        get
        {
            return (Guid)accountComboBox.SelectedValue!;
        }
        set
        {
            accountComboBox.SelectedValue = value;
        }
    }

    public StatementForm(Company company)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        DialogResult = DialogResult.Cancel;
        accountComboBox.DataSource = new AccountViewBindingList(company, x => !x.ReadOnly);
        accountComboBox.ValueMember = nameof(AccountView.Id);
        accountComboBox.DisplayMember = nameof(AccountView.DisplayName);
    }

    private void OnAcceptButtonClick(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
    }
}
