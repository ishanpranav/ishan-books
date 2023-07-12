using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Liber;

internal static class ObjectLoader
{
    private const string SettingsKey = "settings.json";

    private static JsonSerializerOptions? s_options;

    public static JsonSerializerOptions Options
    {
        get
        {
            if (s_options == null)
            {
                s_options = new JsonSerializerOptions()
                {
                    AllowTrailingCommas = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };

                s_options.Converters.Add(new JsonCultureInfoConverter());
                s_options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: true));
            }

            return s_options;
        }
    }

    private static string GetDiskPath(string key)
    {
        string path = Path.GetDirectoryName(Application.ExecutablePath) ?? string.Empty;

        return Path.Combine(path, "objects", key);
    }

    public static Settings LoadSettings()
    {
        try
        {
            string path = GetDiskPath(SettingsKey);

            if (File.Exists(path))
            {
                using FileStream utf8Json = File.OpenRead(path);

                Settings? result = JsonSerializer.Deserialize<Settings>(utf8Json, Options);

                if (result != null)
                {
                    return result;
                }
            }
        }
        catch (IOException) { }
        catch (JsonException)
        {
            throw;
        }

        return new Settings();
    }

    public static async Task SaveCompanyAsync(string path, Company value)
    {
        await using FileStream utf8Json = File.Create(path);

        await JsonSerializer.SerializeAsync(utf8Json, value, Options);
    }

    public static async Task SaveSettingsAsync(Settings value)
    {
        try
        {
            await using FileStream utf8Json = File.Create(GetDiskPath(SettingsKey));

            await JsonSerializer.SerializeAsync(utf8Json, value, Options);
        }
        catch (IOException) { }
    }
}
