// SqliteSerializer.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

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

    private static async Task ExecuteNonQueryAsync(SqliteConnection connection, string text)
    {
        SqliteCommand command = connection.CreateCommand();

        command.CommandText = text;

        await command.ExecuteNonQueryAsync();
    }

    public static async Task SerializeAsync(string path, Company company)
    {
        await using SqliteConnection connection = CreateConnection(path);

        await connection.OpenAsync();
        await ExecuteNonQueryAsync(connection, Queries.Create);
    }

    public static Task<Company> DeserializeAsync(string path)
    {
        return Task.FromResult(null as Company);
    }
}
