-- GnuCashSelectLines.sql
-- Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
-- Licensed under the MIT License.

SELECT 
    tx_guid "TransactionId",
    account_guid "AccountId",
    (CAST(value_num AS REAL) / value_denom) "Balance",
    memo "Description",
    (CASE WHEN reconcile_state = 'y' THEN reconcile_date END) "Reconciled" 
FROM splits;
