﻿-- InsertTransaction.sql
-- Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
-- Licensed under the MIT License.

INSERT INTO "main"."Transaction" (
    "Id",
    "Posted",
    "Number",
    "Name",
    "Memo"
) VALUES (
    @id,
    @posted,
    @number,
    @name,
    @memo
);
