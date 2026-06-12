// ColorExtensions.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using Liber.Forms;

namespace System.Drawing;

internal static class ColorExtensions
{
    public static double GetLuma(this Color value)
    {
        return ((0.2126 * value.R) + (0.7152 * value.G) + (0.0722 * value.B)) / 255;
    }

    public static Color GetForeColor(this Color value)
    {
        if (value.IsEmpty)
        {
            return Colors.Dark;
        }

        if (value.GetLuma() < 0.5)
        {
            return Colors.Light;
        }

        return Colors.Dark;
    }

    public static Color Mix(this Color source, Color other, double weight)
    {
        double r = source.R * weight + other.R * (1 - weight);
        double g = source.G * weight + other.G * (1 - weight);
        double b = source.B * weight + other.B * (1 - weight);

        return Color.FromArgb(
            (int)double.Round(r),
            (int)double.Round(g),
            (int)double.Round(b));
    }

    public static Color Tint(this Color source, double weight)
    {
        return source.Mix(Color.White, weight);
    }

    public static Color Shade(this Color source, double weight)
    {
        return source.Mix(Color.Black, weight);
    }

    public static Color Soften(this Color value, double targetLuma = 0.5)
    {
        if (value.IsEmpty)
        {
            return value;
        }

        double luma = value.GetLuma();

        if (luma >= targetLuma)
        {
            return value;
        }

        double weight = (targetLuma - luma) / (1 - luma);

        weight = double.Clamp(weight, min: 0, max: 1);

        return value.Tint(weight);
    }
}
