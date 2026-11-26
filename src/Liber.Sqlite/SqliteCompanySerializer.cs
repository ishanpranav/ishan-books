// SqliteCompanySerializer.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace Liber.Sqlite;

public static class SqliteCompanySerializer
{
    public static async Task SerializeAsync(string path, Company value, IProgress progress)
    {
        await using SqliteConnection connection = SqliteUtilities.CreateConnection(path, value.Password);

        await connection.OpenAsync();

        await using (SqliteCommand command = connection.CreateCommand())
        {
            command.CommandText = Queries.CreateCompany;

            command.Parameters.AddWithValue("@name", SqliteUtilities.ValueOf(value.Name));
            command.Parameters.AddWithValue("@nextAccountNumber", value.NextAccountNumber);
            command.Parameters.AddWithValue("@nextTransactionNumber", value.NextTransactionNumber);
            command.Parameters.AddWithValue("@type", value.Type);
            command.Parameters.AddWithValue("@color", SqliteUtilities.ValueOf(value.Color));
            command.Parameters.AddWithValue("@equityAccount", value.EquityAccountId);
            command.Parameters.AddWithValue("@otherEquityAccount", value.OtherEquityAccountId);
            command.Parameters.AddWithValue("@fiscalYearStarted", value.FiscalYearStarted);
            command.Parameters.AddWithValue("@fiscalYearPosted", value.FiscalYearPosted);
            command.Parameters.AddWithValue("@reportingPeriod", value.ReportingPeriod);
            command.Parameters.AddWithValue("@customStarted", SqliteUtilities.ValueOf(value.CustomStarted));
            command.Parameters.AddWithValue("@customPosted", SqliteUtilities.ValueOf(value.CustomPosted));

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
                    command.Parameters.AddWithValue("@parentId", SqliteUtilities.ValueOf(account.ParentId));
                    command.Parameters.AddWithValue("@number", SqliteUtilities.ValueOf(account.Number));
                    command.Parameters.AddWithValue("@name", account.Name);
                    command.Parameters.AddWithValue("@type", account.Type);
                    command.Parameters.AddWithValue("@placeholder", account.Placeholder);
                    command.Parameters.AddWithValue("@description", SqliteUtilities.ValueOf(account.Description));
                    command.Parameters.AddWithValue("@memo", SqliteUtilities.ValueOf(account.Memo));
                    command.Parameters.AddWithValue("@color", SqliteUtilities.ValueOf(account.Color));
                    command.Parameters.AddWithValue("@taxType", account.TaxType);
                    command.Parameters.AddWithValue("@inactive", account.Inactive);
                    command.Parameters.AddWithValue("@cashFlow", account.CashFlow);

                    await command.ExecuteNonQueryAsync();
                }

                progress.Tick();
            }

            foreach (Transaction transaction in value.Transactions)
            {
                await using (SqliteCommand command = connection.CreateCommand())
                {
                    command.Transaction = dbTransaction;
                    command.CommandText = Queries.InsertTransaction;

                    command.Parameters.AddWithValue("@id", transaction.Id);
                    command.Parameters.AddWithValue("@posted", transaction.Posted);
                    command.Parameters.AddWithValue("@number", SqliteUtilities.ValueOf(transaction.Number));
                    command.Parameters.AddWithValue("@name", SqliteUtilities.ValueOf(transaction.Name));
                    command.Parameters.AddWithValue("@memo", SqliteUtilities.ValueOf(transaction.Memo));

                    await command.ExecuteNonQueryAsync();
                }

                foreach (Line line in transaction.Lines)
                {
                    await using (SqliteCommand command = connection.CreateCommand())
                    {
                        command.Transaction = dbTransaction;
                        command.CommandText = Queries.InsertLine;

                        command.Parameters.AddWithValue("@transactionId", transaction.Id);
                        command.Parameters.AddWithValue("@accountId", line.AccountId);
                        command.Parameters.AddWithValue("@balance", line.Balance);
                        command.Parameters.AddWithValue("@description", SqliteUtilities.ValueOf(line.Description));
                        command.Parameters.AddWithValue("@reconciled", SqliteUtilities.ValueOf(line.Reconciled));

                        await command.ExecuteNonQueryAsync();
                    }
                }

                progress.Tick();
            }
            await dbTransaction.CommitAsync();
        }

        await using (SqliteCommand command = connection.CreateCommand())
        {
            command.CommandText = Queries.EnableForeignKeys;

            await command.ExecuteNonQueryAsync();
        }
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

                    accounts.Add(new Account(
                        id,
                        reader.GetGuid(1),
                        reader.GetDecimal(2),
                        reader.GetString(3),
                        await reader.GetFieldValueAsync<AccountType>(4))
                    {
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

        Dictionary<Guid, List<Line>> lines = new Dictionary<Guid, List<Line>>();

        await using (SqliteCommand command = connection.CreateCommand())
        {
            command.CommandText = Queries.SelectLines;

            await using (SqliteDataReader reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    Guid transactionId = reader.GetGuid(0);

                    if (!lines.TryGetValue(transactionId, out List<Line>? values))
                    {
                        values = new List<Line>();
                        lines[transactionId] = values;
                    }

                    values.Add(new Line(
                        reader.GetGuid(1),
                        reader.GetDecimal(2),
                        await SqliteUtilities.GetStringAsync(reader, 3),
                        await SqliteUtilities.GetDateTimeAsync(reader, 4)));
                }
            }
        }

        List<Transaction> transactions = new List<Transaction>();

        await using (SqliteCommand command = connection.CreateCommand())
        {
            command.CommandText = Queries.SelectTransactions;

            await using (SqliteDataReader reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    Guid id = reader.GetGuid(0);

                    transactions.Add(new Transaction(
                        id,
                        reader.GetDecimal(2),
                        await SqliteUtilities.GetStringAsync(reader, 3),
                        reader.GetDateTime(1).Date,
                        lines[id])
                    {
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

                return new Company(
                    accounts,
                    transactions,
                    reader.GetDecimal(1),
                    reader.GetDecimal(2))
                {
                    Name = await SqliteUtilities.GetStringAsync(reader, 0),
                    Type = await reader.GetFieldValueAsync<CompanyType>(3),
                    Color = await SqliteUtilities.GetColorAsync(reader, 4),
                    EquityAccountId = reader.GetGuid(5),
                    OtherEquityAccountId = reader.GetGuid(6),
                    FiscalYearStarted = reader.GetDateTime(7),
                    FiscalYearPosted = reader.GetDateTime(8),
                    ReportingPeriod = await reader.GetFieldValueAsync<ReportingPeriod>(9),
                    CustomStarted = await SqliteUtilities.GetDateTimeAsync(reader, 10),
                    CustomPosted = await SqliteUtilities.GetDateTimeAsync(reader, 11),
                    Password = password
                };
            }
        }
    }
}
