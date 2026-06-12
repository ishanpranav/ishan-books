// SqliteSerializer.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using SQLitePCL;

namespace Liber.Sqlite;

public static class SqliteSerializer
{
    static SqliteSerializer()
    {
        Batteries.Init();
    }

    private static object ValueOf(string? value)
    {
        if (value == null)
        {
            return DBNull.Value;
        }

        return value;
    }

    private static object ValueOf(Color value)
    {
        if (value.IsEmpty)
        {
            return DBNull.Value;
        }

        return value.ToArgb();
    }

    private static object ValueOf(decimal value)
    {
        if (value == 0)
        {
            return DBNull.Value;
        }

        return value;
    }

    private static object ValueOf(Guid value)
    {
        if (value == Guid.Empty)
        {
            return DBNull.Value;
        }

        return value;
    }

    private static object ValueOf(DateTime? value)
    {
        if (value == null)
        {
            return DBNull.Value;
        }

        return value.Value;
    }

    public static async Task SerializeAsync(string path, Company value, IProgress progress)
    {
        await using SqliteConnection connection = SqliteUtilities.CreateConnection(path, value.Password);

        await connection.OpenAsync();

        await using (SqliteCommand command = connection.CreateCommand())
        {
            command.CommandText = Queries.Create;

            command.Parameters.AddWithValue("@name", ValueOf(value.Name));
            command.Parameters.AddWithValue("@nextAccountNumber", value.NextAccountNumber);
            command.Parameters.AddWithValue("@nextTransactionNumber", value.NextTransactionNumber);
            command.Parameters.AddWithValue("@type", value.Type);
            command.Parameters.AddWithValue("@color", ValueOf(value.Color));
            command.Parameters.AddWithValue("@equityAccount", value.EquityAccountId);
            command.Parameters.AddWithValue("@otherEquityAccount", value.OtherEquityAccountId);
            command.Parameters.AddWithValue("@fiscalYearStarted", value.FiscalYearStarted);
            command.Parameters.AddWithValue("@fiscalYearPosted", value.FiscalYearPosted);
            command.Parameters.AddWithValue("@reportingPeriod", value.ReportingPeriod);
            command.Parameters.AddWithValue("@customStarted", ValueOf(value.CustomStarted));
            command.Parameters.AddWithValue("@customPosted", ValueOf(value.CustomPosted));

            await command.ExecuteNonQueryAsync();
        }

        await using (SqliteCommand command = connection.CreateCommand())
        {
            command.CommandText = Queries.DisableForeignKeys;

            await command.ExecuteNonQueryAsync();
        }

        await using (SqliteTransaction dbTransaction = connection.BeginTransaction())
        {
            foreach (Account account in value.Accounts)
            {
                await using (SqliteCommand command = connection.CreateCommand())
                {
                    command.Transaction = dbTransaction;
                    command.CommandText = Queries.InsertAccount;

                    command.Parameters.AddWithValue("@id", account.Id);
                    command.Parameters.AddWithValue("@parentId", ValueOf(account.ParentId));
                    command.Parameters.AddWithValue("@number", ValueOf(account.Number));
                    command.Parameters.AddWithValue("@name", account.Name);
                    command.Parameters.AddWithValue("@type", account.Type);
                    command.Parameters.AddWithValue("@placeholder", account.Placeholder);
                    command.Parameters.AddWithValue("@description", ValueOf(account.Description));
                    command.Parameters.AddWithValue("@memo", ValueOf(account.Memo));
                    command.Parameters.AddWithValue("@color", ValueOf(account.Color));
                    command.Parameters.AddWithValue("@taxType", account.TaxType);
                    command.Parameters.AddWithValue("@inactive", account.Inactive);
                    command.Parameters.AddWithValue("@cashFlow", account.CashFlow);

                    await command.ExecuteNonQueryAsync();
                }

                progress.WriteAccount();
            }

            foreach (Transaction transaction in value.Transactions)
            {
                await using (SqliteCommand command = connection.CreateCommand())
                {
                    command.Transaction = dbTransaction;
                    command.CommandText = Queries.InsertTransaction;

                    command.Parameters.AddWithValue("@id", transaction.Id);
                    command.Parameters.AddWithValue("@posted", transaction.Posted);
                    command.Parameters.AddWithValue("@number", ValueOf(transaction.Number));
                    command.Parameters.AddWithValue("@name", ValueOf(transaction.Name));
                    command.Parameters.AddWithValue("@memo", ValueOf(transaction.Memo));

                    await command.ExecuteNonQueryAsync();
                }

                foreach (Line line in transaction.Lines)
                {
                    await using (SqliteCommand command = connection.CreateCommand())
                    {
                        command.Transaction = dbTransaction;
                        command.CommandText = Queries.InsertLine;

                        command.Parameters.AddWithValue("@accountId", line.AccountId);
                        command.Parameters.AddWithValue("@balance", line.Balance);
                        command.Parameters.AddWithValue("@description", ValueOf(line.Description));
                        command.Parameters.AddWithValue("@transactionId", transaction.Id);

                        await command.ExecuteNonQueryAsync();
                    }
                }

                progress.WriteTransaction();
            }

            await dbTransaction.CommitAsync();
        }

        await using (SqliteCommand command = connection.CreateCommand())
        {
            command.CommandText = Queries.EnableForeignKeys;

            await command.ExecuteNonQueryAsync();
        }
    }

