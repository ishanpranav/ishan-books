// ImportTransactionsForm.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using Liber.Forms.Accounts;

namespace Liber.Forms.Transactions;

internal sealed class ImportTransactionsForm : ImportForm
{
    private readonly Company _company;
    private readonly Dictionary<Guid, (GnuCashLine, List<Line>)> _lookup = new Dictionary<Guid, (GnuCashLine, List<Line>)>();

    public ImportTransactionsForm(Company company, IReadOnlyCollection<GnuCashLine> lines)
    {
        Dictionary<string, Guid> accounts = company.Accounts.ToDictionary(x => x.Name, x => x.Id);

        foreach (GnuCashLine line in lines)
        {
            if (!_lookup.TryGetValue(line.TransactionId, out (GnuCashLine Line, List<Line> Values) result))
            {
                result = (line, new List<Line>());
                _lookup[line.TransactionId] = result;
            }

            result.Values.Add(new Line(
                accounts[line.AccountName],
                line.Value.Balance,
                line.Value.Description,
                line.Value.Reconciled));
        }

        SetDataSource(lines.ToList());

        _company = company;
    }

    protected override void CommitChanges()
    {
        foreach (KeyValuePair<Guid, (GnuCashLine Line, List<Line> Values)> entry in _lookup)
        {
            _company.AddTransaction(
                new Transaction(
                    Guid.Empty,
                    entry.Value.Line.TransactionNumber,
                    entry.Value.Line.TransactionName,
                    entry.Value.Line.TransactionPosted,
                    entry.Value.Values)
                {
                    Memo = entry.Value.Line.TransactionMemo,
                });
        }
    }
}
