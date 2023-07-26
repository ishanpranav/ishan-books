// SkiaSerializer.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using SkiaSharp;

namespace Liber.Skia;

public static class SkiaSerializer
{
    private static void Draw(SKCanvas canvas, SKDrawable drawable)
    {
        drawable.Draw(canvas, x: 0, y: 0);
    }

    private static void Serialize(SKDocument document, SKDrawable drawable)
    {
        SKCanvas canvas = document.BeginPage(width: 256, height: 256);

        Draw(canvas, drawable);
        document.EndPage();
    }

    public static void SerializePdf(Stream output, SKDrawable drawable)
    {
        using SKDocument document = SKDocument.CreatePdf(output);

        Serialize(document, drawable);
    }

    public static void SerializeXps(Stream output, SKDrawable drawable)
    {
        using SKDocument document = SKDocument.CreateXps(output);

        Serialize(document, drawable);
    }

    public static void SerializeImage(Stream output, SKDrawable drawable, SKEncodedImageFormat format)
    {
        SKBitmap bitmap = new SKBitmap();
        SKCanvas canvas = new SKCanvas(bitmap);

        Draw(canvas, drawable);
        bitmap.Encode(output, format, quality: 100);
    }
}
