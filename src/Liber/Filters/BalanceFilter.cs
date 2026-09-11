// BalanceFilter.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.ComponentModel;
using System.Text.Json.Serialization;
using Liber.Properties;

namespace Liber.Filters;

public class BalanceFilter : Filter
{
    private decimal _from;
    private decimal _to;

    [Browsable(false)]
    [JsonIgnore]
    public override string Name
    {
        get
        {
            decimal from = From;
            decimal to = To;

            if (from == 0 && to == 0)
            {
                return Resources.DebitCredit;
            }

            if (from == to)
            {
                return $"{Resources.DebitCredit} = {from}";
            }

            return $"{from} ≤ {Resources.DebitCredit} ≤ {to}";
        }
    }

    [LocalizedCategory(nameof(From))]
    [LocalizedDescription(nameof(From))]
    [LocalizedDisplayName(nameof(From))]
    public decimal From
    {
        get
        {
            return decimal.Abs(_from);
        }
        set
        {
            value = decimal.Abs(value);

            if (value > _to)
            {
                _to = value;
            }

            _from = value;
        }
    }

    [LocalizedCategory(nameof(To))]
    [LocalizedDescription(nameof(To))]
    [LocalizedDisplayName(nameof(To))]
    public decimal To
    {
        get
        {
            return decimal.Abs(_to);
        }
        set
        {
            value = decimal.Abs(value);

            if (value < _from)
            {
                _from = value;
            }

            _to = value;
        }
    }

    public override bool IsMatch(Line value)
    {
        decimal balance = decimal.Abs(value.Balance);

        return balance >= From && balance <= To;
    }

    public override Filter Clone()
    {
        return new BalanceFilter()
        {
            From = From,
            To = To
        };
    }
}
