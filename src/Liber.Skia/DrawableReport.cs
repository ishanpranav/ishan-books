// DrawableReport.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.ComponentModel;
using System.Drawing;
using SkiaSharp;

namespace Liber.Skia;

public abstract class DrawableReport : SKDrawable
{
    public const float Centimeters = 28.346f; // GnuCash magic constant?
    public const float Inches = Centimeters * 2.54f;
    public const float Points = Centimeters * 0.0352778f;

    [Browsable(false)]
    public float X { get; set; }

    [Browsable(false)]
    public float Y { get; set; }

    [Browsable(false)]
    public float Width { get; set; }

    [Browsable(false)]
    public float Height { get; set; }

    public float RotationDegrees { get; set; }

    public SizeF SizeCentimeters
    {
        get
        {
            return new SizeF(Width / Centimeters, Height / Centimeters);
        }
        set
        {
            Width = value.Width * Centimeters;
            Height = value.Height * Centimeters;
        }
    }

    public SizeF SizeInches
    {
        get
        {
            return new SizeF(Width / Inches, Height / Inches);
        }
        set
        {
            Width = value.Width * Inches;
            Height = value.Height * Inches;
        }
    }

    public PointF LocationCentimeters
    {
        get
        {
            return new PointF(X / Centimeters, Y / Centimeters);
        }
        set
        {
            X = value.X * Centimeters;
            Y = value.Y * Centimeters;
        }
    }

    public PointF LocationInches
    {
        get
        {
            return new PointF(X / Inches, Y / Inches);
        }
        set
        {
            X = value.X * Inches;
            Y = value.Y * Inches;
        }
    }
}
