using System;

namespace Liber;

internal sealed class RecentItem : IComparable, IComparable<RecentItem>, IEquatable<RecentItem>
{
    public RecentItem(string path)
    {
        Path = path;
    }

    public string Path { get; }
    public DateTime Opened { get; set; }

    int IComparable.CompareTo(object? obj)
    {
        if (obj == null)
        {
            return 1;
        }

        if (obj is not RecentItem other)
        {
            throw new ArgumentException(message: null, nameof(obj));
        }

        return CompareTo(other);
    }

    public int CompareTo(RecentItem? other)
    {
        if (other == null)
        {
            return 1;
        }
        
        if (Equals(other))
        {
            return 0;
        }

        return -Opened.CompareTo(other.Opened);
    }

    public bool Equals(RecentItem? other)
    {
        return other != null && Path.Equals(other.Path, StringComparison.OrdinalIgnoreCase);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as RecentItem);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Path, Opened);
    }
}
