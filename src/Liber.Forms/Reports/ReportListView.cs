// ReportListView.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Forms;
using Liber.Forms.Accounts;

namespace Liber.Forms.Reports;

internal class ReportListView : ListViewEx
{
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new ListViewItemCollection Items
    {
        get
        {
            return base.Items;
        }
    }

    public ReportListView()
    {
        FullRowSelect = true;
        LargeImageList = ReportImageListManager.ImageList;
        SmallImageList = ReportImageListManager.ImageList;

        Columns.Add(Liber.Properties.Resources.Name);
    }

    public void Initialize(ReportEngine engine)
    {
        BeginUpdate();

        try
        {
            foreach (KeyValuePair<string, IReportView> prototype in engine.Prototypes
                .OrderBy(x => x.Value.SortOrder)
                .ThenBy(x => x.Value.GenericTitle))
            {
                IReportView view = prototype.Value.Clone();

                view.Properties.Title = view.GenericTitle;

                InsertReport(Items.Count, engine, prototype.Key, view);
            }

            AutoResizeColumns();
            Sort();
        }
        finally
        {
            EndUpdate();
        }
    }

    public void Initialize(ReportEngine engine, IEnumerable<IReportView> views)
    {
        BeginUpdate();

        try
        {
            foreach (IReportView view in views)
            {
                InsertReport(Items.Count, engine, Guid.NewGuid().ToString(), view);
            }

            AutoResizeColumns();
        }
        finally
        {
            EndUpdate();
        }
    }

    public void InsertReport(int index, ReportEngine engine, string key, IReportView view)
    {
        ListViewItem item = Items.Insert(index, key, view.Properties.Title, ReportImageListManager.GetImageIndex(view));

        item.Tag = view;

        if (view.Properties is IntervalView report)
        {
            report.Accounts = new AccountsView(engine.Company);
        }
    }

    public bool TryGetSelection([NotNullWhen(true)] out IReportView? value)
    {
        if (SelectedItems.Count == 0)
        {
            value = null;

            return false;
        }

        value = (IReportView)SelectedItems[0].Tag!;

        return true;
    }
}
