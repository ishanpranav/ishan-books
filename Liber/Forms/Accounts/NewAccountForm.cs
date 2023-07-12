using System;

namespace Liber.Forms.Accounts;

internal sealed class NewAccountForm : AccountForm
{
    public NewAccountForm(MainContext context, Guid _) : base(context.Company, Guid.Empty)
    {
        Number = context.Company.NextAccountNumber;
    }
}
