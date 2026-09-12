// IReport.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using Liber.Forms.Reports.Binder;
using Liber.Forms.Reports.Gdi;
using Liber.Forms.Reports.Html;
using Liber.Forms.Reports.Pdf;
using Liber.Forms.Reports.Xsl;

namespace Liber.Forms.Reports;

[JsonDerivedType(typeof(BinderReportView), "binder")]
[JsonDerivedType(typeof(GdiCheckReport), "gdi-check")]
[JsonDerivedType(typeof(HtmlReport), "html")]
[JsonDerivedType(typeof(PdfReportView), "pdf")]
[JsonDerivedType(typeof(XslReport), "xsl")]
internal interface IReport
{
    string Title { get; set; }

    IReportView CreateView(Company company);
}
