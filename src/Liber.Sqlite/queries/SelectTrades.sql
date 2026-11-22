-- SelectTrades.sql
-- Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
-- Licensed under the MIT License.

SELECT 
    "Id",
    "SecurityId",
    "Posted",
    "Type",
    "Cost",
    "Quantity",
    "Commission"
FROM "main"."Trade";
