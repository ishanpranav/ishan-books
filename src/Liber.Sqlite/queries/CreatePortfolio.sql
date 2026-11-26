-- CreatePortfolio.sql
-- Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
-- Licensed under the MIT License.

PRAGMA foreign_keys = OFF;

DROP TABLE IF EXISTS "Portfolio";
DROP TABLE IF EXISTS "Security";
DROP TABLE IF EXISTS "Trade";
DROP TABLE IF EXISTS "Price";
DROP TABLE IF EXISTS "Dividend";

PRAGMA foreign_keys = ON;

CREATE TABLE IF NOT EXISTS "Security" (
    "Id"        TEXT NOT NULL UNIQUE,
    "Symbol"    TEXT NOT NULL UNIQUE,
    "Name"      TEXT,
    "Color"     INT,
    PRIMARY KEY("Id")
);

CREATE TABLE IF NOT EXISTS "Trade" (
    "SecurityId" TEXT NOT NULL,
    "Posted"     TEXT NOT NULL,
    "Type"       INTEGER NOT NULL,
    "Cost"       INTEGER NOT NULL,
    "Quantity"   INTEGER NOT NULL,
    "Commission" INTEGER NOT NULL,
    FOREIGN KEY("SecurityId") REFERENCES "Security"("Id")
);

CREATE TABLE IF NOT EXISTS "Price" (
    "SecurityId"    TEXT NOT NULL,
    "Timestamp"     TEXT NOT NULL,
    "Close"         INTEGER NOT NULL,
    "AdjustedClose" INTEGER,
    PRIMARY KEY("SecurityId", "Timestamp"),
    FOREIGN KEY("SecurityId") REFERENCES "Security"("Id")
);

CREATE TABLE IF NOT EXISTS "Dividend" (
    "SecurityId" TEXT NOT NULL,
    "Timestamp"  TEXT NOT NULL,
    "Paid"       TEXT NOT NULL,
    "Value"      INTEGER NOT NULL,
    PRIMARY KEY("SecurityId", "Timestamp"),
    FOREIGN KEY("SecurityId") REFERENCES "Security"("Id")
);

INSERT INTO "main"."Portfolio" (
    "Name",
    "Color"
) VALUES (
    @name,
    @color
);
