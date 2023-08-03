// GdiCheckReport.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using Liber.Forms.Lines;

namespace Liber.Forms.Reports.Gdi;

internal sealed class GdiCheckReport : GdiReport, IDisposable
{
    private Font? _font = new Font("Courier New", 10 * Points, FontStyle.Regular, GraphicsUnit.Point);
    private SolidBrush? _brush = new SolidBrush(Color.Black);

    public GdiCheckReport(Company company, string title)
    {
        Check = new CheckView(company);
        Title = title;
    }

    public override string Title { get; }

    [LocalizedCategory(nameof(Font))]
    [LocalizedDescription(nameof(Font))]
    [LocalizedDisplayName(nameof(Font))]
    public Font? Font
    {
        get
        {
            return _font;
        }
        set
        {
            FreeFont();

            if (value == null)
            {
                return;
            }

            _font = value;
        }
    }

    [LocalizedCategory(nameof(Color))]
    [LocalizedDescription(nameof(Color))]
    [LocalizedDisplayName(nameof(Color))]
    public Color Color
    {
        get
        {
            if (_brush == null)
            {
                return Color.Black;
            }

            return _brush.Color;
        }
        set
        {
            FreeBrush();

            _brush = new SolidBrush(value);
        }
    }

    [LocalizedCategory(nameof(Check))]
    [LocalizedDescription(nameof(Check))]
    [LocalizedDisplayName(nameof(Check))]
    public CheckView Check { get; set; }

    [Browsable(false)]
    public ICollection<CheckItem> Items { get; } = new List<CheckItem>();

    private void DrawCheckItem(Graphics graphics, CheckItem value, string text)
    {
        if (_font == null || _brush == null)
        {
            return;
        }

        if (value.BlockingChars)
        {
            text = $"***{text}***";
        }

        RectangleF rectangle = value.Bounds;

        if (rectangle.Width == 0 && rectangle.Height == 0)
        {
            graphics.DrawString(text, _font, _brush, rectangle.Location);
        }
        else
        {
            SizeF size;

            switch (value.Alignment)
            {
                case CheckAlignment.Right:
                    size = graphics.MeasureString(text, _font);
                    rectangle = new RectangleF(rectangle.X + rectangle.Width - size.Width, rectangle.Y, rectangle.Width, rectangle.Height);
                    break;

                case CheckAlignment.Center:
                    size = graphics.MeasureString(text, _font);
                    rectangle = new RectangleF(rectangle.X + rectangle.Width - (size.Width / 2), rectangle.Y, rectangle.Width, rectangle.Height);
                    break;
            }

            graphics.DrawString(text, _font, _brush, rectangle);
        }
    }

    public override void Draw(Graphics graphics)
    {
        Line? line = Check.Value;

        if (line == null)
        {
            return;
        }

        Transaction? transaction = line.Transaction;

        if (transaction == null)
        {
            return;
        }

        foreach (CheckItem item in Items)
        {
            switch (item.Type.ToUpperInvariant())
            {
                case "PAYEE":
                    if (transaction.Name != null)
                    {
                        DrawCheckItem(graphics, item, transaction.Name);
                    }
                    break;

                case "DATE":
                    DrawCheckItem(graphics, item, transaction.Posted.ToShortDateString());
                    break;

                case "AMOUNT_WORDS":
                    decimal amount = Math.Abs(line.Balance);

                    if (amount > int.MaxValue)
                    {
                        DrawCheckItem(graphics, item, amount.ToString("n2"));

                        break;
                    }

                    DrawCheckItem(graphics, item, FormattedStrings.GetCheckWords(amount));
                    break;

                case "AMOUNT_NUMBER":
                    DrawCheckItem(graphics, item, Math.Abs(line.Balance).ToString("n2"));
                    break;
            }
        }

        graphics.TranslateTransform(X, Y);
    }

    private void FreeFont()
    {
        if (_font != null)
        {
            _font.Dispose();
            _font = null;
        }
    }

    private void FreeBrush()
    {
        if (_brush != null)
        {
            _brush.Dispose();
            _brush = null;
        }
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            FreeFont();
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
