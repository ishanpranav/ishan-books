using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace Liber.GnuCash;

public static class GnuCashSerializer
{
    private static async Task SerializeAsync<T>(Stream output, IEnumerable<T> items)
    {
        await using StreamWriter streamWriter = new StreamWriter(output);
        await using CsvWriter csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

        await csvWriter.WriteRecordsAsync(items);
    }

    private static async Task<IReadOnlyCollection<T>> DeserializeAsync<T>(Stream input)
    {
        using StreamReader streamReader = new StreamReader(input);
        using CsvReader csvReader = new CsvReader(streamReader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            BadDataFound = null
        });

        List<T> results = new List<T>();

        await foreach (T record in csvReader.GetRecordsAsync<T>())
        {
            results.Add(record);
        }

        return results;
    }

    public static async Task SerializeAccountsAsync(Stream output, IEnumerable<Account> accounts)
    {
        List<GnuCashAccount> gnuCashAccounts = new List<GnuCashAccount>();

        foreach (Account account in accounts)
        {
            gnuCashAccounts.Add(new GnuCashAccount(account));
        }

        await SerializeAsync(output, gnuCashAccounts);
    }
    
    public static async Task<IReadOnlyCollection<Account>> DeserializeAccountsAsync(Stream input)
    {
        IReadOnlyCollection<GnuCashAccount> gnuCashAccounts = await DeserializeAsync<GnuCashAccount>(input);
        List<Account> results = new List<Account>();

        foreach (GnuCashAccount account in gnuCashAccounts)
        {
            results.Add(account.ToAccount());
        }

        return results;
    }
}
