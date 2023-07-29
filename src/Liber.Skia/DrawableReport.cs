// DrawableReport.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using System.Drawing;
using SkiaSharp;

namespace Liber.Skia;

public abstract class DrawableReport : IDisposable
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

    [LocalizedCategory(nameof(RotationDegrees))]
    [LocalizedDescription(nameof(RotationDegrees))]
    [LocalizedDisplayName(nameof(RotationDegrees))]
    public float RotationDegrees { get; set; }

    [LocalizedCategory(nameof(SizeCentimeters))]
    [LocalizedDescription(nameof(SizeCentimeters))]
    [LocalizedDisplayName(nameof(SizeCentimeters))]
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

    [LocalizedCategory(nameof(SizeInches))]
    [LocalizedDescription(nameof(SizeInches))]
    [LocalizedDisplayName(nameof(SizeInches))]
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

    [LocalizedCategory(nameof(XCentimeters))]
    [LocalizedDescription(nameof(XCentimeters))]
    [LocalizedDisplayName(nameof(XCentimeters))]
    public float XCentimeters
    {
        get
        {
            return X / Centimeters;
        }
        set
        {
            X = value * Centimeters;
        }
    }

    [LocalizedCategory(nameof(YCentimeters))]
    [LocalizedDescription(nameof(YCentimeters))]
    [LocalizedDisplayName(nameof(YCentimeters))]
    public float YCentimeters
    {
        get
        {
            return Y / Centimeters;
        }
        set
        {
            Y = value * Centimeters;
        }
    }

    [LocalizedCategory(nameof(XInches))]
    [LocalizedDescription(nameof(XInches))]
    [LocalizedDisplayName(nameof(XInches))]
    public float XInches
    {
        get
        {
            return X / Inches;
        }
        set
        {
            X = value * Inches;
        }
    }

    [LocalizedCategory(nameof(YInches))]
    [LocalizedDescription(nameof(YInches))]
    [LocalizedDisplayName(nameof(YInches))]
    public float YInches
    {
        get
        {
            return Y / Inches;
        }
        set
        {
            Y = value * Inches;
        }
    }

    public abstract void Draw(SKCanvas canvas);

    protected virtual void Dispose(bool disposing) { }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
