using CsvHelper.Configuration.Attributes;
using System.Resources;

namespace Liber;

public enum TaxType
{
    [Name("F")]
    None = 0,

    [Name("T")]
    Other = 1
}

public static class TaxTypeExtensions
{
    private static readonly ResourceManager s_resourceManager = new ResourceManager(typeof(TaxTypeExtensions));

    public static string ToLocalizedString(this TaxType value)
    {
        string key = value.ToString();

        return s_resourceManager.GetString(key) ?? key;
    }
}
