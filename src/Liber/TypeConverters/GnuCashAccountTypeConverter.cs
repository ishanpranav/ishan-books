// GnuCashAccountTypeConverter.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using CsvHelper.Configuration;
using Liber;

namespace CsvHelper.TypeConversion;

internal sealed class GnuCashAccountTypeConverter : DefaultTypeConverter
{
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        switch (text)
        {
            case "BANK":
            case "CASH":
                return AccountType.Bank;

            case "ASSET":
                return AccountType.OtherCurrentAsset;

            case "LIABILITY":
                return AccountType.OtherCurrentLiability;

            case "CREDIT":
                return AccountType.CreditCard;

            case "EQUITY":
                return AccountType.Equity;

            case "INCOME":
                return AccountType.Income;

            case "EXPENSE":
                return AccountType.Expense;

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

            case AccountType.OtherCurrentAsset:
            case AccountType.FixedAsset:
            case AccountType.OtherAsset:
                return "ASSET";

            case AccountType.CreditCard:
                return "CREDIT";

            case AccountType.OtherCurrentLiability:
            case AccountType.LongTermLiability:
                return "LIABILITY";

            case AccountType.Equity:
                return "EQUITY";

            case AccountType.Income:
            case AccountType.OtherIncomeExpense:
                return "INCOME";

            case AccountType.Expense:
            case AccountType.Cost:
            case AccountType.IncomeTaxExpense:
                return "EXPENSE";

            default:
                return string.Empty;
        }
    }
}
