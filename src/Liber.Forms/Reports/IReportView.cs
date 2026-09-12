// IReportView.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.Forms.Reports;

internal interface IReportView
{
    string GenericTitle { get; }
    string ImageKey { get; }
    int SortOrder { get; }
    IReport Properties { get; }

    void Generate();
    void Navigate(ReportWebView webView);
    IReportView Clone();
}
