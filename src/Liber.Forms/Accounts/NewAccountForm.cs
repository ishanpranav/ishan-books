// NewAccountForm.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Drawing;

namespace Liber.Forms.Accounts;

internal sealed class NewAccountForm : AccountForm
{
    public NewAccountForm(Company company) : base(company)
    {
        numberNumericUpDown.Value = company.NextAccountNumber;
        _colorButton.BackColor = company.Color;
        _colorButton.ForeColor = company.Color.GetForeColor();

        parentComboBox.Items.Add(NullAccountView.Value);
        parentComboBox.Initialize(company, validator: null);
    }

    public NewAccountForm(Company company, Guid parentId) : this(company)
    {
        parentComboBox.SelectedItem = parentId;
    }

    protected override void CommitChanges()
    {
        Account account = new Account();

        ApplyChanges(account);
        Company.AddAccount(account, parentComboBox.SelectedItem);
    }
}
