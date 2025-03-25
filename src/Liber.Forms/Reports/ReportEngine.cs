// ReportEngine.cs
// Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using Liber.Forms.Reports.Gdi;
using Liber.Forms.Reports.Html;
using Liber.Forms.Reports.Xsl;

namespace Liber.Forms.Reports;

internal sealed class ReportEngine
{
    private readonly Dictionary<string, IReportView> _views = new Dictionary<string, IReportView>();

    public ReportEngine(Company company)
    {
        XslReportView.InitializeReports(Path.Combine("data", Path.ChangeExtension("reports", "json")));

        InitializeReports(
            path: "styles",
            searchPattern: "*.xslt",
            x => new XslReportView(company, x));

        IReportView createReportView(string path)
        {
            GdiCheckReport report = IniSerializer.DeserializeGdiCheckReport(company, path);

            return new GdiReportView(report);
        }

        InitializeReports(
            path: "checks",
            searchPattern: "*.chk",
            createReportView);
        InitializeReports(
            path: "checks",
            searchPattern: "*.ini",
            createReportView);
        InitializeReports(
            path: "pages",
            searchPattern: "*.html",
            x => new HtmlReportView(company, x));
    }

    public IReadOnlyDictionary<string, IReportView> Views
    {
        get
        {
            return _views;
        }
    }

    private void InitializeReports(string path, string searchPattern, Func<string, IReportView> viewFactory)
    {
        string[] files = Directory.GetFiles(path, searchPattern);

        if (files.Length == 0)
        {
            return;
        }

        foreach (string file in files)
        {
            try
            {
                _views.Add(Path.GetFileNameWithoutExtension(file), viewFactory(file));
            }
            catch { }
        }
    }
}
