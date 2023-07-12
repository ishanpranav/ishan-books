using CsvHelper.Configuration.Attributes;
using MessagePack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text.Json.Serialization;

namespace Liber;

[MessagePackObject]
public class Account
{
    internal readonly HashSet<Account> children = new HashSet<Account>();

    public Account() { }

    [JsonConstructor]
    [SerializationConstructor]
    public Account(Guid parentKey)
    {
        ParentKey = parentKey;
    }

    [Browsable(false)]
    [Key(0)]
    public Guid ParentKey { get; internal set; }

    [Key(1)]
    [LocalizedDisplayName(nameof(Name))]
    public string? Name { get; set; }

    [Browsable(false)]
    [IgnoreMember]
    [JsonIgnore]
    public string? Path { get; set; }

    [Key(2)]
    [LocalizedDisplayName(nameof(Number))]
    public decimal Number { get; set; }

    [Key(3)]
    [LocalizedDisplayName(nameof(Type))]
    public AccountType Type { get; set; }

    [Key(4)]
    [LocalizedDisplayName(nameof(Placeholder))]
    public bool Placeholder { get; set; }

    [Key(5)]
    [LocalizedDisplayName(nameof(Hidden))]
    public bool Hidden { get; set; }

    [Key(6)]
    [LocalizedDisplayName(nameof(Description))]
    public string? Description { get; set; }

    [Key(7)]
    [LocalizedDisplayName(nameof(Notes))]
    public string Notes { get; set; }

    [Key(8)]
    [LocalizedDisplayName(nameof(Color))]
    public Color Color { get; set; }

    [Browsable(false)]
    [IgnoreMember]
    [JsonIgnore]
    public decimal Balance
    {
        get
        {
            decimal result = 0;

            foreach (Line line in Lines)
            {
                result += line.Debit;
            }

            return Type.ToBalance(result);
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
    public IReadOnlyCollection<Line> Lines { get; } = new HashSet<Line>();

    public override string ToString()
    {
        return $"{Number} - {Name}";
    }
}
