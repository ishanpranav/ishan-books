// XslReport.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Humanizer;

namespace Liber.Forms.Reports.Xsl;

/// <summary>
/// Represents an XSL template for generating formatted financial reports.
/// </summary>
[XmlRoot("report")]
public class XslReport : IntervalView, IXmlSerializable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="XslReport"/> class.
    /// </summary>
    public XslReport() : base(string.Empty, new Company()) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="XslReport"/> class with the specified title and company.
    /// </summary>
    /// <param name="name">The name of the report.</param>
    /// <param name="company">The <see cref="Company"/> associated with the report.</param>
    public XslReport(string name, Company company) : base(name, company) { }

    [Browsable(false)]
    public string GenericTitle
    {
        get
        {
            return FormattedStrings.GetTitle(Name, CompanyType.None);
        }
    }

    [LocalizedCategory(nameof(Redaction))]
    [LocalizedDescription(nameof(Redaction))]
    [LocalizedDisplayName(nameof(Redaction))]
    public string? Redaction { get; set; }

    [Browsable(false)]
    public ReportTypes Type { get; set; } = ReportTypes.CurrentPosted;

    [DefaultValue(typeof(decimal), "0.01")]
    [LocalizedCategory(nameof(Multiple))]
    [LocalizedDescription(nameof(Multiple))]
    [LocalizedDisplayName(nameof(Multiple))]
    public decimal Multiple { get; set; } = 0.01m;

    /// <summary>
    /// Formats a date value as a long date string.
    /// </summary>
    /// <remarks>This method corresponds to the <c>liber:fdatel</c> XSL function.</remarks>
    /// <param name="value">The date value to format.</param>
    /// <returns>The formatted long date string.</returns>
    [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
    public string fdatel(DateTime value)
    {
        return value.ToLongDateString();
    }

    [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
    public string fdates(DateTime value)
    {
        return value.ToShortDateString();
    }

    [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
    public string fm(decimal value)
    {
        if (!string.IsNullOrEmpty(Redaction))
        {
            return Redaction;
        }

        return value.ToLocalizedString(Multiple);
    }

    [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
    public string fm(string type, decimal balance)
    {
        return fm(Enum
            .Parse<AccountType>(type)
            .ToBalance(balance));
    }

    [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
    public string fp(double percentage)
    {
        if (!string.IsNullOrEmpty(Redaction))
        {
            return Redaction;
        }

        double rounded = double.Round(percentage, digits: 3, MidpointRounding.ToEven);

        if (rounded == 0)
        {
            if (percentage > 0)
            {
                return " 0.0% ";
            }

            if (percentage < 0)
            {
                return "(0.0%)";
            }
        }

        return rounded.ToString(" #,##0.0% ;(#,##0.0%);   -  ");
    }

    public string fp(string type, decimal balance, decimal denominator)
    {
        if (denominator == 0)
        {
            if (!string.IsNullOrEmpty(Redaction))
            {
                return Redaction;
            }

            return "   -  ";
        }

        return fp((double)(Enum
            .Parse<AccountType>(type)
            .ToBalance(balance) / denominator));
    }

    [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
    public string ftspanl(DateTime started, DateTime posted)
    {
        return started.ToShortDateString() + " \u2013 " + posted.ToShortDateString();
    }

    [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
    public string gettitle(string name, string type)
    {
        return FormattedStrings.GetTitle(name, Enum.Parse<CompanyType>(type));
    }

    /// <summary>
    /// Formats the time span between two dates, representing an accounting period, as a human-readable string.
    /// </summary>
    /// <remarks>This method corresponds to the <c>liber:ftspans</c> XSL function.</remarks>
    /// <param name="started">The start date of the accounting period.</param>
    /// <param name="posted">The end date of the accounting period.</param>
    /// <returns>A human-readable string representing the time span between the start and end dates.</returns>
    [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
    public string ftspans(DateTime started, DateTime posted)
    {
        DateTime effectiveEnd = posted;

        posted = posted.Date.AddDays(1);

        if (effectiveEnd.Day != 1)
        {
            effectiveEnd = posted;
        }

        if (posted == started.Date.AddYears(1))
        {
            return started.Year.ToString();
        }

        if (posted == started.Date.AddMonths(1))
        {
            DateTime month = new DateTime(started.Year, started.Month, 1);

            return month.ToString("MMMM yyyy");
        }

        if (started.Day == 1 && effectiveEnd.Day == 1)
        {
            int months = ((effectiveEnd.Year - started.Year) * 12) + effectiveEnd.Month - started.Month;

            return TimeSpan
                .FromDays(months * 32)
                .Humanize(precision: 2, countEmptyUnits: true, maxUnit: TimeUnit.Year);
        }

        return (posted - started.Date).Humanize(precision: 2, countEmptyUnits: true, maxUnit: TimeUnit.Year);
    }

    [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
    public string fgets(string key, object value)
    {
        return string
            .Format(gets(key), value)
            .Transform(To.SentenceCase);
    }

    [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
    public string fgets(string key, object first, object second)
    {
        return string
            .Format(gets(key), first, second)
            .Transform(To.SentenceCase);
    }

    [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
    [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
    public string pngets(string key, decimal value)
    {
        //if (value < 0)
        //{
        //    string? result = FormattedStrings.ResourceManager.GetString("_n_" + key);

        //    if (result != null)
        //    {
        //        return result;
        //    }
        //}

        return gets("_p_" + key);
    }

    [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
    public string gets(string key)
    {
        return FormattedStrings.GetString(key);
    }

    public XmlSchema? GetSchema()
    {
        return null;
    }

    void IXmlSerializable.ReadXml(XmlReader reader)
    {
        throw new NotImplementedException();
    }

    private void WriteAccountXml(XmlWriter writer, Account value, ParentKey key, BalanceInfo balances)
    {
        writer.WriteStartElement("account");
        writer.WriteElementString("name", value.Name);
        writer.WriteElementString("type", key.Type.ToString());

        decimal debit;
        decimal credit;

        writer.WriteElementString("equity", XmlConvert.ToString(value == Company.Accounts[Company.EquityAccountId]));
        writer.WriteElementString("other-equity", XmlConvert.ToString(value == Company.Accounts[Company.OtherEquityAccountId]));

        if (balances.Balance < 0)
        {
            debit = 0;
            credit = -balances.Balance;
        }
        else
        {
            debit = balances.Balance;
            credit = 0;
        }

        writer.WriteElementString("balance", XmlConvert.ToString(balances.Balance));
        writer.WriteElementString("average-daily-balance", XmlConvert.ToString(balances.AverageDailyBalance));
        writer.WriteElementString("previous", XmlConvert.ToString(balances.Previous));
        writer.WriteElementString("debit", XmlConvert.ToString(debit));
        writer.WriteElementString("credit", XmlConvert.ToString(credit));
        writer.WriteElementString("cash-flow", key.CashFlow.ToString());
        writer.WriteElementString("tax-type", XmlConvert.ToString(key.TaxType));
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
        writer.WriteElementString("other-equity", XmlConvert.ToString(value.Lines.Any(x => x.AccountId == Company.OtherEquityAccountId)));

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

        Line? sibling = value.Sibling;

        if (sibling != null)
        {
            writer.WriteElementString("sibling", Company.Accounts[sibling.AccountId].Name);
        }
        else
        {
            writer.WriteElementString("sibling", "Various");
        }

        writer.WriteElementString("debit", XmlConvert.ToString(value.Debit));
        writer.WriteElementString("credit", XmlConvert.ToString(value.Credit));
        writer.WriteElementString("description", value.Description);
        writer.WriteElementString("other-equity", XmlConvert.ToString(value.AccountId == Company.OtherEquityAccountId));
        writer.WriteEndElement();
    }

    public void WriteXml(XmlWriter writer)
    {
        writer.WriteElementString("started", XmlConvert.ToString(Started, XmlDateTimeSerializationMode.Utc));
        writer.WriteElementString("posted", XmlConvert.ToString(Posted, XmlDateTimeSerializationMode.Utc));
        writer.WriteElementString("title", Title);
        writer.WriteStartElement("company");
        writer.WriteElementString("name", Company.DisplayName);
        writer.WriteElementString("type", Company.Type.ToString());
        writer.WriteElementString("detail", XmlConvert.ToString(Level != ReportLevel.ByType));
        writer.WriteElementString("multiple", FormattedStrings.GetMultipleWords(Multiple));

        if (Level == ReportLevel.ByAccount)
        {
            foreach ((Account account, ParentKey key, BalanceInfo balances) in Company.GetBalancesByKey(
                Accounts.Values,
                Type,
                Started,
                Posted,
                Filter))
            {
                WriteAccountXml(writer, account, key, balances);
            }
        }
        else
        {
            foreach ((Account account, BalanceInfo balances) in Company.GetBalances(Accounts.Values, Type, Started, Posted, Filter))
            {
                WriteAccountXml(writer, account, new ParentKey(account), balances);
            }
        }

        foreach (Transaction transaction in Company
            .GetTransactionsBetween(Started, Posted)
            .Where(transaction => transaction.Lines
                .Any(line => Accounts.Values
                    .Contains(Company.Accounts[line.AccountId]))))
        {
            WriteTransactionXml(writer, transaction);
        }

        writer.WriteEndElement();
    }
}
