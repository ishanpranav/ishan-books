// Queries.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.Sqlite;

public static class Queries
{
    public const string Create = @"CREATE TABLE ""Account"" (
	""Id""	TEXT NOT NULL UNIQUE,
	""ParentId""	TEXT NOT NULL,
	""Number""	INTEGER NOT NULL DEFAULT 1 UNIQUE,
	""Name""	TEXT NOT NULL UNIQUE,
	""Type""	INTEGER NOT NULL DEFAULT 0,
	""Placeholder""	INTEGER NOT NULL DEFAULT 0,
	""Description""	TEXT,
	""Memo""	TEXT,
	""Color""	INTEGER NOT NULL DEFAULT 0,
	""TaxType""	INTEGER DEFAULT 0,
	FOREIGN KEY(""ParentId"") REFERENCES ""Account""(""Id""),
	PRIMARY KEY(""Id"")
);
";
}
