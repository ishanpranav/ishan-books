-- InsertSecurity.sql
-- Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
-- Licensed under the MIT License.

INSERT INTO "main"."Security" (
    "Id",
    "Symbol",
    "Name"
) VALUES (
    @id,
    @symbol,
    @name
);
