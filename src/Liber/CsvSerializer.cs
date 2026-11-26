// GnuCashSerializer.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;

namespace Liber;

/// <summary>
/// Provides methods for serializing and deserializing data using the CSV format for integration with GnuCash and Yahoo! Finance.
/// </summary>
public static class CsvSerializer
{
    /// <summary>
    /// Asynchronously Serializes a collection of items to a stream using the CSV format.
    /// </summary>
    /// <typeparam name="T">The type of items to serialize.</typeparam>
    /// <param name="output">The output stream.</param>
    /// <param name="items">The items to serialize.</param>
    /// <returns>A task representing the asynchronous serialization operation.</returns>
    public static async Task SerializeAsync<T>(Stream output, IEnumerable<T> items)
    {
        await using StreamWriter streamWriter = new StreamWriter(output);
        await using CsvWriter csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

        await csvWriter.WriteRecordsAsync(items);
    }

    /// <summary>
    /// Asynchronously deserializes a stream of CSV data into a collection of items.
    /// </summary>
    /// <typeparam name="T">The type of items to deserialize.</typeparam>
    /// <param name="input">The input stream to read from.</param>
    /// <returns>A task representing the asynchronous deserialization operation.</returns>
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
}
