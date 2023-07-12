using System;

namespace Liber.Forms.Accounts;

internal sealed class EditAccountForm : AccountForm
{
    public EditAccountForm(Company company, Guid key) : base(company)
    {
        Account account = company.Accounts[key];

        Key = key;
        Number = account.Number;
        AccountName = account.Name;
        Type = account.Type;
        Placeholder = account.Placeholder;
        ParentKey = account.ParentKey;
    }

    public Guid Key { get; }

    protected override void CommitChanges()
    {
        Account account = Company.Accounts[Key];

        account.Number = Number;
        account.Name = AccountName;
        account.Type = Type;
        account.Placeholder = Placeholder;

        Company.UpdateAccount(Key, ParentKey);
    }

    protected override bool IsValid(Guid parentKey)
    {
        return parentKey != Key;
    }
}
