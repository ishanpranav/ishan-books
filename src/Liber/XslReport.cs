// Report.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using System.Resources;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Humanizer;
using Humanizer.Localisation;
using Liber;

namespace Liber;

[XmlRoot("report")]
public class XslReport : IXmlSerializable
{ 
    private static readonly ResourceManager s_resourceManager = new ResourceManager(typeof(XslReport));

    public XslReport()
    {
        Company = new Company();
    }

    public XslReport(Company company)
    {
        Company = company;
    }

    [Browsable(false)]
    public Company Company { get; }

    public DateTime Started { get; set; } = new DateTime(DateTime.Today.Year, 1, 1);

    public DateTime Posted { get; set; } = DateTime.Today;

    [Browsable(false)]
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

    [Browsable(false)]
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

    public string fdate(DateTime value)
    {
        return value.ToShortDateString();
    }

    public string fdatel()
    {
        return Posted.ToLongDateString();
    }

    public string fm(decimal value)
    {
        return value.ToLocalizedString();
    }

    public string ftspanl()
    {
        return Started.ToShortDateString() + " - " + Posted.ToShortDateString();
    }

    public string ftspans()
    {
        return (Posted - Started).Humanize(maxUnit: TimeUnit.Year);
    }

    public string pngets(string key, decimal value)
    {
        if (value < 0)
        {
            return gets("_n_" + key);
        }

        return gets("_p_" + key);
    }

    public string gets(string key)
    {
        return GetString(key);
    }

    public static string GetString(string key)
    {
        string? result = s_resourceManager.GetString(key);

        if (result == null)
        {
            return key;
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
        XmlSerializers.Company.Serialize(writer, Company);
    }
}
