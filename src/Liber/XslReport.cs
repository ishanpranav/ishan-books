// Report.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Resources;
using System.Xml.Serialization;
using Humanizer;
using Humanizer.Localisation;
using Liber;

namespace Liber;

[XmlRoot("report")]
public class XslReport
{
    private static readonly ResourceManager s_resourceManager = new ResourceManager(typeof(XslReport));

    private DateTime _started = new DateTime(DateTime.Today.Year, 1, 1);
    private DateTime _posted = DateTime.Today;

    public XslReport()
    {
        Company = new Company();
        Title = string.Empty;
    }

    public XslReport(string title, Company company)
    {
        Title = title;
        Company = company;
    }

    [Browsable(false)]
    [XmlElement("company")]
    public Company Company { get; set; }

    [LocalizedCategory(nameof(Title))]
    [LocalizedDescription(nameof(Title))]
    [LocalizedDisplayName(nameof(Title))]
    [XmlElement("title")]
    public string Title { get; set; }

    [LocalizedCategory(nameof(Started))]
    [LocalizedDescription(nameof(Started))]
    [LocalizedDisplayName(nameof(Started))]
    [XmlElement("started")]
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
    [XmlElement("posted")]
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

    [Browsable(false)]
    [LocalizedCategory(nameof(EquityMode))]
    [LocalizedDescription(nameof(EquityMode))]
    [LocalizedDisplayName(nameof(EquityMode))]
    [XmlIgnore]
    public EquityModes EquityMode { get; set; }

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

    public string ftspanl(DateTime from, DateTime to)
    {
        return from.ToShortDateString() + " \u2013 " + to.ToShortDateString();
    }

    public string ftspans(DateTime from, DateTime to)
    {
        return (to - from).Humanize(precision: 2, countEmptyUnits: true, maxUnit: TimeUnit.Year);
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
}
