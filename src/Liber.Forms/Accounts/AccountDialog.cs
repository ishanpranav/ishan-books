// AccountDialog.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Liber.Forms.AccountViews;

namespace Liber.Forms.Accounts;

internal partial class AccountDialog : Form
{
    public AccountDialog(EditableAccountView value, Func<Account, bool>? validator)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        DialogResult = DialogResult.Cancel;
        Value = value;

        _accountListView.Initialize(value.Company, new EmptySet<Account>(), validator);
    }

    public EditableAccountView Value { get; private set; }

    public void AddNullAccount()
    {
        _accountListView.AddNullAccount();
    }

    private void OnAcceptButtonClick(object sender, EventArgs e)
    {
        Guid id = _accountListView.GetSelectedAccountId();

        if (id == Guid.Empty)
        {
            return;
        }

        Value = new EditableAccountView(Value.Company, id);
        DialogResult = DialogResult.OK;

        Close();
    }

    private void OnCancelButtonClick(object sender, EventArgs e)
    {
        Close();
    }
}
