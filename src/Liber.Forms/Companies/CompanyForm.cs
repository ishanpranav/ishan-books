// CompanyForm.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Humanizer;
using Liber.Forms.AccountViews;

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
        equityAccountComboBox.DataSource = new AccountViewBindingList(company, IsEquity);
        equityAccountComboBox.ValueMember = nameof(AccountView.Id);
        equityAccountComboBox.DisplayMember = nameof(AccountView.DisplayName);
        otherEquityAccountComboBox.DataSource = new AccountViewBindingList(company, IsEquity);
        otherEquityAccountComboBox.ValueMember = nameof(AccountView.Id);
        otherEquityAccountComboBox.DisplayMember = nameof(AccountView.DisplayName);
        equityAccountComboBox.SelectedItem = company.EquityAccountId;
        otherEquityAccountComboBox.SelectedItem = company.OtherEquityAccountId;
        passwordTextBox.Text = company.Password;
        fiscalYearStartedDatePicker.Value = company.FiscalYearStarted;
        fiscalYearPostedDatePicker.Value = company.FiscalYearPosted;
        currentRadioButton.Checked = company.ReportingPeriod == ReportingPeriod.FiscalYear;
        ytdRadioButton.Checked = company.ReportingPeriod == ReportingPeriod.FiscalYearToDate;
        lastRadioButton.Checked = company.ReportingPeriod == ReportingPeriod.PreviousFiscalYear;
        customRadioButton.Checked = company.ReportingPeriod == ReportingPeriod.Custom;
        customStartedDatePicker.Value = company.CustomStarted ?? company.FiscalYearStarted;
        customPostedDatePicker.Value = company.CustomPosted ?? company.FiscalYearPosted;
    }

    private bool IsEquity(Guid id)
    {
        return Company.GetAccount(id).Type == AccountType.Equity;
    }

    private void OnTypeComboBoxFormat(object sender, ListControlConvertEventArgs e)
    {
        e.Value = ((CompanyType)e.ListItem!).Humanize();
    }

    private void OnFiscalYearStartedDatePickerValueChanged(object sender, EventArgs e)
    {
        if (fiscalYearStartedDatePicker.Value != Company.FiscalYearStarted)
        {
            fiscalYearPostedDatePicker.Value = fiscalYearStartedDatePicker.Value.AddYears(1).AddDays(-1);
        }
    }

    private void OnCustomRadioButtonCheckedChanged(object sender, EventArgs e)
    {
        customStartedDatePicker.Enabled = customRadioButton.Checked;
        customPostedDatePicker.Enabled = customRadioButton.Checked;
    }

    private void OnCustomStartedDatePickerValueChanged(object sender, EventArgs e)
    {
        if (customStartedDatePicker.Value != (Company.CustomStarted ?? Company.FiscalYearStarted))
        {
            customPostedDatePicker.Value = customStartedDatePicker.Value.AddYears(1).AddDays(-1);
        }
    }

    private void OnAcceptButtonClick(object sender, EventArgs e)
    {
        Company.Name = nameTextBox.Text;
        Company.Color = _colorButton.BackColor;
        Company.Type = (CompanyType)typeComboBox.SelectedItem!;
        Company.EquityAccountId = (Guid?)equityAccountComboBox.SelectedValue ?? Guid.Empty;
        Company.OtherEquityAccountId = (Guid?)otherEquityAccountComboBox.SelectedValue ?? Guid.Empty;
        Company.Password = passwordTextBox.Text;
        Company.FiscalYearStarted = fiscalYearStartedDatePicker.Value;
        Company.FiscalYearPosted = fiscalYearPostedDatePicker.Value;

        DateTime? customStarted = null;
        DateTime? customPosted = null;

        if (currentRadioButton.Checked)
        {
            Company.ReportingPeriod = ReportingPeriod.FiscalYear;
        }
        else if (ytdRadioButton.Checked)
        {
            Company.ReportingPeriod = ReportingPeriod.FiscalYearToDate;
        }
        else if (lastRadioButton.Checked)
        {
            Company.ReportingPeriod = ReportingPeriod.PreviousFiscalYear;
        }
        else
        {
            Company.ReportingPeriod = ReportingPeriod.Custom;
            customStarted = customStartedDatePicker.Value;
            customPosted = customPostedDatePicker.Value;
        }

        Company.CustomStarted = customStarted;
        Company.CustomPosted = customPosted;
        DialogResult = DialogResult.OK;

        Close();
    }

    private void OnCancelButtonClick(object sender, EventArgs e)
    {
        Close();
    }
}
