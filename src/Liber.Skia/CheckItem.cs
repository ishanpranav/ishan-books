// CheckItem.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using SkiaSharp;

namespace Liber.Skia;

internal sealed class CheckItem
{
    public CheckItem(string type)
    {
        Type = type;
    }

    public string Type { get; }
    public bool BlockingChars { get; set; }
    public SKPoint Location { get; set; }
    public SKTextAlign TextAlign { get; set; }
}
