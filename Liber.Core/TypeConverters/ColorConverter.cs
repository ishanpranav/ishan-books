using CsvHelper.Configuration;
using System;
using System.Drawing;

namespace CsvHelper.TypeConversion;

public class ColorConverter : DefaultTypeConverter
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
            int.Parse(segments[0]),
            int.Parse(segments[1]),
            int.Parse(segments[2]));
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
