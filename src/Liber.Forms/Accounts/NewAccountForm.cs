// NewAccountForm.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.Forms.Accounts;

internal sealed class NewAccountForm : AccountForm
{
    public NewAccountForm(Company company) : base(company)
    {
        numberNumericUpDown.Value = company.NextAccountNumber;
        _colorButton.BackColor = company.Color;
        _colorButton.ForeColor = Colors.GetForeColor(company.Color);
        parentComboBox.Initialize(company, validator: null);
    }

    protected override void CommitChanges()
    {
        Account account = new Account();

        ApplyChanges(account);
        Company.AddAccount(account, parentComboBox.SelectedItem);
    }
}
