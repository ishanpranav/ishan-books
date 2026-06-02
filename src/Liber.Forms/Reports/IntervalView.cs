using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Liber.Forms.Accounts;

namespace Liber.Forms.Reports;

public abstract class IntervalView : IStandardValuesProvider
{
    private DateTime _started = new DateTime(DateTime.Today.Year, month: 1, day: 1);
    private DateTime _posted = DateTime.Today;

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
        Accounts = new AccountsView(company);

        Refresh();
    }

    public TypeConverter.StandardValuesCollection GetStandardValues()
    {
        SortedSet<string> results = new SortedSet<string>(FormattedStrings
            .GetStringsBySuffix(Name));

        return new TypeConverter.StandardValuesCollection(results);
    }

    [MemberNotNull(nameof(Title))]
    public void Refresh()
    {
        Title = FormattedStrings.GetTitle(Name, Company.Type);
    }
}
