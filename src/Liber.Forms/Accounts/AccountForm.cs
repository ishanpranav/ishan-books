// AccountForm.cs
// Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using System.Windows.Forms;
using Humanizer;

namespace Liber.Forms.Accounts;

internal abstract partial class AccountForm : Form
{
    protected AccountForm(Company company)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        new ComponentResourceManager(GetType()).ApplyResources(this, "$this");

        Company = company;
        DialogResult = DialogResult.Cancel;
        typeComboBox.DataSource = AccountTypeExtensions.GetSortedValues();
        cashFlowComboBox.DataSource = Enum.GetValues<CashFlow>();
        numberNumericUpDown.Maximum = decimal.MaxValue;
    }

    public Company Company { get; }

    protected AccountType Type
    {
        get
        {
            return (AccountType)typeComboBox.SelectedItem!;
        }
        set
        {
            typeComboBox.SelectedItem = value;
        }
    }

    protected string? TaxType
    {
        get
        {
            return taxTypeComboBox.Text;
        }
        set
        {
            taxTypeComboBox.Text = value;
        }
    }

    protected CashFlow CashFlow
    {
        get
        {
            if (cashFlowComboBox.SelectedItem == null)
            {
                return CashFlow.None;
            }

            return (CashFlow)cashFlowComboBox.SelectedItem;
        }
        set
        {
            cashFlowComboBox.SelectedItem = value;
        }
    }

    protected abstract void CommitChanges();

    protected void ApplyChanges(Account account)
    {
        account.Number = numberNumericUpDown.Value;
        account.Name = nameTextBox.Text;
        account.Type = Type;
        account.Placeholder = placeholderCheckBox.Checked;
        account.Description = descriptionTextBox.Text;
        account.Memo = memoTextBox.Text;
        account.Color = _colorButton.BackColor;
        account.TaxType = TaxType;
        account.Inactive = inactiveCheckBox.Checked;
        account.CashFlow = CashFlow;
    }

    private void OnComboBoxFormat(object sender, ListControlConvertEventArgs e)
    {
        e.Value = ((Enum)e.ListItem!).Humanize();
    }

    private void OnTypeComboBoxSelectedIndexChanged(object sender, EventArgs e)
    {
        if (CashFlow == CashFlow.None)
        {
            CashFlow = Type.ToCashFlow();
        }
    }

    private void OnAcceptButtonClick(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;

        CommitChanges();
        Close();
    }

    private void OnCancelButtonClick(object sender, EventArgs e)
    {
        Close();
    }
}
