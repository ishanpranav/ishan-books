// BinderForm.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Windows.Forms;

namespace Liber.Forms.Reports;

internal partial class BinderForm : Form
{
    private readonly ReportEngine _engine;

    public BinderForm(ReportEngine engine)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);
        _listView.Initialize(engine);

        _engine = engine;
    }

    private void OnAddToolStripMenuItemClick(object sender, EventArgs e)
    {
        foreach (ListViewItem selectedItem in _listView.SelectedItems)
        {
            _customReportListView.InsertReport(
                _customReportListView.Items.Count,
                _engine,
                Guid.NewGuid().ToString(),
                (IReportView)selectedItem.Tag!);
            _customReportListView.AutoResizeColumns();
        }
    }

    private void OnEditToolStripMenuItemClick(object sender, EventArgs e)
    {

    }

    private void OnRenameToolStripMenuItemClick(object sender, EventArgs e)
    {
        if (_customReportListView.SelectedItems.Count == 1)
        {
            _customReportListView.SelectedItems[0].BeginEdit();
        }
    }

    private void OnDuplicateToolStripMenuItemClick(object sender, EventArgs e)
    {
        foreach (ListViewItem item in _customReportListView.SelectedItems)
        {
            _customReportListView.InsertReport(
                item.Index + 1,
                _engine, Guid.NewGuid().ToString(),
                (IReportView)item.Tag!);
        }
    }

    private void OnRemoveToolStripMenuItemClick(object sender, EventArgs e)
    {
        foreach (ListViewItem item in _customReportListView.SelectedItems)
        {
            item.Remove();
        }
    }

    private void OnMoveUpToolStripMenuItemClick(object sender, EventArgs e)
    {
        ListViewItem[] selectedItems = new ListViewItem[_customReportListView.SelectedItems.Count];

        _customReportListView.SelectedItems.CopyTo(selectedItems, index: 0);
        Array.Sort(selectedItems, (left, right) => left.Index - right.Index);

        foreach (ListViewItem item in selectedItems)
        {
            int index = item.Index;

            if (index == 0)
            {
                continue;
            }

            _customReportListView.Items.RemoveAt(index);
            _customReportListView.Items.Insert(index - 1, item);
        }
    }

    private void OnMoveDownToolStripMenuItemClick(object sender, EventArgs e)
    {
        ListViewItem[] selectedItems = new ListViewItem[_customReportListView.SelectedItems.Count];

        _customReportListView.SelectedItems.CopyTo(selectedItems, index: 0);
        Array.Sort(selectedItems, (left, right) => right.Index - left.Index);

        int lastIndex = _customReportListView.Items.Count - 1;

        foreach (ListViewItem item in selectedItems)
        {
            int index = item.Index;

            if (index == lastIndex)
            {
                continue;
            }

            _customReportListView.Items.RemoveAt(index);
            _customReportListView.Items.Insert(index + 1, item);
        }
    }

    private void OnSelectAllToolStripMenuItemClick(object sender, EventArgs e)
    {
        _listView.SelectAll();
    }

    private void OnCustomReportSelectAllToolStripMenuItemClick(object sender, EventArgs e)
    {
        _customReportListView.SelectAll();
    }

    private void OnCustomReportListViewAfterLabelEdit(object sender, LabelEditEventArgs e)
    {
        if (e.Label != null)
        {
            IReportView view = (IReportView)_customReportListView.Items[e.Item].Tag!;

            view.Title = e.Label;
        }
    }
}
