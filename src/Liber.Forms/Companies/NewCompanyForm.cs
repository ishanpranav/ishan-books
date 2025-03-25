// NewCompanyForm.cs
// Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.Forms.Companies;

internal sealed class NewCompanyForm : CompanyForm
{
    public NewCompanyForm() : base(new Company())
    {
        closingAccountsGroupBox.Enabled = false;
    }
}
