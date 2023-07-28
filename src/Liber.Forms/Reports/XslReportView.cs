// XslReportView.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Xsl;
using Microsoft.Web.WebView2.Core;

namespace Liber.Forms.Reports;

internal sealed class XslReportView : IReportView
{
    private static readonly Dictionary<string, XslCompiledTransform> s_styles = new Dictionary<string, XslCompiledTransform>();

    private readonly string _path;
    private readonly XslReport _report;

    private string? _xhtml;
    private CoreWebView2? _coreWebView2;

    public XslReportView(Company company, string path)
    {
        _report = new XslReport(company);
        _path = path;
    }

    public object Properties
    {
        get
        {
            return _report;
        }
    }

    public void InitializeReport(CoreWebView2 coreWebView2)
    {
        if (!s_styles.TryGetValue(_path, out XslCompiledTransform? style))
        {
            style = XmlReportSerializer.DeserializeTransform(_path);
            s_styles[_path] = style;
        }

        _xhtml = XmlReportSerializer.Serialize(style, _report);
        _coreWebView2 = coreWebView2;

        coreWebView2.NavigateToString(_xhtml);
    }

    public async Task PrintAsync(string path)
    {
        if (_coreWebView2 == null)
        {
            return;
        }

        await using FileStream output = File.Create(path);

        string extension = Path.GetExtension(path);

        switch (extension.ToUpperInvariant())
        {
            case ".PDF":
                await _coreWebView2.PrintToPdfAsync(path);
                break;

            case ".HTM":
            case ".HTML":
                await File.WriteAllTextAsync(path, _xhtml);
                break;

            default:
                FormattedStrings.ShowNotSupportedMessage(extension);
                break;
        }
    }
}
