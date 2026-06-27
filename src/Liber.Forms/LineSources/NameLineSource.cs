// NameLineSource.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Liber.Forms.LineSources;

internal class NameLineSource : ILineSource
{
    private readonly Company _company;

    public string Name { get; }
    public Color Color { get; }

    public NameLineSource(Company company, string name)
    {
        Color = company.Color;
        Name = name;
        _company = company;
    }

    public bool Contains(Line value)
    {
        return value.Transaction.Name == Name;
    }

    public bool CanEditSibling(Line value)
    {
        return false;
    }

    public bool CanGetNewLines(Guid siblingId)
    {
        return false;
    }

    public IReadOnlyCollection<Line> GetNewLines(Guid siblingId, decimal balance)
    {
        throw new InvalidOperationException();
    }

    public IEnumerable<Line> GetOrderedLines()
    {
        List<Line> results = new List<Line>();

        foreach (Account account in _company.Accounts)
        {
            foreach (Line line in account.Lines)
            {
                if (line.Transaction.Name == Name)
                {
                    results.Add(line);
                }
            }
        }

        results.Sort();

        return results;
    }

    public AccountType GetRepresentativeType()
    {
        decimal balance = 0;

        foreach (Line line in GetOrderedLines())
        {
            balance += line.Balance;
        }

        if (balance < 0)
        {
            return AccountTypeExtensions.Credit;
        }

        return AccountTypeExtensions.Debit;
    }

    public bool IsInvalidatedByAccountRemoved(Guid id)
    {
        return true;
    }

    public bool IsInvalidatedByAccountUpdated(Guid id)
    {
        return true;
    }

    public bool IsInvalidatedByTransactionAdded(Guid id)
    {
        return _company.GetTransaction(id).Name == Name;
    }

    public bool IsInvalidatedByTransactionReconciled(Guid id)
    {
        return _company.GetTransaction(id).Name == Name;
    }
}
