// ListViewItemComparer.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace System.Windows.Forms;

internal sealed class ListViewItemComparer : Comparer<ListViewItem>
{
    private readonly ListViewEx _listView;

    public ListViewItemComparer(ListViewEx listView)
    {
        _listView = listView;
    }

    public override int Compare(ListViewItem? x, ListViewItem? y)
    {
        object? xTag = x?.SubItems[_listView.SortColumn].Tag;
        object? yTag = y?.SubItems[_listView.SortColumn].Tag;
        int comparison;

        if (xTag is DateTime xDateTime && yTag is DateTime yDateTime)
        {
            comparison = xDateTime.CompareTo(yDateTime);
        }
        else if (xTag is decimal xDecimal && yTag is decimal yDecimal)
        {
            comparison = xDecimal.CompareTo(yDecimal);
        }
        else
        {
            comparison = string.Compare(
                x?.SubItems[_listView.SortColumn].Text,
                y?.SubItems[_listView.SortColumn].Text);
        }

        switch (_listView.SortOrder)
        {
            case SortOrder.Ascending:
                return comparison;

            case SortOrder.Descending:
                return -comparison;

            default:
                return 0;
        }
    }
}
