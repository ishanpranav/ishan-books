// LocalizedEnumConverter.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Humanizer;

namespace System.ComponentModel;

/// <summary>
/// Provides a type converter to convert localized enumeration values to and from strings.
/// </summary>
public class LocalizedEnumConverter<TEnum> : EnumConverter where TEnum : struct, Enum
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LocalizedEnumConverter"/> class.
    /// </summary>
    /// <param name="type">The type of the enumeration to convert.</param>
    public LocalizedEnumConverter() : base(typeof(TEnum)) { }

    /// <inheritdoc/>
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
    }

    /// <inheritdoc/>
    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is not string text)
        {
            return base.ConvertFrom(context, culture, value);
        }

        TEnum? result = text.DehumanizeTo<TEnum>(OnNoMatch.ReturnsNull);

        if (result != null)
        {
            return result;
        }

        if (Enum.TryParse(text, ignoreCase: true, out TEnum parsed))
        {
            return parsed;
        }

        return Activator.CreateInstance<TEnum>();
    }

    /// <inheritdoc/>
    public override bool CanConvertTo(ITypeDescriptorContext? context, [NotNullWhen(true)] Type? destinationType)
    {
        return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
    }

    /// <inheritdoc/>
    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        if (value is not TEnum enumValue)
        {
            return base.ConvertTo(context, culture, value, destinationType);
        }

        return enumValue.Humanize();
    }
}
