-- InsertLine.sql
-- Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
-- Licensed under the MIT License.

INSERT INTO "main"."Line" (
    "TransactionId",
    "AccountId",
    "Balance",
    "Description",
    "Reconciled"
) VALUES (
    @transactionId,
    @accountId,
    @balance,
    @description,
    @reconciled
);
