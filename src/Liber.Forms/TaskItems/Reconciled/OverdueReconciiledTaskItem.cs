// OverdueReconciliationTaskItem.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using Humanizer;
using Liber.Forms.Forms;

namespace Liber.Forms.TaskItems.Reconciled;

internal class OverdueReconciiledTaskItem : ReconciledTaskItem
{
    private readonly TimeSpan _overdue;

    public override int Priority
    {
        get
        {
            return Account.Type.IsBankOrCreditCard() ? 1 : 2;
        }
    }

    public override string Description
    {
        get
        {
            return FormattedStrings.GetOverdueReconciledDescription(Account, _overdue);
        }
    }

    public OverdueReconciiledTaskItem(Company company, FormFactory factory, Account account, TimeSpan overdue)
        : base(company, factory, account)
    {
        _overdue = overdue;
    }
}
