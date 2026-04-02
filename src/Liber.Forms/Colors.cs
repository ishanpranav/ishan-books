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
}
