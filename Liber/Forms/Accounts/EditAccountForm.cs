using System;

namespace Liber.Forms.Accounts;

internal sealed class EditAccountForm : AccountForm
{
    public EditAccountForm(MainContext context, Guid id) : base(context.Company, id) {
        Account account = context.Company.Accounts[id];

        Number = account.Number;
        AccountName = account.Name;
        Type = account.Type;
        Locked = account.Locked;
        CompanionId = account.CompanionId;
     }
}
