// EmptySet.cs
// Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Linq;

namespace System.Collections.Generic;

internal sealed class EmptySet<T> : IReadOnlySet<T>
{
    public int Count
    {
        get
        {
            return 0;
        }
    }

    public bool Contains(T item)
    {
        return false;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return Enumerable.Empty<T>().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public bool IsProperSubsetOf(IEnumerable<T> other)
    {
        return other.Any();
    }

    public bool IsProperSupersetOf(IEnumerable<T> other)
    {
        return false;
    }

    public bool IsSubsetOf(IEnumerable<T> other)
    {
        return true;
    }

    public bool IsSupersetOf(IEnumerable<T> other)
    {
        return !other.Any();
    }

    public bool Overlaps(IEnumerable<T> other)
    {
        return false;
    }

    public bool SetEquals(IEnumerable<T> other)
    {
        return !other.Any();
    }
}
