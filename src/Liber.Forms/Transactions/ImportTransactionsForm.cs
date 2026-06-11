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

            line.Value.AccountId = accounts[line.AccountName];
            result.Values.Add(line.Value);
        }

        SetDataSource(lines.Select(x => x.Value).ToList());
    }

    protected override void CommitChanges()
    {
        foreach (KeyValuePair<Guid, (GnuCashLine Line, List<Line> Values)> entry in lookup)
        {
            Company.AddTransaction(new Transaction()
            {
                Number = entry.Value.Line.TransactionNumber,
                Name = entry.Value.Line.TransactionName,
                Posted = entry.Value.Line.TransactionPosted,
                Memo = entry.Value.Line.TransactionMemo,
            }, entry.Value.Values);
        }
    }
}
