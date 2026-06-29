-- GnuCashSelectAccounts.sql
-- Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
-- Licensed under the MIT License.

SELECT
	account."guid" "Id",
	COALESCE(account.parent_guid, '00000000-0000-0000-0000-000000000000') "ParentId",
	COALESCE(account.code, 0) "Number",
	account."name" "Name",
	account.account_type "Type",
	account.placeholder "Placeholder",
	account."description" "Description",
	MIN(CASE WHEN slot."name" = 'notes' THEN slot.string_val END) "Memo",
	MIN(CASE WHEN slot."name" = 'color' THEN slot.string_val END) "Color",
	MIN(CASE WHEN slot."name" = 'tax-related' THEN
            CASE WHEN slot.string_val = 'true' THEN 1 ELSE 0 END
        END) "TaxRelated",
	account."hidden" "Inactive",
    NULL "CashFlow",
    MIN(CASE WHEN slot."name" = 'reconcile-info/last-date' THEN slot.int64_val END) "Reconciled"
FROM accounts account
LEFT JOIN slots slot
ON account."guid" = slot.obj_guid
WHERE account.account_type <> 'ROOT'
GROUP BY account."guid";
