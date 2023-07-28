// DrawableCheck.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Globalization;
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

    private readonly List<CheckItem> _items = new List<CheckItem>();

    private SKTypeface? _typeface;
    private SKFont? _font;
    private SKPaint? _paint;

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
        KeyDataCollection items = data["Check Items"];
        
        if (liber.ContainsKey("Translation"))
        {
            string[] translation = top["Translation"].Split(';', Options);

            X = float.Parse(translation[0], CultureInfo.InvariantCulture);
            Y = float.Parse(translation[1], CultureInfo.InvariantCulture);
        }

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

        if (top.ContainsKey("Rotation"))
        {
            RotationDegrees = float.Parse(top["Rotation"], CultureInfo.InvariantCulture);
        }

        if (top.ContainsKey("Font"))
        {
            string[] font = top["Font"].Split(' ', Options);

            _typeface = SKTypeface.FromFamilyName(font[0]);
            _font = new SKFont(_typeface, float.Parse(font[1], CultureInfo.InvariantCulture) * Points);
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
    public override float RotationDegrees { get; }

    private static void Deconstruct(string key, out float first, out float second)
    {
        string[] values = key.Split(';', Options);

        first = float.Parse(values[0], CultureInfo.InvariantCulture);
        second = float.Parse(values[1], CultureInfo.InvariantCulture);
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
        //canvas.RotateDegrees(RotationDegrees);

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
            if (_typeface != null)
            {
                _typeface.Dispose();
                _typeface = null;
            }

            if (_font != null)
            {
                _font.Dispose();
                _font = null;
            }

            if (_paint != null)
            {
                _paint.Dispose();
                _paint = null;
            }
        }

        base.Dispose(disposing);
    }
}
