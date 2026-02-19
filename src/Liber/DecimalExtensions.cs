// DecimalExtensions.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
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

        decimal rounded = Math.Round(value / multiple, MidpointRounding.ToEven);

        if (multiple % 10 != 0)
        {
            rounded *= multiple;

            if (multiple < 1)
            {
                return rounded.ToString(Format);
            }
        }

        if (rounded == 0)
        {
            if (value > 0)
            {
                return " 0 ";
            }

            if (value < 0)
            {
                return "(0)";
            }
        }

        return rounded.ToString(" #,##0 ;(#,##0);   -   ");
    }
}
