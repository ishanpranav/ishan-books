// XslReport.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using System.Linq;
using System.Resources;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Humanizer;
using Humanizer.Localisation;
using Liber;
using Liber.Forms.Accounts;

namespace Liber.Forms.Reports.Xsl;

[XmlRoot("report")]
public class XslReport : IXmlSerializable
{
    private static readonly ResourceManager s_resourceManager = new ResourceManager(typeof(XslReport));

    private DateTime _started = new DateTime(DateTime.Today.Year, 1, 1);
    private DateTime _posted = DateTime.Today;

    public XslReport()
    {
        Title = string.Empty;
        Company = new Company();
        Accounts = new AccountsView(Company);
    }

    public XslReport(string title, Company company)
    {
        Title = title;
        Company = company;
        Accounts = new AccountsView(company);
    }

    [Browsable(false)]
    public Company Company { get; set; }

    [LocalizedCategory(nameof(Title))]
    [LocalizedDescription(nameof(Title))]
    [LocalizedDisplayName(nameof(Title))]
    public string Title { get; set; }

    [LocalizedCategory(nameof(Started))]
    [LocalizedDescription(nameof(Started))]
    [LocalizedDisplayName(nameof(Started))]
    public DateTime Started
    {
        get
        {
            return _started;
        }
        set
        {
            if (value > _posted)
            {
                _posted = value;
            }

            _started = value;
        }
    }

    [LocalizedCategory(nameof(Posted))]
    [LocalizedDescription(nameof(Posted))]
    [LocalizedDisplayName(nameof(Posted))]
    public DateTime Posted
    {
        get
        {
            return _posted;
        }
        set
        {
            if (value < _started)
            {
                _started = value;
            }

            _posted = value;
        }
    }

    [Browsable(false)]
    [LocalizedCategory(nameof(EquityMode))]
    [LocalizedDescription(nameof(EquityMode))]
    [LocalizedDisplayName(nameof(EquityMode))]
    public EquityModes EquityMode { get; set; }

    [LocalizedCategory(nameof(Accounts))]
    [LocalizedDescription(nameof(Accounts))]
    [LocalizedDisplayName(nameof(Accounts))]
    public AccountsView Accounts { get; set; }

    public string fdatel(DateTime value)
    {
        return value.ToLongDateString();
    }

    public string fdates(DateTime value)
    {
        return value.ToShortDateString();
    }

    public string fm(decimal value)
    {
        return value.ToLocalizedString();
    }

    public string fm(string type, decimal balance)
    {
        return Enum
            .Parse<AccountType>(type)
            .ToBalance(balance)
            .ToLocalizedString();
    }

    public string ftspanl(DateTime started, DateTime posted)
    {
        return started.ToShortDateString() + " \u2013 " + posted.ToShortDateString();
    }

    public string ftspans(DateTime started, DateTime posted)
    {
        return (posted - started).Humanize(precision: 2, countEmptyUnits: true, maxUnit: TimeUnit.Year);
    }

    public string fgets(string key, object value)
    {
        return string
            .Format(gets(key), value)
            .Transform(To.SentenceCase);
    }

    public string fgets(string key, object first, object second)
    {
        return string
            .Format(gets(key), first, second)
            .Transform(To.SentenceCase);
    }

    public string pngets(string key, decimal value)
    {
        if (value < 0)
        {
            string? result = s_resourceManager.GetString("_n_" + key);

            if (result != null)
            {
                return result;
            }
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

    private void WriteAccountXml(XmlWriter writer, Account value)
    {
        writer.WriteStartElement("account");
        writer.WriteElementString("name", value.Name);
        writer.WriteElementString("type", value.Type.ToString());

        decimal balance = 0;
        decimal previous = 0;
        decimal debit;
        decimal credit;

        if (value == Company.EquityAccount)
        {
            writer.WriteElementString("equity", XmlConvert.ToString(true));

            if (EquityMode.HasFlag(EquityModes.CurrentPosted))
            {
                balance = Company.GetEquity(Posted);
            }

            if (EquityMode.HasFlag(EquityModes.CurrentStarted))
            {
                balance = Company.GetEquity(Started);
            }

            previous = Company.GetEquity(Started);
        }
        else if (value == Company.OtherEquityAccount)
        {
            writer.WriteElementString("other-equity", XmlConvert.ToString(true));

            if (EquityMode.HasFlag(EquityModes.CurrentPosted))
            {
                balance = Company.GetOtherEquity(Posted);
            }

            if (EquityMode.HasFlag(EquityModes.CurrentStarted))
            {
                balance = Company.GetOtherEquity(Started);
            }

            previous = Company.GetOtherEquity(Started);
        }
        else if (value.Temporary)
        {
            balance = value.GetBalance(Started, Posted);
        }
        else
        {
            balance = value.GetBalance(Posted);
            previous = value.GetBalance(Started);
        }

        if (balance < 0)
        {
            debit = 0;
            credit = -balance;
        }
        else
        {
            debit = balance;
            credit = 0;
        }

        writer.WriteElementString("balance", XmlConvert.ToString(balance));
        writer.WriteElementString("previous", XmlConvert.ToString(previous));
        writer.WriteElementString("debit", XmlConvert.ToString(debit));
        writer.WriteElementString("credit", XmlConvert.ToString(credit));
        writer.WriteEndElement();
    }

    private void WriteTransactionXml(XmlWriter writer, Transaction value)
    {
        writer.WriteStartElement("transaction");
        writer.WriteElementString("posted", XmlConvert.ToString(value.Posted, XmlDateTimeSerializationMode.Utc));

        if (value.Number == 0)
        {
            writer.WriteElementString("number", value: null);
        }
        else
        {
            writer.WriteElementString("number", XmlConvert.ToString(value.Number));
        }

        writer.WriteElementString("name", value.Name);

        IOrderedEnumerable<Line> lines = value.OrderedLines.ThenBy(x => Company.Accounts[x.AccountId].Number);

        foreach (Line line in lines)
        {
            WriteLineXml(writer, line);
        }

        writer.WriteEndElement();
    }

    private void WriteLineXml(XmlWriter writer, Line value)
    {
        writer.WriteStartElement("line");
        writer.WriteElementString("account", Company.Accounts[value.AccountId].Name);
        writer.WriteElementString("debit", XmlConvert.ToString(value.Debit));
        writer.WriteElementString("credit", XmlConvert.ToString(value.Credit));
        writer.WriteElementString("description", value.Description);
        writer.WriteEndElement();
    }

    public void WriteXml(XmlWriter writer)
    {
        writer.WriteElementString("started", XmlConvert.ToString(Started, XmlDateTimeSerializationMode.Utc));
        writer.WriteElementString("posted", XmlConvert.ToString(Posted, XmlDateTimeSerializationMode.Utc));
        writer.WriteElementString("title", Title);
        writer.WriteStartElement("company");
        writer.WriteElementString("name", Company.Name ?? Liber.Properties.Resources.DefaultCompanyName);
        writer.WriteElementString("type", Company.Type.ToString());

        foreach (Account account in Accounts.Values)
        {
            WriteAccountXml(writer, account);
        }

        foreach (Transaction transaction in Company.GetTransactionsBetween(Started, Posted))
        {
            WriteTransactionXml(writer, transaction);
        }

        writer.WriteEndElement();
    }
}
