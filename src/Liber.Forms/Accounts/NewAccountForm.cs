// NewAccountForm.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Drawing;
using Liber.Forms.AccountViews;

namespace Liber.Forms.Accounts;

internal sealed class NewAccountForm : AccountForm
{
    public NewAccountForm(Company company) : base(company)
    {
        numberNumericUpDown.Value = company.NextAccountNumber;
        _colorButton.BackColor = company.Color;
        _colorButton.ForeColor = company.Color.GetForeColor();
        parentComboBox.DataSource = new AccountViewBindingList(company, validator: null);
        parentComboBox.ValueMember = nameof(AccountView.Id);
        parentComboBox.DisplayMember = nameof(AccountView.DisplayName);
    }

    public NewAccountForm(Company company, Guid parentId) : this(company)
    {
        parentComboBox.SelectedValue = parentId;

        if (parentId == Guid.Empty)
        {
            return;
        }

        Account parent = Company.GetAccount(parentId);

        typeComboBox.SelectedItem = parent.Type;
        cashFlowComboBox.SelectedItem = parent.CashFlow;
        taxTypeCheckBox.Checked = parent.TaxType;
    }

    protected override void CommitChanges()
    {
        Account account = new Account();

        ApplyChanges(account);
        Company.AddAccount(account, (Guid?)parentComboBox.SelectedValue ?? Guid.Empty);
    }
}
