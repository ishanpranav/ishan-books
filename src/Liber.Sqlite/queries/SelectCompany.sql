-- SelectCompany.sql
-- Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
-- Licensed under the MIT License.

SELECT 
    "Name",
    "NextAccountNumber",
    "NextTransactionNumber",
    "Type",
    "Color",
    "EquityAccount",
    "OtherEquityAccount",
    "FiscalYearStarted",
    "FiscalYearPosted",
    "ReportingPeriod",
    "CustomStarted",
    "CustomPosted"
FROM "main"."Company"
LIMIT 1;
