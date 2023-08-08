// GnuCashTransactionWriter.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Liber.Writers;

public class GnuCashTransactionWriter : IWriter
{
    public  async Task SerializeAsync(Stream output, Company company)
    {
        List<GnuCashLine> lines = new List<GnuCashLine>();

        foreach (Transaction transaction in company.Transactions)
        {
            foreach (Line line in transaction.Lines)
            {
                Account account = company.Accounts[line.AccountId];
                string balanceWithSymbol = line.Balance.ToString("c");

                lines.Add(new GnuCashLine()
                {
                    Value = line,
                    AccountName = account.Name,
                    AccountPath = GnuCashSerializer.GetPath(company, account),
                    Amount = line.Balance,
                    AmountWithSymbol = balanceWithSymbol,
                    ValueWithSymbol = balanceWithSymbol
                });
            }
        }

        await GnuCashSerializer.SerializeAsync(output, lines);
    }
}
