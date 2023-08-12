// GnuCashAccountTypeConverter.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace Liber.TypeConverters;

internal sealed class GnuCashAccountTypeConverter : DefaultTypeConverter
{
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        switch (text)
        {
            case "BANK":
            case "CASH":
                return AccountType.Bank;

            case "CREDIT":
                return AccountType.CreditCard;

            case "EQUITY":
                return AccountType.Equity;

            case "EXPENSE":
                return AccountType.Expense;

            case "INCOME":
                return AccountType.Income;

            case "ASSET":
                return AccountType.OtherCurrentAsset;

            case "LIABILITY":
                return AccountType.OtherCurrentLiability;

            default:
                return AccountType.None;
        }
    }

    public override string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
    {
        if (value == null)
        {
            return string.Empty;
        }

        switch ((AccountType)value)
        {
            case AccountType.Bank:
                return "BANK";

            case AccountType.CreditCard:
                return "CREDIT";

            case AccountType.Equity:
                return "EQUITY";

            case AccountType.Expense:
            case AccountType.Cost:
            case AccountType.IncomeTaxExpense:
            case AccountType.OtherIncomeExpense:
                return "EXPENSE";

            case AccountType.Income:
                return "INCOME";

            case AccountType.OtherCurrentAsset:
            case AccountType.FixedAsset:
            case AccountType.OtherAsset:
                return "ASSET";

            case AccountType.OtherCurrentLiability:
            case AccountType.LongTermLiability:
                return "LIABILITY";

            default:
                return string.Empty;
        }
    }
}
