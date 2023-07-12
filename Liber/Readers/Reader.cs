using Liber.Forms;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Liber.Readers;

[JsonDerivedType(typeof(CsvAccountsReader), "csv-accounts")]
[JsonDerivedType(typeof(CsvJournalReader), "csv-journal")]
[JsonDerivedType(typeof(JsonCompanyReader), "json-company")]
internal abstract class Reader
{
    public string DisplayName
    {
        get
        {
            return FormattedStrings.GetString(GetType().Name + "Name");
        }
    }

    public int FilterIndex { get; set; }

    public abstract Task ReadAsync(MainContext context, string path);
}
