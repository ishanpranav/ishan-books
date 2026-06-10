// CompanyForm.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Humanizer;

namespace Liber.Forms.Companies;

internal abstract partial class CompanyForm : Form
{
    public Company Company { get; }

    protected CompanyForm() : this(new Company()) { }

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
        _colorButton.ForeColor = company.Color.GetForeColor();
        typeComboBox.DataSource = Enum.GetValues<CompanyType>();
        typeComboBox.SelectedItem = company.Type;
        equityAccountComboBox.Initialize(company, x => company.Accounts[x].Type == AccountType.Equity);
        otherEquityAccountComboBox.Initialize(company, x => company.Accounts[x].Type == AccountType.Equity);
        equityAccountComboBox.SelectedItem = company.EquityAccountId;
        otherEquityAccountComboBox.SelectedItem = company.OtherEquityAccountId;
        passwordTextBox.Text = company.Password;
        fiscalYearStartedDatePicker.Value = company.FiscalYearStarted;
        fiscalYearPostedDatePicker.Value = company.FiscalYearPosted;
        currentRadioButton.Checked = company.ReportingPeriod == ReportingPeriod.FiscalYear;
        lastRadioButton.Checked = company.ReportingPeriod == ReportingPeriod.PreviousFiscalYear;
        customRadioButton.Checked = company.ReportingPeriod == ReportingPeriod.Custom;
        customStartedDatePicker.Value = company.CustomStarted ?? company.FiscalYearStarted;
        customPostedDatePicker.Value = company.CustomPosted ?? company.FiscalYearPosted;
    }

    private void OnTypeComboBoxFormat(object sender, ListControlConvertEventArgs e)
    {
        e.Value = ((CompanyType)e.ListItem!).Humanize();
    }

    private void OnCustomRadioButtonCheckedChanged(object sender, EventArgs e)
    {
        customStartedDatePicker.Enabled = customRadioButton.Checked;
        customPostedDatePicker.Enabled = customRadioButton.Checked;
    }

    private void OnAcceptButtonClick(object sender, EventArgs e)
    {
        Company.Name = nameTextBox.Text;
        Company.Color = _colorButton.BackColor;
        Company.Type = (CompanyType)typeComboBox.SelectedItem!;
        Company.EquityAccountId = equityAccountComboBox.SelectedItem;
        Company.OtherEquityAccountId = otherEquityAccountComboBox.SelectedItem;
        Company.Password = passwordTextBox.Text;
        Company.FiscalYearStarted = fiscalYearStartedDatePicker.Value;
        Company.FiscalYearPosted = fiscalYearPostedDatePicker.Value;

        if (currentRadioButton.Checked)
        {
            Company.ReportingPeriod = ReportingPeriod.FiscalYear;
        }
        else if (lastRadioButton.Checked)
        {
            Company.ReportingPeriod = ReportingPeriod.PreviousFiscalYear;
        }
        else
        {
            Company.ReportingPeriod = ReportingPeriod.Custom;
            Company.CustomStarted = customStartedDatePicker.Value;
            Company.CustomPosted = customPostedDatePicker.Value;
        }

        DialogResult = DialogResult.OK;

        Close();
    }

    private void OnCancelButtonClick(object sender, EventArgs e)
    {
        Close();
    }
}
