using System.Collections.Generic;

namespace System.Windows.Forms;

internal class ListViewItemComparer : Comparer<ListViewItem>
{
    private readonly ListViewEx _listView;

    public ListViewItemComparer(ListViewEx listView)
    {
        _listView = listView;
    }

    public override int Compare(ListViewItem? x, ListViewItem? y)
    {
        int comparison = string.Compare(
             x?.SubItems[_listView.SortColumn].Text,
             y?.SubItems[_listView.SortColumn].Text);

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
