namespace Liber.Forms.Accounts;

internal sealed class NewAccountForm : AccountForm
{
    public NewAccountForm(Company company) : base(company)
    {
        Number = company.NextAccountNumber;
    }

    protected override void CommitChanges()
    {
        Company.AddAccount(new Account()
        {
            Name = AccountName,
            Number = Number,
            Type = Type,
            Placeholder = Placeholder
        }, ParentKey);
    }
}
