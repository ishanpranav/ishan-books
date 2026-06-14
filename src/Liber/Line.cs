// Line.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.Json.Serialization;
using CsvHelper.Configuration.Attributes;

namespace Liber;

public class Line :
    ICloneable,
    IComparable,
    IComparable<Line>,
    IEquatable<Line>
{
    internal Transaction? transaction;

    [Browsable(false)]
    [Ignore]
    public Guid AccountId { get; private set; }

    [Browsable(false)]
    [Index(14)]
    [Name("Value Num.")]
    [NumberStyles(NumberStyles.Currency)]
    public decimal Balance { get; private set; }

    [JsonIgnore]
    [LocalizedDisplayName(nameof(Debit))]
    public decimal Debit
    {
        get
        {
            if (Balance < 0)
            {
                return 0;
            }

            return Balance;
        }
    }

    [JsonIgnore]
    [LocalizedDisplayName(nameof(Credit))]
    public decimal Credit
    {
        get
        {
            if (Balance > 0)
            {
                return 0;
            }

            return -Balance;
        }
    }

    [Index(8)]
    [LocalizedDisplayName(nameof(Description))]
    [Name("Memo")]
    [NullValues("")]
    [Optional]
    public string? Description { get; internal set; }

    [Browsable(false)]
    [Ignore]
    [JsonIgnore]
    public Transaction Transaction
    {
        get
        {
            return transaction!;
        }
    }

    [Browsable(false)]
    [Ignore]
    [JsonIgnore]
    public Line? Sibling
    {
        get
        {
            if (transaction == null)
            {
                return null;
            }

            return transaction.GetDoubleEntry(this);
        }
    }

    public Line() { }

    [JsonConstructor]
    public Line(Guid accountId, decimal balance, string? description)
    {
        AccountId = accountId;
        Balance = balance;
        Description = description;
    }

    public override string ToString()
    {
        return $"{AccountId}: {Balance.ToLocalizedString()} {Description}";
    }

    public Line Clone()
    {
        return new Line(AccountId, Balance, Description);
    }

    object ICloneable.Clone()
    {
        return Clone();
    }

    public int CompareTo(object? obj)
    {
        if (obj == null)
        {
            return 1;
        }

        if (obj is not Line line)
        {
            throw new ArgumentException(message: null, nameof(obj));
        }

        return CompareTo(line);
    }

    public int CompareTo(Line? other)
    {
        if (other is null)
        {
            return 1;
        }

        int result = 0;

        if (transaction != null)
        {
            result = transaction.CompareTo(other.transaction);
        }

        if (result != 0)
        {
            return result;
        }

        int debitOrder = Balance >= 0 ? -1 : 1;
        int otherDebitOrder = other.Balance >= 0 ? -1 : 1;

        result = debitOrder - otherDebitOrder;

        if (result != 0)
        {
            return result;
        }

        result = decimal.Abs(other.Balance).CompareTo(decimal.Abs(Balance));

        if (result != 0)
        {
            return result;
        }

        result = AccountId.CompareTo(other.AccountId);

        if (result != 0)
        {
            return result;
        }

        if (Description == null)
        {
            if (other.Description == null)
            {
                return 0;
            }

            return -1;
        }

        return Description.CompareTo(other.Description);
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

        return obj is Line other && Equals(other);
    }

    public bool Equals(Line? other)
    {
        if (other is null)
        {
            return false;
        }

        return transaction == other.transaction &&
            Balance == other.Balance &&
            AccountId == other.AccountId &&
            Description == other.Description;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(transaction, Balance, AccountId, Description);
    }

    public static bool operator ==(Line? left, Line? right)
    {
        if (left is null)
        {
            return right is null;
        }

        return left.Equals(right);
    }

    public static bool operator !=(Line? left, Line? right)
    {
        return !(left == right);
    }

    public static bool operator <(Line? left, Line? right)
    {
        return left is null ? right is not null : left.CompareTo(right) < 0;
    }

    public static bool operator <=(Line? left, Line? right)
    {
        return left is null || left.CompareTo(right) <= 0;
    }

    public static bool operator >(Line? left, Line? right)
    {
        return left is not null && left.CompareTo(right) > 0;
    }

    public static bool operator >=(Line? left, Line? right)
    {
        return left is null ? right is null : left.CompareTo(right) >= 0;
    }
}
