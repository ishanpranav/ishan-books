using Liber.Forms.Accounts;
using Liber.Forms.Components;
using System;
using System.Collections.Generic;

namespace Liber.Forms.Transactions;

internal sealed class ImportTransactionsForm : ImportForm
{
    private readonly Dictionary<Guid, Transaction> _transactions = new Dictionary<Guid, Transaction>();

    public ImportTransactionsForm(Company company, FormFactory factory, IReadOnlyCollection<GnuCashLine> lines) : base(company, factory)
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

            line.Value.AccountKey = accounts[line.AccountName];

            transaction.Lines.Add(line.Value);
        }

        foreach (Transaction transaction in _transactions.Values)
        {
            if (!string.IsNullOrWhiteSpace(transaction.Name))
            {
                _listView.Items.Add(transaction.Name);

                continue;
            }

            if (!string.IsNullOrWhiteSpace(transaction.Memo))
            {
                _listView.Items.Add(transaction.Memo);

                continue;
            }

            _listView.Items.Add(transaction.Posted.ToLongDateString());
        }
    }

    protected override void CommitChanges()
    {
        foreach (Transaction transaction in _transactions.Values)
        {
            Company.AddTransaction(transaction);
        }
    }
}
