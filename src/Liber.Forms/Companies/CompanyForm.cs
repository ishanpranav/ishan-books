// CompanyForm.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Liber.Forms.Companies;

internal abstract partial class CompanyForm : Form
{
    protected CompanyForm(Company company)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        ComponentResourceManager resourceManager = new ComponentResourceManager(GetType());

        resourceManager.ApplyResources(this, "$this");

        Company = company;
        DialogResult = DialogResult.Cancel;
        nameTextBox.Text = company.Name;
        _colorButton.BackColor = company.Color;
        _colorButton.ForeColor = Colors.GetForeColor(company.Color);
        typeComboBox.DataSource = Enum.GetValues<CompanyType>();
        typeComboBox.SelectedItem = company.Type;
        equityAccountComboBox.Initialize(company, x => company.Accounts[x].Type == AccountType.Equity);
        otherEquityAccountComboBox.Initialize(company, x => company.Accounts[x].Type == AccountType.Equity);
        equityAccountComboBox.SelectedItem = company.EquityAccountId;
        otherEquityAccountComboBox.SelectedItem = company.OtherEquityAccountId;
        passwordTextBox.Text = company.Password;
    }

    public Company Company { get; }

    private void OnTypeComboBoxFormat(object sender, ListControlConvertEventArgs e)
    {
        e.Value = ((CompanyType)e.ListItem!).ToLocalizedString();
    }

    private void OnAcceptButtonClick(object sender, EventArgs e)
    {
        Company.Name = nameTextBox.Text;
        Company.Color = _colorButton.BackColor;
        Company.Type = (CompanyType)typeComboBox.SelectedItem!;
        Company.EquityAccountId = equityAccountComboBox.SelectedItem;
        Company.OtherEquityAccountId = otherEquityAccountComboBox.SelectedItem;
        Company.Password = passwordTextBox.Text;
        DialogResult = DialogResult.OK;

        Close();
    }

    private void OnCancelButtonClick(object sender, EventArgs e)
    {
        Close();
    }
}
