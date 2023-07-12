using System;

namespace Liber.Forms.Companies;

internal sealed class EditCompanyForm : CompanyForm
{
    public EditCompanyForm(MainContext context, Guid _) : base(context.Company) { }
}
