// FilterIndex.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.IO;

namespace Liber.Forms;

internal enum FilterIndex
{
    None = 0,
    Offset = -2,
    AllFiles = 1,
    AllSupportedFiles = 2,
    Liber = 3,
    Json = 4,
    Sqlite = 5,
    Csv = 6,
    Pdf = 7,
    GnuCashSqlite = 10,
    Iif = 8,
    Xml = 9
}

internal static class FilterIndexExtensions
{
    public static FilterIndex FromExtension(string value)
    {
        switch (value.ToUpperInvariant())
        {
            case ".IZBK": return FilterIndex.Liber;

            case ".SQLITE":
            case ".SQLITE3":
            case ".DB":
            case ".DB3":
            case ".S3DB":
            case ".SL3":
                return FilterIndex.Sqlite;

            case ".JSON": return FilterIndex.Json;
            case ".XML": return FilterIndex.Xml;
            case ".GNUCASH": return FilterIndex.GnuCashSqlite;
            case ".CSV": return FilterIndex.Csv;
            case ".IIF": return FilterIndex.Iif;
        }

        return FilterIndex.None;
    }
}
