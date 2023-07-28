// IReportView.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Threading.Tasks;
using Microsoft.Web.WebView2.Core;

namespace Liber.Forms.Reports;

internal interface IReportView
{
    object Properties { get; }

    void InitializeReport(CoreWebView2 coreWebView);

    Task PrintAsync(string path);
}
