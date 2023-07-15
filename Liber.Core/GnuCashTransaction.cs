using CsvHelper.Configuration.Attributes;

namespace Liber;

[NewLine("\n")]
internal sealed class GnuCashTransaction
{
    public GnuCashTransaction(Transaction transaction)
    {
        Transaction = transaction;
    }

    public Transaction Transaction { get; }

    [Index(5)]
    [Name("Commodity/Currency")]
    [Optional]
    public string Symbol { get; set; } = "USD";

    [Index(6)]
    [Name("Void Reason")]
    [Optional]
    public string? Void { get; set; }

    [Index(7)]
    [Name("Void Reason")]
    [Optional]
    public string? VoidMemo { get; set; }

}
