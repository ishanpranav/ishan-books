using Liber.Forms;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Liber.Readers;

internal sealed class JsonCompanyReader : Reader
{
    public override async Task ReadAsync(MainContext context, string path)
    {
        await using FileStream utf8Json = File.OpenRead(path);

        Company? result = await JsonSerializer.DeserializeAsync<Company>(utf8Json, ObjectLoader.Options);

        if (result == null)
        {
            throw new JsonException();
        }

        context.Company = result;
        context.Path = path;

        context.Settings.AddRecentItem(path);
    }
}
