using CsvHelper.Configuration.Attributes;

namespace Liber.Readers;

internal sealed class CsvTransaction : Transaction
{
    [Name("Account Name")]
    public string GnuAccountName { get; set; } = string.Empty;

    [Name("Number")]
    public int? GnuNumber { get; set; }
}
