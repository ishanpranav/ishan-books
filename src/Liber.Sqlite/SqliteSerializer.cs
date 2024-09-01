// SqliteSerializer.cs
// Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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

    private static async Task<string?> GetStringAsync(DbDataReader reader, int ordinal)
    {
        if (await reader.IsDBNullAsync(ordinal))
        {
            return null;
        }

        return reader.GetString(ordinal);
    }

    private static async Task<Color> GetColorAsync(DbDataReader reader, int ordinal)
    {
        if (await reader.IsDBNullAsync(ordinal))
        {
            return Color.Empty;
        }

        return Color.FromArgb(reader.GetInt32(ordinal));
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

    private static SqliteConnection CreateConnection(string path, string? password)
    {
        SqliteConnectionStringBuilder connectionStringBuilder = new SqliteConnectionStringBuilder()
        {
            DataSource = path
        };

        if (password != null)
        {
            connectionStringBuilder.Password = password;
        }

        return new SqliteConnection(connectionStringBuilder.ConnectionString);
    }

    public static async Task SerializeAsync(string path, Company value)
    {
        await using SqliteConnection connection = CreateConnection(path, value.Password);

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

            await command.ExecuteNonQueryAsync();
        }

        await using (DbTransaction dbTransaction = await connection.BeginTransactionAsync())
        {
            foreach (KeyValuePair<Guid, Account> account in value.Accounts)
            {
                await using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = Queries.InsertAccount;

                    command.Parameters.AddWithValue("@id", account.Key);
                    command.Parameters.AddWithValue("@parentId", ValueOf(account.Value.ParentId));
                    command.Parameters.AddWithValue("@number", ValueOf(account.Value.Number));
                    command.Parameters.AddWithValue("@name", account.Value.Name);
                    command.Parameters.AddWithValue("@type", account.Value.Type);
                    command.Parameters.AddWithValue("@placeholder", account.Value.Placeholder);
                    command.Parameters.AddWithValue("@description", ValueOf(account.Value.Description));
                    command.Parameters.AddWithValue("@memo", ValueOf(account.Value.Memo));
                    command.Parameters.AddWithValue("@color", ValueOf(account.Value.Color));
                    command.Parameters.AddWithValue("@taxType", account.Value.TaxType);
                    command.Parameters.AddWithValue("@inactive", account.Value.Inactive);
                    command.Parameters.AddWithValue("@cashFlow", account.Value.CashFlow);

                    await command.ExecuteNonQueryAsync();
                }
            }

            foreach (Transaction transaction in value.Transactions)
            {
                await using (SqliteCommand command = connection.CreateCommand())
                {
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
                        command.CommandText = Queries.InsertLine;

                        command.Parameters.AddWithValue("@accountId", line.AccountId);
                        command.Parameters.AddWithValue("@balance", line.Balance);
                        command.Parameters.AddWithValue("@description", ValueOf(line.Description));
                        command.Parameters.AddWithValue("@transactionId", transaction.Id);

                        await command.ExecuteNonQueryAsync();
                    }
                }
            }

            await dbTransaction.CommitAsync();
        }
    }

    public static async Task<Company> DeserializeAsync(string path, string password)
    {
        await using SqliteConnection connection = CreateConnection(path, password);

        await connection.OpenAsync();

        Dictionary<Guid, Account> accounts = new Dictionary<Guid, Account>();
        Dictionary<Guid, Transaction> transactions = new Dictionary<Guid, Transaction>();

        await using (SqliteCommand command = connection.CreateCommand())
        {
            command.CommandText = Queries.SelectAccounts;

            await using (SqliteDataReader reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    accounts.Add(reader.GetGuid(0), new Account(reader.GetGuid(1))
                    {
                        Number = reader.GetDecimal(2),
                        Name = reader.GetString(3),
                        Type = await reader.GetFieldValueAsync<AccountType>(4),
                        Placeholder = reader.GetBoolean(5),
                        Description = await GetStringAsync(reader, 6),
                        Memo = await GetStringAsync(reader, 7),
                        Color = await GetColorAsync(reader, 8),
                        TaxType = await reader.GetFieldValueAsync<TaxType>(9),
                        Inactive = reader.GetBoolean(10),
                        CashFlow = await reader.GetFieldValueAsync<CashFlow>(11)
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

                    transactions.Add(id, new Transaction()
                    {
                        Id = id,
                        Posted = reader.GetDateTime(1),
                        Number = reader.GetDecimal(2),
                        Name = await GetStringAsync(reader, 3),
                        Memo = await GetStringAsync(reader, 4)
                    });
                }
            }
        }

        await using (SqliteCommand command = connection.CreateCommand())
        {
            command.CommandText = Queries.SelectLines;

            await using (SqliteDataReader reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    Guid id = reader.GetGuid(0);

                    transactions[id].Lines.Add(new Line()
                    {
                        AccountId = reader.GetGuid(1),
                        Balance = reader.GetDecimal(2),
                        Description = await GetStringAsync(reader, 3)
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

                return new Company(accounts, transactions.Values, reader.GetDecimal(1), reader.GetDecimal(2))
                {
                    Name = await GetStringAsync(reader, 0),
                    Type = await reader.GetFieldValueAsync<CompanyType>(3),
                    Color = await GetColorAsync(reader, 4),
                    EquityAccountId = reader.GetGuid(5),
                    OtherEquityAccountId = reader.GetGuid(6),
                    Password = password
                };
            }
        }
    }
}
