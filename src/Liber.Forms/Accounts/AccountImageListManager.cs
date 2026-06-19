// AccountImageListManager.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Liber.Forms.Accounts;

internal static class AccountImageListManager
{
    private static readonly Dictionary<Color, int> _imageIndices = new Dictionary<Color, int>();

    public static ImageList ImageList { get; } = new ImageList()
    {
        ColorDepth = ColorDepth.Depth32Bit,
        TransparentColor = Color.Transparent
    };

    public static int GetImageIndex(Color color)
    {
        if (!_imageIndices.TryGetValue(color, out int imageIndex))
        {
            imageIndex = ImageList.Images.Count;
            _imageIndices[color] = imageIndex;

            ImageList.Images.Add(CreateColorImage(color));
        }

        return imageIndex;
    }

    private static Bitmap CreateColorImage(Color color)
    {
        Bitmap result = new Bitmap(16, 16);

        using Graphics graphics = Graphics.FromImage(result);

        graphics.Clear(Color.Transparent);

        using SolidBrush solidBrush = new SolidBrush(color);
        using Pen pen = new Pen(Colors.Gray);

        graphics.FillRectangle(solidBrush, x: 3, y: 3, width: 10, height: 10);
        graphics.DrawRectangle(pen, x: 3, y: 3, width: 9, height: 9);

        return result;
    }
}
