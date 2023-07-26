// Colors.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Drawing;

namespace Liber.Forms;

internal static class Colors
{
    public static Color GetForeColor(Color backColor)
    {
        double luma = ((0.2126 * backColor.R) + (0.7152 * backColor.G) + (0.0722 * backColor.B)) / 255;

        if (luma < 0.5)
        {
            return Color.White;
        }

        return Color.Black;
    }
}
