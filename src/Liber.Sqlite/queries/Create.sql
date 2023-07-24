-- Create.sql
-- Copyright (c) 2023 Ishan Pranav. All rights reserved.
-- Licensed under the MIT License.

CREATE TABLE "Account" (
	"Id"	        TEXT NOT NULL UNIQUE,
	"ParentId"	    TEXT NOT NULL,
	"Number"	    INTEGER NOT NULL DEFAULT 1 UNIQUE,
	"Name"	        TEXT NOT NULL UNIQUE,
	"Type"	        INTEGER NOT NULL DEFAULT 0,
	"Placeholder"	INTEGER NOT NULL DEFAULT 0,
	"Description"	TEXT,
	"Memo"	        TEXT,
	"Color"	        INTEGER NOT NULL DEFAULT 0,
	"TaxType"	    INTEGER DEFAULT 0,
	PRIMARY KEY("Id")
);
