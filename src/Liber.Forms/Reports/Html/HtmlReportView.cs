// HtmlReportView.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;
using Microsoft.Web.WebView2.Core;

namespace Liber.Forms.Reports.Html;

internal sealed class HtmlReportView : IReportView
{
    private readonly string _path;

    public HtmlReportView(string path)
    {
        _path = path;
    }

    public string Title
    {
        get
        {
            return _path;
        }
    }

    public object Properties
    {
        get
        {
            return new { };
        }
    }

    public void InitializeReport()
    {

    }

    public void Navigate(CoreWebView2 coreWebView2)
    {
        coreWebView2.Navigate(Path.GetFullPath(_path));
    }
}
