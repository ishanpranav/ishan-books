// DecimalExtensions.cs
// Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace System;

public static class DecimalExtensions
{
    public const string Format = " #,##0.00 ;(#,##0.00);   -   ";

    public static string ToLocalizedString(this decimal value)
    {
        return value.ToString(Format);
    }
}
