// BalanceFilter.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.Filters;

public class BalanceFilter : Filter
{
    private decimal _from;
    private decimal _to;

    public override string Name
    {
        get
        {
            if (_from == 0 && _to == 0)
            {
                return "Balance";
            }

            if (_from == _to)
            {
                return $"Balance = {_from}";
            }

            return $"Balance from {_from} to {_to}";
        }
    }

    public decimal From
    {
        get
        {
            return _from;
        }
        set
        {
            if (value > _to)
            {
                _to = value;
            }

            _from = value;
        }
    }

    public decimal To
    {
        get
        {
            return _to;
        }
        set
        {
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
        decimal from = decimal.Abs(_from);
        decimal to = decimal.Abs(_to);

        return balance >= from && balance <= to;
    }

    public override Filter Clone()
    {
        return new BalanceFilter()
        {
            From = _from,
            To = _to
        };
    }
}
