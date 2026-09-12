// GdiReportView.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Drawing.Printing;

namespace Liber.Forms.Reports.Gdi;

internal sealed class GdiReportView : ICloneable, IReportView
{
    private readonly Company _company;
    private readonly string _path;
    private readonly GdiReport _report;

    public GdiReportView(Company company, string path)
    {
        _report = IniSerializer.DeserializeGdiCheckReport(company, path);
        _company = company;
        _path = path;
    }

    public string GenericTitle
    {
        get
        {
            return _report.Title;
        }
    }

    public string ImageKey
    {
        get
        {
            return _report.ImageKey;
        }
    }

    public int SortOrder
    {
        get
        {
            return 5;
        }
    }

    public IReport Properties
    {
        get
        {
            return _report;
        }
    }

    public void Generate()
    {
        PrintDocument document = new PrintDocument()
        {
            PrinterSettings = new PrinterSettings()
            {
                PrinterName = "Microsoft Print to PDF",
                PrintToFile = true,
                PrintFileName = ReportEngine.DocumentPath
            },
            DocumentName = _report.Title
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

    public void Navigate(ReportWebView webView)
    {
        webView.CoreWebView2.Navigate(ReportEngine.DocumentPath);
    }

    public GdiReportView Clone()
    {
        return new GdiReportView(_company, _path);
    }

    IReportView IReportView.Clone()
    {
        return Clone();
    }

    object ICloneable.Clone()
    {
        return Clone();
    }
}
