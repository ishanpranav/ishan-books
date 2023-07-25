// EditAccountForm.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Liber.Forms.Accounts;

internal sealed class EditAccountForm : AccountForm
{
    public EditAccountForm(Company company, Guid id) : base(company)
    {
        Account account = company.Accounts[id];

        Id = id;
        numberNumericUpDown.Value = account.Number;
        nameTextBox.Text = account.Name;
        placeholderCheckBox.Checked = account.Placeholder;
        descriptionTextBox.Text = account.Description;
        memoTextBox.Text = account.Memo;
        _colorButton.BackColor = account.Color;
        Type = account.Type;
        ParentId = account.ParentId;
        TaxType = account.TaxType;
    }

    public Guid Id { get; }

    protected override void CommitChanges()
    {
        Account account = Company.Accounts[Id];

        ApplyChanges(account);
        Company.UpdateAccount(Id, ParentId);
    }

    protected override bool IsValid(Guid parentId)
    {
        return parentId != Id;
    }
}
