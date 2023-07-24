// NewAccountForm.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.Forms.Accounts;

internal sealed class NewAccountForm : AccountForm
{
    public NewAccountForm(Company company) : base(company)
    {
        numberNumericUpDown.Value = company.NextAccountNumber;
    }

    protected override void CommitChanges()
    {
        Account account = new Account();

        ApplyChanges(account);
        Company.AddAccount(account, ParentId);
    }
}
