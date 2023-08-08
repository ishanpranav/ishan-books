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

namespace Liber.Writers;

public class GnuCashSerializer
{
    public static async Task SerializeAsync<T>(Stream output, IEnumerable<T> items)
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

    public static string GetPath(Company company, Account account)
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
}
