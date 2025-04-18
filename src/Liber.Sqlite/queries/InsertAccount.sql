﻿-- InsertAccount.sql
-- Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
-- Licensed under the MIT License.

INSERT INTO "main"."Account" (
    "Id",
    "ParentId",
    "Number",
    "Name",
    "Type",
    "Placeholder",
    "Description",
    "Memo",
    "Color",
    "TaxType",
    "Inactive",
    "CashFlow"
) VALUES (
    @id,
    @parentId,
    @number,
    @name,
    @type,
    @placeholder,
    @description,
    @memo,
    @color,
    @taxType,
    @inactive,
    @cashFlow
);
