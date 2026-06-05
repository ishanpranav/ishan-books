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

        Dictionary<Guid, Account> accounts = new Dictionary<Guid, Account>();
        Dictionary<Account, Guid> accountIds = new Dictionary<Account, Guid>();
        Dictionary<Guid, Transaction> transactions = new Dictionary<Guid, Transaction>();
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
                type = await GetCompanyTypeAsync(reader, 1);
                emptyParentId = reader.GetGuid(2);
            }
        }

        await using (SqliteCommand command = connection.CreateCommand())
        {
            command.CommandText = Queries.GnuCashSelectAccounts;

            await using (SqliteDataReader reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    try
                    {
                        Guid id = reader.GetGuid(0);
                        Guid parentId = reader.GetGuid(1);
                        Account account = new Account(parentId == emptyParentId ? Guid.Empty : parentId)
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
                        };

                        accounts.Add(id, account);
                        accountIds.Add(account, id);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
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

                    transactions.Add(id, new Transaction()
                    {
                        Id = id,
                        Posted = reader.GetDateTime(1),
                        Number = reader.GetDecimal(2),
                        Name = await SqliteUtilities.GetStringAsync(reader, 3),
                        Memo = await SqliteUtilities.GetStringAsync(reader, 4)
                    });
                }
            }
        }

        await using (SqliteCommand command = connection.CreateCommand())
        {
            command.CommandText = Queries.GnuCashSelectLines;

            await using (SqliteDataReader reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    Guid id = reader.GetGuid(0);

                    transactions[id].Lines.Add(new Line()
                    {
                        AccountId = reader.GetGuid(1),
                        Balance = reader.GetDecimal(2),
                        Description = await SqliteUtilities.GetStringAsync(reader, 3)
                    });
                }
            }
        }

        Company result = new Company(accounts, transactions.Values, nextAccountNumber: 1, nextTransactionNumber: 1)
        {
            Name = name,
            Type = type
        };
        ImportContext context = new ImportContext(accounts.Values)
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
            result.EquityAccountId = accountIds[context.EquityAccount];
        }

        if (context.OtherEquityAccount == null)
        {
            result.ResetOtherEquityAccount();
        }
        else
        {
            result.OtherEquityAccountId = accountIds[context.OtherEquityAccount];
        }

        return result;
    }
}
