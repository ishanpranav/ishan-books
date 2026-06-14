// EditAccountForm.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using Liber.Forms.AccountViews;

namespace Liber.Forms.Accounts;

internal sealed class EditAccountForm : AccountForm
{
    private readonly Account _account;

    public EditAccountForm(Company company, Account account) : base(company)
    {
        numberNumericUpDown.Value = account.Number;
        nameTextBox.Text = account.Name;
        placeholderCheckBox.Checked = account.Placeholder;
        descriptionTextBox.Text = account.Description;
        memoTextBox.Text = account.Memo;
        _colorButton.BackColor = company.GetColorOrDefault(account);
        Type = account.Type;
        TaxType = account.TaxType;
        inactiveCheckBox.Checked = account.Inactive;
        CashFlow = account.CashFlow;
        parentComboBox.DataSource = new AccountViewBindingList(company, x => x.Id != account.Id);
        parentComboBox.ValueMember = nameof(AccountView.Id);
        parentComboBox.DisplayMember = nameof(AccountView.DisplayName);
        parentComboBox.SelectedValue = account.ParentId;
        _account = account;
    }

    protected override void CommitChanges()
    {
        ApplyChanges(_account);
        Company.UpdateAccount(
            _account.Id,
            (Guid?)parentComboBox.SelectedValue ?? Guid.Empty,
            numberNumericUpDown.Value,
            nameTextBox.Text,
            Type);
    }
}
