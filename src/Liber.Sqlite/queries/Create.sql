-- Create.sql
-- Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
-- Licensed under the MIT License.

PRAGMA foreign_keys = OFF;

DROP TABLE IF EXISTS "Company";
DROP TABLE IF EXISTS "Account";
DROP TABLE IF EXISTS "Transaction";
DROP TABLE IF EXISTS "Line";

PRAGMA foreign_keys = ON;

CREATE TABLE "Company" (
    "Id"                    INTEGER NOT NULL UNIQUE,
    "Name"                  TEXT,
    "NextAccountNumber"     INTEGER NOT NULL,
    "NextTransactionNumber" INTEGER NOT NULL,
    "Type"                  INTEGER NOT NULL,
    "Color"                 INTEGER,
    "EquityAccount"         TEXT,
    "OtherEquityAccount"    TEXT,
    PRIMARY KEY("Id" AUTOINCREMENT)
);

CREATE TABLE "Account" (
	"Id"	        TEXT    NOT NULL UNIQUE,
	"ParentId"	    TEXT,
	"Number"	    INTEGER          DEFAULT 0,
	"Name"	        TEXT    NOT NULL UNIQUE,
	"Type"	        INTEGER NOT NULL DEFAULT 0,
	"Placeholder"	INTEGER NOT NULL DEFAULT 0,
	"Description"	TEXT,
	"Memo"	        TEXT,
	"Color"	        INTEGER,
	"TaxType"	    INTEGER          DEFAULT 0,
    "Inactive"      INTEGER NOT NULL DEFAULT 0,
    "Adjustment"    INTEGER NOT NULL DEFAULT 0,
	PRIMARY KEY("Id"),
    FOREIGN KEY("ParentId") REFERENCES "Account"("Id")
);

CREATE TABLE "Transaction" (
    "Id"     TEXT NOT NULL UNIQUE,
    "Posted" TEXT NOT NULL,
    "Number" INTEGER       DEFAULT 0,
    "Name"   TEXT,
    "Memo"   TEXT,
    PRIMARY KEY("Id")
);

CREATE TABLE "Line" (
    "TransactionId" TEXT    NOT NULL,
    "AccountId"     TEXT    NOT NULL,
    "Balance"       INTEGER,
    "Description"   TEXT,
    "Reconciled"    TEXT,
    FOREIGN KEY("TransactionId") REFERENCES "Transaction"("Id"),
    FOREIGN KEY("AccountId")     REFERENCES "Account"("Id")
);

INSERT INTO "main"."Company" (
    "Name",
    "NextAccountNumber",
    "NextTransactionNumber",
    "Type",
    "Color",
    "EquityAccount",
    "OtherEquityAccount"
) VALUES (
    @name,
    @nextAccountNumber,
    @nextTransactionNumber,
    @type,
    @color,
    @equityAccount,
    @otherEquityAccount
);
