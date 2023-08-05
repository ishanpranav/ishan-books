// TaxType.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.ComponentModel;
using CsvHelper.Configuration.Attributes;

namespace Liber;

public enum TaxType
{
    [LocalizedDescription(nameof(None))]
    [Name("F")]
    None = 0,

    [LocalizedDescription(nameof(Other))]
    [Name("T")]
    Other = 1
}
