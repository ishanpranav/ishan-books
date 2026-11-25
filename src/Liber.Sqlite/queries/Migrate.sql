-- Migrate.sql
-- Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
-- Licensed under the MIT License.

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
