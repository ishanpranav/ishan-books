// ReportTypes.cs
// Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Liber;

[Flags]
public enum ReportTypes
{
    None = 0,
    CurrentStarted = 1,
    CurrentPosted = 2,
    CashBasis = 4
}
