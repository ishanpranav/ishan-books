// ReconciledTaskItem.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using Liber.Forms.Accounts;
using Liber.Forms.Forms;

namespace Liber.Forms.TaskItems.Reconciled;

internal abstract class ReconciledTaskItem : TaskItem
{
    protected Company Company { get; }
    protected FormFactory Factory { get; }
    protected Account Account { get; }

    public override int Priority
    {
        get
        {
            return 1;
        }
    }

    public override string Description
    {
        get
        {
            return string.Format("{0} has never been reconciled", Account.Name);
        }
    }

    protected ReconciledTaskItem(Company company, FormFactory factory, Account account)
    {
        Company = company;
        Factory = factory;
        Account = account;
    }

    public override void Begin()
    {
        AccountHelpers.BeginReconcile(Company, Factory, Account.Id);
    }
}
