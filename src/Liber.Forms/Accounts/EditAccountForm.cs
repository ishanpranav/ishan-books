// EditAccountForm.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Windows.Forms;

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
        _colorButton.BackColor = account.Color;
        Type = account.Type;
        ParentKey = account.ParentKey;
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
