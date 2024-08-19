// GnuCashAccountTypeConverter.cs
// Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using CsvHelper.Configuration;
using Liber;

namespace CsvHelper.TypeConversion;

internal sealed class GnuCashAccountTypeConverter : DefaultTypeConverter
{
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        string? code = row["Account Code"];

        switch (text)
        {
            case "BANK":
            case "CASH":
                return AccountType.Bank;

            case "ASSET":
                if (code != null)
                {
                    if (code.StartsWith("17"))
                    {
                        return AccountType.FixedAsset;
                    }

                    if (code.StartsWith("16") || code.StartsWith("18") || code.StartsWith("19"))
                    {
                        return AccountType.OtherAsset;
                    }
                }

                return AccountType.OtherCurrentAsset;

            case "LIABILITY":
                if (code != null)
                {
                    if (code.StartsWith("25") || code.StartsWith("26") || code.StartsWith("27") ||
                        code.StartsWith("28") || code.StartsWith("29"))
                    {
                        return AccountType.LongTermLiability;
                    }
                }

                return AccountType.OtherCurrentLiability;

            case "CREDIT": return AccountType.CreditCard;
            case "EQUITY": return AccountType.Equity;

            case "INCOME":
                if (code != null)
                {
                    if (code.StartsWith("49"))
                    {
                        return AccountType.OtherIncomeExpense;
                    }
                }

                return AccountType.Income;

            case "EXPENSE":
                if (code != null)
                {
                    if (code.StartsWith("49") || code.StartsWith("69"))
                    {
                        return AccountType.OtherIncomeExpense;
                    }

                    if (code.StartsWith("7"))
                    {
                        return AccountType.IncomeTaxExpense;
                    }
                }

                return AccountType.Expense;

            default: return AccountType.None;
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
            case AccountType.Bank: return "BANK";

            case AccountType.OtherCurrentAsset:
            case AccountType.FixedAsset:
            case AccountType.OtherAsset:
                return "ASSET";

            case AccountType.CreditCard: return "CREDIT";

            case AccountType.OtherCurrentLiability:
            case AccountType.LongTermLiability:
                return "LIABILITY";

            case AccountType.Equity: return "EQUITY";

            case AccountType.Income:
            case AccountType.OtherIncomeExpense:
                return "INCOME";

            case AccountType.Expense:
            case AccountType.Cost:
            case AccountType.IncomeTaxExpense:
                return "EXPENSE";

            default: return string.Empty;
        }
    }
}
