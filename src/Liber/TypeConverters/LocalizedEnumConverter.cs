// LocalizedEnumConverter.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Humanizer;

namespace System.ComponentModel;

/// <summary>
/// Provides a type converter to convert localized enumeration values to and from strings.
/// </summary>
public class LocalizedEnumConverter : EnumConverter
{
    private readonly Type _type;

    /// <summary>
    /// Initializes a new instance of the <see cref="LocalizedEnumConverter"/> class.
    /// </summary>
    /// <param name="type">The type of the enumeration to convert.</param>
    public LocalizedEnumConverter(Type type) : base(type)
    {
        _type = type;
    }

    /// <inheritdoc/>
    public override bool CanConvertTo(ITypeDescriptorContext? context, [NotNullWhen(true)] Type? destinationType)
    {
        return destinationType == typeof(string);
    }

    /// <inheritdoc/>
    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        if (value is not Enum enumValue)
        {
            return null;
        }

        return enumValue.Humanize();
    }

    /// <inheritdoc/>
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        return sourceType == typeof(string);
    }

    /// <inheritdoc/>
    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is not string text)
        {
            return Activator.CreateInstance(_type);
        }

        object? result = text.DehumanizeTo(_type, OnNoMatch.ReturnsNull);

        if (result != null)
        {
            return result;
        }

        if (Enum.TryParse(_type, text, ignoreCase: true, out result))
        {
            return result;
        }

        return Activator.CreateInstance(_type);
    }
}
