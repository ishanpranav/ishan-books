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
        if (multiple <= 0)
        {
            return ToLocalizedString(value);
        }

        decimal rounded = decimal.Round(value / multiple, MidpointRounding.ToEven);

        if (!multiple.IsPow10() || multiple < 1)
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

    public static bool IsPow10(this decimal value)
    {
        if (value <= 0)
        {
            return false;
        }

        for (; value <= 1; value *= 10) ;

        for (; value >= 10; value /= 10)
        {
            if (value % 10 != 0)
            {
                return false;
            }
        }

        return value == 1;
    }
}
