// FindForm.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Linq;
using System.Windows.Forms;

namespace Liber.Forms.Filters;

internal partial class FindForm : Form
{
    private readonly Company _company;

    public FindForm(Company company)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        _company = company;
    }

    private void OnFindButtonClick(object sender, EventArgs e)
    {
        _listView.Initialize(_company.Accounts
            .SelectMany(x => x.Lines)
            .Where(_filterControl.Value.IsMatch),
            AccountTypeExtensions.Debit,
            x => (x.AccountId.ToString(), _company.GetAccount(x.AccountId).Name));
    }

    private void OnCloseButtonClick(object sender, EventArgs e)
    {
        Close();
    }
}
