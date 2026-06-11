// Account.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using CsvHelper.Configuration.Attributes;
using CsvHelper.TypeConversion;

namespace Liber;

/// <summary>
/// Represents a financial account.
/// </summary>
public class Account : IComparable<Account>, IComparable
{
    internal readonly HashSet<Account> children = new HashSet<Account>();
    internal readonly HashSet<Line> lines = new HashSet<Line>();

    /// <summary>
    /// Initializes a new instance of the <see cref="Account"/> class.
    /// </summary>
    public Account() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Account"/> class with a specified parent identifier.
    /// </summary>
    /// <param name="parentId">The identifier of the parent account.</param>
    [JsonConstructor]
    public Account(Guid parentId)
    {
        ParentId = parentId;
    }

    /// <summary>
    /// Gets the identifier of the parent account.
    /// </summary>
    /// <value>The parent identifier.</value>
    [Browsable(false)]
    [Ignore]
    public Guid ParentId { get; internal set; }

    /// <summary>
    /// Gets or sets the account number.
    /// </summary>
    /// <value>The account number.</value>
    [Default(0)]
    [Index(3)]
    [LocalizedDisplayName(nameof(Number))]
    [Name("Account Code")]
    [Optional]
    public decimal Number { get; set; }

    /// <summary>
    /// Gets or sets the account name.
    /// </summary>
    /// <value>The account name.</value>
    [Index(2)]
    [LocalizedDisplayName(nameof(Name))]
    [Name("Account Name")]
    [Optional]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the account type.
    /// </summary>
    /// <value>The account type.</value>
    [Index(0)]
    [LocalizedDisplayName(nameof(Type))]
    [Name("Type")]
    [Optional]
    [CsvHelper.Configuration.Attributes.TypeConverter(typeof(GnuCashAccountTypeConverter))]
    [System.ComponentModel.TypeConverter(typeof(LocalizedEnumConverter<AccountType>))]
    public AccountType Type { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the account is a placeholder.
    /// </summary>
    /// <value><see langword="true"/> if the account is a placeholder; otherwise, <see langword="false"/>.</value>
    [BooleanFalseValues("F")]
    [BooleanTrueValues("T")]
    [Index(11)]
    [LocalizedDisplayName(nameof(Placeholder))]
    [Name("Placeholder")]
    [Optional]
    public bool Placeholder { get; set; }

    /// <summary>
    /// Gets or sets the description of the account.
    /// </summary>
    /// <value>The description of the account.</value>
    [Index(4)]
    [LocalizedDisplayName(nameof(Description))]
    [Name("Description")]
    [NullValues("")]
    [Optional]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the memo associated with the account.
    /// </summary>
    /// <value>The memo associated with the account.</value>
    [Index(6)]
    [LocalizedDisplayName(nameof(Memo))]
    [Name("Notes")]
    [NullValues("")]
    [Optional]
    public string? Memo { get; set; }

    /// <summary>
    /// Gets or sets the color associated with the account.
    /// </summary>
    /// <value>The color associated with the account.</value>
    [Index(5)]
    [LocalizedDisplayName(nameof(Color))]
    [Name("Account Color")]
    [Optional]
    [CsvHelper.Configuration.Attributes.TypeConverter(typeof(CsvHelper.TypeConversion.ColorConverter))]
    public Color Color { get; set; }

    /// <summary>
    /// Gets or sets the tax category associated with the account.
    /// </summary>
    /// <value>The tax category associated with the account.</value>
    [BooleanFalseValues("F")]
    [BooleanTrueValues("T")]
    [LocalizedDisplayName(nameof(TaxType))]
    [Name("Tax Info")]
    public bool TaxType { get; set; }

    [Ignore]
    [LocalizedDisplayName(nameof(CashFlow))]
    [System.ComponentModel.TypeConverter(typeof(LocalizedEnumConverter<CashFlow>))]
    public CashFlow CashFlow { get; set; }

    [BooleanFalseValues("F")]
    [BooleanTrueValues("T")]
    [Index(9)]
    [LocalizedDisplayName(nameof(Inactive))]
    [Name("Hidden")]
    [Optional]
    public bool Inactive { get; set; }

    /// <summary>
    /// Gets the current balance of the account.
    /// </summary>
    /// <value>The current balance of the account.</value>
    [Browsable(false)]
    [Ignore]
    [JsonIgnore]
    public decimal Balance
    {
        get
        {
            decimal result = 0;

            foreach (Line line in lines)
            {
                result += line.Balance;
            }

            return result;
        }
    }

    /// <summary>
    /// Gets the subaccounts of the current account.
    /// </summary>
    /// <value>The account children.</value>
    [Browsable(false)]
    [JsonIgnore]
    public IReadOnlyCollection<Account> Children
    {
        get
        {
            return children;
        }
    }

    /// <summary>
    /// Gets the line items posted to the account.
    /// </summary>
    /// <value>The line items posted to the account.</value>
    [Browsable(false)]
    [JsonIgnore]
    public IReadOnlyCollection<Line> Lines
    {
        get
        {
            return lines;
        }
    }

    /// <summary>
    /// Gets the line items posted to the account, ordered chronologically by date with debits listed before credits and balances ordered by magnitude from greatest to least.
    /// </summary>
    /// <value>Gets the line items posted to the account, sorted in their natural order.</value>
    [Browsable(false)]
    [JsonIgnore]
    public IOrderedEnumerable<Line> OrderedLines
    {
        get
        {
            return lines
                .OrderBy(x => x.Transaction)
                .ThenBy(x => x.Debit > 0 ? -1 : 1)
                .ThenByDescending(x => Math.Abs(x.Balance));
        }
    }

    [Browsable(false)]
    [JsonIgnore]
    public bool ReadOnly
    {
        get
        {
            return Inactive || Placeholder || children.Count > 0;
        }
    }

    /// <summary>
    /// Gets the balance of the account up to a specific posted date.
    /// </summary>
    /// <param name="posted">The inclusive posted date up to which to calculate the balance.</param>
    /// <returns>The balance of the account up to (and including) the specified posted date.</returns>
    public decimal GetBalance(DateTime posted, Regex filter)
    {
        decimal result = 0;

        foreach (Line line in lines)
        {
            Transaction transaction = line.Transaction!;

            if (transaction!.Posted <= posted && filter.IsMatch(transaction.Memo ?? string.Empty))
            {
                result += line.Balance;
            }
        }

        return result;
    }

    /// <summary>
    /// Gets the balance of the account within a specific date range.
    /// </summary>
    /// <param name="started">The inclusive start date of the date range.</param>
    /// <param name="posted">The inclusive end date of the date range.</param>
    /// <returns>The balance of the account within the specified date range, including the start and end dates.</returns>
    public decimal GetBalance(DateTime started, DateTime posted, Regex filter)
    {
        decimal result = 0;

        foreach (Line line in lines)
        {
            Transaction transaction = line.Transaction!;

            if (transaction.Posted >= started && transaction.Posted <= posted && filter.IsMatch(transaction.Memo ?? string.Empty))
            {
                result += line.Balance;
            }
        }

        return result;
    }

    public decimal GetAverageDailyBalance(DateTime started, DateTime posted, Regex filter)
    {
        decimal weightedSum = 0;
        decimal balance = GetBalance(started, filter);
        decimal totalDays = 0;
        decimal days;
        DateTime previous = started;

        foreach (Line line in lines)
        {
            Transaction transaction = line.Transaction!;

            if (transaction.Posted >= started && transaction.Posted <= posted && filter.IsMatch(transaction.Memo ?? string.Empty))
            {
                days = (decimal)Math.Round((transaction.Posted - previous).TotalDays);
                weightedSum += balance * days;
                balance += line.Balance;
                totalDays += days;
                previous = transaction.Posted;
            }
        }

        days = (decimal)Math.Round((posted - previous).TotalDays) + 1;
        weightedSum += balance * days;
        totalDays += days;

        return weightedSum / totalDays;
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return Name;
    }

    int IComparable.CompareTo(object? obj)
    {
        if (obj == null)
        {
            return 1;
        }

        if (obj is not Account account)
        {
            throw new ArgumentException(message: null, nameof(obj));
        }

        return CompareTo(account);
    }

    public int CompareTo(Account? other)
    {
        if (other is null)
        {
            return 1;
        }

        int result = Number.CompareTo(other.Number);

        if (result != 0)
        {
            return result;
        }

        result = Name.CompareTo(other.Name);

        if (result != 0)
        {
            return result;
        }

        result = Type.CompareTo(other.Type);

        if (result != 0)
        {
            return result;
        }

        result = CashFlow.CompareTo(other.CashFlow);

        if (result != 0)
        {
            return result;
        }

        result = TaxType.CompareTo(other.TaxType);

        if (result != 0)
        {
            return result;
        }

        result = other.Balance.CompareTo(Balance);

        if (result != 0)
        {
            return result;
        }

        return ParentId.CompareTo(other.ParentId);
    }
}
