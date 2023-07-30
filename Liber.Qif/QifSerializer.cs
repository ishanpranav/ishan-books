// QifSerializer.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using Nerdbank.Qif;

namespace Liber.Qif;

public static class QifSerializer
{
    private static Nerdbank.Qif.AccountType ToQif(AccountType value)
    {
        switch (value)
        {
            case AccountType.Equity:
            case AccountType.None:
                return Nerdbank.Qif.AccountType.Cash;

            case AccountType.Bank:
                return Nerdbank.Qif.AccountType.Bank;

            case AccountType.CreditCard:
                return Nerdbank.Qif.AccountType.CreditCard;

            case AccountType.Income:
                break;
            case AccountType.OtherCurrentAsset:
                break;
            case AccountType.OtherCurrentLiability:
                break;
            case AccountType.Cost:
                break;
            case AccountType.FixedAsset:
                break;
            case AccountType.OtherAsset:
                break;
            case AccountType.LongTermLiability:
                break;
            case AccountType.OtherComprehensiveIncome:
                break;
            case AccountType.IncomeTaxExpense:
                break;
            case AccountType.OtherIncomeExpense:
                break;
        }
    }

    public static async Task SerializeAccountsAsync(Stream output, Company company)
    {
        QifDocument document = new QifDocument();

        foreach (Account account in company.Accounts.Values)
        {
            Nerdbank.Qif.Account qifAccount;

            qifAccount = new BankAccount(Nerdbank.Qif.Account.Types.Investment, account.Name);

            foreach (Transaction transaction in account.Lines)
            {
                document.Transactions.Add(new BankTransaction(Nerdbank.Qif.AccountType.Bank, transaction.));
            }

            document.Accounts.Add(qifAccount);
        }

        await using StreamWriter writer = new StreamWriter(output);

        document.Save(writer);
    }
}
