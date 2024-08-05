// CheckItem.cs
// Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Drawing;

namespace Liber.Forms.Reports.Gdi;

internal sealed class CheckItem
{
    public CheckItem(string type)
    {
        Type = type;
    }

    public string Type { get; }
    public bool BlockingChars { get; set; }
    public RectangleF Bounds { get; set; }
    public CheckAlignment Alignment { get; set; }
}
