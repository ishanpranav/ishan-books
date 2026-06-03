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
using System.Text.Json.Serialization;

namespace Liber.Forms.Reports.Html;

[ClassInterface(ClassInterfaceType.AutoDual)]
[ComVisible(true)]
public class HtmlReport : IntervalView
{
    private static readonly JsonSerializerOptions s_options = new JsonSerializerOptions(FormattedStrings.JsonOptions);

    public HtmlReport(string name, Company company) : base(name, company)
    {
        s_options.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
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

    private IEnumerable<(string, AccountType, Color, BalanceInfo)> GetBalances(bool partition = false)
    {
        switch (Level)
        {
            case ReportLevel.ByAccount:
                if (partition)
                {
                    return Company
                        .GetBalancesByTypeAndParent(Accounts.Values, ReportTypes.None, Started, Posted, Filter)
                        .Select(x => (x.Parent.Name, x.Type, Company.GetColorOrDefault(x.Parent), x.Balances));
                }

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

        return JsonSerializer.Serialize(new ChartJSChartData(labels, datasets), s_options);
    }

    // https://github.com/ishanpranav/codebook/blob/master/lib/hashes/djb2_hash.c
    private static ulong Djb2(string value, int offset, int length)
    {
        ulong result = 5381;

        for (int i = offset; i < offset + length; i++)
        {
            result = ((result << 5) + result) + value[i];
        }

        return result;
    }

    private static Color GetBaseColor(AccountType type, decimal debit)
    {
        decimal balance = type.ToBalance(debit);

        if (type == AccountType.Equity)
        {
            return Colors.DarkAmethyst;
        }

        if (type.IsTemporary())
        {
            if (type.IsDebit(balance))
            {
                return Colors.HoneyBronze;
            }

            return Colors.JungleTeal;
        }

        if (type.IsDebit(balance))
        {
            return Colors.BlueBell;
        }

        return Colors.TigerFlame;
    }

    private Color GetColorOrDefault(string name, AccountType type, Color color, decimal debit)
    {
        if (color == Company.Color)
        {
            ulong hash = Djb2(name, offset: 0, name.Length) >> 16;
            bool tint = (hash & 0x1) == 0;

            hash >>= 1;

            double factor = 0.35 + (hash % 100) / 100d * 0.5;

            Color baseColor = GetBaseColor(type, debit);
            Color backgroundColor = tint
                ? baseColor.Tint(factor)
                : baseColor.Shade(factor);

            return backgroundColor;
        }

        return color;
    }

    public string GetPieChart()
    {
        List<string> labels = new List<string>();
        List<double> data = new List<double>();
        List<Color> backgroundColors = new List<Color>();

        foreach ((string name, AccountType type, Color color, BalanceInfo balances) in GetBalances())
        {
            if (balances.Balance == 0)
            {
                continue;
            }

            labels.Add(name);
            data.Add(double.Round(double.Abs((double)balances.Balance), 2));
            backgroundColors.Add(GetColorOrDefault(name, type, color, balances.Balance));
        }

        ChartJSChartDataset dataset = new ChartJSChartDataset(data)
        {
            BackgroundColors = backgroundColors
        };

        return JsonSerializer.Serialize(new ChartJSChartData(labels, new ChartJSChartDataset[]
        {
            dataset
        }), s_options);
    }

    public string GetAccountMap()
    {
        List<ChartJSChartDatasetTree> nodes = new List<ChartJSChartDatasetTree>();
        HashSet<AccountType> types = new HashSet<AccountType>();
        IEnumerable<(string, AccountType Type, Color, BalanceInfo)> accounts =
            GetBalances(partition: true);

        accounts = accounts.OrderBy(x => x.Type.ToInt32());

        foreach ((string name, AccountType type, Color color, BalanceInfo balances) in accounts)
        {
            string parent = FormattedStrings.GetString(type.ToString());

            if (types.Add(type))
            {
                nodes.Add(new ChartJSChartDatasetTree(parent));
            }

            Color backgroundColor = GetColorOrDefault(name, type, color, balances.Balance);

            nodes.Add(new ChartJSChartDatasetTree(name)
            {
                Parent = parent,
                Value = double.Round(double.Abs((double)balances.Balance), 2),
                Color = backgroundColor.GetForeColor(),
                BackgroundColor = backgroundColor
            });
        }

        return JsonSerializer.Serialize(nodes, s_options);
    }

    private static Color GetHeatColor(double value, double min, double max, AccountType type)
    {
        if (value == 0)
        {
            return Colors.Light;
        }

        bool isTemporary = type.IsTemporary();
        bool isDebit = type.IsDebit(double.IsPositiveInfinity(value)
            ? decimal.MaxValue
            : (decimal)value);
        double t;

        if ((isTemporary && isDebit) || (!isTemporary && !isDebit))
        {
            if (double.IsPositiveInfinity(value))
            {
                return Colors.Danger;
            }

            double range = min;

            if (range == 0)
            {
                return Colors.Danger;
            }

            t = double.Clamp(value / range, min: 0, max: 1);

            return Colors.Danger.Mix(Colors.Light, 1 - t);
        }

        if (double.IsPositiveInfinity(value))
        {
            return Colors.Danger;
        }

        if (max == 0)
        {
            return Colors.Success;
        }

        t = double.Clamp(value / max, min: 0, max: 1);

        return Colors.Success.Mix(Colors.Light, 1 - t);
    }

    public string GetHeatAccountMap()
    {
        List<ChartJSChartDatasetTree> nodes = new List<ChartJSChartDatasetTree>();
        HashSet<AccountType> types = new HashSet<AccountType>();
        IEnumerable<(string, AccountType Type, Color, BalanceInfo)> accounts =
            GetBalances(partition: true);

        accounts = accounts.OrderBy(x => x.Type.ToInt32());

        double min = 0;
        double max = 0;
        List<(double Value, AccountType Type)> values = new List<(double, AccountType)>();

        foreach ((string name, AccountType type, Color _, BalanceInfo balances) in accounts)
        {
            double value = balances.Previous == 0
                ? double.PositiveInfinity
                : double.Round((double)type.ToBalance((balances.Balance / balances.Previous) - 1) * 100, digits: 2);

            if (value < min)
            {
                min = value;
            }

            if (value > max)
            {
                max = value;
            }

            nodes.Add(new ChartJSChartDatasetTree(name)
            {
                Parent = FormattedStrings.GetString(type.ToString()),
                Value = (double)type.ToBalance(balances.Balance)
            });
            values.Add((value, type));
            types.Add(type);
        }

        for (int i = 0; i < nodes.Count; i++)
        {
            Color backgroundColor = GetHeatColor(values[i].Value, min, max, values[i].Type);

            nodes[i].Color = backgroundColor.GetForeColor();
            nodes[i].BackgroundColor = backgroundColor;
        }

        foreach (AccountType type in types)
        {
            nodes.Add(new ChartJSChartDatasetTree(FormattedStrings.GetString(type.ToString())));
        }

        return JsonSerializer.Serialize(nodes, s_options);
    }

    public string GetSankeyDiagram()
    {
        List<ChartJSChartDatasetSankeyData> data = new List<ChartJSChartDatasetSankeyData>();
        List<(string, AccountType, Color, BalanceInfo)> balances = GetBalances(partition: true).ToList();
        decimal grossProfit = 0;
        decimal operatingIncome = 0;
        decimal pretaxIncome = 0;
        decimal netIncome = 0;

        foreach ((string Name, AccountType Type, Color Color, BalanceInfo Balances) in balances)
        {
            decimal flow = Type.ToBalance(Balances.Balance);

            switch (Type)
            {
                case AccountType.Income:
                    grossProfit += flow;
                    break;
                case AccountType.Cost:
                    grossProfit -= flow;
                    break;
                case AccountType.Expense:
                    operatingIncome -= flow;
                    break;
                case AccountType.OtherIncomeExpense:
                    pretaxIncome += flow;
                    break;
                case AccountType.IncomeTaxExpense:
                    netIncome -= flow;
                    break;
            }
        }

        operatingIncome += grossProfit;
        pretaxIncome += operatingIncome;
        netIncome += pretaxIncome;

        foreach ((string Name, AccountType Type, Color Color, BalanceInfo Balances) in balances)
        {
            if (!Type.IsTemporary())
            {
                continue;
            }

            double flow = (double)Type.ToBalance(Balances.Balance);

            if (flow == 0)
            {
                continue;
            }

            string typeString = FormattedStrings.GetString(Type.ToString());
            Color accountColor = GetColorOrDefault(Name, Type, Color, Balances.Balance);
            Color typeColor = GetBaseColor(Type, Type.IsDebit(1) ? 1 : -1);

            switch (Type)
            {
                case AccountType.Income:
                    data.Add(new ChartJSChartDatasetSankeyData(Name, typeString, flow)
                    {
                        FromColor = accountColor,
                        ToColor = typeColor
                    });
                    data.Add(new ChartJSChartDatasetSankeyData(typeString, FormattedStrings.GrossProfit, flow)
                    {
                        FromColor = typeColor,
                        ToColor = Colors.Primary
                    });
                    break;

                case AccountType.Cost:
                    data.Add(new ChartJSChartDatasetSankeyData(FormattedStrings.GrossProfit, typeString, flow)
                    {
                        FromColor = Colors.Primary,
                        ToColor = typeColor
                    });
                    data.Add(new ChartJSChartDatasetSankeyData(typeString, Name, flow)
                    {
                        FromColor = typeColor,
                        ToColor = accountColor
                    });
                    break;

                case AccountType.Expense:
                    data.Add(new ChartJSChartDatasetSankeyData(FormattedStrings.OperatingIncome, typeString, flow)
                    {
                        FromColor = Colors.Primary,
                        ToColor = typeColor
                    });
                    data.Add(new ChartJSChartDatasetSankeyData(typeString, Name, flow)
                    {
                        FromColor = typeColor,
                        ToColor = accountColor
                    });
                    break;

                case AccountType.OtherIncomeExpense:
                    data.Add(new ChartJSChartDatasetSankeyData(Name, typeString, flow)
                    {
                        FromColor = accountColor,
                        ToColor = typeColor
                    });
                    data.Add(new ChartJSChartDatasetSankeyData(typeString, FormattedStrings.PretaxIncome, flow)
                    {
                        FromColor = typeColor,
                        ToColor = Colors.Primary
                    });
                    break;

                case AccountType.IncomeTaxExpense:
                    data.Add(new ChartJSChartDatasetSankeyData(FormattedStrings.PretaxIncome, typeString, flow)
                    {
                        FromColor = Colors.Primary,
                        ToColor = typeColor
                    });
                    data.Add(new ChartJSChartDatasetSankeyData(typeString, Name, flow)
                    {
                        FromColor = typeColor,
                        ToColor = accountColor
                    });
                    break;
            }
        }

        if (grossProfit > 0)
        {
            data.Add(new ChartJSChartDatasetSankeyData(FormattedStrings.GrossProfit, FormattedStrings.OperatingIncome, (double)grossProfit)
            {
                FromColor = Colors.Primary,
                ToColor = Colors.Primary
            });
        }

        if (operatingIncome > 0)
        {
            data.Add(new ChartJSChartDatasetSankeyData(FormattedStrings.OperatingIncome, FormattedStrings.PretaxIncome, (double)operatingIncome)
            {
                FromColor = Colors.Primary,
                ToColor = Colors.Primary
            });
        }

        if (pretaxIncome > 0)
        {
            data.Add(new ChartJSChartDatasetSankeyData(FormattedStrings.PretaxIncome, FormattedStrings.NetIncome, (double)pretaxIncome)
            {
                FromColor = Colors.Primary,
                ToColor = Colors.Primary
            });
        }

        return JsonSerializer.Serialize(data, s_options);
    }
}
