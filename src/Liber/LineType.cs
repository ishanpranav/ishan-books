// LineType.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.ComponentModel;

namespace Liber;

public enum LineType
{
    [Description("")]
    None = 0,

    [Description("GENJRN")]
    GeneralJournal,

    [Description("TNF")]
    Transfer,

    [Description("DEP")]
    Deposit,

    [Description("CHK")]
    Check,

    [Description("PMT")]
    Payment,

    [Description("BILL")]
    Bill
}
