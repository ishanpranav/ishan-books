// BalanceInfo.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.Forms.Reports.Xsl;

internal sealed class BalanceInfo
{
    public decimal Balance { get; set; }
    public decimal Previous { get; set; }
    public decimal AverageDailyBalance { get; set; }
}
