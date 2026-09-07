// HtmlReportView.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Web.WebView2.Core;

namespace Liber.Forms.Reports.Html;

internal sealed class HtmlReportView : ICloneable, IReportView
{
    private readonly Company _company;
    private readonly string _path;
    private readonly HtmlReport _report;

    public HtmlReportView(Company company, string path)
    {
        _report = new HtmlReport(Path.GetFileNameWithoutExtension(path), company);
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
        set
        {
            _report.Title = value;
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
            return 2;
        }
    }

    public object Properties
    {
        get
        {
            return _report;
        }
    }

    public void Generate() { }

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

    public HtmlReportView Clone()
    {
        return new HtmlReportView(_company, _path);
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
