// EditAccountForm.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using Liber.Forms.AccountViews;

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
        _colorButton.BackColor = company.GetColorOrDefault(account);
        Type = account.Type;
        TaxType = account.TaxType;
        inactiveCheckBox.Checked = account.Inactive;
        CashFlow = account.CashFlow;
        parentComboBox.DataSource = new AccountViewBindingList(company, x => x != Id);
        parentComboBox.ValueMember = nameof(IAccountView.Id);
        parentComboBox.DisplayMember = nameof(IAccountView.DisplayName);
        parentComboBox.SelectedValue = account.ParentId;
    }

    public Guid Id { get; }

    protected override void CommitChanges()
    {
        Account account = Company.Accounts[Id];

        ApplyChanges(account);
        Company.UpdateAccount(Id, (Guid?)parentComboBox.SelectedValue ?? Guid.Empty);
    }
}
