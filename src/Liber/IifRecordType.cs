// IifRecordType.cs
// Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using CsvHelper.Configuration.Attributes;

namespace Liber;

public enum IifRecordType
{
    None = 0,

    [Name("HDR")]
    Company,

    [Name("ACCNT")]
    Account
}
