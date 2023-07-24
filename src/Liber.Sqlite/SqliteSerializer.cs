// SqliteSerializer.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace Liber.Sqlite;

public static class SqliteSerializer
{
    private static SqliteConnection CreateConnection(string path)
    {
        return new SqliteConnection(new SqliteConnectionStringBuilder()
        {
            DataSource = path
        }.ConnectionString);
    }

    public static async Task SerializeAsync(string path, Company company)
    {
        File.Delete(path);

        await using SqliteConnection connection = CreateConnection(path);

        await connection.OpenAsync();

        await using (SqliteCommand command = connection.CreateCommand())
        {
            command.CommandText = Queries.Create;

            await command.ExecuteNonQueryAsync();
        }

        await using (DbTransaction transaction = await connection.BeginTransactionAsync())
        {
            foreach (KeyValuePair<Guid, Account> account in company.Accounts)
            {
                await using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = Queries.InsertAccount;

                    command.Parameters.AddWithValue("@id", account.Key);
                    command.Parameters.AddWithValue("@parentId", account.Value.ParentKey);
                    command.Parameters.AddWithValue("@number", account.Value.Number);
                    command.Parameters.AddWithValue("@name", account.Value.Name);
                    command.Parameters.AddWithValue("@type", account.Value.Type);
                    command.Parameters.AddWithValue("@placeholder", account.Value.Placeholder);
                    command.Parameters.AddWithValue("@description", (object?)account.Value.Description ?? DBNull.Value);
                    command.Parameters.AddWithValue("@memo", (object?)account.Value.Memo ?? DBNull.Value);
                    command.Parameters.AddWithValue("@color", account.Value.Color.ToArgb());
                    command.Parameters.AddWithValue("@taxType", account.Value.TaxType);

                    await command.ExecuteNonQueryAsync();
                }
            }

            await transaction.CommitAsync();
        }
    }

    public static Task<Company> DeserializeAsync(string path)
    {
        return Task.FromResult(new Company());
    }
}
