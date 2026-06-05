-- GnuCashSelectCompany.sql
-- Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
-- Licensed under the MIT License.

SELECT
    (SELECT slot.string_val
     FROM slots slot
     WHERE slot."name" = 'options/Business/Company Name'
     LIMIT 1) "Name",
    (SELECT slot.string_val
     FROM slots slot
     WHERE slot."name" = 'tax_US/type'
     LIMIT 1) "Type",
    (SELECT account."guid"
     FROM accounts account
     WHERE account.account_type = 'ROOT'
     LIMIT 1) "EmptyParentId"
;
