// IifAccountWriter.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;

namespace Liber.Writers;

/// <summary>
/// An <see cref="IWriter"/> for Intuit QuickBooks (IIF) account data.
/// </summary>
public class IifAccountWriter : IWriter
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IifAccountWriter"/> class.
    /// </summary>
    public IifAccountWriter() { }

    private static IifExtra GetExtra(Company company, Guid accountId)
    {
        Account account = company.Accounts[accountId];

        if (account.Type == AccountType.Cost)
        {
            return IifExtra.Cost;
        }

        if (accountId == company.EquityAccountId)
        {
            return IifExtra.Equity;
        }

        return IifExtra.None;
    }

    /// <inheritdoc/>
    public async Task WriteAsync(Stream output, Company company)
    {
        int i = 0;
        IifAccount[] accounts = new IifAccount[company.Accounts.Count];

        foreach (KeyValuePair<Guid, Account> account in company.Accounts)
        {
            accounts[i] = new IifAccount(account.Value, i + 1, GetExtra(company, account.Key));
            i++;
        }

        await using StreamWriter streamWriter = new StreamWriter(output);
        await using CsvWriter csvWriter = new CsvWriter(streamWriter, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = "\t"
        });

        csvWriter.WriteHeader<IifCompany>();

        await csvWriter.NextRecordAsync();

        csvWriter.WriteRecord(new IifCompany());

        await csvWriter.NextRecordAsync();

        csvWriter.WriteHeader<IifAccount>();

        await csvWriter.NextRecordAsync();
        await csvWriter.WriteRecordsAsync(accounts);
    }
}
