// GnuCashLine.cs
// Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Globalization;
using CsvHelper.Configuration.Attributes;

namespace Liber;

[NewLine("\n")]
public class GnuCashLine
{
    [Format("M/d/yyyy")]
    [Index(0)]
    [Name("Date")]
    public DateTime TransactionPosted { get; set; }

    [Index(1)]
    [Name("Transaction ID")]
    public Guid TransactionId { get; set; }

    [Default(0)]
    [Index(2)]
    [Name("Number")]
    [Optional]
    public decimal TransactionNumber { get; set; }

    [Index(3)]
    [Name("Description")]
    [NullValues("")]
    [Optional]
    public string? TransactionName { get; set; }

    [Index(4)]
    [Name("Notes")]
    [NullValues("")]
    [Optional]
    public string? TransactionMemo { get; set; }

    [Index(1)]
    [Name("Commodity/Currency")]
    [NullValues("")]
    [Optional]
    public string Symbol { get; set; } = "USD";

    [Index(6)]
    [Name("Void Reason")]
    [Optional]
    public string? Void { get; set; }

    [Index(7)]
    [Name("Action")]
    [Optional]
    public string? Action { get; set; }

    [Index(9)]
    [Name("Full Account Name")]
    public string AccountPath { get; set; } = string.Empty;

    [Index(10)]
    [Name("Account Name")]
    public string AccountName { get; set; } = string.Empty;

    [Format("c2")]
    [Index(11)]
    [Name("Amount With Sym")]
    public decimal AmountWithSymbol
    {
        get
        {
            return Amount;
        }
    }

    [Format("n2")]
    [Index(12)]
    [Name("Amount Num.")]
    [NumberStyles(NumberStyles.Currency)]
    public decimal Amount { get; set; }

    [Format("c2")]
    [Index(13)]
    [Name("Value With Sym")]
    public decimal NumericValueWithSymbol
    {
        get
        {
            return AmountWithSymbol;
        }
    }

    [Format("n2")]
    [Index(14)]
    [Name("Value Num.")]
    public decimal NumericValue
    {
        get
        {
            return Amount;
        }
    }

    [Index(15)]
    [BooleanFalseValues("n")]
    [BooleanTrueValues("y")]
    [Name("Reconcile")]
    public bool IsReconciled
    {
        get
        {
            return Value.Reconciled.HasValue;
        }
    }

    public Line Value { get; set; } = new Line();
}
