-- GnuCashSelectTransactions.sql
-- Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
-- Licensed under the MIT License.

SELECT 
    "transaction"."guid" "Id",
    "transaction".post_date "Posted",
    COALESCE(NULLIF("transaction".num, ''), 0) "Number",
    "transaction"."description" "Name",
    MIN(CASE WHEN slot."name" = 'notes' THEN slot.string_val END) "Memo"
FROM transactions "transaction"
LEFT JOIN slots slot
ON "transaction"."guid" = slot.obj_guid
GROUP BY "transaction"."guid";
