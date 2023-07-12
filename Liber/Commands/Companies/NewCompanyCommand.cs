using Liber.Forms;
using Liber.Forms.Companies;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Liber.Commands.Companies;

internal sealed class NewCompanyCommand : Command
{
    public override async Task ExecuteAsync(MainContext context)
    {
        Guid key = new Guid("3441FF73-E251-4AC0-972C-7584E9481EDF");

        if (context.TryKill(key))
        {
            return;
        }

        if (await context.Form.TryCancelAsync())
        {
            return;
        }

        NewCompanyForm form = new NewCompanyForm();

        form.FormClosed += (_, _) =>
        {
            if (form.DialogResult == DialogResult.OK)
            {
                context.Company = form.Company;
            }
        };

        context.Register(key, form);
    }
}
