// EquityModes.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Liber;

[Flags]
public enum EquityModes
{
    None = 0,
    CurrentStarted = 1,
    CurrentPosted = 2
}
