﻿using CsvHelper.Configuration.Attributes;
using System;

namespace Liber;

[NewLine("\n")]
public class GnuCashAccount
{
    [Ignore]
    public Guid Key { get; set; }

    [Index(1)]
    [Name("Account Full Name", "Full Account Name")]
    [Optional]
    public string Path { get; set; } = string.Empty;

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

    public Account Value { get; set; } = new Account();
}
