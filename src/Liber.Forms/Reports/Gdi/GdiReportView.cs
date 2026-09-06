// GdiReportView.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Drawing.Printing;
using System.IO;
using Microsoft.Web.WebView2.Core;

namespace Liber.Forms.Reports.Gdi;

internal sealed class GdiReportView : ICloneable, IReportView
{
    private static readonly string s_path = Path.GetFullPath(Path.ChangeExtension("document", "pdf"));

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

    public int SortOrder
    {
        get
        {
            return 3;
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
