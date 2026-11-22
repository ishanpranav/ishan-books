// GnuCashLine.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using System.Globalization;
using CsvHelper.Configuration.Attributes;

namespace Liber;

[NewLine("\n")]
public class GnuCashLine
{
    [Format("M/d/yyyy")]
    [Index(0)]
    [LocalizedDisplayName(nameof(TransactionPosted))]
    [Name("Date")]
    public DateTime TransactionPosted { get; set; }

    [Browsable(false)]
    [Index(1)]
    [Name("Transaction ID")]
    public Guid TransactionId { get; set; }

    [Default(0)]
    [Index(2)]
    [LocalizedDisplayName(nameof(TransactionNumber))]
    [Name("Number")]
    [Optional]
    public decimal TransactionNumber { get; set; }

    [Index(3)]
    [LocalizedDisplayName(nameof(TransactionName))]
    [Name("Description")]
    [NullValues("")]
    [Optional]
    public string? TransactionName { get; set; }

    [Index(4)]
    [LocalizedDisplayName(nameof(TransactionMemo))]
    [Name("Notes")]
    [NullValues("")]
    [Optional]
    public string? TransactionMemo { get; set; }

    [Browsable(false)]
    [Index(1)]
    [Name("Commodity/Currency")]
    [NullValues("")]
    [Optional]
    public string Symbol { get; set; } = "USD";

    [Browsable(false)]
    [Index(6)]
    [Name("Void Reason")]
    [Optional]
    public string? Void { get; set; }

    [Browsable(false)]
    [Index(7)]
    [Name("Action")]
    [Optional]
    public string? Action { get; set; }

    [Index(9)]
    [LocalizedDisplayName(nameof(AccountPath))]
    [Name("Full Account Name")]
    public string AccountPath { get; set; } = string.Empty;

    [Ignore]
    [LocalizedDisplayName(nameof(Debit))]
    public decimal Debit
    {
        get
        {
            return Value.Debit;
        }
    }

    [Ignore]
    [LocalizedDisplayName(nameof(Credit))]
    public decimal Credit
    {
        get
        {
            return Value.Credit;
        }
    }

    [Browsable(false)]
    [Index(10)]
    [Name("Account Name")]
    public string AccountName { get; set; } = string.Empty;

    [Browsable(false)]
    [Format("c2")]
    [Index(11)]
    [Name("Amount With Sym")]
    public decimal AmountWithSymbol
    {
        get
        {
            return Balance;
        }
    }

    [Browsable(false)]
    [Format("n2")]
    [Index(12)]
    [Name("Amount Num.")]
    [NumberStyles(NumberStyles.Currency)]
    public decimal Balance
    {
        get
        {
            return Value.Balance;
        }
        set
        {
            Value.Balance = value;
        }
    }

    [Browsable(false)]
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

    [Browsable(false)]
    [Format("n2")]
    [Index(14)]
    [Name("Value Num.")]
    public decimal NumericValue
    {
        get
        {
            return Balance;
        }
    }

    [Browsable(false)]
    [Index(15)]
    [BooleanFalseValues("n")]
    [BooleanTrueValues("y")]
    [Name("Reconcile")]
    public bool IsReconciled
    {
        get
        {
            return Value.Reconciled != null;
        }
    }

    [Index(16)]
    [LocalizedDisplayName(nameof(Reconciled))]
    [Name("Reconcile Date")]
    [NullValues("")]
    [Optional]
    public DateTime? Reconciled
    {
        get
        {
            return Value.Reconciled;
        }
        set
        {
            Value.Reconciled = value;
        }
    }

    [Index(8)]
    [LocalizedDisplayName(nameof(Description))]
    [Name("Memo")]
    [NullValues("")]
    [Optional]
    public string? Description
    {
        get
        {
            return Value.Description;
        }
        set
        {
            Value.Description = value;
        }
    }

    [Browsable(false)]
    [Ignore]
    public Line Value { get; set; } = new Line();
}
