using CsvHelper.Configuration.Attributes;
using System;
using System.Text.Json.Serialization;

namespace Liber;

/// <summary>
/// 
/// </summary>
public class Transaction : ICloneable, IComparable, IComparable<Transaction>, IEquatable<Transaction>
{
    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    [JsonIgnore]
    [Name("Transaction ID")]
    public Guid Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    [Ignore]
    public Guid AccountId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    [Ignore]
    public int Number { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    [Name("Date")]
    public DateOnly Posted { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    [Name("Amount Num.")]
    public decimal Debit { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    [Name("Description")]
    [NullValues("")]
    public string? Description { get; set; }

    /// <inheritdoc/>
    int IComparable.CompareTo(object? obj)
    {
        if (obj == null)
        {
            return 1;
        }

        if (obj is not Transaction other)
        {
            throw new ArgumentException(message: null, nameof(obj));
        }

        return CompareTo(other);
    }

    /// <inheritdoc/>
    public int CompareTo(Transaction? other)
    {
        if (other == null)
        {
            return 1;
        }

        if (this == other)
        {
            return 0;
        }

        if (Posted != other.Posted)
        {
            return Posted.CompareTo(other.Posted);
        }

        if (Number != other.Number)
        {
            return Number - other.Number;
        }

        if (Debit != other.Debit)
        {
            return other.Debit.CompareTo(Debit);
        }

        if (Description != other.Description)
        {
            return StringComparer.CurrentCultureIgnoreCase.Compare(Description, other.Description);
        }

        return Id.CompareTo(other.Id);
    }

    /// <inheritdoc/>
    public bool Equals(Transaction? other)
    {
        return other != null && Id == other.Id;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return Equals(obj as Transaction);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    /// <inheritdoc/>
    object ICloneable.Clone()
    {
        return MemberwiseClone();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Transaction Clone()
    {
        return (Transaction)MemberwiseClone();
    }
}
