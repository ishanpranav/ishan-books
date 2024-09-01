// CashFlow.cs
// Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.ComponentModel;

namespace Liber;

public enum CashFlow
{
    [LocalizedDescription(nameof(None))]
    None,

    [LocalizedDescription(nameof(Operating))]
    Operating,

    [LocalizedDescription(nameof(Investing))]
    Investing,

    [LocalizedDescription(nameof(Financing))]
    Financing,

    [LocalizedDescription(nameof(Adjustment))]
    Adjustment
}
