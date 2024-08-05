-- SelectTransactions.sql
-- Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
-- Licensed under the MIT License.

SELECT 
    "Id",
    "Posted",
    COALESCE("Number", 0),
    "Name",
    "Memo"
FROM "main"."Transaction";
