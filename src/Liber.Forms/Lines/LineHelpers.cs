// LineHelpers.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using Liber.Forms.Forms;
using Liber.Forms.LineSources;
using Liber.Forms.Transactions;

namespace Liber.Forms.Lines;

internal static class LineHelpers
{
    public static void BeginTransactions(Company company, FormFactory factory, Line line)
    {
        Guid id = line.AccountId;
        Account account = company.GetAccount(id);

        if (factory.TryActivate(id))
        {
            if (factory.Forms[id] is TransactionsForm transactionsForm)
            {
                transactionsForm.SelectLine(line);
            }

            return;
        }

        if (account.ReadOnly)
        {
            return;
        }

        TransactionsForm form = new TransactionsForm(
            company,
            new AccountLineSource(company, account),
            factory,
            line);

        factory.Register(id, form);
    }
}
