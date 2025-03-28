﻿-- SelectAccounts.sql
-- Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
-- Licensed under the MIT License.

SELECT 
    "Id",
    COALESCE("ParentId", "00000000-0000-0000-0000-000000000000"),
    COALESCE("Number", 0),
    "Name",
    "Type",
    "Placeholder",
    "Description",
    "Memo",
    "Color",
    "TaxType",
    "Inactive",
    "CashFlow"
FROM "main"."Account";
