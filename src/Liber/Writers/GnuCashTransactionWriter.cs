// GnuCashTransactionWriter.cs
// Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Liber.Writers;

/// <summary>
/// An <see cref="IWriter"/> for GnuCash (CSV) transaction data.
/// </summary>
public class GnuCashTransactionWriter : IWriter
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GnuCashTransactionWriter"/> class.
    /// </summary>
    public GnuCashTransactionWriter() { }

    /// <inheritdoc/>
    public async Task WriteAsync(Stream output, Company company)
    {
        List<GnuCashLine> lines = new List<GnuCashLine>();

        foreach (Transaction transaction in company.Transactions)
        {
            foreach (Line line in transaction.Lines)
            {
                Account account = company.Accounts[line.AccountId];

                lines.Add(new GnuCashLine()
                {
                    Value = line,
                    AccountName = account.Name,
                    AccountPath = GnuCashSerializer.GetPath(company, account)
                });
            }
        }

        await GnuCashSerializer.SerializeAsync(output, lines);
    }
}
