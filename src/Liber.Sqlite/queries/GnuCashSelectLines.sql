-- GnuCashSelectLines.sql
-- Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
-- Licensed under the MIT License.

SELECT 
    tx_guid "TransactionId",
    account_guid "AccountId",
    (value_num / value_denom) "Balance",
    memo "Description"
FROM splits;
