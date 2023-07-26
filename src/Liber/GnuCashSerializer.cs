// GnuCashSerializer.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;

namespace Liber;

public static class GnuCashSerializer
{
    private static async Task SerializeAsync<T>(Stream output, IEnumerable<T> items)
    {
        await using StreamWriter streamWriter = new StreamWriter(output);
        await using CsvWriter csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

        await csvWriter.WriteRecordsAsync(items);
    }

    public static async Task<IReadOnlyCollection<T>> DeserializeAsync<T>(Stream input)
    {
        using StreamReader streamReader = new StreamReader(input);
        using CsvReader csvReader = new CsvReader(streamReader, new CsvConfiguration(CultureInfo.InvariantCulture));

        List<T> results = new List<T>();

        await foreach (T record in csvReader.GetRecordsAsync<T>())
        {
            results.Add(record);
        }

        return results;
    }

    private static string GetPath(Company company, Account account)
    {
        StringBuilder pathBuilder = new StringBuilder();

        pathBuilder.Append(account.Name);

        Account current = account;

        while (current.ParentId != Guid.Empty)
        {
            current = company.Accounts[current.ParentId];

            pathBuilder
                .Insert(0, ':')
                .Insert(0, current.Name);
        }

        return pathBuilder.ToString();
    }

    public static async Task SerializeAccountsAsync(Stream output, Company company)
    {
        int i = 0;
        GnuCashAccount[] gnuCashAccounts = new GnuCashAccount[company.Accounts.Count];

        foreach (Account account in company.Accounts.Values)
        {
            gnuCashAccounts[i] = new GnuCashAccount()
            {
                Value = account,
                Path = GetPath(company, account)
            };
            i++;
        }

        await SerializeAsync(output, gnuCashAccounts);
    }

    public static async Task SerializeTransactionsAsync(Stream output, Company company)
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
                    AccountPath = GetPath(company, account),
                    Amount = line.Balance,
                    AmountWithSymbol = balanceWithSymbol,
                    ValueWithSymbol = balanceWithSymbol
                });
            }
        }

        await SerializeAsync(output, lines);
    }
}
