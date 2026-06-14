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
    private readonly Dictionary<Guid, (GnuCashLine, List<Line>)> lookup = new Dictionary<Guid, (GnuCashLine, List<Line>)>();

    public ImportTransactionsForm(Company company, IReadOnlyCollection<GnuCashLine> lines) : base(company)
    {
        Dictionary<string, Guid> accounts = new Dictionary<string, Guid>(company.Accounts.Count);

        foreach (Account account in company.Accounts)
        {
            accounts.Add(account.Name, account.Id);
        }

        foreach (GnuCashLine line in lines)
        {
            if (!lookup.TryGetValue(line.TransactionId, out (GnuCashLine Line, List<Line> Values) result))
            {
                result = (line, new List<Line>());
                lookup[line.TransactionId] = result;
            }

            result.Values.Add(new Line(accounts[line.AccountName], line.Value.Balance, line.Value.Description));
        }

        SetDataSource(lines.Select(x => x.Value).ToList());
    }

    protected override void CommitChanges()
    {
        foreach (KeyValuePair<Guid, (GnuCashLine Line, List<Line> Values)> entry in lookup)
        {
            Company.AddTransaction(
                new Transaction(
                    Guid.Empty,
                    entry.Value.Line.TransactionNumber,
                    entry.Value.Line.TransactionName,
                    entry.Value.Line.TransactionPosted,
                    entry.Value.Values)
                {
                    Memo = entry.Value.Line.TransactionMemo,
                },
                entry.Value.Line.TransactionName,
                entry.Value.Values);
        }
    }
}
