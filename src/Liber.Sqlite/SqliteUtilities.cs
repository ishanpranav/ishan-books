// SqliteUtilities.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Data.Common;
using System.Drawing;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace Liber.Sqlite;

internal static class SqliteUtilities
{
    public static SqliteConnection CreateConnection(string path, string? password = null)
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

    public static async Task<string?> GetStringAsync(DbDataReader reader, int ordinal)
    {
        if (await reader.IsDBNullAsync(ordinal))
        {
            return null;
        }

        return reader.GetString(ordinal);
    }

    public static async Task<Color> GetColorAsync(DbDataReader reader, int ordinal)
    {
        if (await reader.IsDBNullAsync(ordinal))
        {
            return Color.Empty;
        }

        return Color.FromArgb(reader.GetInt32(ordinal));
    }

    public static async Task<DateTime?> GetDateTimeAsync(SqliteDataReader reader, int ordinal)
    {
        if (await reader.IsDBNullAsync(ordinal))
        {
            return null;
        }

        return reader.GetDateTime(ordinal).Date;
    }
}
