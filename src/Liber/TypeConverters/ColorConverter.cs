// ColorConverter.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Drawing;
using System.Globalization;
using CsvHelper.Configuration;

namespace CsvHelper.TypeConversion;

internal sealed class ColorConverter : DefaultTypeConverter
{
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return Color.Empty;
        }

        string[] segments = text
            .Substring(4)
            .TrimEnd(')')
            .Split(',', StringSplitOptions.TrimEntries);

        return Color.FromArgb(
            int.Parse(segments[0], CultureInfo.InvariantCulture),
            int.Parse(segments[1], CultureInfo.InvariantCulture),
            int.Parse(segments[2], CultureInfo.InvariantCulture));
    }

    public override string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
    {
        if (value == null)
        {
            return null;
        }

        Color color = (Color)value;

        if (color.IsEmpty)
        {
            return string.Empty;
        }

        return $"rgb({color.R},{color.G},{color.B})";
    }
}
