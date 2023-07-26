// SkiaReportView.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.IO;
using System.Threading.Tasks;
using Liber.Skia;
using Microsoft.Web.WebView2.Core;
using SkiaSharp;

namespace Liber.Forms.Reports;

internal sealed class SkiaReportView : IReportView
{
    private readonly SKDrawable _drawable;

    public SkiaReportView(SKDrawable drawable)
    {
        _drawable = drawable;
    }

    public void InitializeReport(CoreWebView2 coreWebView2, Report report)
    {
        using FileStream output = File.Create("document.pdf");

        SkiaSerializer.SerializePdf(output, _drawable);
        coreWebView2.Navigate("document.pdf");
    }

    public Task PrintAsync(string path)
    {
        using FileStream output = File.Create(path);

        string extension = Path.GetExtension(path);

        switch (extension.ToUpperInvariant())
        {
            case ".PDF":
                SkiaSerializer.SerializePdf(output, _drawable);
                break;

            case ".XPS":
                SkiaSerializer.SerializeXps(output, _drawable);
                break;

            case ".PNG":
                SkiaSerializer.SerializeImage(output, _drawable, SKEncodedImageFormat.Png);
                break;

            default:
                FormattedStrings.ShowNotSupportedMessage(extension);
                break;
        }

        return Task.CompletedTask;
    }
}
