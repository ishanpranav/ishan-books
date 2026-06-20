// RecentPath.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Liber.Forms.RecentPaths;

internal sealed class RecentPath : IComparable<RecentPath>, IComparable
{
    public string Path { get; }
    public DateTime LastModified { get; }

    public RecentPath(string path, DateTime lastModified)
    {
        Path = path;
        LastModified = lastModified;
    }

    public int CompareTo(object? obj)
    {
        if (obj == null)
        {
            return 1;
        }

        if (obj is not RecentPath other)
        {
            throw new ArgumentException(message: null, nameof(obj));
        }

        return CompareTo(other);
    }

    public int CompareTo(RecentPath? other)
    {
        if (other == null)
        {
            return 1;
        }

        return other.LastModified.CompareTo(LastModified);
    }
}