    public static async Task<bool> CheckPasswordAsync(string path, string password)
    {
        try
        {
            await using SqliteConnection connection = SqliteUtilities.CreateConnection(path, password);

            await connection.OpenAsync();

            await using (SqliteCommand command = connection.CreateCommand())
            {
                command.CommandText = Queries.SelectCompany;

                await using (SqliteDataReader reader = await command.ExecuteReaderAsync(CommandBehavior.SingleRow))
                {
                    await reader.ReadAsync();
                }
            }
        }
        catch (SqliteException)
        {
            return false;
        }

        return true;
    }

    public static async Task<Company> DeserializeAsync(string path, string password)
    {
        await using SqliteConnection connection = SqliteUtilities.CreateConnection(path, password);

        await connection.OpenAsync();

        List<Account> accounts = new List<Account>();

        await using (SqliteCommand command = connection.CreateCommand())
        {
            command.CommandText = Queries.SelectAccounts;

            await using (SqliteDataReader reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    Guid id = reader.GetGuid(0);

                    accounts.Add(new Account(id, reader.GetGuid(1))
                    {
                        Number = reader.GetDecimal(2),
                        Name = reader.GetString(3),
                        Type = await reader.GetFieldValueAsync<AccountType>(4),
                        Placeholder = reader.GetBoolean(5),
                        Description = await SqliteUtilities.GetStringAsync(reader, 6),
                        Memo = await SqliteUtilities.GetStringAsync(reader, 7),
                        Color = await SqliteUtilities.GetColorAsync(reader, 8),
                        TaxType = !await reader.IsDBNullAsync(9) && reader.GetBoolean(9),
                        Inactive = reader.GetBoolean(10),
                        CashFlow = await reader.GetFieldValueAsync<CashFlow>(11)
                    });
                }
            }
        }

        List<Transaction> transactions = new List<Transaction>();
        Dictionary<Guid, List<Line>> lines = new Dictionary<Guid, List<Line>>();

        await using (SqliteCommand command = connection.CreateCommand())
        {
            command.CommandText = Queries.SelectLines;

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
            command.CommandText = Queries.SelectTransactions;

            await using (SqliteDataReader reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    Guid id = reader.GetGuid(0);

                    transactions.Add(new Transaction(id, await SqliteUtilities.GetStringAsync(reader, 3), lines[id])
                    {
                        Posted = reader.GetDateTime(1),
                        Number = reader.GetDecimal(2),
                        Memo = await SqliteUtilities.GetStringAsync(reader, 4)
                    });
                }
            }
        }

        await using (SqliteCommand command = connection.CreateCommand())
        {
            command.CommandText = Queries.SelectCompany;

            await using (SqliteDataReader reader = await command.ExecuteReaderAsync(CommandBehavior.SingleRow))
            {
                await reader.ReadAsync();

                return new Company(accounts, transactions, reader.GetDecimal(1), reader.GetDecimal(2))
                {
                    Name = await SqliteUtilities.GetStringAsync(reader, 0),
                    Type = await reader.GetFieldValueAsync<CompanyType>(3),
                    Color = await SqliteUtilities.GetColorAsync(reader, 4),
                    EquityAccountId = reader.GetGuid(5),
                    OtherEquityAccountId = reader.GetGuid(6),
                    FiscalYearStarted = reader.GetDateTime(7),
                    FiscalYearPosted = reader.GetDateTime(8),
                    ReportingPeriod = await reader.GetFieldValueAsync<ReportingPeriod>(9),
                    CustomPosted = await SqliteUtilities.GetDateTimeAsync(reader, 10),
                    CustomStarted = await SqliteUtilities.GetDateTimeAsync(reader, 11),
                    Password = password
                };
            }
        }
    }
}
