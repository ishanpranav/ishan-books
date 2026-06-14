// AccountLineSource.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Liber.Forms.LineSources;

internal class AccountLineSource : ILineSource
{
    private readonly Company _company;
    private readonly Account _value;

    public AccountLineSource(Company company, Account value)
    {
        _company = company;
        _value = value;
    }

    public string Name
    {
        get
        {
            return _value.Name;
        }
    }

    public Color Color
    {
        get
        {
            return _company.GetColorOrDefault(_value);
        }
    }

    public bool Contains(Line value)
    {
        return value.AccountId == _value.Id;
    }

    public bool CanGetNewLines(Guid siblingId)
    {
        return siblingId != _value.Id;
    }

    public IReadOnlyCollection<Line> GetNewLines(Guid siblingId, decimal balance)
    {
        return new Line[]
        {
            new Line(_value.Id,  balance, description: null),
            new Line(siblingId, -balance, description: null)
        };
    }

    public IEnumerable<Line> GetOrderedLines()
    {
        return _value.Lines.Order();
    }

    public AccountType GetRepresentativeType()
    {
        return _value.Type;
    }

    public bool IsInvalidatedByAccountRemoved(Guid id)
    {
        return id == _value.Id;
    }

    public bool IsInvalidatedByAccountUpdated(Guid id)
    {
        return id == _value.Id;
    }

    public bool IsInvalidatedByTransactionAdded(Guid id)
    {
        Transaction transaction = _company.GetTransaction(id);

        return transaction.Lines.Any(x => x.AccountId == _value.Id);
    }

    public bool IsInvalidatedByTransactionRemoved(Guid id)
    {
        return true;
    }

    public bool IsInvalidatedByTransactionUpdated(Guid id)
    {
        return true;
    }

    public override string ToString()
    {
        return Name;
    }
}
