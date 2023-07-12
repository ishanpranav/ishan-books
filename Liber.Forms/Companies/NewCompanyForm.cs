namespace Liber.Forms.Companies;

internal sealed class NewCompanyForm : CompanyForm
{
    public NewCompanyForm() : base(new Company())
    {
        ControlBox = false;
        ShowCancelButton = false;
    }
}
