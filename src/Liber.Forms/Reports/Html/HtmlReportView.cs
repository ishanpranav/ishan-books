// HtmlReportView.cs
// Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Web.WebView2.Core;

namespace Liber.Forms.Reports.Html;

internal sealed class HtmlReportView : IReportView
{
    private readonly string _path;
    private readonly HtmlReport _report;

    public HtmlReportView(Company company, string path)
    {
        _path = path;
        _report = new HtmlReport(Path.GetFileNameWithoutExtension(path), company);
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

    public void InitializeReport()
    {

    }

    public void Navigate(CoreWebView2 coreWebView2)
    {
        try
        {
            coreWebView2.RemoveHostObjectFromScript("report");
        }
        catch (COMException) { }

        coreWebView2.AddHostObjectToScript("report", _report);
        coreWebView2.Navigate(Path.GetFullPath(_path));
    }
}
