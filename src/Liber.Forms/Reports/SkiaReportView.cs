// SkiaReportView.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;
using Liber.Skia;
using Microsoft.Web.WebView2.Core;

namespace Liber.Forms.Reports;

internal abstract class SkiaReportView : IDisposable, IReportView
{
    private DrawableReport? _report;

    public SkiaReportView(DrawableReport report)
    {
        _report = report;
    }

    public object Properties
    {
        get
        {
            if (_report == null)
            {
                throw new InvalidOperationException();
            }

            return _report;
        }
    }

    public void InitializeReport(CoreWebView2 coreWebView2)
    {
        if (_report == null)
        {
            throw new InvalidOperationException();
        }

        using (FileStream output = File.Create("document.pdf"))
        {
            SkiaSerializer.SerializePdf(output, _report);
        }

        coreWebView2.Navigate(Path.GetFullPath("document.pdf"));
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (_report != null)
            {
                _report.Dispose();
                _report = null;
            }
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
