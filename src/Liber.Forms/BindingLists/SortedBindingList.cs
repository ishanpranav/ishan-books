// SortedBindingList.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace System.ComponentModel;

internal class SortedBindingList<T> : BindingList<T> where T : IComparable<T>
{
    protected SortedBindingList()
    {
        AllowNew = false;
        AllowEdit = false;
        AllowRemove = false;
    }

    protected int BinarySearch(T item)
    {
        IList<T> items = Items;
        int left = 0;
        int right = items.Count - 1;

        while (left <= right)
        {
            int index = left + ((right - left) / 2);
            int comparison = items[index].CompareTo(item);

            if (comparison == 0)
            {
                return index;
            }

            if (comparison < 0)
            {
                left = index + 1;
            }
            else
            {
                right = index - 1;
            }
        }

        return ~left;
    }

    protected int FindByKey<TKey>(TKey key, Func<T, TKey> keySelector) where TKey : IComparable<TKey>
    {
        IList<T> items = Items;
        int left = 0;
        int right = items.Count - 1;

        while (left <= right)
        {
            int mid = left + ((right - left) >> 1);
            int comparison = keySelector(items[mid]).CompareTo(key);

            if (comparison == 0)
            {
                return mid;
            }
            else if (comparison < 0)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }

        return -1;
    }

    protected int InsertSorted(T item)
    {
        int index = BinarySearch(item);

        if (index < 0)
        {
            index = ~index;
        }

        Items.Insert(index, item);
        OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, index));

        return index;
    }

    protected void RemoveSortedAt(int index)
    {
        Items.RemoveAt(index);
        OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
    }

    protected void NotifyItemChanged(int index)
    {
        ResetItem(index);
    }

    protected override void InsertItem(int index, T item)
    {
        throw new NotSupportedException();
    }

    protected override void RemoveItem(int index)
    {
        throw new NotSupportedException();
    }

    protected override void SetItem(int index, T item)
    {
        throw new NotSupportedException();
    }

    protected override void ClearItems()
    {
        throw new NotSupportedException();
    }
}
