using CsvHelper.Configuration.Attributes;
using System;

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

    public Line Value { get; set; } = new Line();
}
