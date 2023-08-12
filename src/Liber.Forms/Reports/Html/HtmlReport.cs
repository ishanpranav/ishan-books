// HtmlReport.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.Json;
using Liber.Forms.Accounts;

namespace Liber.Forms.Reports.Html;

[ClassInterface(ClassInterfaceType.AutoDual)]
[ComVisible(true)]
public class HtmlReport
{
    private DateTime _started = new DateTime(DateTime.Today.Year, 1, 1);
    private DateTime _posted = DateTime.Today;

    public HtmlReport(string title, Company company)
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
    public string Title { get; set; } = string.Empty;

    [LocalizedCategory(nameof(Accounts))]
    [LocalizedDescription(nameof(Accounts))]
    [LocalizedDisplayName(nameof(Accounts))]
    public AccountsView Accounts { get; set; }

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

    [LocalizedCategory(nameof(Periodicity))]
    [LocalizedDescription(nameof(Periodicity))]
    [LocalizedDisplayName(nameof(Periodicity))]
    public Periodicity Periodicity { get; set; }

    private DateTime Start(DateTime current)
    {
        CultureInfo culture = CultureInfo.CurrentCulture;
        DayOfWeek first = culture.DateTimeFormat.FirstDayOfWeek;
        DayOfWeek actual = current.DayOfWeek;

        switch (Periodicity)
        {
            case Periodicity.Weekly:
                return culture.Calendar.AddDays(current, first - actual);

            case Periodicity.Biweekly:
                return culture.Calendar.AddDays(current, first - actual + 14);

            case Periodicity.Monthly:
                return new DateTime(current.Year, current.Month, 1);

            case Periodicity.Annually:
                return new DateTime(current.Year, 1, 1);

            default:
                return current;
        }
    }

    private DateTime Next(DateTime current)
    {
        Calendar calendar = CultureInfo.CurrentCulture.Calendar;

        switch (Periodicity)
        {
            case Periodicity.Daily:
                return calendar.AddDays(current, days: 1);

            case Periodicity.Weekly:
                return calendar.AddWeeks(current, weeks: 1);

            case Periodicity.Biweekly:
                return calendar.AddWeeks(current, weeks: 2);

            case Periodicity.Monthly:
                return calendar.AddMonths(current, months: 1);

            case Periodicity.Annually:
                return calendar.AddYears(current, years: 1);

            default:
                return Posted;
        }
    }

    private IEnumerable<(DateTime started, DateTime posted)> EnumerateRanges()
    {
        DateTime current = Start(Started);

        while (current <= Posted)
        {
            DateTime next = Next(current);

            yield return (current, next);

            current = CultureInfo.CurrentCulture.Calendar.AddDays(next, days: 1);
        }
    }

    public string GetTimeSeries()
    {
        List<string> labels = new List<string>();
        List<ChartJSChartDataset> datasets = new List<ChartJSChartDataset>();

        foreach ((DateTime started, DateTime posted) in EnumerateRanges())
        {
            labels.Add(started.ToShortDateString() + " \u2013 " + posted.ToShortDateString());
        }

        foreach (Account account in Accounts.Values)
        {
            int label = 0;
            bool nonZero = false;
            double[] data = new double[labels.Count];

            foreach ((DateTime started, DateTime posted) in EnumerateRanges())
            {
                if (account.Type.IsTemporary())
                {
                    data[label] = (double)account.Type.ToBalance(account.GetBalance(started, posted));
                }
                else
                {
                    data[label] = (double)account.Type.ToBalance(account.GetBalance(posted));
                }

                if (data[label] != 0)
                {
                    nonZero = true;
                }

                label++;
            }

            if (nonZero)
            {
                datasets.Add(new ChartJSChartDataset(data)
                {
                    Label = account.Name,
                    BorderWidth = 1,
                    LineTension = 0.25
                });
            }
        }

        return JsonSerializer.Serialize(new ChartJSChartData(labels, datasets), FormattedStrings.JsonOptions);
    }

    public string GetAnalysis()
    {
        List<string> labels = new List<string>();
        List<double> data = new List<double>();

        foreach (Account account in Accounts.Values)
        {
            decimal balance;

            if (account.Type.IsTemporary())
            {
                balance = account.GetBalance(Started, Posted);
            }
            else
            {
                balance = account.GetBalance(Posted);
            }

            if (balance != 0)
            {
                labels.Add(account.Name);
                data.Add((double)account.Type.ToBalance(balance));
            }
        }

        return JsonSerializer.Serialize(new ChartJSChartData(labels, new ChartJSChartDataset[] { new ChartJSChartDataset(data) }), FormattedStrings.JsonOptions);
    }
}
