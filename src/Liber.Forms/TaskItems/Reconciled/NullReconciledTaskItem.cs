// NullReconciledTaskItem
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using Liber.Forms.Forms;

namespace Liber.Forms.TaskItems.Reconciled;

internal class NullReconciledTaskItem : ReconciledTaskItem
{
    public override int Priority
    {
        get
        {
            return Account.Type.IsBankOrCreditCard() ? 0 : 3;
        }
    }

    public override string Description
    {
        get
        {
            return FormattedStrings.GetNullReconciledDescription(Account);
        }
    }

    public NullReconciledTaskItem(Company company, FormFactory factory, Account account) :
        base(company, factory, account)
    { }
}
