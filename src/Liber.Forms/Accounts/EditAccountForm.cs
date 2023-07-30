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
        _colorButton.ForeColor = Colors.GetForeColor(account.Color);
        Type = account.Type;
        TaxType = account.TaxType;
        parentComboBox.Initialize(company, x => x != Id);
        parentComboBox.SelectedItem = account.ParentId;

        if (Id == company.EquityAccountId || Id == company.OtherEquityAccountId)
        {
            placeholderCheckBox.Enabled = false;
        }
    }

    public Guid Id { get; }

    protected override void CommitChanges()
    {
        Account account = Company.Accounts[Id];

        ApplyChanges(account);
        Company.UpdateAccount(Id, parentComboBox.SelectedItem);
    }
}
