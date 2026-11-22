-- InsertPrice.sql
-- Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
-- Licensed under the MIT License.

INSERT INTO "main"."Price" (
    "SecurityId",
    "Timestamp",
    "Close",
    "AdjustedClose",
    "Dividend"
) VALUES (
    @securityId,
    @timestamp,
    @close,
    @adjustedClose,
    @dividend
);
