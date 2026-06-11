// ReportEngine.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Liber.Forms.Reports.Gdi;
using Liber.Forms.Reports.Html;
using Liber.Forms.Reports.Xsl;

namespace Liber.Forms.Reports;

internal sealed class ReportEngine
{
    public const string AccountMapReport = "account-map";
    public const string GeneralJournalReport = "general-journal";
    public const string CheckReport = "CPA 006 Middle ASAP";

    private readonly Company _company;
    private readonly Dictionary<string, IReportView> _views = new Dictionary<string, IReportView>();

    public IReadOnlyDictionary<string, IReportView> Views
    {
        get
        {
            return _views;
        }
    }

    public ReportEngine(Company company)
    {
        _company = company;

        XslReportView.InitializeReports(Path.Combine("data", Path.ChangeExtension("reports", "json")));
        InitializeReports(
            path: "styles",
            searchPattern: "*.xslt",
            x => new XslReportView(company, x));
        InitializeReports(
            path: "checks",
            searchPattern: "*.chk",
            CreateReportView);
        InitializeReports(
            path: "checks",
            searchPattern: "*.ini",
            CreateReportView);
        InitializeReports(
            path: "pages",
            searchPattern: "*.html",
            x => new HtmlReportView(company, x));
    }

    public bool TryGetReport<TReport>(string key, [NotNullWhen(true)] out TReport? result)
    {
        if (!Views.TryGetValue(key, out IReportView? view) ||
            view.Properties is not TReport report)
        {
            result = default;

            return false;
        }

        result = report;

        return true;
    }

    private IReportView CreateReportView(string path)
    {
        GdiCheckReport report = IniSerializer.DeserializeGdiCheckReport(_company, path);

        return new GdiReportView(report);
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
