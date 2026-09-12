// HtmlReportView.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Liber.Forms.Reports.Html;

internal sealed class HtmlReportView : ICloneable, IReportView
{
    private readonly Company _company;
    private readonly string _path;
    private readonly HtmlReport _report;

    public HtmlReportView(Company company, string path)
    {
        _report = new HtmlReport(path, company);
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
            return 3;
        }
    }

    public IReport Properties
    {
        get
        {
            return _report;
        }
    }

    void IReportView.Generate() { }

    public void Navigate(ReportWebView webView)
    {
        try
        {
            webView.CoreWebView2.RemoveHostObjectFromScript("report");
        }
        catch (COMException) { }

        webView.CoreWebView2.AddHostObjectToScript("report", _report);
        webView.CoreWebView2.Navigate(Path.GetFullPath(_path));
    }

    public HtmlReportView Clone()
    {
        HtmlReportView result = new HtmlReportView(_company, _path);

        result._report.Title = _report.Title;
        result._report.Started = _report.Started;
        result._report.Posted = _report.Posted;
        result._report.Level = _report.Level;
        result._report.Filter = _report.Filter;

        return result;
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
