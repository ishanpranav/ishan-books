// EditCompanyForm.cs
// Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.Forms.Companies;

internal sealed class EditCompanyForm : CompanyForm
{
    public EditCompanyForm(Company company) : base(company)
    {
        passwordTextBox.Text = company.Password;
        passwordTextBox.Enabled = false;
    }

    public bool IsPasswordEnabled
    {
        get
        {
            return passwordTextBox.Enabled;
        }
        set
        {
            passwordTextBox.Enabled = value;
        }
    }
}
