// ReportEngine.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Liber.Forms.RecentPaths;
using Liber.Forms.Reports.Binder;
using Liber.Forms.Reports.Gdi;
using Liber.Forms.Reports.Html;
using Liber.Forms.Reports.Xsl;

namespace Liber.Forms.Reports;

internal sealed class ReportEngine
{
    public const string AccountMapReport = "account-map";
    public const string GeneralJournalReport = "general-journal";
    public const string CheckReport = "CPA 006 Middle ASAP";
    private readonly Dictionary<string, IReportView> _prototypes = new Dictionary<string, IReportView>();

    public static readonly string DocumentPath = Path.GetFullPath(Path.ChangeExtension("document", "pdf"));

    public ReportEngine(Company company)
    {
        Company = company;

        XslReportView.InitializeReports(Path.Combine("data", Path.ChangeExtension("reports", "json")));
        _prototypes.Add("Binder", new BinderReportView(this, new List<IReportView>()));
        InitializeReports(
            path: "checks",
            searchPattern: "*.chk",
            CreateGdiReportView);
        InitializeReports(
            path: "checks",
            searchPattern: "*.ini",
            CreateGdiReportView);
        InitializeReports(
            path: "pages",
            searchPattern: "*.html",
            x => new HtmlReportView(company, x));
        //_prototypes.Add("Pdf", new PdfReportView());
        InitializeReports(
            path: "styles",
            searchPattern: "*.xslt",
            x => new XslReportView(company, x));
    }

    public Company Company { get; }

    public IReadOnlyDictionary<string, IReportView> Prototypes
    {
        get
        {
            return _prototypes;
        }
    }

    public bool Find<TReport>(
        string key,
        [NotNullWhen(true)] out IReportView? view,
        [NotNullWhen(true)] out TReport? report)
    {
        if (!_prototypes.TryGetValue(key, out view) ||
            view.Properties is not TReport)
        {
            view = null;
            report = default;

            return false;
        }

        view = view.Clone();
        report = (TReport)view.Properties;

        return true;
    }

    private GdiReportView CreateGdiReportView(string path)
    {
        return new GdiReportView(Company, path);
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
                _prototypes.Add(Path.GetFileNameWithoutExtension(file), viewFactory(file));
            }
            catch { }
        }
    }
}
