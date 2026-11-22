// SavingProgress.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Linq;
using Liber.Sqlite;

namespace Liber.Forms.Saving;

internal sealed class SavingProgress : IProgress
{
    private const int AccountFactor = 30;
    private const int TransactionFactor = 3;
    private const int TradeFactor = 2;
    private const int SecurityFactor = 20;
    private const int PriceFactor = 1;

    private readonly SavingForm _form;

    public SavingProgress(SavingForm form, Company company)
    {
        _form = form;

        form.MaxProgress =
            company.Accounts.Count * AccountFactor +
            company.Transactions.Count * TransactionFactor +
            company.Trades.Count * TradeFactor +
            company.Securities.Count * SecurityFactor +
            company.Prices.Count() * PriceFactor;
    }

    public void WriteAccount()
    {
        _form.Progress += AccountFactor;
    }

    public void WriteTransaction()
    {
        _form.Progress += TransactionFactor;
    }

    public void WriteTrade()
    {
        _form.Progress += TradeFactor;
    }

    public void WriteSecurity()
    {
        _form.Progress += SecurityFactor;
    }

    public void WritePrice()
    {
        _form.Progress += PriceFactor;
    }
}
