// BinderReportView.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;
using Liber.Forms.Properties;

namespace Liber.Forms.Reports.Binder;

internal sealed class BinderReportView :
    ICloneable,
    IReport,
    IReportView
{
    public BinderReportView(ReportEngine engine, IReadOnlyCollection<IReportView> views)
    {
        Binder = new Binder(engine, views);
        Title = GenericTitle;
    }

    [Browsable(false)]
    [JsonIgnore]
    public string GenericTitle
    {
        get
        {
            return Resources.BinderGenericTitle;
        }
    }

    [Browsable(false)]
    [JsonIgnore]
    public string ImageKey
    {
        get
        {
            return "ReportDesign";
        }
    }

    [Browsable(false)]
    [JsonIgnore]
    public int SortOrder
    {
        get
        {
            return 1;
        }
    }

    [LocalizedCategory(nameof(Binder))]
    [LocalizedDescription(nameof(Binder))]
    [LocalizedDisplayName(nameof(Binder))]
    public Binder Binder { get; set; }

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
    public string Title { get; set; }

    void IReportView.Generate() { }

    public void Navigate(ReportWebView webView)
    {
        Queue<IReportView> queue = new Queue<IReportView>(Binder.Views);

        webView.PrintQueue(ReportEngine.DocumentPath, queue, progressCallback: null, () =>
        {
            webView.CoreWebView2.Navigate(ReportEngine.DocumentPath);
        });
    }

    public BinderReportView Clone()
    {
        BinderReportView result = new BinderReportView(Binder.Engine, Binder.Views)
        {
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
