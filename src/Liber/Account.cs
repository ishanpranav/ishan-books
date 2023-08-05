// Account.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text.Json.Serialization;
using CsvHelper.Configuration.Attributes;
using MessagePack;
using MessagePack.Formatters;

namespace Liber;

[MessagePackObject]
public class Account
{
    internal readonly HashSet<Account> children = new HashSet<Account>();
    internal readonly HashSet<Line> lines = new HashSet<Line>();

    public Account() { }

    [JsonConstructor]
    [SerializationConstructor]
    public Account(Guid parentId)
    {
        ParentId = parentId;
    }

    [Browsable(false)]
    [Ignore]
    [Key(0)]
    public Guid ParentId { get; internal set; }

    [Default(0)]
    [Index(3)]
    [Key(2)]
    [LocalizedDisplayName(nameof(Number))]
    [Name("Account Code")]
    [Optional]
    public decimal Number { get; set; }

    [Index(2)]
    [Key(1)]
    [LocalizedDisplayName(nameof(Name))]
    [Name("Account Name")]
    [Optional]
    public string Name { get; set; } = string.Empty;

    [Index(0)]
    [Key(3)]
    [LocalizedDisplayName(nameof(Type))]
    [Name("Type")]
    [Optional]
    public AccountType Type { get; set; }

    [BooleanFalseValues("F")]
    [BooleanTrueValues("T")]
    [Index(11)]
    [Key(4)]
    [LocalizedDisplayName(nameof(Placeholder))]
    [Name("Placeholder")]
    [Optional]
    public bool Placeholder { get; set; }

    [Index(4)]
    [Key(5)]
    [LocalizedDisplayName(nameof(Description))]
    [Name("Description")]
    [NullValues("")]
    [Optional]
    public string? Description { get; set; }

    [Index(6)]
    [Key(6)]
    [LocalizedDisplayName(nameof(Memo))]
    [Name("Notes")]
    [NullValues("")]
    [Optional]
    public string? Memo { get; set; }

    [Index(5)]
    [Key(7)]
    [LocalizedDisplayName(nameof(Color))]
    [MessagePackFormatter(typeof(MessagePackColorFormatter))]
    [Name("Account Color")]
    [Optional]
    [CsvHelper.Configuration.Attributes.TypeConverter(typeof(CsvHelper.TypeConversion.ColorConverter))]
    public Color Color { get; set; }

    [Index(10)]
    [Key(8)]
    [LocalizedDisplayName(nameof(TaxType))]
    [Name("Tax Info")]
    [Optional]
    public TaxType TaxType { get; set; }

    [Browsable(false)]
    [Ignore]
    [IgnoreMember]
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

    [Browsable(false)]
    [IgnoreMember]
    [JsonIgnore]
    public bool Temporary
    {
        get
        {
            switch (Type)
            {
                case AccountType.Expense:
                case AccountType.Income:
                case AccountType.Cost:
                case AccountType.OtherIncomeExpense:
                case AccountType.IncomeTaxExpense:
                case AccountType.OtherComprehensiveIncome:
                    return true;

                default:
                    return false;
            }
        }
    }

    [Browsable(false)]
    [IgnoreMember]
    [JsonIgnore]
    public bool Virtual
    {
        get
        {
            switch (Type)
            {
                case AccountType.Bank:
                case AccountType.CreditCard:
                    return false;

                default:
                    return true;
            }
        }
    }

    [Browsable(false)]
    [IgnoreMember]
    [JsonIgnore]
    public IReadOnlyCollection<Account> Children
    {
        get
        {
            return children;
        }
    }

    [Browsable(false)]
    [IgnoreMember]
    [JsonIgnore]
    public IReadOnlyCollection<Line> Lines
    {
        get
        {
            return lines;
        }
    }

    [Browsable(false)]
    [IgnoreMember]
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

    public decimal GetBalance(DateTime posted)
    {
        decimal result = 0;

        foreach (Line line in lines)
        {
            if (line.Transaction!.Posted <= posted)
            {
                result += line.Balance;
            }
        }

        return result;
    }

    public decimal GetBalance(DateTime started, DateTime posted)
    {
        decimal result = 0;

        foreach (Line line in lines)
        {
            Transaction transaction = line.Transaction!;

            if (transaction.Posted >= started && transaction.Posted <= posted)
            {
                result += line.Balance;
            }
        }

        return result;
    }

    public IEnumerable<Line> GetChecks()
    {
        if (Type != AccountType.Bank)
        {
            yield break;
        }

        foreach (Line line in lines)
        {
            if (line.Transaction?.Name != null && line.Balance < 0)
            {
                yield return line;
            }
        }
    }

    public override string ToString()
    {
        return Name;
    }
}
