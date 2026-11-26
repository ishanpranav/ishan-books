// SqliteUtilities.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using SQLitePCL;

namespace Liber.Sqlite;

public static class SqliteUtilities
{
    static SqliteUtilities()
    {
        Batteries.Init();
    }

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

    internal static async Task<decimal?> GetDecimalAsync(SqliteDataReader reader, int ordinal)
    {
        if (await reader.IsDBNullAsync(ordinal))
        {
            return null;
        }

        return reader.GetDecimal(ordinal);
    }
    public static object ValueOf(string? value)
    {
        if (value == null)
        {
            return DBNull.Value;
        }

        return value;
    }

    public static object ValueOf(Color value)
    {
        if (value.IsEmpty)
        {
            return DBNull.Value;
        }

        return value.ToArgb();
    }

    public static object ValueOf(decimal value)
    {
        if (value == 0)
        {
            return DBNull.Value;
        }

        return value;
    }

    public static object ValueOf(decimal? value)
    {
        if (value == null)
        {
            return DBNull.Value;
        }

        return value;
    }

    public static object ValueOf(Guid value)
    {
        if (value == Guid.Empty)
        {
            return DBNull.Value;
        }

        return value;
    }

    public static object ValueOf(DateTime? value)
    {
        if (value == null)
        {
            return DBNull.Value;
        }

        return value.Value;
    }
}
