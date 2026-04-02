// ListViewEx.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace System.Windows.Forms;

internal class ListViewEx : ListView
{
    public ListViewEx()
    {
        AllowColumnReorder = true;
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

    private static TextFormatFlags HorizontalAlignmentToTextFormatFlags(HorizontalAlignment value)
    {
        switch (value)
        {
            case HorizontalAlignment.Right: return TextFormatFlags.Right;
            case HorizontalAlignment.Center: return TextFormatFlags.HorizontalCenter;
        }

        return TextFormatFlags.Left;
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

    public bool TryGetSelection<T>([MaybeNullWhen(false)] out T value)
    {
        if (SelectedItems.Count == 0)
        {
            value = default;

            return false;
        }

        value = (T)SelectedItems[0].Tag!;

        return true;
    }
}
