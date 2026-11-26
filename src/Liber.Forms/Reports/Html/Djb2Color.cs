// Djb2Color.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Drawing;

namespace Liber.Forms.Reports.Html;

internal static class Djb2Color
{
    // https://github.com/ishanpranav/codebook/blob/master/lib/hashes/djb2_hash.c
    private static ulong Djb2(string value, int offset, int length)
    {
        ulong result = 5381;

        for (int i = offset; i < offset + length; i++)
        {
            result = ((result << 5) + result) + value[i];
        }

        return result;
    }

    public static Color GetColorFromHash(Color baseColor, string key)
    {
        int offset = 0;
        int length = key.Length;

        while (offset < length && !char.IsLetter(key[offset]))
        {
            offset++;
            length--;
        }

        ulong hash = Djb2(key, offset, length) >> 16;
        bool tint = (hash & 0x1) == 0;

        hash >>= 1;

        double factor = 0.35 + (hash % 100) / 100d * 0.5;

        Color backgroundColor = tint
            ? baseColor.Tint(factor)
            : baseColor.Shade(factor);

        return backgroundColor;
    }

    public static Color GetColorOrDefault(string name, AccountType type, Color color, Color defaultColor, decimal debit)
    {
        if (color.IsEmpty || color == defaultColor)
        {
            return GetColorFromHash(type.GetDefaultColor(debit), name);
        }

        return color;
    }
}
