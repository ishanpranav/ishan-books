// DecimalExtensions.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace System;

public static class DecimalExtensions
{
    public static string ToLocalizedString(this decimal value)
    {
        return string.Format("{0: #,##0.00 ;(#,##0.00);   -   }", value);
    }
}
