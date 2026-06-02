// HtmlReport.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace Liber.Forms.Reports.Html;

[ClassInterface(ClassInterfaceType.AutoDual)]
[ComVisible(true)]
public class HtmlReport : IntervalView
{
    public HtmlReport(string name, Company company) : base(name, company) { }

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

    private IEnumerable<(string, AccountType, Color, BalanceInfo)> GetBalances()
    {
        switch (Level)
        {
            case ReportLevel.ByAccount:
                return Company
                    .GetBalancesByParent(Accounts.Values, ReportTypes.None, Started, Posted, Filter)
                    .Select(x => (x.Parent.Name, x.Parent.Type, Company.GetColorOrDefault(x.Parent), x.Balances));

            case ReportLevel.ByType:
                return Company
                    .GetBalancesByType(Accounts.Values, ReportTypes.None, Started, Posted, Filter)
                    .Select(x => (FormattedStrings.GetString(x.Type.ToString()), x.Type, Company.Color, x.Balances));
        }

        return Company
            .GetBalances(Accounts.Values, ReportTypes.None, Started, Posted, Filter)
            .Select(x => (x.Account.Name, x.Account.Type, Company.GetColorOrDefault(x.Account), x.Balances));
    }

    public string GetTimeSeries()
    {
        List<string> labels = new List<string>();
        List<ChartJSChartDataset> datasets = new List<ChartJSChartDataset>();

        foreach ((DateTime started, DateTime posted) in EnumerateRanges())
        {
            labels.Add(started.ToShortDateString() + " \u2013 " + posted.ToShortDateString());
        }

        foreach ((string name, AccountType type, Color color, BalanceInfo balances) in GetBalances())
        {
            int label = 0;
            bool nonZero = false;
            double[] data = new double[labels.Count];

            foreach ((DateTime started, DateTime posted) in EnumerateRanges())
            {
                data[label] = (double)type.ToBalance(balances.Balance);

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
                    Label = name,
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
        List<Color> backgroundColors = new List<Color>();
        bool hasColor = false;
        bool tint = true;

        foreach ((string name, AccountType type, Color color, BalanceInfo balances) in GetBalances())
        {
            if (balances.Balance == 0)
            {
                continue;
            }

            labels.Add(name);
            data.Add((double)type.ToBalance(balances.Balance));

            if (color == Company.Color)
            {
                if (tint)
                {
                    backgroundColors.Add(Colors.Primary.Tint(Random.Shared.NextDouble()));
                }
                else
                {
                    backgroundColors.Add(Colors.Primary.Shade(Random.Shared.NextDouble()));
                }

                tint = !tint;
            }
            else
            {
                hasColor = true;

                backgroundColors.Add(color);
            }
        }

        ChartJSChartDataset dataset = new ChartJSChartDataset(data);

        if (hasColor)
        {
            dataset.BackgroundColors = backgroundColors;
        }

        return JsonSerializer.Serialize(new ChartJSChartData(labels, new ChartJSChartDataset[]
        {
            dataset
        }), FormattedStrings.JsonOptions);
    }
}
