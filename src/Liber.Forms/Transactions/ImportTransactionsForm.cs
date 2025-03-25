// ImportTransactionsForm.cs
// Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using Liber.Forms.Accounts;

namespace Liber.Forms.Transactions;

internal sealed class ImportTransactionsForm : ImportForm
{
    private readonly Dictionary<Guid, Transaction> _transactions = new Dictionary<Guid, Transaction>();

    public ImportTransactionsForm(Company company, IReadOnlyCollection<GnuCashLine> lines) : base(company)
    {
        Dictionary<string, Guid> accounts = new Dictionary<string, Guid>(company.Accounts.Count);

        foreach (KeyValuePair<Guid, Account> account in company.Accounts)
        {
            accounts.Add(account.Value.Name, account.Key);
        }

        foreach (GnuCashLine line in lines)
        {
            if (!_transactions.TryGetValue(line.TransactionId, out Transaction? transaction))
            {
                transaction = new Transaction()
                {
                    Id = line.TransactionId,
                    Number = line.TransactionNumber,
                    Name = line.TransactionName,
                    Posted = line.TransactionPosted,
                    Memo = line.TransactionMemo
                };
                _transactions[line.TransactionId] = transaction;
            }

            line.Value.AccountId = accounts[line.AccountName];

            transaction.Lines.Add(line.Value);
        }

        SetDataSource(lines.Select(x => x.Value).ToList());
    }

    protected override void CommitChanges()
    {
        foreach (Transaction transaction in _transactions.Values)
        {
            Company.AddTransaction(transaction);
        }
    }
}
