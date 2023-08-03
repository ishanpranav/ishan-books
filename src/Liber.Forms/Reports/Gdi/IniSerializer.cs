// IniSerializer.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using IniParser;
using IniParser.Model;
using IniParser.Model.Configuration;
using IniParser.Parser;

namespace Liber.Forms.Reports.Gdi;

internal sealed class IniSerializer
{
    public static GdiCheckReport DeserializeGdiCheckReport(Company company, string path)
    {
        const StringSplitOptions options =
            StringSplitOptions.TrimEntries |
            StringSplitOptions.RemoveEmptyEntries;

        IniDataParser baseParser = new IniDataParser(new IniParserConfiguration()
        {
            AllowDuplicateKeys = true,
            CaseInsensitive = true,
            CommentString = "#"
        });
        FileIniDataParser parser = new FileIniDataParser(baseParser);
        IniData data = parser.ReadFile(path);
        KeyDataCollection top = data["Top"];
        KeyDataCollection items = data["Check Items"];
        string? font = top["Font"];
        string title = top["Title"] ?? Path.GetFileNameWithoutExtension(path) ?? string.Empty;
        string? translation = top["Translation"];
        GdiCheckReport result = new GdiCheckReport(company, title);

        if (translation != null)
        {
            string[] values = translation.Split(';', options);

            result.X = float.Parse(values[0], CultureInfo.InvariantCulture);
            result.Y = float.Parse(values[1], CultureInfo.InvariantCulture);
        }

        if (font != null)
        {
            string[] fonts = font.Split(' ', options);

            result.Font = new Font(fonts[0], float.Parse(fonts[1], CultureInfo.InvariantCulture) * GdiReport.Points, FontStyle.Regular, GraphicsUnit.Point);
        }

        for (int i = 1; true; i++)
        {
            string? type = items["Type_" + i];
            string? blockingChars = items["Blocking_Chars_" + i];
            string? alignment = items["Align_" + i];
            string? bounds = items["Coords_" + i];

            if (type == null)
            {
                break;
            }

            CheckItem item = new CheckItem(type);

            if (bounds != null)
            {
                string[] values = bounds.Split(' ');
                float x = float.Parse(values[0], CultureInfo.InvariantCulture);
                float y = float.Parse(values[1], CultureInfo.InvariantCulture);
                float width = 0;
                float height = 0;

                if (values.Length > 3)
                {
                    width = float.Parse(values[2], CultureInfo.InvariantCulture);
                    height = float.Parse(values[3], CultureInfo.InvariantCulture);
                }

                item.Bounds = new RectangleF(x, y, width, height);
            }

            if (blockingChars != null)
            {
                item.BlockingChars = bool.Parse(blockingChars);
            }

            if (alignment != null)
            {
                item.Alignment = Enum.Parse<CheckAlignment>(alignment, ignoreCase: true);
            }

            result.Items.Add(item);
        }

        return result;
    }
}
