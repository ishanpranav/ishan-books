// PdfReportView.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Liber.Forms.Reports.Pdf;

internal class PdfReportView : IReportView
{
    public string GenericTitle
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public string ImageKey
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public int SortOrder
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public IReport Properties
    {
        get
        {
            throw new NotImplementedException();
        }
    }

    public IReportView Clone()
    {
        throw new NotImplementedException();
    }

    public void Generate()
    {
        throw new NotImplementedException();
    }

    public void Navigate(ReportWebView webView)
    {
        throw new NotImplementedException();
    }
}
