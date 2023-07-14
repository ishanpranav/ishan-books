using CsvHelper.Configuration.Attributes;
using System;

namespace Liber;

[NewLine("\n")]
public class GnuCashAccount
{
    public GnuCashAccount(Account account, [Optional] string path)
    {
        Account = account;
        Path = path;
    }

    [Index(1)]
    [Name("Account Full Name")]
    public string Path { get; }

    [Index(7)]
    [Name("Symbol")]
    public string Symbol { get; set; } = "USD";

    [Index(8)]
    [Name("Namespace")]
    public string Namespace { get; set; } = "CURRENCY";

    [BooleanFalseValues("F")]
    [BooleanTrueValues("T")]
    [Index(9)]
    [Name("Hidden")]
    public bool Hidden { get; set; }

    [Index(3)]
    [Name("Account Code")]
    public string Code
    {
        get
        {
            return Account.Number.ToString();
        }
        set
        {
            if (!decimal.TryParse(Code, out decimal number))
            {
                return;
            }

            Account.Number = number;
        }
    }

    public Account Account { get; set; }
}
