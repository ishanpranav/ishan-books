// FilterConverter.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace System.ComponentModel;

internal class FilterConverter : TypeConverter
{
    public override bool CanConvertTo(ITypeDescriptorContext? context, [NotNullWhen(true)] Type? destinationType)
    {
        return destinationType == typeof(string) ||  base.CanConvertTo(context, destinationType);
    }

    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        if (value == null || destinationType != typeof(string))
        {
            return base.ConvertTo(context, culture, value, destinationType);
        }

        return value.ToString();
    }
}
