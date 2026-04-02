// ColorExtensions.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using Liber.Forms;

namespace System.Drawing;

internal static class ColorExtensions
{
    public static Color GetForeColor(this Color source)
    {
        if (source.IsEmpty)
        {
            return Colors.Dark;
        }

        double luma = ((0.2126 * source.R) + (0.7152 * source.G) + (0.0722 * source.B)) / 255;

        if (luma < 0.5)
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

        return Color.FromArgb((int)r, (int)g, (int)b);
    }

    public static Color Tint(this Color source, double weight)
    {
        return source.Mix(Color.White, weight);
    }

    public static Color Shade(this Color source, double weight)
    {
        return source.Mix(Color.Black, weight);
    }
}
