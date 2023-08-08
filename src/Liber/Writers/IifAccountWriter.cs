// IifAccountWriter.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;

namespace Liber.Writers;

public class IifAccountWriter : IWriter
{
    private static IifExtra GetExtra(Company company, Account account)
    {
        if (account.Type == AccountType.Cost)
        {
            return IifExtra.Cost;
        }

        if (company.EquityAccount == account)
        {
            return IifExtra.Equity;
        }

        return IifExtra.None;
    }

    public async Task SerializeAsync(Stream output, Company company)
    {
        int i = 0;
        IifAccount[] accounts = new IifAccount[company.Accounts.Count];

        foreach (Account account in company.Accounts.Values)
        {
            accounts[i] = new IifAccount(account, i + 1, GetExtra(company, account));
            i++;
        }

        await using StreamWriter streamWriter = new StreamWriter(output);
        await using CsvWriter csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

        csvWriter.WriteHeader<IifCompany>();

        await csvWriter.NextRecordAsync();

        csvWriter.WriteRecord(new IifCompany());

        await csvWriter.NextRecordAsync();
        await csvWriter.WriteRecordsAsync(accounts);
    }
}
