// Account.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using CsvHelper.Configuration.Attributes;
using MessagePack;
using MessagePack.Formatters;

namespace Liber;

[MessagePackObject]
[XmlRoot("account")]
public class Account : IXmlSerializable
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

    [Ignore]
    [Key(0)]
    public Guid ParentId { get; internal set; }

    [Default(0)]
    [Index(3)]
    [Key(2)]
    [Name("Account Code")]
    [Optional]
    public decimal Number { get; set; }

    [Index(2)]
    [Key(1)]
    [Name("Account Name")]
    [Optional]
    public string Name { get; set; } = string.Empty;

    [Index(0)]
    [Key(3)]
    [Name("Type")]
    [Optional]
    public AccountType Type { get; set; }

    [BooleanFalseValues("F")]
    [BooleanTrueValues("T")]
    [Index(11)]
    [Key(4)]
    [Name("Placeholder")]
    [Optional]
    public bool Placeholder { get; set; }

    [Index(4)]
    [Key(5)]
    [Name("Description")]
    [NullValues("")]
    [Optional]
    public string? Description { get; set; }

    [Index(6)]
    [Key(6)]
    [Name("Notes")]
    [NullValues("")]
    [Optional]
    public string? Memo { get; set; }

    [Index(5)]
    [Browsable(false)]
    [Key(7)]
    [MessagePackFormatter(typeof(MessagePackColorFormatter))]
    [Name("Account Color")]
    [Optional]
    [CsvHelper.Configuration.Attributes.TypeConverter(typeof(CsvHelper.TypeConversion.ColorConverter))]
    public Color Color { get; set; }

    [Index(10)]
    [Key(8)]
    [Name("Tax Info")]
    [Optional]
    public TaxType TaxType { get; set; }

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
    public IReadOnlyCollection<Line> Lines
    {
        get
        {
            return lines;
        }
    }

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
        writer.WriteElementString("name", Name);
        writer.WriteElementString("type", Type.ToString());

        decimal balance;

        if (writer is XmlReportWriter reportWriter)
        {
            if (this == reportWriter.Report.Company.EquityAccount)
            {
                if (reportWriter.Report.Started != reportWriter.Report.Posted)
                {
                    balance = reportWriter.Report.Company.GetEquity(reportWriter.Report.Started);
                }
                else
                {
                    balance = reportWriter.Report.Company.GetEquity(reportWriter.Report.Posted);
                }
            }
            else if (this == reportWriter.Report.Company.OtherEquityAccount)
            {
                if (reportWriter.Report.Started != reportWriter.Report.Posted)
                {
                    balance = reportWriter.Report.Company.GetOtherEquity(reportWriter.Report.Started);
                }
                else
                {
                    balance = reportWriter.Report.Company.GetOtherEquity(reportWriter.Report.Posted);
                }
            }
            else if (Temporary)
            {
                balance = GetBalance(reportWriter.Report.Started, reportWriter.Report.Posted);
            }
            else
            {
                balance = GetBalance(reportWriter.Report.Posted);
            }
        }
        else
        {
            balance = Balance;
        }

        Accounting.DebitCredit(balance, out decimal debit, out decimal credit);

        writer.WriteElementString("debit", XmlConvert.ToString(debit));
        writer.WriteElementString("credit", XmlConvert.ToString(credit));
    }
}
