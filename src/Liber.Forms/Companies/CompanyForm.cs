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
        ClickOnce.Initialize(this);

        ComponentResourceManager resourceManager = new ComponentResourceManager(GetType());

        resourceManager.ApplyResources(this, "$this");

        Company = company;
        DialogResult = DialogResult.Cancel;
        nameTextBox.Text = company.Name;
        _colorButton.BackColor = company.Color;
        _colorButton.ForeColor = Colors.GetForeColor(company.Color);
        passwordTextBox.Text = company.Password;
    }

    public Company Company { get; }

    private void OnAcceptButtonClick(object sender, EventArgs e)
    {
        Company.Name = nameTextBox.Text;
        Company.Color = _colorButton.BackColor;
        Company.Password = passwordTextBox.Text;
        DialogResult = DialogResult.OK;

        Close();
    }

    private void OnCancelButtonClick(object sender, EventArgs e)
    {
        Close();
    }
}
