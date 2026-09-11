// TransactionPostedFilter.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using System.Text.Json.Serialization;
using Liber.Properties;

namespace Liber.Filters;

public class TransactionPostedFilter : Filter
{
    private DateTime _from;
    private DateTime _to;

    [Browsable(false)]
    [JsonIgnore]
    public override string Name
    {
        get
        {
            if (_from == default && _to == default)
            {
                return Resources.Posted;
            }

            if (_from == _to)
            {
                return $"{Resources.Posted}: {_from:d}";
            }

            return $"{Resources.Posted}: {_from:d} - {_to:d}";
        }
    }

    [LocalizedCategory(nameof(From))]
    [LocalizedDescription(nameof(From))]
    [LocalizedDisplayName(nameof(From))]
    public DateTime From
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
    public DateTime To
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
        DateTime number = value.Transaction.Posted;

        return number >= _from && number <= _to;
    }

    public override Filter Clone()
    {
        return new TransactionPostedFilter()
        {
            From = _from,
            To = _to
        };
    }
}
