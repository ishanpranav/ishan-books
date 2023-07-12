using CsvHelper;
using Liber.Forms;
using Liber.Forms.Accounts;
using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace Liber.Readers;

internal sealed class CsvAccountsReader : Reader
{
    public override async Task ReadAsync(MainContext context, string path)
    {
        Guid key = typeof(AccountsForm).GUID;

        context.Kill(key);

        int errors = context.Errors.Count;

        using StreamReader streamReader = File.OpenText(path);
        using CsvReader csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);

        try
        {
            await foreach (Account account in csvReader.GetRecordsAsync<Account>())
            {
                try
                {
                    context.Company.AddOrUpdateAccount(
                        Guid.Empty,
                        account.Number,
                        account.Name,
                        account.Type,
                        account.Locked,
                        account.CompanionId);
                }
                catch (Exception exception)
                {
                    context.Errors.Add(new Error(DateTime.Now, exception.Message)
                    {
                        RawString = csvReader.Parser.RawRecord
                    });
                }
            }
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
            context.Register(key, new AccountsForm(context, key));
        }
    }
}
