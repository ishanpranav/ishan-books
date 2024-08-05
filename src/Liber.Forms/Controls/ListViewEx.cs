// ListViewEx.cs
// Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;

namespace System.Windows.Forms;

internal sealed class ListViewEx : ListView
{
    public ListViewEx()
    {
        ListViewItemSorter = new ListViewItemComparer(this);
        AllowColumnReorder = true;
    }

    public SortOrder SortOrder { get; set; }
    public int SortColumn { get; set; }

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

        value = (T)SelectedItems[0].Tag;

        return true;
    }
}
