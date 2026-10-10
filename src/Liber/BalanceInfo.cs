// BalanceInfo.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber;

public readonly struct BalanceInfo
{
    public BalanceInfo() { }

    public BalanceInfo(
        decimal balance,
        decimal previous,
        decimal averageDailyBalance,
        decimal reconciledBalance)
    {
        Balance = balance;
        Previous = previous;
        AverageDailyBalance = averageDailyBalance;
        ReconciledBalance = reconciledBalance;
    }

    public decimal Balance { get; }
    public decimal Previous { get; }
    public decimal AverageDailyBalance { get; }
    public decimal ReconciledBalance { get; }

    public static BalanceInfo operator +(BalanceInfo left, BalanceInfo right)
    {
        return new BalanceInfo(
            left.Balance + right.Balance,
            left.Previous + right.Previous,
            left.AverageDailyBalance + right.AverageDailyBalance,
            left.ReconciledBalance + right.ReconciledBalance
        );
    }
}
