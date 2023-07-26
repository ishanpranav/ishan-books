// SkiaSerializer.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.IO;
using SkiaSharp;

namespace Liber.Skia;

public static class SkiaSerializer
{
    private static void Draw(SKCanvas canvas, DrawableReport report)
    {
        report.Draw(canvas, report.X, report.Y);
    }

    private static void Serialize(SKDocument document, DrawableReport report)
    {
        using SKCanvas canvas = document.BeginPage(report.Width, report.Height);

        Draw(canvas, report);
        document.EndPage();
    }

    public static void SerializePdf(Stream output, DrawableReport report)
    {
        using SKDocument document = SKDocument.CreatePdf(output);

        Serialize(document, report);
    }

    public static void SerializeXps(Stream output, DrawableReport report)
    {
        using SKDocument document = SKDocument.CreateXps(output);

        Serialize(document, report);
    }

    public static void SerializeImage(Stream output, DrawableReport report, SKEncodedImageFormat format)
    {
        using SKBitmap bitmap = new SKBitmap();
        using SKCanvas canvas = new SKCanvas(bitmap);
        
        Draw(canvas, report);
        bitmap.Encode(output, format, quality: 100);
    }
}
