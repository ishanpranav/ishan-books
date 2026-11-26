// SavingProgress.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using Liber.Sqlite;

namespace Liber.Forms.Saving;

internal sealed class SavingProgress : IProgress
{
    private readonly SavingForm _form;

    public SavingProgress(SavingForm form, Company company)
    {
        _form = form;

        form.MaxProgress = company.Accounts.Count + company.Transactions.Count;
    }

    public SavingProgress(SavingForm form, Portfolio portfolio)
    {
        _form = form;

        form.MaxProgress = portfolio.Securities.Count;
    }

    public void Tick()
    {
        _form.Progress++;
    }
}
