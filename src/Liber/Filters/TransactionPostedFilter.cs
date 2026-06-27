// TransactionPostedFilter.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Liber.Filters;

public class TransactionPostedFilter : Filter
{
    private DateTime _from;
    private DateTime _to;

    public override string Name
    {
        get
        {
            if (_from == default && _to == default)
            {
                return "Date";
            }

            if (From == To)
            {
                return $"Date = {From:d}";
            }

            return $"Date from {From:d} to {To:d}";
        }
    }

    public DateTime From
    {
        get
        {
            return _from.Date;
        }
        set
        {
            if (value.Date > _to)
            {
                _to = value.Date;
            }

            _from = value.Date;
        }
    }

    public DateTime To
    {
        get
        {
            return _to.Date;
        }
        set
        {
            if (value < _from)
            {
                _from = value.Date;
            }

            _to = value.Date;
        }
    }

    public override bool IsMatch(Line value)
    {
        DateTime posted = value.Transaction.Posted;

        return posted >= From && posted <= To;
    }

    public override Filter Clone()
    {
        return new TransactionPostedFilter()
        {
            From = From,
            To = To
        };
    }
}
