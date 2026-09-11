// ReportImageListManager.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Liber.Forms.Reports;

internal static class ReportImageListManager
{
    private static readonly Dictionary<string, int> s_imageIndices = new Dictionary<string, int>();

    public static ImageList ImageList { get; } = new ImageList()
    {
        ColorDepth = ColorDepth.Depth32Bit,
        TransparentColor = Color.Transparent
    };

    public static int GetImageIndex(IReportView view)
    {
        string key = view.ImageKey;

        if (!s_imageIndices.TryGetValue(key, out int imageIndex))
        {
            imageIndex = ImageList.Images.Count;
            s_imageIndices[key] = imageIndex;

            Bitmap image = (Bitmap)VisualStudioImageLibrary.ResourceManager.GetObject(key)!;

            ImageList.Images.Add(image);
        }

        return imageIndex;
    }
}
