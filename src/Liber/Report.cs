// Report.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Liber;

[XmlRoot("report")]
public class Report : IXmlSerializable
{
    public Report(Company company, DateTime posted)
    {
        Company = company;
        Posted = posted;
    }

    public Company Company { get; }
    public DateTime Started { get; set; }
    public DateTime Posted { get; }

    public Transaction MinTransaction
    {
        get
        {
            return new Transaction()
            {
                Posted = Started
            };
        }
    }

    public Transaction MaxTransaction
    {
        get
        {
            return new Transaction()
            {
                Posted = Posted
            };
        }
    }

    public XmlSchema? GetSchema()
    {
        throw new NotImplementedException();
    }

    public void ReadXml(XmlReader reader)
    {
        throw new NotImplementedException();
    }

    public void WriteXml(XmlWriter writer)
    {
        writer.WriteStartElement("company");
        XmlSerializers.Company.Serialize(writer, Company);
        writer.WriteEndElement();
    }
}
