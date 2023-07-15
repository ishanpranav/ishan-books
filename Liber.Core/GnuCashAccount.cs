using CsvHelper.Configuration.Attributes;
using System;

namespace Liber;

[NewLine("\n")]
public class GnuCashAccount
{
    public GnuCashAccount(Account value)
    {
        Value = value;
    }

    [Index(1)]
    [Name("Full Account Name")]
    [Optional]
    public string? Path { get; set; }

    [Index(7)]
    [Name("Symbol")]
    [Optional]
    public string Symbol { get; set; } = "USD";

    [Index(8)]
    [Name("Namespace")]
    [Optional]
    public string Namespace { get; set; } = "CURRENCY";

    [BooleanFalseValues("F")]
    [BooleanTrueValues("T")]
    [Index(9)]
    [Name("Hidden")]
    [Optional]
    public bool Hidden { get; set; }

    public Account Value { get; set; }
}
