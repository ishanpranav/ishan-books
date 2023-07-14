using CsvHelper.Configuration.Attributes;
using MessagePack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Liber;

[MessagePackObject]
[XmlRoot("account")]
public class Account : IXmlSerializable
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
    [Ignore]
    [Key(0)]
    public Guid ParentKey { get; internal set; }

    [Ignore]
    [Key(2)]
    [LocalizedDisplayName(nameof(Number))]
    public decimal Number { get; set; }

    [DataMember]
    [Index(2)]
    [Key(1)]
    [LocalizedDisplayName(nameof(Name))]
    [Name("Account Name")]
    public string Name { get; set; } = string.Empty;

    [Index(0)]
    [Key(3)]
    [LocalizedDisplayName(nameof(Type))]
    [Name("Type")]
    public AccountType Type { get; set; }

    [BooleanFalseValues("F")]
    [BooleanTrueValues("T")]
    [Index(11)]
    [Key(4)]
    [LocalizedDisplayName(nameof(Placeholder))]
    [Name("Placeholder")]
    public bool Placeholder { get; set; }

    [Index(4)]
    [Key(5)]
    [LocalizedDisplayName(nameof(Description))]
    [Name("Description")]
    public string? Description { get; set; }

    [Index(6)]
    [Key(6)]
    [LocalizedDisplayName(nameof(Notes))]
    [Name("Notes")]
    public string? Notes { get; set; }

    [Index(5)]
    [Browsable(false)]
    [Key(7)]
    [Name("Account Color")]
    [CsvHelper.Configuration.Attributes.TypeConverter(typeof(CsvHelper.TypeConversion.ColorConverter))]
    public Color Color { get; set; }

    [Index(10)]
    [Browsable(false)]
    [Key(8)]
    [Name("Tax Info")]
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

            foreach (Line line in Lines)
            {
                result += line.Balance;
            }

            return result;
        }
    }

    [Browsable(false)]
    [Ignore]
    [IgnoreMember]
    [JsonIgnore]
    public decimal Debit
    {
        get
        {
            decimal result = 0;

            foreach (Line line in Lines)
            {
                result += line.Debit;
            }

            return result;
        }
    }

    [Browsable(false)]
    [Ignore]
    [IgnoreMember]
    [JsonIgnore]
    public decimal Credit
    {
        get
        {
            decimal result = 0;

            foreach (Line line in Lines)
            {
                result += line.Credit;
            }

            return result;
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

    public XmlSchema? GetSchema()
    {
        return null;
    }

    void IXmlSerializable.ReadXml(XmlReader reader)
    {
        throw new NotImplementedException();
    }

    public void WriteXml(XmlWriter writer)
    {
        writer.WriteElementString("name", ToString());
        writer.WriteElementString("debit", XmlConvert.ToString(Debit));
        writer.WriteElementString("credit", XmlConvert.ToString(Credit));
    }
}
