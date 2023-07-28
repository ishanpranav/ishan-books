// DrawableReport.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using SkiaSharp;

namespace Liber.Skia;

public abstract class DrawableReport : SKDrawable
{
    public const float Centimeters = 28.346f; // GnuCash magic constant?
    public const float Inches = Centimeters * 2.54f;
    public const float Points = Centimeters * 0.0352778f;

    public abstract float X { get; }
    public abstract float Y { get; }
    public abstract float Width { get; }
    public abstract float Height { get; }
    public abstract float RotationDegrees { get; }
}
