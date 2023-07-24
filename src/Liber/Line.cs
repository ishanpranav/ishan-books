// Line.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Globalization;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using CsvHelper.Configuration.Attributes;
using MessagePack;

namespace Liber;

[MessagePackObject]
[XmlRoot("line")]
public class Line : IXmlSerializable
{
    [Ignore]
    [Key(0)]
    public Guid AccountKey { get; set; }

    [Index(11)]
    [Key(1)]
    [Name("Value Num.")]
    [NumberStyles(NumberStyles.Currency)]
    public decimal Balance { get; set; }

    [IgnoreMember]
    [JsonIgnore]
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

    [IgnoreMember]
    [JsonIgnore]
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
    [Key(2)]
    [Name("Memo")]
    [NullValues("")]
    [Optional]
    public string? Description { get; set; }

    [IgnoreMember]
    [JsonIgnore]
    public Transaction? Transaction { get; internal set; }

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
        string account;

        if (writer is XmlReportWriter reportWriter)
        {
            account = reportWriter.Report.Company!.Accounts[AccountKey].Name;
        }
        else
        {
            account = AccountKey.ToString();
        }

        Accounting.DebitCredit(Balance, out decimal debit, out decimal credit);
        writer.WriteElementString("account", account);
        writer.WriteElementString("debit", XmlConvert.ToString(debit));
        writer.WriteElementString("credit", XmlConvert.ToString(credit));
        writer.WriteElementString("description", Description);
    }
}
