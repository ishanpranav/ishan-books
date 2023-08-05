// HtmlReport.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    [TypeConverter(typeof(LocalizedEnumConverter))]
    public Periodicity Periodicity { get; set; }

    public string GetTimeSeries()
    {
        TimeSpan increment = TimeSpan.FromDays(14);
        int labelCount = (int)Math.Ceiling((Posted - Started) / increment);
        string[] labels = new string[labelCount];
        List<ChartJSChartDataset> datasets = new List<ChartJSChartDataset>();

        for (int i = 1; i < labelCount; i++)
        {
            DateTime started = Started + (increment * (i - 1));
            DateTime posted = Started + (increment * i);

            labels[i] = started.ToShortDateString() + " \u2013 " + posted.ToShortDateString();
        }

        foreach (Account account in Accounts.Values)
        {
            bool nonZero = false;
            double[] data = new double[labelCount];

            for (int label = 1; label < labelCount; label++)
            {
                DateTime started = Started + (increment * (label - 1));
                DateTime posted = Started + (increment * label);

                if (account.Temporary)
                {
                    data[label] = Math.Abs((double)account.GetBalance(started, posted));
                }
                else
                {
                    data[label] = Math.Abs((double)account.GetBalance(posted));
                }

                if (data[label] != 0)
                {
                    nonZero = true;
                }
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
}
