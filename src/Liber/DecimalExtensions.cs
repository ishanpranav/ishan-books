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

    public static string ToLocalizedString(this decimal value, decimal multiple)
    {
        if (multiple == 0)
        {
            return ToLocalizedString(value);
        }

        value = Math.Round(value / multiple, MidpointRounding.ToEven);

        if (multiple < 1)
        {
            value *= multiple;
        }

        return value.ToString(" #,##0.## ;(#,##0.##);   -   ");
    }
}
