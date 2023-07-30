-- SelectCompany.sql
-- Copyright (c) 2023 Ishan Pranav. All rights reserved.
-- Licensed under the MIT License.

SELECT 
    "Name",
    "NextAccountNumber",
    "NextTransactionNumber",
    "Type",
    "Color",
    "EquityAccount",
    "OtherEquityAccount"
FROM "main"."Company"
LIMIT 1;
