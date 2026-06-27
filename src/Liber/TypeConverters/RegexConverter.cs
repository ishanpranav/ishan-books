// RegexConverter.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Globalization;
using System.Text.RegularExpressions;
using Liber;

namespace System.ComponentModel;

public class RegexConverter : TypeConverter
{
    /// <inheritdoc/>
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
    }

    /// <inheritdoc/>
    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is not string pattern)
        {
            return base.ConvertFrom(context, culture, value);
        }

        try
        {
            return new Regex(pattern);
        }
        catch (ArgumentException)
        {
            return Regexes.Any();
        }
    }
}
