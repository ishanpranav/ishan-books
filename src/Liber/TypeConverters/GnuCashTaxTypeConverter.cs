// GnuCashTaxTypeConverter.cs
// Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using CsvHelper.Configuration;
using Liber;

namespace CsvHelper.TypeConversion;

internal sealed class GnuCashTaxTypeConverter : DefaultTypeConverter
{
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return TaxType.None;
        }

        if (Enum.TryParse(text, ignoreCase: true, out TaxType result))
        {
            return result;
        }

        return TaxType.Other;
    }

    public override string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
    {
        if (value is TaxType.None)
        {
            return "F";
        }
        else
        {
            return "T";
        }
    }
}
