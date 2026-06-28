// FindForm.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Linq;
using System.Windows.Forms;
using Liber.Forms.Forms;
using Liber.Forms.Lines;

namespace Liber.Forms.Filters;

internal partial class FindForm : Form
{
    private readonly Company _company;
    private readonly FormFactory _factory;

    public FindForm(Company company, FormFactory factory)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        _company = company;
        _factory = factory;
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

    private void OnListViewItemActivate(object sender, EventArgs e)
    {
        if (!_listView.TryGetSelection(out Line? line))
        {
            return;
        }

        LineHelpers.BeginTransactions(_company, _factory, line);
    }
}
