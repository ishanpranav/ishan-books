-- SelectPrices.sql
-- Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
-- Licensed under the MIT License.

SELECT 
    "SecurityId",
    "Timestamp",
    "Close",
    "AdjustedClose",
    "Dividend"
FROM "main"."Price";
