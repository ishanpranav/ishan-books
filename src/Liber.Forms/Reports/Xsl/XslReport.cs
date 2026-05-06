// XslReport.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Humanizer;
using Humanizer.Localisation;
using Liber.Forms.Accounts;

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
    public XslReport()
    {
        Title = string.Empty;
        Company = new Company();
        Accounts = new AccountsView(Company);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="XslReport"/> class with the specified title and company.
    /// </summary>
    /// <param name="title">The title of the report.</param>
    /// <param name="company">The <see cref="Company"/> associated with the report.</param>
    public XslReport(string title, Company company)
    {
        Title = title;
        Company = company;
        Accounts = new AccountsView(company);
    }

    /// <summary>
    /// Gets or sets the <see cref="Company"/> associated with the report.
    /// </summary>
    /// <value>The company associated with the report.</value>
    [Browsable(false)]
    public Company Company { get; set; }

    [LocalizedCategory(nameof(Title))]
    [LocalizedDescription(nameof(Title))]
    [LocalizedDisplayName(nameof(Title))]
    public string Title { get; set; }

    [LocalizedCategory(nameof(Redaction))]
    [LocalizedDescription(nameof(Redaction))]
    [LocalizedDisplayName(nameof(Redaction))]
    public string? Redaction { get; set; }

    [LocalizedCategory(nameof(Filter))]
    [LocalizedDescription(nameof(Filter))]
    [LocalizedDisplayName(nameof(Filter))]
    [TypeConverter(typeof(RegexConverter))]
    public Regex Filter { get; set; } = Filters.Any();

    [Browsable(false)]
    public ReportTypes Type { get; set; }

    [DefaultValue(ReportLevel.ByAccount)]
    [LocalizedCategory(nameof(Level))]
    [LocalizedDescription(nameof(Level))]
    [LocalizedDisplayName(nameof(Level))]
    [TypeConverter(typeof(LocalizedEnumConverter))]
    public ReportLevel Level { get; set; } = ReportLevel.ByAccount;

    [LocalizedCategory(nameof(Accounts))]
    [LocalizedDescription(nameof(Accounts))]
    [LocalizedDisplayName(nameof(Accounts))]
    public AccountsView Accounts { get; set; }

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
        if (!string.IsNullOrEmpty(Redaction))
        {
            return Redaction;
        }

        return value.ToLocalizedString(Multiple);
    }

    public string fm(string type, decimal balance)
    {
        if (!string.IsNullOrEmpty(Redaction))
        {
            return Redaction;
        }

        return Enum
            .Parse<AccountType>(type)
            .ToBalance(balance)
            .ToLocalizedString(Multiple);
    }

    public string ftspanl(DateTime started, DateTime posted)
    {
        return started.ToShortDateString() + " \u2013 " + posted.ToShortDateString();
    }

    /// <summary>
    /// Formats the time span between two dates, representing an accounting period, as a human-readable string.
    /// </summary>
    /// <remarks>This method corresponds to the <c>liber:ftspans</c> XSL function.</remarks>
    /// <param name="started">The start date of the accounting period.</param>
    /// <param name="posted">The end date of the accounting period.</param>
    /// <returns>A human-readable string representing the time span between the start and end dates.</returns>
    public string ftspans(DateTime started, DateTime posted)
    {
        posted = posted.Date.AddDays(1);

        if (posted == started.Date.AddYears(1))
        {
            return started.Year.ToString();
        }

        if (posted == started.Date.AddMonths(1))
        {
            DateTime month = new DateTime(started.Year, started.Month, 1);

            return month.ToString("MMMM yyyy");
        }

        return (posted - started.Date).Humanize(precision: 2, countEmptyUnits: true, maxUnit: TimeUnit.Year);
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

    private BalanceInfo ComputeBalances(Account value)
    {
        if (value == Company.Accounts[Company.EquityAccountId])
        {
            BalanceInfo result = new BalanceInfo()
            {
                Previous = Company.GetEquity(Started.AddDays(-1), Filter)
            };

            if (Type.HasFlag(ReportTypes.CurrentPosted))
            {
                result.Balance = Company.GetEquity(Posted, Filter);
            }

            if (Type.HasFlag(ReportTypes.CurrentStarted))
            {
                result.Balance = Company.GetEquity(Started.AddDays(-1), Filter);
            }

            return result;
        }

        if (value == Company.Accounts[Company.OtherEquityAccountId])
        {
            return new BalanceInfo()
            {
                Balance = value.GetBalance(Posted, Filter),
                Previous = value.GetBalance(Started, Filter)
            };
        }

        if (value.Type.IsTemporary())
        {
            return new BalanceInfo()
            {
                Balance = value.GetBalance(Started, Posted, Filter)
            };
        }

        return new BalanceInfo()
        {
            Balance = value.GetBalance(Posted, Filter),
            Previous = value.GetBalance(Started.AddDays(-1), Filter),
            AverageDailyBalance = value.GetAverageDailyBalance(Started, Posted, Filter)
        };
    }

    private void ComputeSubtreeBalances(Account value, Dictionary<ParentKey, BalanceInfo> keyedBalances)
    {
        BalanceInfo balances = ComputeBalances(value);
        IEnumerable<Account> visibleChildren = value.Children.Where(Accounts.Values.Contains);

        if (!visibleChildren.Any())
        {
            ParentKey key = new ParentKey(value.Type, value.CashFlow);

            if (keyedBalances.TryGetValue(key, out BalanceInfo? existing))
            {
                existing.Balance += balances.Balance;
                existing.Previous += balances.Previous;
                existing.AverageDailyBalance += balances.AverageDailyBalance;
            }
            else
            {
                keyedBalances[key] = balances;
            }
        }

        foreach (Account child in visibleChildren)
        {
            ComputeSubtreeBalances(child, keyedBalances);
        }
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

        if (Level == ReportLevel.ByAccount)
        {
            foreach (Account account in Accounts.Values.Where(a => a.ParentId == Guid.Empty))
            {
                Dictionary<ParentKey, BalanceInfo> keyedBalances = new Dictionary<ParentKey, BalanceInfo>();

                ComputeSubtreeBalances(account, keyedBalances);

                foreach (KeyValuePair<ParentKey, BalanceInfo> entry in keyedBalances)
                {
                    WriteAccountXml(writer, account, entry.Key, entry.Value);
                }
            }
        }
        else
        {
            foreach (Account account in Accounts.Values)
            {
                WriteAccountXml(writer, account, new ParentKey(account.Type, account.CashFlow), ComputeBalances(account));
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
