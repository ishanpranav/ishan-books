using CsvHelper.Configuration.Attributes;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Liber;

public class Transaction :
    IComparable,
    IComparable<Transaction>, 
    IEquatable<Transaction>
{
    public Transaction()
    {
        Lines = new HashSet<Line>();
    }

    [JsonConstructor]
    [SerializationConstructor]
    public Transaction(ICollection<Line> lines)
    {
        Lines = lines;
    }

    [Index(1)]
    [Key(0)]
    [Name("Transaction ID")]
    public Guid Id { get; set; }

    [Index(2)]
    [Key(1)]
    [Name("Number")]
    public decimal Number { get; set; }

    [Index(0)]
    [Key(2)]
    [Name("Date")]
    public DateTime Posted { get; set; }

    [Index(3)]
    [Key(3)]
    [Name("Description")]
    public string? Description { get; set; }

    [Index(4)]
    [Key(4)]
    [Name("Notes")]
    public string? Memo { get; set; }

    [IgnoreMember]
    public decimal Balance
    {
        get
        {
            decimal result = 0;

            foreach (Line line in Lines)
            {
                result += line.Balance;
            }

            return result;
        }
    }

    [Key(5)]
    public ICollection<Line> Lines { get; }

    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }

        if (ReferenceEquals(obj, this))
        {
            return true;
        }

        return obj is Transaction other && Equals(other);
    }

    public bool Equals(Transaction? other)
    {
        if (other is null)
        {
            return false;
        }

        return Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    int IComparable.CompareTo(object? obj)
    {
        if (obj == null)
        {
            return 1;
        }

        if (obj is not Transaction transaction)
        {
            throw new ArgumentException(message: null, nameof(obj));
        }

        return CompareTo(transaction);
    }

    public int CompareTo(Transaction? other)
    {
        if (other is null)
        {
            return 1;
        }

        int result = Posted.CompareTo(other.Posted);

        if (result != 0)
        {
            return result;
        }

        result = Number.CompareTo(other.Number);

        if (result != 0)
        {
            return result;
        }

        return Id.CompareTo(other.Id);
    }

    public static bool operator ==(Transaction? left, Transaction? right)
    {
        if (ReferenceEquals(left, null))
        {
            return ReferenceEquals(right, null);
        }

        return left.Equals(right);
    }

    public static bool operator !=(Transaction? left, Transaction? right)
    {
        return !(left == right);
    }

    public static bool operator <(Transaction? left, Transaction? right)
    {
        return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0;
    }

    public static bool operator <=(Transaction? left, Transaction? right)
    {
        return ReferenceEquals(left, null) || left.CompareTo(right) <= 0;
    }

    public static bool operator >(Transaction? left, Transaction? right)
    {
        return !ReferenceEquals(left, null) && left.CompareTo(right) > 0;
    }

    public static bool operator >=(Transaction? left, Transaction? right)
    {
        return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0;
    }
}
