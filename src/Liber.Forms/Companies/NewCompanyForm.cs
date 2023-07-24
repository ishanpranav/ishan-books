// NewCompanyForm.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.Forms.Companies;

internal sealed class NewCompanyForm : CompanyForm
{
    public NewCompanyForm() : base(new Company())
    {
        ControlBox = false;
        ShowCancelButton = false;
    }
}
