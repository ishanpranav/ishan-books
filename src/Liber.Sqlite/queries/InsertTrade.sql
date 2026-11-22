-- InsertTrade.sql
-- Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
-- Licensed under the MIT License.

INSERT INTO "main"."Trade" (
    "Id",
    "SecurityId",
    "Posted",
    "Type",
    "Cost",
    "Quantity",
    "Commission"
) VALUES (
    @id,
    @securityId,    
    @posted,
    @type,
    @cost,
    @quantity,
    @commission
);
