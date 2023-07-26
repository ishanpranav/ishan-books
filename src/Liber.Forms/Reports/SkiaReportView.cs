// SkiaReportView.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;
using System.Threading.Tasks;
using Liber.Skia;
using Microsoft.Web.WebView2.Core;
using SkiaSharp;

namespace Liber.Forms.Reports;

internal sealed class SkiaReportView : IDisposable, IReportView
{
    private readonly DrawableReport _report;

    public SkiaReportView(DrawableReport report)
    {
        _report = report;
    }

    public void InitializeReport(CoreWebView2 coreWebView2, Report report)
    {
        using (FileStream output = File.Create("document.pdf"))
        {
            SkiaSerializer.SerializePdf(output, _report);
        }

        coreWebView2.Navigate(Path.GetFullPath("document.pdf"));
    }

    public Task PrintAsync(string path)
    {
        using FileStream output = File.Create(path);

        string extension = Path.GetExtension(path);

        switch (extension.ToUpperInvariant())
        {
            case ".PDF":
                SkiaSerializer.SerializePdf(output, _report);
                break;

            case ".XPS":
                SkiaSerializer.SerializeXps(output, _report);
                break;

            case ".PNG":
                SkiaSerializer.SerializeImage(output, _report, SKEncodedImageFormat.Png);
                break;

            default:
                FormattedStrings.ShowNotSupportedMessage(extension);
                break;
        }

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _report.Dispose();
    }
}
