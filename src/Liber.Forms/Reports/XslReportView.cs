// XslReportView.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml.Xsl;
using Microsoft.Web.WebView2.Core;

namespace Liber.Forms.Reports;

internal sealed class XslReportView : IReportView
{
    private static readonly Dictionary<string, XslCompiledTransform> s_styles = new Dictionary<string, XslCompiledTransform>();

    private static Dictionary<string, EquityModes>? s_reports;

    private readonly string _path;
    private readonly XslReport _report;

    private string? _xhtml;

    public XslReportView(Company company, string path)
    {
        _report = new XslReport(XslReport.GetString(Path.GetFileNameWithoutExtension(path)), company);
        _path = path;
    }

    public string Title
    {
        get
        {
            return XslReport.GetString(Path.GetFileNameWithoutExtension(_path));
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
        if (s_reports != null && s_reports.TryGetValue(Path.GetFileNameWithoutExtension(_path), out EquityModes value))
        {
            _report.EquityMode = value;
        }

        if (!s_styles.TryGetValue(_path, out XslCompiledTransform? style))
        {
            style = XmlReportSerializer.DeserializeTransform(_path);
            s_styles[_path] = style;
        }

        _xhtml = XmlReportSerializer.Serialize(style, _report);
    }

    public void Navigate(CoreWebView2 coreWebView2)
    {
        coreWebView2.NavigateToString(_xhtml);
    }

    public static void InitializeReports(string path)
    {
        if (s_reports != null)
        {
            return;
        }

        using FileStream input = File.OpenRead(path);

        s_reports = JsonSerializer.Deserialize<Dictionary<string, EquityModes>>(input, FormattedStrings.JsonOptions);
    }
}
