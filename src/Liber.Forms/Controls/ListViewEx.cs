// ListViewEx.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.ComponentModel;

namespace System.Windows.Forms;

internal class ListViewEx : ListView
{
    public ListViewEx()
    {
        AllowColumnReorder = true;
        GridLines = true;
        ListViewItemSorter = new ListViewItemComparer(this);
    }

    [DefaultValue(SortOrder.None)]
    public SortOrder SortOrder { get; set; }

    [DefaultValue(-1)]
    public int SortColumn { get; set; } = -1;

    protected override void OnColumnClick(ColumnClickEventArgs e)
    {
        if (e.Column == SortColumn)
        {
            if (SortOrder == SortOrder.Ascending)
            {
                SortOrder = SortOrder.Descending;
            }
            else
            {
                SortOrder = SortOrder.Ascending;
            }
        }
        else
        {
            SortColumn = e.Column;
            SortOrder = SortOrder.Ascending;
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

    public void AutoResizeColumns()
    {
        AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
    }
}
