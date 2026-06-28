// TransactionNumberFilter.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.ComponentModel;
using Liber.Properties;

namespace Liber.Filters;

public class TransactionNumberFilter : Filter
{
    private decimal _from;
    private decimal _to;

    [Browsable(false)]
    public override string Name
    {
        get
        {
            if (_from == 0 && _to == 0)
            {
                return Resources.Number;
            }

            if (_from == _to)
            {
                return $"{Resources.Number} = {_from}";
            }

            return $"{_from} ≤ {Resources.Number} ≤ {_to}";
        }
    }

    [LocalizedCategory(nameof(From))]
    [LocalizedDescription(nameof(From))]
    [LocalizedDisplayName(nameof(From))]
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

    [LocalizedCategory(nameof(To))]
    [LocalizedDescription(nameof(To))]
    [LocalizedDisplayName(nameof(To))]
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
        decimal number = value.Transaction.Number;

        return number >= From && number <= To;
    }

    public override Filter Clone()
    {
        return new TransactionNumberFilter()
        {
            From = _from,
            To = _to
        };
    }
}
