using System;
using System.Drawing;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Liber;

internal static class FormattedStrings
{
    private static readonly ResourceManager s_resourceManager = new ResourceManager(typeof(FormattedStrings));

    public static string GetString([CallerMemberName] string? key = null)
    {
        if (key == null)
        {
            return string.Empty;
        }

        return s_resourceManager.GetString(key) ?? key;
    }

    public static Image? GetImage([CallerMemberName] string? key = null)
    {
        if (key == null)
        {
            return null;
        }

        return s_resourceManager.GetObject(key) as Image;
    }

    public static string ToLocalizedString(this Enum value)
    {
        return GetString(value.GetType().Name + value);
    }

    public static string CancelText(Company company)
    {
        return string.Format(GetString(), company.DisplayName);
    }

    public static string ErrorCount(int count)
    {
        return GetSingularPluralOrZeroString(count);
    }

    public static string GetSingularPluralOrZeroString(
        int count,
        [CallerMemberName] string? key = null)
    {
        switch (count)
        {
            case 0:
                break;

            case 1:
                key += "Singular";
                break;

            default:
                key += "Plural";
                break;
        }

        return string.Format(GetString(key), count);
    }
}
