// DrawableCheck.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using Humanizer;
using IniParser;
using IniParser.Model;
using IniParser.Model.Configuration;
using IniParser.Parser;
using SkiaSharp;

namespace Liber.Skia;

public class DrawableCheck : DrawableReport
{
    private const StringSplitOptions Options =
        StringSplitOptions.TrimEntries |
        StringSplitOptions.RemoveEmptyEntries;

    private readonly float _rotationDegrees;
    private readonly bool _showGrid;
    private readonly bool _showBoxes;
    private readonly SKTypeface _typeface;
    private readonly SKFont _font;
    private readonly SKPaint _paint;
    private readonly CheckPositions _positions;
    private readonly List<CheckItem> _items = new List<CheckItem>();

    public DrawableCheck(string path)
    {
        IniDataParser baseParser = new IniDataParser(new IniParserConfiguration()
        {
            AllowDuplicateKeys = true,
            CaseInsensitive = true,
            CommentString = "#"
        });
        FileIniDataParser parser = new FileIniDataParser(baseParser);
        IniData data = parser.ReadFile(path);
        KeyDataCollection liber = data["Liber"];
        KeyDataCollection top = data["Top"];
        KeyDataCollection positions = data["Check Positions"];
        KeyDataCollection items = data["Check Items"];

        if (liber.ContainsKey("Translation"))
        {
            string[] translation = top["Translation"].Split(';', Options);

            X = float.Parse(translation[0]);
            Y = float.Parse(translation[1]);
        }

        _rotationDegrees = float.Parse(top["Rotation"] ?? "0");
        _showBoxes = bool.Parse(top["Show_Boxes"] ?? bool.FalseString);
        _showGrid = bool.Parse(top["Show_Grid"] ?? bool.FalseString);
        //_positions = Enum.Parse<CheckPositions>(string.Join(", ", positions["Names"].Split(';', options)));

        if (liber.ContainsKey("Size"))
        {
            Deconstruct(liber["Size"], out float width, out float height);

            Width = width;
            Height = height;
        }
        else
        {
            Width = 8.5f * Inches;
            Height = 11f * Inches;
        }

        if (top.ContainsKey("Font"))
        {
            string[] font = top["Font"].Split(' ', Options);

            _typeface = SKTypeface.FromFamilyName(font[0]);
            _font = new SKFont(_typeface, float.Parse(font[1]) * Points);
        }
        else
        {
            _typeface = SKTypeface.FromFamilyName("Courier New");
            _font = new SKFont(_typeface, 10 * Points);
        }

        _paint = new SKPaint(_font);

        for (int i = 1; true; i++)
        {
            if (!items.ContainsKey("Type_" + i))
            {
                break;
            }

            Deconstruct(items["Coords_" + i], out float x, out float y);

            CheckItem item = new CheckItem(items["Type_" + i])
            {
                Location = new SKPoint(x, y)
            };

            if (items.ContainsKey("Blocking_Chars_" + i))
            {
                item.BlockingChars = bool.Parse(items["Blocking_Chars_" + i]);
            }

            if (items.ContainsKey("Align_" + i))
            {
                item.TextAlign = Enum.Parse<SKTextAlign>(items["Align_" + i], ignoreCase: true);
            }

            _items.Add(item);
        }
    }

    public override float X { get; }
    public override float Y { get; }
    public override float Width { get; }
    public override float Height { get; }

    private static void Deconstruct(string key, out float first, out float second)
    {
        string[] values = key.Split(';', Options);

        first = float.Parse(values[0]);
        second = float.Parse(values[1]);
    }

    private void DrawCheckItem(SKCanvas canvas, CheckItem value, string text)
    {
        if (value.Location.IsEmpty)
        {
            return;
        }

        if (value.BlockingChars)
        {
            text = $"***{text}***";
        }

        canvas.DrawText(text, value.Location, _paint);
    }

    protected override void OnDraw(SKCanvas canvas)
    {
        //canvas.RotateDegrees(_rotationDegrees);

        decimal amount = 9999.99m;

        foreach (CheckItem item in _items)
        {
            switch (item.Type.ToUpperInvariant())
            {
                case "PAYEE":
                    DrawCheckItem(canvas, item, "Payee");
                    break;

                case "DATE":
                    DrawCheckItem(canvas, item, DateTime.Today.ToShortDateString());
                    break;

                case "AMOUNT_WORDS":
                    if (amount > int.MaxValue)
                    {
                        DrawCheckItem(canvas, item, amount.ToString("n2"));

                        break;
                    }

                    int integral = (int)amount;
                    int fractional = (int)((amount - integral) * 100);

                    DrawCheckItem(canvas, item, string.Format("{0} and {1}/XX", integral.ToWords(), fractional));
                    break;

                case "AMOUNT_NUMBER":
                    DrawCheckItem(canvas, item, amount.ToString("n2"));
                    break;
            }
        }

        base.OnDraw(canvas);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _typeface.Dispose();
            _font.Dispose();
            _paint.Dispose();
        }

        base.Dispose(disposing);
    }
}
