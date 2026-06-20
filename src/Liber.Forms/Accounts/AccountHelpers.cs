// AccountHelpers.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Windows.Forms;
using Liber.Forms.Forms;
using Liber.Forms.LineSources;
using Liber.Forms.Transactions;

namespace Liber.Forms.Accounts;

internal static class AccountHelpers
{
    public static void BeginReconcile(Company company, FormFactory factory, Guid accountId)
    {
        using StatementForm form = new StatementForm(company)
        {
            AccountId = accountId
        };

        if (form.ShowDialog() != DialogResult.OK)
        {
            return;
        }

        factory.AutoRegister(() => new ReconcileForm(
            company,
            form.Reconciled,
            form.ReconciledBalance,
            form.EndingBalance,
            company.GetAccount(form.AccountId)));
    }

    public static void BeginTransactions(Company company, FormFactory factory, Account account)
    {
        if (factory.TryActivate(account.Id) || account.ReadOnly)
        {
            return;
        }

        TransactionsForm form = new TransactionsForm(company, new AccountLineSource(company, account), factory);

        factory.Register(account.Id, form);
    }
}
