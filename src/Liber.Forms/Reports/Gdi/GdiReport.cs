// GdiReport.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.ComponentModel;
using System.Drawing;

namespace Liber.Forms.Reports.Gdi;

internal abstract class GdiReport
{
    /// <summary>
    /// Specifies the conversion factor used to convert dimensions provided in
    /// centimeters to GnuCash pixels.
    /// </summary>
    public const float Centimeters = 28.346f;

    public const float Inches = Centimeters * 2.54f;
    public const float Points = Inches / 72;

    [Browsable(false)]
    public abstract string Title { get; }

    [Browsable(false)]
    public float X { get; set; }

    [Browsable(false)]
    public float Y { get; set; }

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

    public abstract void Draw(Graphics graphics);
}
