// Colors.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Drawing;

namespace Liber.Forms;

internal static class Colors
{
    public static readonly Color White = Color.White;
    public static readonly Color Black = Color.Black;
    public static readonly Color Gray = ColorTranslator.FromHtml("#6c757d");
    public static readonly Color DarkGray = ColorTranslator.FromHtml("#343a40");
    public static readonly Color Primary = ColorTranslator.FromHtml("#32174d");
    public static readonly Color Light = ColorTranslator.FromHtml("#f8f9fa");
    public static readonly Color Dark = ColorTranslator.FromHtml("#212529");
    public static readonly Color ButtonHoverBackground = Primary.Shade(0.2);
    public static readonly Color ButtonActiveBackground = Primary.Shade(0.15);

    public static Color FromHsv(double h, double s, double v)
    {
        int hi = (int)double.Floor(h / 60) % 6;
        double f = h / 60 - double.Floor(h / 60);

        v *= 255;

        int value = (int)v;
        int p = (int)(v * (1 - s));
        int q = (int)(v * (1 - f * s));
        int t = (int)(v * (1 - (1 - f) * s));

        switch (hi)
        {
            case 0: return Color.FromArgb(value, t, p);
            case 1: return Color.FromArgb(q, value, p);
            case 2: return Color.FromArgb(p, value, t);
            case 3: return Color.FromArgb(p, q, value);
            case 4: return Color.FromArgb(t, p, value);
        }

        return Color.FromArgb(255, value, p, q);
    }
}
