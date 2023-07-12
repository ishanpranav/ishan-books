using System;

namespace Liber.Forms;

internal sealed class Error : IComparable, IComparable<Error>
{
    public Error(DateTime created, string description)
    {
        Created = created;
        Description = description;
    }

    public DateTime Created { get; }
    public string Description { get; }
    public string? RawString { get; set; }

    int IComparable.CompareTo(object? obj)
    {
        if (obj == null)
        {
            return 1;
        }

        if (obj is not Error other)
        {
            throw new ArgumentException(message: null, nameof(obj));
        }

        return CompareTo(other);
    }

    public int CompareTo(Error? other)
    {
        if (other == null)
        {
            return 1;
        }

        return -Created.CompareTo(other.Created);
    }
}
