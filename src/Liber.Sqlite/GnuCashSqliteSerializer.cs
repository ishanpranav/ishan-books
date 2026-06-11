// GnuCashSqliteSerializer.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using CsvHelper.TypeConversion;
using Microsoft.Data.Sqlite;

namespace Liber.Sqlite;

public static class GnuCashSqliteSerializer
{
    private static async Task<CompanyType> GetCompanyTypeAsync(DbDataReader reader, int ordinal)
    {
        if (await reader.IsDBNullAsync(ordinal))
        {
            return CompanyType.None;
        }

        switch (reader.GetString(ordinal))
        {
            case "F1040": return CompanyType.Individual;
            case "F1065": return CompanyType.Partnership;

            case "F1120":
            case "F1120S":
                return CompanyType.Corporation;
        }

        return CompanyType.None;
    }

    public static async Task<Company> DeserializeAsync(string path, IEnumerable<ImportRule> rules)
    {
        await using SqliteConnection connection = SqliteUtilities.CreateConnection(path);

        await connection.OpenAsync();

        string? name;
        CompanyType type;
        Guid emptyParentId;

        await using (SqliteCommand command = connection.CreateCommand())
        {
            command.CommandText = Queries.GnuCashSelectCompany;

            await using (SqliteDataReader reader = await command.ExecuteReaderAsync(CommandBehavior.SingleRow))
            {
                await reader.ReadAsync();

                name = await SqliteUtilities.GetStringAsync(reader, 0);

                if (string.IsNullOrWhiteSpace(name))
                {
                    name = null;
                }

                type = await GetCompanyTypeAsync(reader, 1);
                emptyParentId = reader.GetGuid(2);
            }
        }

        List<Account> accounts = new List<Account>();

        await using (SqliteCommand command = connection.CreateCommand())
        {
            command.CommandText = Queries.GnuCashSelectAccounts;

            await using (SqliteDataReader reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    Guid id = reader.GetGuid(0);
                    Guid parentId = reader.GetGuid(1);

                    accounts.Add(new Account(id, parentId == emptyParentId ? Guid.Empty : parentId)
                    {
                        Number = reader.GetDecimal(2),
                        Name = reader.GetString(3),
                        Type = GnuCashAccountTypeConverter.Parse(await SqliteUtilities.GetStringAsync(reader, 4)),
                        Placeholder = reader.GetBoolean(5),
                        Description = await SqliteUtilities.GetStringAsync(reader, 6),
                        Memo = await SqliteUtilities.GetStringAsync(reader, 7),
                        Color = await SqliteUtilities.GetColorAsync(reader, 8),
                        TaxType = !await reader.IsDBNullAsync(9) && reader.GetBoolean(9),
                        Inactive = reader.GetBoolean(10)
                    });
                }
            }
        }

        List<Transaction> transactions = new List<Transaction>();
        Dictionary<Guid, List<Line>> lines = new Dictionary<Guid, List<Line>>();

        await using (SqliteCommand command = connection.CreateCommand())
        {
            command.CommandText = Queries.GnuCashSelectLines;

            await using (SqliteDataReader reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    Guid id = reader.GetGuid(0);

                    if (!lines.TryGetValue(id, out List<Line>? values))
                    {
                        values = new List<Line>();
                        lines[id] = values;
                    }

                    values.Add(new Line()
                    {
                        AccountId = reader.GetGuid(1),
                        Balance = reader.GetDecimal(2),
                        Description = await SqliteUtilities.GetStringAsync(reader, 3)
                    });
                }
            }
        }

        await using (SqliteCommand command = connection.CreateCommand())
        {
            command.CommandText = Queries.GnuCashSelectTransactions;

            await using (SqliteDataReader reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    Guid id = reader.GetGuid(0);

                    transactions.Add(new Transaction(id, lines[id])
                    {
                        Posted = reader.GetDateTime(1).Date,
                        Number = reader.GetDecimal(2),
                        Name = await SqliteUtilities.GetStringAsync(reader, 3),
                        Memo = await SqliteUtilities.GetStringAsync(reader, 4)
                    });
                }
            }
        }

        Company result = new Company(accounts, transactions, nextAccountNumber: 1, nextTransactionNumber: 1)
        {
            Name = name,
            Type = type
        };
        ImportContext context = new ImportContext(accounts)
        {
            Color = result.Color
        };

        context.Apply(rules);

        if (context.EquityAccount == null)
        {
            result.ResetEquityAccount();
        }
        else
        {
            result.EquityAccountId = context.EquityAccount.Id;
        }

        if (context.OtherEquityAccount == null)
        {
            result.ResetOtherEquityAccount();
        }
        else
        {
            result.OtherEquityAccountId = context.OtherEquityAccount.Id;
        }

        return result;
    }
}
