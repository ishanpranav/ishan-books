// Transaction.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json.Serialization;
using CsvHelper.Configuration.Attributes;

namespace Liber;

public class Transaction :
    IComparable,
    IComparable<Transaction>,
    IEquatable<Transaction>
{
    internal readonly SortedSet<Line> lines = new SortedSet<Line>();

    public Transaction() { }

    [JsonConstructor]
    public Transaction(Guid id, string? name, IReadOnlyCollection<Line> lines)
    {
        Id = id;
        Name = name;
        this.lines = new SortedSet<Line>(lines);

        foreach (Line line in lines)
        {
            line.transaction = this;
        }
    }

    [Index(1)]
    [Name("Transaction ID")]
    public Guid Id { get; internal set; }

    [Format("M/d/yyyy")]
    [Index(0)]
    [Name("Date")]
    public DateTime Posted { get; set; }

    [Default(0)]
    [Index(2)]
    [Name("Number")]
    [Optional]
    public decimal Number { get; set; }

    [Index(3)]
    [Name("Description")]
    [NullValues("")]
    [Optional]
    public string? Name { get; internal set; }

    [Index(4)]
    [Name("Notes")]
    [NullValues("")]
    [Optional]
    public string? Memo { get; set; }

    [Index(16)]
    [LocalizedDisplayName(nameof(Reconciled))]
    [Name("Reconcile Date")]
    [NullValues("")]
    [Optional]
    public DateTime? Reconciled { get; set; }

    [Ignore]
    public IReadOnlyCollection<Line> Lines
    {
        get
        {
            return lines;
        }
    }

    public Line? GetDoubleEntry(Line value)
    {
        IEnumerable<Line> lines = Lines.Where(x => x.Balance == -value.Balance);

        if (lines.Skip(1).Any())
        {
            return null;
        }

        return lines.SingleOrDefault();
    }

    public int CompareTo(object? obj)
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

    public static bool operator ==(Transaction? left, Transaction? right)
    {
        if (left is null)
        {
            return right is null;
        }

        return left.Equals(right);
    }

    public static bool operator !=(Transaction? left, Transaction? right)
    {
        return !(left == right);
    }

    public static bool operator <(Transaction? left, Transaction? right)
    {
        return left is null ? right is not null : left.CompareTo(right) < 0;
    }

    public static bool operator <=(Transaction? left, Transaction? right)
    {
        return left is null || left.CompareTo(right) <= 0;
    }

    public static bool operator >(Transaction? left, Transaction? right)
    {
        return left is not null && left.CompareTo(right) > 0;
    }

    public static bool operator >=(Transaction? left, Transaction? right)
    {
        return left is null ? right is null : left.CompareTo(right) >= 0;
    }
}
