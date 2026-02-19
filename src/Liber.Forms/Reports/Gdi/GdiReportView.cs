// GdiReportView.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Drawing.Printing;
using System.IO;
using Microsoft.Web.WebView2.Core;

namespace Liber.Forms.Reports.Gdi;

internal sealed class GdiReportView : IReportView
{
    private static readonly string s_path = Path.GetFullPath(Path.ChangeExtension("document", "pdf"));

    private readonly GdiReport _report;

    public string Title
    {
        get
        {
            return _report.Title;
        }
    }

    public object Properties
    {
        get
        {
            return _report;
        }
    }

    public GdiReportView(GdiReport report)
    {
        _report = report;
    }

    public void InitializeReport()
    {
        PrintDocument document = new PrintDocument()
        {
            PrinterSettings = new PrinterSettings()
            {
                PrinterName = "Microsoft Print to PDF",
                PrintToFile = true,
                PrintFileName = s_path
            },
            DocumentName = Title
        };

        document.QueryPageSettings += (sender, e) =>
        {
            e.PageSettings = _report.PageSettings;
        };

        document.PrintPage += (sender, e) =>
        {
            if (e.Graphics == null)
            {
                return;
            }

            _report.Draw(e.Graphics);
        };

        document.Print();
    }

    public void Navigate(CoreWebView2 coreWebView2)
    {
        coreWebView2.Navigate(s_path);
    }
}
