// CheckPositions.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Liber.Skia;

[Flags]
internal enum CheckPositions
{
    None = 0,
    Top = 1,
    Middle = 2,
    Bottom = 4
}
