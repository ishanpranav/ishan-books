// ListViewEx.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.ComponentModel;

namespace System.Windows.Forms;

internal class ListViewEx : ListView
{
    private readonly HashSet<int> _hiddenColumnIndices = new HashSet<int>();

    public ListViewEx()
    {
        AllowColumnReorder = true;
        GridLines = true;
        ListViewItemSorter = new ListViewItemComparer(this);
    }

    [DefaultValue(-1)]
    public int SortColumn { get; set; } = -1;

    public IReadOnlySet<int> HiddenColumnIndices { get { return _hiddenColumnIndices; } }

    protected override void OnColumnClick(ColumnClickEventArgs e)
    {
        if (e.Column == SortColumn)
        {
            if (Sorting == SortOrder.Ascending)
            {
                Sorting = SortOrder.Descending;
            }
            else
            {
                Sorting = SortOrder.Ascending;
            }
        }
        else
        {
            SortColumn = e.Column;
            Sorting = SortOrder.Ascending;
        }

        Sort();

        base.OnColumnClick(e);
    }

    protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
    {
        e.DrawDefault = true;
        base.OnDrawColumnHeader(e);
    }

    protected override void OnGotFocus(EventArgs e)
    {
        Invalidate();
        base.OnGotFocus(e);
    }

    protected override void OnLostFocus(EventArgs e)
    {
        Invalidate();
        base.OnLostFocus(e);
    }

    public void ShowColumn(int index)
    {
        if (_hiddenColumnIndices.Remove(index))
        {
            AutoResizeColumn(index, ColumnHeaderAutoResizeStyle.ColumnContent);
            AutoResizeColumn(index, ColumnHeaderAutoResizeStyle.HeaderSize);
        }
    }

    public void HideColumn(int index)
    {
        if (_hiddenColumnIndices.Add(index))
        {
            Columns[index].Width = 0;
        }
    }

    public void AutoResizeColumns()
    {
        if (_hiddenColumnIndices.Count == 0)
        {
            AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            return;
        }

        for (int i = 0; i < Columns.Count; i++)
        {
            if (!_hiddenColumnIndices.Contains(i))
            {
                AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.ColumnContent);
                AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }
    }

    public void SelectAll()
    {
        foreach (ListViewItem item in Items)
        {
            item.Selected = true;
        }
    }
}
