﻿// GnuCashSerializer.cs
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

    public static async Task SerializeAccountsAsync(Stream output, IReadOnlyDictionary<Guid, Account> accounts)
    {
        List<GnuCashAccount> gnuCashAccounts = new List<GnuCashAccount>();
        StringBuilder pathBuilder = new StringBuilder();

        foreach (Account account in accounts.Values)
        {
            pathBuilder
                .Clear()
                .Append(account.Name);

            Account current = account;

            while (current.ParentKey != Guid.Empty)
            {
                current = accounts[current.ParentKey];

                pathBuilder
                    .Insert(0, ':')
                    .Insert(0, current.Name);
            }

            gnuCashAccounts.Add(new GnuCashAccount()
            {
                Value = account,
                Path = pathBuilder.ToString()
            });
        }

        await SerializeAsync(output, gnuCashAccounts);
    }
}