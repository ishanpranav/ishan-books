using CsvHelper;
using Liber.Forms;
using Liber.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Liber.Readers;

internal sealed class CsvJournalReader : Reader
{
    public override async Task ReadAsync(MainContext context, string path)
    {
        Guid key = typeof(JournalForm).GUID;

        context.Kill(key);

        int errors = context.Errors.Count;

        using StreamReader streamReader = File.OpenText(path);
        using CsvReader csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);

        Dictionary<Guid, int> numbers = new Dictionary<Guid, int>();
        Dictionary<string, Guid> accounts = context.Company.Accounts.ToDictionary(
            x => x.Value.QualifiedName,
            x => x.Key,
            StringComparer.OrdinalIgnoreCase);

        try
        {
            List<Transaction> transactions = new List<Transaction>();

            await foreach (CsvTransaction transaction in csvReader.GetRecordsAsync<CsvTransaction>())
            {
                if (!numbers.TryGetValue(transaction.Id, out int number))
                {
                    if (transaction.GnuNumber is int gnuNumber)
                    {
                        number = gnuNumber;
                    }
                    else
                    {
                        number = context.Company.NextJournalNumber;
                    }

                    numbers[transaction.Id] = number;
                }

                transaction.Id = Guid.NewGuid();
                transaction.Number = number;
                context.Company.UpdateJournal(number);

                if (context.Company.Journal(transaction.Number).Any())
                {
                    context.Errors.Add(new Error(DateTime.Now, Resources.JournalNumberException));

                    continue;
                }

                transaction.AccountId = accounts[transaction.GnuAccountName];

                transactions.Add(transaction);
            }

            context.Company.AddTransactions(transactions);
        }
        catch (Exception exception)
        {
            context.Errors.Add(new Error(DateTime.Now, exception.Message));
        }

        if (context.Errors.Count > errors)
        {
            context.ReportErrors();
        }
        else
        {
            context.Register(key, new JournalForm(context, key));
        }
    }
}
