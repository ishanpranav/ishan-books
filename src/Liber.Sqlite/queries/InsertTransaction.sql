-- InsertTransaction.sql
-- Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
-- Licensed under the MIT License.

INSERT INTO "main"."Transaction" (
    "Id",
    "Posted",
    "Number",
    "Name",
    "Memo"
) VALUES (
    @id,
    @posted,
    @number,
    @name,
    @memo
);
