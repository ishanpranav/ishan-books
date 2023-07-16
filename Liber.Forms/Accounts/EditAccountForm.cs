using System;

namespace Liber.Forms.Accounts;

internal sealed class EditAccountForm : AccountForm
{
    public EditAccountForm(Company company, Guid key) : base(company)
    {
        Account account = company.Accounts[key];

        Key = key;
        numberNumericUpDown.Value = account.Number;
        nameTextBox.Text = account.Name;
        placeholderCheckBox.Checked = account.Placeholder;
        descriptionTextBox.Text = account.Description;
        memoTextBox.Text = account.Memo;
        Type = account.Type;
        ParentKey = account.ParentKey;
        Color = account.Color;
        TaxType = account.TaxType;
    }

    public Guid Key { get; }

    protected override void CommitChanges()
    {
        Account account = Company.Accounts[Key];

        ApplyChanges(account);
        Company.UpdateAccount(Key, ParentKey);
    }

    protected override bool IsValid(Guid parentKey)
    {
        return parentKey != Key;
    }
}
