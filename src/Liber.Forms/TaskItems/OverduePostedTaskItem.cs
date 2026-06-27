// OverdueTransactionTaskItem.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using Humanizer;
using Liber.Forms.Accounts;
using Liber.Forms.Forms;

namespace Liber.Forms.TaskItems;

internal class OverduePostedTaskItem : TaskItem
{
    private Company _company;
    private FormFactory _factory;
    private Account _account;
    private TimeSpan _overdue;

    public override int Priority
    {
        get
        {
            return _account.Type.IsBankOrCreditCard() ? 4 : 5;
        }
    }

    public override string Description
    {
        get
        {
            return FormattedStrings.GetOverduePostedDescription(_account, _overdue);
        }
    }

    public OverduePostedTaskItem(Company company, FormFactory factory, Account account, TimeSpan overdue)
    {
        _company = company;
        _factory = factory;
        _account = account;
        _overdue = overdue;
    }

    public override void Begin()
    {
        AccountHelpers.BeginTransactions(_company, _factory, _account);
    }
}
