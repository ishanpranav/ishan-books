// SqlitePortfolioSerializer.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace Liber.Sqlite;

public static class SqlitePortfolioSerializer
{
    public static async Task SerializeAsync(string path, Portfolio value, IProgress progress)
    {
        await using SqliteConnection connection = SqliteUtilities.CreateConnection(path, value.Password);

        await connection.OpenAsync();

        await using (SqliteCommand command = connection.CreateCommand())
        {
            command.CommandText = Queries.CreatePortfolio;

            command.Parameters.AddWithValue("@name", SqliteUtilities.ValueOf(value.Name));
            command.Parameters.AddWithValue("@color", SqliteUtilities.ValueOf(value.Color));

            await command.ExecuteNonQueryAsync();
        }

        await using (SqliteCommand command = connection.CreateCommand())
        {
            command.CommandText = Queries.DisableForeignKeys;

            await command.ExecuteNonQueryAsync();
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
                    command.Parameters.AddWithValue("@name", SqliteUtilities.ValueOf(security.Name));
                    command.Parameters.AddWithValue("@color", SqliteUtilities.ValueOf(security.Color));

                    await command.ExecuteNonQueryAsync();
                }

                progress.Tick();

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
                        command.Parameters.AddWithValue("@adjustedClose", SqliteUtilities.ValueOf(price.AdjustedClose));

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
                        command.Parameters.AddWithValue("@paid", dividend.Paid);
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

    public static async Task<Portfolio> DeserializeAsync(string path, string password)
    {
        await using SqliteConnection connection = SqliteUtilities.CreateConnection(path, password);

        await connection.OpenAsync();

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

                    values.Add(new Dividend(reader.GetDateTime(1), reader.GetDateTime(2))
                    {
                        Value = reader.GetDecimal(3)
                    });
                }
            }
        }

        List<Security> securities = new List<Security>();

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

                    securities.Add(new Security(id, reader.GetString(1), securityTrades, securityPrices, securityDividends)
                    {
                        Name = await SqliteUtilities.GetStringAsync(reader, 2),
                        Color = await SqliteUtilities.GetColorAsync(reader, 3)
                    });
                }
            }
        }

        await using (SqliteCommand command = connection.CreateCommand())
        {
            command.CommandText = Queries.SelectPortfolio;

            await using (SqliteDataReader reader = await command.ExecuteReaderAsync(CommandBehavior.SingleRow))
            {
                await reader.ReadAsync();

                return new Portfolio(securities)
                {
                    Name = await SqliteUtilities.GetStringAsync(reader, 0),
                    Color = await SqliteUtilities.GetColorAsync(reader, 1),
                    Password = password
                };
            }
        }
    }
}
