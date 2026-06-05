// Queries.generated.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.Sqlite;

internal static class Queries
{
    public const string Create = " PRAGMA foreign_keys = OFF; DROP TABLE IF EXISTS \"Company\"; DROP TABLE IF EXISTS \"Account\"; DROP TABLE IF EXISTS \"Transaction\"; DROP TABLE IF EXISTS \"Line\"; PRAGMA foreign_keys = ON; CREATE TABLE \"Company\" ( \"Id\" INTEGER NOT NULL UNIQUE, \"Name\" TEXT, \"NextAccountNumber\" INTEGER NOT NULL, \"NextTransactionNumber\" INTEGER NOT NULL, \"Type\" INTEGER NOT NULL, \"Color\" INTEGER, \"EquityAccount\" TEXT, \"OtherEquityAccount\" TEXT, PRIMARY KEY(\"Id\" AUTOINCREMENT) ); CREATE TABLE \"Account\" ( \"Id\" TEXT NOT NULL UNIQUE, \"ParentId\" TEXT, \"Number\" INTEGER DEFAULT 0, \"Name\" TEXT NOT NULL UNIQUE, \"Type\" INTEGER NOT NULL DEFAULT 0, \"Placeholder\" INTEGER NOT NULL DEFAULT 0, \"Description\" TEXT, \"Memo\" TEXT, \"Color\" INTEGER, \"TaxType\" TEXT, \"Inactive\" INTEGER NOT NULL DEFAULT 0, \"CashFlow\" INTEGER NOT NULL DEFAULT 0, PRIMARY KEY(\"Id\"), FOREIGN KEY(\"ParentId\") REFERENCES \"Account\"(\"Id\") ); CREATE TABLE \"Transaction\" ( \"Id\" TEXT NOT NULL UNIQUE, \"Posted\" TEXT NOT NULL, \"Number\" INTEGER DEFAULT 0, \"Name\" TEXT, \"Memo\" TEXT, PRIMARY KEY(\"Id\") ); CREATE TABLE \"Line\" ( \"TransactionId\" TEXT NOT NULL, \"AccountId\" TEXT NOT NULL, \"Balance\" INTEGER, \"Description\" TEXT, \"Reconciled\" TEXT, FOREIGN KEY(\"TransactionId\") REFERENCES \"Transaction\"(\"Id\"), FOREIGN KEY(\"AccountId\") REFERENCES \"Account\"(\"Id\") ); INSERT INTO \"main\".\"Company\" ( \"Name\", \"NextAccountNumber\", \"NextTransactionNumber\", \"Type\", \"Color\", \"EquityAccount\", \"OtherEquityAccount\" ) VALUES ( @name, @nextAccountNumber, @nextTransactionNumber, @type, @color, @equityAccount, @otherEquityAccount ); ";

    public const string GnuCashSelectAccounts = " SELECT account.\"guid\" \"Id\", COALESCE(account.parent_guid, '00000000-0000-0000-0000-000000000000') \"ParentId\", COALESCE(account.code, 0) \"Number\", account.\"name\" \"Name\", account.account_type \"Type\", account.placeholder \"Placeholder\", account.\"description\" \"Description\", MIN(CASE WHEN slot.\"name\" = 'notes' THEN slot.string_val END) \"Memo\", NULL \"Color\", MIN(CASE WHEN slot.\"name\" = 'tax-related' THEN CASE WHEN slot.string_val = 'true' THEN 1 ELSE 0 END END) \"TaxRelated\", account.\"hidden\" \"Inactive\", NULL \"CashFlow\" FROM accounts account LEFT JOIN slots slot ON account.\"guid\" = slot.obj_guid WHERE account.account_type <> 'ROOT' GROUP BY account.\"guid\"; ";

    public const string GnuCashSelectCompany = " SELECT (SELECT slot.string_val FROM slots slot WHERE slot.\"name\" = 'options/Business/Company Name' LIMIT 1) \"Name\", (SELECT slot.string_val FROM slots slot WHERE slot.\"name\" = 'tax_US/type' LIMIT 1) \"Type\", (SELECT account.\"guid\" FROM accounts account WHERE account.account_type = 'ROOT' LIMIT 1) \"EmptyParentId\" ; ";

    public const string InsertAccount = " INSERT INTO \"main\".\"Account\" ( \"Id\", \"ParentId\", \"Number\", \"Name\", \"Type\", \"Placeholder\", \"Description\", \"Memo\", \"Color\", \"TaxType\", \"Inactive\", \"CashFlow\" ) VALUES ( @id, @parentId, @number, @name, @type, @placeholder, @description, @memo, @color, @taxType, @inactive, @cashFlow ); ";

    public const string InsertLine = " INSERT INTO \"main\".\"Line\" ( \"TransactionId\", \"AccountId\", \"Balance\", \"Description\" ) VALUES ( @transactionId, @accountId, @balance, @description ); ";

    public const string InsertTransaction = " INSERT INTO \"main\".\"Transaction\" ( \"Id\", \"Posted\", \"Number\", \"Name\", \"Memo\" ) VALUES ( @id, @posted, @number, @name, @memo ); ";

    public const string SelectAccounts = " SELECT \"Id\", COALESCE(\"ParentId\", '00000000-0000-0000-0000-000000000000'), COALESCE(\"Number\", 0), \"Name\", \"Type\", \"Placeholder\", \"Description\", \"Memo\", \"Color\", \"TaxType\", \"Inactive\", \"CashFlow\" FROM \"main\".\"Account\"; ";

    public const string SelectCompany = " SELECT \"Name\", \"NextAccountNumber\", \"NextTransactionNumber\", \"Type\", \"Color\", \"EquityAccount\", \"OtherEquityAccount\" FROM \"main\".\"Company\" LIMIT 1; ";

    public const string SelectLines = " SELECT \"TransactionId\", \"AccountId\", \"Balance\", \"Description\" FROM \"main\".\"Line\"; ";

    public const string SelectTransactions = " SELECT \"Id\", \"Posted\", COALESCE(\"Number\", 0), \"Name\", \"Memo\" FROM \"main\".\"Transaction\"; ";
}
