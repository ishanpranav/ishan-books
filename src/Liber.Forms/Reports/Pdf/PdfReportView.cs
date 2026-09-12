// PdfReportView.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Text.Json.Serialization;
using Liber.Forms.Editors;
using Liber.Forms.Properties;

namespace Liber.Forms.Reports.Pdf;

internal class PdfReportView :
    ICloneable,
    IReport,
    IReportView
{
    private string? _title;

    [Browsable(false)]
    [JsonIgnore]
    public string GenericTitle
    {
        get
        {
            return System.IO.Path.GetFileNameWithoutExtension(Path) ?? Resources.PdfGenericTitle;
        }
    }

    [Browsable(false)]
    [JsonIgnore]
    public string ImageKey
    {
        get
        {
            return "ImageMapFile";
        }
    }

    [Browsable(false)]
    [JsonIgnore]
    public int SortOrder
    {
        get
        {
            return 4;
        }
    }

    IReport IReportView.Properties
    {
        get
        {
            return this;
        }
    }

    [LocalizedCategory(nameof(Title))]
    [LocalizedDescription(nameof(Title))]
    [LocalizedDisplayName(nameof(Title))]
    public string Title
    {
        get
        {
            return _title ?? GenericTitle;
        }
        set
        { 
            _title = value;
        }
    }

    [LocalizedCategory(nameof(Path))]
    [LocalizedDescription(nameof(Path))]
    [LocalizedDisplayName(nameof(Path))]
    [Editor(typeof(OpenFileDialogEditor), typeof(UITypeEditor))]
    public string? Path { get; set; } 

    void IReportView.Generate() { }

    public void Navigate(ReportWebView webView)
    {
        if (File.Exists(Path))
        {
            webView.CoreWebView2.Navigate(Path);
        }
    }

    public PdfReportView Clone()
    {
        PdfReportView result = new PdfReportView()
        {
            Path = Path,
            Title = Title
        };

        return result;
    }

    IReportView IReportView.Clone()
    {
        return Clone();
    }

    object ICloneable.Clone()
    {
        return Clone();
    }

    IReportView IReport.CreateView(Company company)
    {
        return Clone();
    }
}
