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

        ComponentResourceManager resourceManager = new ComponentResourceManager(GetType());

        resourceManager.ApplyResources(this, "$this");

        Company = company;
        DialogResult = DialogResult.Cancel;
    }

    public Company Company { get; }

    protected bool ShowCancelButton
    {
        get
        {
            return cancelButton.Enabled;
        }
        set
        {
            cancelButton.Enabled = value;
        }
    }

    private void OnLoad(object sender, EventArgs e)
    {
        nameTextBox.DataBindings.Add(
            propertyName: nameof(nameTextBox.Text),
            dataSource: Company,
            dataMember: nameof(Company.Name),
            formattingEnabled: true,
            DataSourceUpdateMode.Never);
    }

    private void OnAcceptButtonClick(object sender, EventArgs e)
    {
        Company.Name = nameTextBox.Text;
        DialogResult = DialogResult.OK;

        Close();
    }

    private void OnCancelButtonClick(object sender, EventArgs e)
    {
        Close();
    }
}
