// IntervalView.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Liber.Forms.Accounts;

namespace Liber.Forms.Reports;

public abstract class IntervalView : IStandardValuesProvider
{
    private DateTime _started;
    private DateTime _posted;

    [Browsable(false)]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="Company"/> associated with the report.
    /// </summary>
    /// <value>The company associated with the report.</value>
    [Browsable(false)]
    public Company Company { get; set; }

    [LocalizedCategory(nameof(Title))]
    [LocalizedDescription(nameof(Title))]
    [LocalizedDisplayName(nameof(Title))]
    [TypeConverter(typeof(StandardValuesStringConverter))]
    public string Title { get; set; } = string.Empty;

    [DefaultValue(ReportLevel.ByAccount)]
    [LocalizedCategory(nameof(Level))]
    [LocalizedDescription(nameof(Level))]
    [LocalizedDisplayName(nameof(Level))]
    [TypeConverter(typeof(LocalizedEnumConverter<ReportLevel>))]
    public ReportLevel Level { get; set; } = ReportLevel.ByAccount;

    [LocalizedCategory(nameof(Accounts))]
    [LocalizedDescription(nameof(Accounts))]
    [LocalizedDisplayName(nameof(Accounts))]
    public AccountsView Accounts { get; set; }

    [LocalizedCategory(nameof(Filter))]
    [LocalizedDescription(nameof(Filter))]
    [LocalizedDisplayName(nameof(Filter))]
    [TypeConverter(typeof(RegexConverter))]
    public Regex Filter { get; set; } = Filters.Any();

    [LocalizedCategory(nameof(Started))]
    [LocalizedDescription(nameof(Started))]
    [LocalizedDisplayName(nameof(Started))]
    public DateTime Started
    {
        get
        {
            return _started.Date;
        }
        set
        {
            if (value.Date > _posted)
            {
                _posted = value.Date;
            }

            _started = value.Date;
        }
    }

    [LocalizedCategory(nameof(Posted))]
    [LocalizedDescription(nameof(Posted))]
    [LocalizedDisplayName(nameof(Posted))]
    public DateTime Posted
    {
        get
        {
            return _posted.Date;
        }
        set
        {
            if (value < _started)
            {
                _started = value.Date;
            }

            _posted = value.Date;
        }
    }

    protected IntervalView(string name, Company company)
    {
        Name = name;
        Company = company;
        Accounts = new AccountsView(Company);
        Title = FormattedStrings.GetTitle(Name, Company.Type);
        Started = Company.Started;
        Posted = Company.Posted;
        company.NameChanged += OnCompanyChanged;
        company.TypeChanged += OnCompanyChanged;
        company.ReportingChanged += (sender, e) =>
        {
            Started = Company.Started;
            Posted = Company.Posted;
        };
    }

    private void OnCompanyChanged(object? sender, EventArgs e)
    {
        Title = FormattedStrings.GetTitle(Name, Company.Type);
    }

    public TypeConverter.StandardValuesCollection GetStandardValues()
    {
        SortedSet<string> results = new SortedSet<string>(FormattedStrings
            .GetStringsBySuffix(Name));

        return new TypeConverter.StandardValuesCollection(results);
    }
}
