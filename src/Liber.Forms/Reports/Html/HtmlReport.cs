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
    private static readonly JsonSerializerOptions s_options = new JsonSerializerOptions(FormattedStrings.JsonOptions)
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.Never
    };

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

    private IEnumerable<(string, AccountType, Color, BalanceInfo)> GetBalancesByTypeAndParent()
    {
        if (Level == ReportLevel.ByAccount)
        {
            return Company
                .GetBalancesByTypeAndParent(Accounts.Values, ReportTypes.None, Started, Posted, Filter)
                .Select(x => (x.Parent.Name, x.Type, Company.GetColorOrDefault(x.Parent), x.Balances));
        }

        return GetBalances();
    }

    private IEnumerable<(string, AccountType, CashFlow, Color, BalanceInfo)> GetBalancesByTypeAndCashFlow()
    {
        IEnumerable<(Account Account, ParentKey Key, BalanceInfo Balances)> keyedBalances = Company
                .GetBalancesByKey(Accounts.Values, ReportTypes.None, Started, Posted, Filter);

        switch (Level)
        {
            case ReportLevel.ByAccount:
                return keyedBalances
                    .GroupBy(x => (x.Account.Name, x.Key.Type, x.Key.CashFlow))
                    .Select(x =>
                    {
                        BalanceInfo balances = new BalanceInfo();

                        foreach ((Account _, ParentKey _, BalanceInfo otherBalances) in x)
                        {
                            balances.Balance += otherBalances.Balance;
                            balances.Previous += otherBalances.Previous;
                            balances.AverageDailyBalance += otherBalances.AverageDailyBalance;
                        }

                        return (x.Key.Name, x.Key.Type, x.Key.CashFlow, Company.GetColorOrDefault(x.First().Account), balances);
                    });

            case ReportLevel.ByType:
                return keyedBalances
                    .GroupBy(x => (x.Key.Type, x.Key.CashFlow))
                    .Select(x =>
                    {
                        BalanceInfo balances = new BalanceInfo();

                        foreach ((Account _, ParentKey _, BalanceInfo otherBalances) in x)
                        {
                            balances.Balance += otherBalances.Balance;
                            balances.Previous += otherBalances.Previous;
                            balances.AverageDailyBalance += otherBalances.AverageDailyBalance;
                        }

                        return (FormattedStrings.GetString(x.Key.Type.ToString()), x.Key.Type, x.Key.CashFlow, Company.Color, balances);
                    });
        }

        return keyedBalances.Select(x => (x.Account.Name, x.Key.Type, x.Key.CashFlow, Company.GetColorOrDefault(x.Account), x.Balances));
    }

    private IEnumerable<(string Name, AccountType Type, Color Color, BalanceInfo Balances)> GetBalances()
    {
        switch (Level)
        {
            case ReportLevel.ByAccount:
                return Company
                    .GetBalancesByParent(Accounts.Values, ReportTypes.None, Started, Posted, Filter)
                    .OrderBy(x => x.Parent)
                    .Select(x => (x.Parent.Name, x.Parent.Type, Company.GetColorOrDefault(x.Parent), x.Balances));

            case ReportLevel.ByType:
                return Company
                    .GetBalancesByType(Accounts.Values, ReportTypes.None, Started, Posted, Filter)
                    .OrderBy(x => x.Type)
                    .Select(x => (FormattedStrings.GetString(x.Type.ToString()), x.Type, Company.Color, x.Balances));
        }

        return Company
            .GetBalances(Accounts.Values, ReportTypes.None, Started, Posted, Filter)
            .OrderBy(x => x.Account)
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

        foreach (Account account in Accounts.Values)
        {
            int label = 0;
            bool nonZero = false;
            double[] data = new double[labels.Count];
            Color[] colors = new Color[labels.Count];

            foreach ((DateTime started, DateTime posted) in EnumerateRanges())
            {
                decimal debit = account.Type.IsTemporary()
                    ? account.GetBalance(started, posted, Filter)
                    : account.GetBalance(posted, Filter);

                data[label] = (double)account.Type.ToBalance(debit);
                colors[label] = GetColorOrDefault(account.Name, account.Type, Company.GetColorOrDefault(account), debit);

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
                    LineTension = 0.25,
                    BackgroundColors = colors,
                    BorderColors = colors
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
            if (type.IsDebit(balance))
            {
                return Colors.IntenseCherry;
            }

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
            int offset = 0;
            int length = name.Length;

            while (offset < length && !char.IsLetter(name[offset]))
            {
                offset++;
                length--;
            }

            ulong hash = Djb2(name, offset, length) >> 16;
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
        IEnumerable<(string, AccountType Type, Color, BalanceInfo)> accounts = GetBalancesByTypeAndParent();

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
        IEnumerable<(string, AccountType Type, Color, BalanceInfo)> accounts = GetBalancesByTypeAndParent();

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

    private static void AddSankeyLink(
        List<ChartJSChartDatasetSankeyData> data,
        string positiveFrom,
        string positiveTo,
        decimal value,
        Color fromColor,
        Color toColor)
    {
        if (value == 0)
        {
            return;
        }

        if (value < 0)
        {
            string swapString = positiveFrom;

            positiveFrom = positiveTo;
            positiveTo = swapString;

            Color swapColor = fromColor;

            fromColor = toColor;
            toColor = swapColor;
            value = -value;
        }

        data.Add(new ChartJSChartDatasetSankeyData(positiveFrom, positiveTo, (double)value)
        {
            FromColor = fromColor,
            ToColor = toColor
        });
    }

    public string GetSankeyDiagram()
    {
        List<ChartJSChartDatasetSankeyData> data = new List<ChartJSChartDatasetSankeyData>();
        List<(string Name, AccountType, CashFlow CashFlow, Color, BalanceInfo)> balances = GetBalancesByTypeAndCashFlow().ToList();
        decimal grossProfit = 0;
        decimal operatingIncome = 0;
        decimal pretaxIncome = 0;
        decimal netIncome = 0;
        decimal totalIncome = 0;
        decimal totalCost = 0;
        decimal totalExpense = 0;
        decimal totalOtherIncomeExpense = 0;
        decimal totalIncomeTaxExpense = 0;
        decimal totalWorkingCapital = 0;
        decimal totalNonCash = 0;
        decimal totalGainLoss = 0;
        decimal totalInvesting = 0;
        decimal totalFinancing = 0;

        foreach ((string, AccountType Type, CashFlow CashFlow, Color, BalanceInfo Balances) entry in balances)
        {
            decimal flow = entry.Type.ToBalance(entry.Balances.Balance);

            switch (entry.Type)
            {
                case AccountType.Income:
                    grossProfit += flow;
                    totalIncome += flow;
                    break;

                case AccountType.Cost:
                    grossProfit -= flow;
                    totalCost += flow;
                    break;

                case AccountType.Expense:
                    operatingIncome -= flow;
                    totalExpense += flow;
                    break;

                case AccountType.OtherIncomeExpense:
                    pretaxIncome += flow;
                    totalOtherIncomeExpense += flow;
                    break;

                case AccountType.IncomeTaxExpense:
                    netIncome -= flow;
                    totalIncomeTaxExpense += flow;
                    break;
            }

            decimal cashFlow = entry.Balances.Previous - entry.Balances.Balance;

            switch (entry.CashFlow)
            {
                case CashFlow.Operating:
                    totalWorkingCapital += cashFlow;
                    break;

                case CashFlow.NonCash:
                    totalNonCash += flow;
                    break;

                case CashFlow.GainLoss:
                    totalGainLoss += flow;
                    break;

                case CashFlow.Investing:
                    totalInvesting += cashFlow;
                    break;

                case CashFlow.Financing:
                    totalFinancing += cashFlow;
                    break;
            }
        }

        operatingIncome += grossProfit;
        pretaxIncome += operatingIncome;
        netIncome += pretaxIncome;
        totalInvesting += totalGainLoss;

        decimal totalOperating = netIncome + totalWorkingCapital + totalNonCash - totalGainLoss;

        Account otherEquityAccount = Company.GetAccount(Company.OtherEquityAccountId);
        (string Name, AccountType Type, CashFlow CashFlow, Color Color, BalanceInfo Balances)? otherEquityEntry = null;

        foreach ((string Name, AccountType Type, CashFlow CashFlow, Color Color, BalanceInfo Balances) entry in balances)
        {
            if (entry.Name == otherEquityAccount.Name)
            {
                otherEquityEntry = entry;
            }

            if (entry.Type.IsTemporary())
            {
                decimal value = entry.Type.ToBalance(entry.Balances.Balance);

                if (value != 0)
                {
                    string typeString = FormattedStrings.GetString(entry.Type.ToString());
                    Color accountColor = GetColorOrDefault(entry.Name, entry.Type, entry.Color, entry.Balances.Balance);
                    bool isDebit = entry.Type.IsDebit(1);
                    Color typeColor = GetBaseColor(entry.Type, isDebit ? 1 : -1);

                    if (isDebit)
                    {
                        AddSankeyLink(data, typeString, entry.Name, value, typeColor, accountColor);
                    }
                    else
                    {
                        AddSankeyLink(data, entry.Name, typeString, value, accountColor, typeColor);
                    }
                }
            }
        }

        if (Level != ReportLevel.ByType)
        {
            foreach (IGrouping<(string Name, CashFlow CashFlow), (string, AccountType, CashFlow, Color, BalanceInfo)> group in balances
                .GroupBy(x => (x.Name, x.CashFlow)))
            {
                decimal cashFlow = 0;
                decimal totalBalance = 0;
                decimal maxAbs = 0;
                (string, AccountType Type, CashFlow, Color Color, BalanceInfo Balances) max = group.First();

                foreach ((string, AccountType Type, CashFlow, Color Color, BalanceInfo Balances) entry in group)
                {
                    totalBalance += entry.Balances.Balance;
                    cashFlow += entry.Balances.Previous;

                    decimal abs = decimal.Abs(max.Balances.Balance);

                    if (abs > maxAbs)
                    {
                        maxAbs = abs;
                        max = entry;
                    }
                }

                cashFlow -= totalBalance;

                if (cashFlow == 0)
                {
                    continue;
                }

                Color accountColor = GetColorOrDefault(group.Key.Name, max.Type, max.Color, max.Balances.Balance);

                switch (group.Key.CashFlow)
                {
                    case CashFlow.Cash:
                        AddSankeyLink(data, FormattedStrings.NetCashFlow, group.Key.Name + "*", -cashFlow, Colors.JungleTeal, accountColor);
                        break;

                    case CashFlow.Operating:
                        AddSankeyLink(data, group.Key.Name, FormattedStrings.WorkingCapital, cashFlow, accountColor, Colors.Primary);
                        break;

                    case CashFlow.NonCash:
                        AddSankeyLink(data, group.Key.Name, FormattedStrings.NonCash, cashFlow, accountColor, Colors.Primary);
                        break;

                    case CashFlow.Investing:
                        AddSankeyLink(data, group.Key.Name, FormattedStrings.Investing, cashFlow, accountColor, Colors.JungleTeal);
                        break;

                    case CashFlow.Financing:
                        AddSankeyLink(data, group.Key.Name, FormattedStrings.Financing, cashFlow, accountColor, Colors.TigerFlame);
                        break;
                }
            }
        }

        Color incomeColor = GetBaseColor(AccountType.Income, AccountType.Income.IsDebit(1) ? 1 : -1);
        Color costColor = GetBaseColor(AccountType.Cost, AccountType.Cost.IsDebit(1) ? 1 : -1);
        Color expenseColor = GetBaseColor(AccountType.Expense, AccountType.Expense.IsDebit(1) ? 1 : -1);
        Color otherIncomeExpenseColor = GetBaseColor(AccountType.OtherIncomeExpense, AccountType.OtherIncomeExpense.IsDebit(1) ? 1 : -1);
        Color incomeTaxColor = GetBaseColor(AccountType.IncomeTaxExpense, AccountType.IncomeTaxExpense.IsDebit(1) ? 1 : -1);
        string incomeString = FormattedStrings.GetString(AccountType.Income.ToString());
        string costString = FormattedStrings.GetString(AccountType.Cost.ToString());
        string expenseString = FormattedStrings.GetString(AccountType.Expense.ToString());
        string otherIncomeExpenseString = FormattedStrings.GetString(AccountType.OtherIncomeExpense.ToString());
        string incomeTaxExpenseString = FormattedStrings.GetString(AccountType.IncomeTaxExpense.ToString());

        AddSankeyLink(data, incomeString, FormattedStrings.GrossProfit, totalIncome, incomeColor, Colors.JungleTeal);

        if (totalCost >= 0)
        {
            AddSankeyLink(data, FormattedStrings.GrossProfit, costString, totalCost, Colors.JungleTeal, costColor);
            AddSankeyLink(data, FormattedStrings.GrossProfit, FormattedStrings.OperatingIncome, grossProfit, Colors.JungleTeal, Colors.JungleTeal);
        }
        else
        {
            AddSankeyLink(data, costString, FormattedStrings.OperatingIncome, -totalCost, costColor, Colors.JungleTeal);
            AddSankeyLink(data, FormattedStrings.GrossProfit, FormattedStrings.OperatingIncome, totalIncome, Colors.JungleTeal, Colors.JungleTeal);
        }

        if (totalExpense >= 0)
        {
            AddSankeyLink(data, FormattedStrings.OperatingIncome, expenseString, totalExpense, Colors.JungleTeal, expenseColor);
            AddSankeyLink(data, FormattedStrings.OperatingIncome, FormattedStrings.PretaxIncome, operatingIncome, Colors.JungleTeal, Colors.JungleTeal);
        }
        else
        {
            AddSankeyLink(data, expenseString, FormattedStrings.PretaxIncome, -totalExpense, expenseColor, Colors.JungleTeal);
            AddSankeyLink(data, FormattedStrings.OperatingIncome, FormattedStrings.PretaxIncome, grossProfit, Colors.JungleTeal, Colors.JungleTeal);
        }

        AddSankeyLink(data, otherIncomeExpenseString, FormattedStrings.PretaxIncome, totalOtherIncomeExpense, otherIncomeExpenseColor, Colors.JungleTeal);

        if (totalIncomeTaxExpense >= 0)
        {
            AddSankeyLink(data, FormattedStrings.PretaxIncome, incomeTaxExpenseString, totalIncomeTaxExpense, Colors.JungleTeal, incomeTaxColor);
            AddSankeyLink(data, FormattedStrings.PretaxIncome, FormattedStrings.NetIncome, netIncome, Colors.JungleTeal, Colors.JungleTeal);
        }
        else
        {
            AddSankeyLink(data, incomeTaxExpenseString, FormattedStrings.NetIncome, -totalIncomeTaxExpense, incomeTaxColor, Colors.JungleTeal);
            AddSankeyLink(data, FormattedStrings.PretaxIncome, FormattedStrings.NetIncome, pretaxIncome, Colors.JungleTeal, Colors.JungleTeal);
        }

        AddSankeyLink(data, FormattedStrings.NetIncome, FormattedStrings.ComprehensiveIncome, netIncome, Colors.JungleTeal, Colors.JungleTeal);

        decimal otherComprehensiveIncome = 0;

        if (otherEquityEntry is (string name, AccountType type, CashFlow _, Color color, BalanceInfo balance))
        {
            otherComprehensiveIncome = balance.Balance - balance.Previous;
            color = color == Company.Color
                ? GetBaseColor(type, balance.Balance)
                : color;

            AddSankeyLink(data, name, FormattedStrings.ComprehensiveIncome, otherComprehensiveIncome, color, Colors.JungleTeal);
        }

        decimal netOperating = totalOperating;
        decimal netInvesting = netOperating + totalInvesting;
        decimal netFinancing = netInvesting + totalFinancing;

        AddSankeyLink(data, FormattedStrings.ComprehensiveIncome, FormattedStrings.Operating, netIncome, Colors.JungleTeal, Colors.JungleTeal);
        AddSankeyLink(data, FormattedStrings.WorkingCapital, FormattedStrings.Operating, totalWorkingCapital, Colors.Primary, Colors.JungleTeal);
        AddSankeyLink(data, FormattedStrings.NonCash, FormattedStrings.Operating, totalNonCash, Colors.Primary, Colors.JungleTeal);
        AddSankeyLink(data, FormattedStrings.Operating, FormattedStrings.LessGain, totalGainLoss, Colors.JungleTeal, Colors.Dark);
        AddSankeyLink(data, FormattedStrings.PlusGain, FormattedStrings.Investing, totalGainLoss, Colors.Dark, Colors.JungleTeal);
        AddSankeyLink(data, FormattedStrings.Operating, FormattedStrings.NetOperating, netOperating, Colors.JungleTeal, Colors.JungleTeal);
        AddSankeyLink(data, FormattedStrings.NetOperating, FormattedStrings.Investing, netOperating, Colors.JungleTeal, Colors.JungleTeal);
        AddSankeyLink(data, FormattedStrings.Investing, FormattedStrings.NetInvesting, netInvesting, Colors.JungleTeal, Colors.JungleTeal);
        AddSankeyLink(data, FormattedStrings.NetInvesting, FormattedStrings.Financing, netInvesting, Colors.JungleTeal, Colors.JungleTeal);
        AddSankeyLink(data, FormattedStrings.Financing, FormattedStrings.NetFinancing, netFinancing, Colors.JungleTeal, Colors.JungleTeal);
        AddSankeyLink(data, FormattedStrings.NetFinancing, FormattedStrings.NetCashFlow, netFinancing, Colors.JungleTeal, Colors.JungleTeal);

        return JsonSerializer.Serialize(data, s_options);
    }
}
