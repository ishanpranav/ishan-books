// Queries.generated.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.Sqlite;

internal static class Queries
{
    public const string Create = @"PRAGMA foreign_keys = OFF;DROP TABLE IF EXISTS ""Company"";DROP TABLE IF EXISTS ""Account"";DROP TABLE IF EXISTS ""Transaction"";DROP TABLE IF EXISTS ""Line"";PRAGMA foreign_keys = ON;CREATE TABLE ""Company"" (""Id"" INTEGER NOT NULL UNIQUE,""Name"" TEXT,""NextAccountNumber"" INTEGER NOT NULL,""NextTransactionNumber"" INTEGER NOT NULL,""Type"" INTEGER NOT NULL,""Color"" INTEGER,""EquityAccount"" TEXT,""OtherEquityAccount"" TEXT,PRIMARY KEY(""Id"" AUTOINCREMENT));CREATE TABLE ""Account"" (""Id"" TEXT NOT NULL UNIQUE,""ParentId"" TEXT,""Number"" INTEGER DEFAULT 0,""Name"" TEXT NOT NULL UNIQUE,""Type"" INTEGER NOT NULL DEFAULT 0,""Placeholder"" INTEGER NOT NULL DEFAULT 0,""Description"" TEXT,""Memo"" TEXT,""Color"" INTEGER,""TaxType"" INTEGER DEFAULT 0,PRIMARY KEY(""Id""),FOREIGN KEY(""ParentId"") REFERENCES ""Account""(""Id""));CREATE TABLE ""Transaction"" (""Id"" TEXT NOT NULL UNIQUE,""Posted"" TEXT NOT NULL,""Number"" INTEGER DEFAULT 0,""Name"" TEXT,""Memo"" TEXT,PRIMARY KEY(""Id""));CREATE TABLE ""Line"" (""TransactionId"" TEXT NOT NULL,""AccountId"" TEXT NOT NULL,""Balance"" INTEGER,""Description"" TEXT,FOREIGN KEY(""TransactionId"") REFERENCES ""Transaction""(""Id""),FOREIGN KEY(""AccountId"") REFERENCES ""Account""(""Id""));INSERT INTO ""main"".""Company"" (""Name"",""NextAccountNumber"",""NextTransactionNumber"",""Type"",""Color"",""EquityAccount"",""OtherEquityAccount"") VALUES (@name,@nextAccountNumber,@nextTransactionNumber,@type,@color,@equityAccount,@otherEquityAccount);";

    public const string InsertAccount = "INSERT INTO \"main\".\"Account\" (\"Id\",\"ParentId\",\"Number\",\"Name\",\"Type\",\"Placeholder" +
    "\",\"Description\",\"Memo\",\"Color\",\"TaxType\") VALUES (@id,@parentId,@number,@name,@t" +
    "ype,@placeholder,@description,@memo,@color,@taxType);";

    public const string InsertLine = "INSERT INTO \"main\".\"Line\" (\"TransactionId\",\"AccountId\",\"Balance\",\"Description\") V" +
    "ALUES (@transactionId,@accountId,@balance,@description);";

    public const string InsertTransaction = "INSERT INTO \"main\".\"Transaction\" (\"Id\",\"Posted\",\"Number\",\"Name\",\"Memo\") VALUES (@" +
    "id,@posted,@number,@name,@memo);";

    public const string SelectAccounts = "SELECT\"Id\",COALESCE(\"ParentId\", \"00000000-0000-0000-0000-000000000000\"),COALESCE(" +
    "\"Number\", 0),\"Name\",\"Type\",\"Placeholder\",\"Description\",\"Memo\",\"Color\",\"TaxType\"F" +
    "ROM \"main\".\"Account\";";

    public const string SelectCompany = "SELECT\"Name\",\"NextAccountNumber\",\"NextTransactionNumber\",\"Type\",\"Color\",\"EquityAc" +
    "count\",\"OtherEquityAccount\"FROM \"main\".\"Company\"LIMIT 1;";

    public const string SelectLines = "SELECT\"TransactionId\",\"AccountId\",\"Balance\",\"Description\"FROM \"main\".\"Line\";";

    public const string SelectTransactions = "SELECT\"Id\",\"Posted\",COALESCE(\"Number\", 0),\"Name\",\"Memo\"FROM \"main\".\"Transaction\";" +
    "";
}
