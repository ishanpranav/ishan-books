// SqliteSerializer.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Transactions;
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

    private static object ValueOf(decimal? value)
    {
        if (value == null)
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
            command.CommandText = Queries.Migrate;

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

                        command.Parameters.AddWithValue("@transactionId", transaction.Id);
                        command.Parameters.AddWithValue("@accountId", line.AccountId);
                        command.Parameters.AddWithValue("@balance", line.Balance);
                        command.Parameters.AddWithValue("@description", ValueOf(line.Description));
                        command.Parameters.AddWithValue("@reconciled", ValueOf(line.Reconciled));

                        await command.ExecuteNonQueryAsync();
                    }
                }

                progress.WriteTransaction();
            }
        }

        await using (SqliteTransaction dbTransaction = connection.BeginTransaction())
        {
            foreach (Security security in value.Securities)
            {
                await using (SqliteCommand command = connection.CreateCommand())
                {
                    command.Transaction = dbTransaction;
                    command.CommandText = Queries.InsertSecurity;

                    command.Parameters.AddWithValue("@id", security.Id);
                    command.Parameters.AddWithValue("@symbol", security.Symbol);
                    command.Parameters.AddWithValue("@name", ValueOf(security.Name));
                    command.Parameters.AddWithValue("@color", ValueOf(security.Color));

                    await command.ExecuteNonQueryAsync();
                }

                progress.WriteSecurity();

                foreach (Trade trade in security.Trades)
                {
                    await using (SqliteCommand command = connection.CreateCommand())
                    {
                        command.Transaction = dbTransaction;
                        command.CommandText = Queries.InsertTrade;

                        command.Parameters.AddWithValue("@securityId", security.Id);
                        command.Parameters.AddWithValue("@posted", trade.Posted);
                        command.Parameters.AddWithValue("@type", trade.Type);
                        command.Parameters.AddWithValue("@cost", trade.Cost);
                        command.Parameters.AddWithValue("@quantity", trade.Quantity);
                        command.Parameters.AddWithValue("@commission", trade.Commission);

                        await command.ExecuteNonQueryAsync();
                    }
                }

                foreach (Price price in security.Prices)
                {
                    await using (SqliteCommand command = connection.CreateCommand())
                    {
                        command.Transaction = dbTransaction;
                        command.CommandText = Queries.InsertPrice;

                        command.Parameters.AddWithValue("@securityId", security.Id);
                        command.Parameters.AddWithValue("@timestamp", price.Timestamp);
                        command.Parameters.AddWithValue("@close", price.Close);
                        command.Parameters.AddWithValue("@adjustedClose", ValueOf(price.AdjustedClose));

                        await command.ExecuteNonQueryAsync();
                    }
                }

                foreach (Dividend dividend in security.Dividends)
                {
                    await using (SqliteCommand command = connection.CreateCommand())
                    {
                        command.Transaction = dbTransaction;
                        command.CommandText = Queries.InsertDividend;

                        command.Parameters.AddWithValue("@securityId", security.Id);
                        command.Parameters.AddWithValue("@timestamp", dividend.Timestamp);
                        command.Parameters.AddWithValue("@value", dividend.Value);

                        await command.ExecuteNonQueryAsync();
                    }
                }
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
        catch (SqliteException exception)
        {
            if (exception.SqliteErrorCode == 26)
            {
                return false;
            }

            throw;
        }

        return true;
    }

    public static async Task<Company> DeserializeAsync(string path, string password)
    {
        await using SqliteConnection connection = SqliteUtilities.CreateConnection(path, password);

        await connection.OpenAsync();

        await using (SqliteCommand command = connection.CreateCommand())
        {
            command.CommandText = Queries.Migrate;

            await command.ExecuteNonQueryAsync();
        }

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

        Dictionary<Guid, List<Trade>> trades = new Dictionary<Guid, List<Trade>>();

        await using (SqliteCommand command = connection.CreateCommand())
        {
            command.CommandText = Queries.SelectTrades;

            await using (SqliteDataReader reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    Guid securityId = reader.GetGuid(0);

                    if (!trades.TryGetValue(securityId, out List<Trade>? values))
                    {
                        values = new List<Trade>();
                        trades[securityId] = values;
                    }

                    values.Add(new Trade(
                        reader.GetDateTime(1),
                        await reader.GetFieldValueAsync<TradeType>(2),
                        reader.GetDecimal(3),
                        reader.GetDecimal(4),
                        reader.GetDecimal(5)));
                }
            }
        }

        Dictionary<Guid, List<Price>> prices = new Dictionary<Guid, List<Price>>();

        await using (SqliteCommand command = connection.CreateCommand())
        {
            command.CommandText = Queries.SelectPrices;

            await using (SqliteDataReader reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    Guid securityId = reader.GetGuid(0);

                    if (!prices.TryGetValue(securityId, out List<Price>? values))
                    {
                        values = new List<Price>();
                        prices[securityId] = values;
                    }

                    values.Add(new Price(reader.GetDateTime(1))
                    {
                        Close = reader.GetDecimal(2),
                        AdjustedClose = await SqliteUtilities.GetDecimalAsync(reader, 3)
                    });
                }
            }
        }

        Dictionary<Guid, List<Dividend>> dividends = new Dictionary<Guid, List<Dividend>>();

        await using (SqliteCommand command = connection.CreateCommand())
        {
            command.CommandText = Queries.SelectDividends;

            await using (SqliteDataReader reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    Guid securityId = reader.GetGuid(0);

                    if (!dividends.TryGetValue(securityId, out List<Dividend>? values))
                    {
                        values = new List<Dividend>();
                        dividends[securityId] = values;
                    }

                    values.Add(new Dividend(reader.GetDateTime(1))
                    {
                        Value = reader.GetDecimal(2)
                    });
                }
            }
        }

        Dictionary<Guid, Security> securities = new Dictionary<Guid, Security>();

        await using (SqliteCommand command = connection.CreateCommand())
        {
            command.CommandText = Queries.SelectSecurities;

            await using (SqliteDataReader reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    Guid id = reader.GetGuid(0);

                    if (!trades.TryGetValue(id, out List<Trade>? securityTrades))
                    {
                        securityTrades = new List<Trade>();
                    }

                    if (!prices.TryGetValue(id, out List<Price>? securityPrices))
                    {
                        securityPrices = new List<Price>();
                    }

                    if (!dividends.TryGetValue(id, out List<Dividend>? securityDividends))
                    {
                        securityDividends = new List<Dividend>();
                    }

                    securities.Add(id, new Security(id, reader.GetString(1), securityTrades, securityPrices, securityDividends)
                    {
                        Name = await SqliteUtilities.GetStringAsync(reader, 2),
                        Color = await SqliteUtilities.GetColorAsync(reader, 3)
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
                    securities.Values,
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
