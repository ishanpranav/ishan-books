// SavingProgress.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using Liber.Sqlite;

namespace Liber.Forms.Saving;

internal sealed class SavingProgress : IProgress
{
    private const int AccountFactor = 15;
    private const int TransactionFactor = 2;

    private readonly SavingForm _form;

    public SavingProgress(SavingForm form, Company company)
    {
        _form = form;

        form.MaxProgress = company.Accounts.Count * AccountFactor + company.Transactions.Count * TransactionFactor;
    }

    public void WriteAccount()
    {
        _form.Progress += AccountFactor;
    }

    public void WriteTransaction()
    {
        _form.Progress += TransactionFactor;
    }
}
