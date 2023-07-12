using MessagePack;
using System;
using System.Collections.Generic;
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

    [Key(0)]
    public Guid ParentKey { get; internal set; }

    [Key(1)]
    public string? Name { get; set; }

    [Key(2)]
    public decimal Number { get; set; }

    [Key(3)]
    public bool Placeholder { get; set; }

    [Key(4)]
    public AccountType Type { get; set; }

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

    [IgnoreMember]
    [JsonIgnore]
    public IReadOnlyCollection<Account> Children
    {
        get
        {
            return children;
        }
    }

    [IgnoreMember]
    [JsonIgnore]
    public IReadOnlyCollection<Line> Lines { get; } = new HashSet<Line>();

    public override string ToString()
    {
        return $"{Number} - {Name}";
    }
}
