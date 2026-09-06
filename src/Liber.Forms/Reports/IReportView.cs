// IReportView.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Web.WebView2.Core;

namespace Liber.Forms.Reports;

internal interface IReportView
{
    string GenericTitle { get; }
    string Title { get; }
    string ImageKey { get; }
    int SortOrder { get; }
    object Properties { get; }

    void Generate();
    void Navigate(CoreWebView2 coreWebView2);
    IReportView Clone();
}
