// IReportView.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Web.WebView2.Core;

namespace Liber.Forms.Reports;

internal interface IReportView
{
    string Title { get; }
    object Properties { get; }

    void InitializeReport();
    void Navigate(CoreWebView2 coreWebView2);
}
