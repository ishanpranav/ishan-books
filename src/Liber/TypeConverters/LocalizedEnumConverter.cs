// LocalizedEnumConverter.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Humanizer;

namespace System.ComponentModel;

public class LocalizedEnumConverter : EnumConverter
{
    private readonly Type _type;

    public LocalizedEnumConverter(Type type) : base(type)
    {
        _type = type;
    }

    public override bool CanConvertTo(ITypeDescriptorContext? context, [NotNullWhen(true)] Type? destinationType)
    {
        return destinationType == typeof(string);
    }

    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        if (value is not Enum enumValue)
        {
            return null;
        }

        return enumValue.Humanize();
    }

    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        return sourceType == typeof(string);
    }

    public override object ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is not string text)
        {
            return 0;
        }

        object? result = text.DehumanizeTo(_type, OnNoMatch.ReturnsNull);

        if (result == null)
        {
            return 0;
        }

        return result;
    }
}
