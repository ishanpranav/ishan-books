// FormattedStrings.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using Liber.Properties;

namespace Liber;

internal static class FormattedStrings
{
    public static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions()
    {
        AllowTrailingCommas = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };
    private static readonly ResourceManager s_resourceManager = new ResourceManager(typeof(FormattedStrings));

    static FormattedStrings()
    {
        JsonOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: true));
        JsonOptions.Converters.Add(new JsonColorConverter());
        JsonOptions.Converters.Add(new TypeConverterJsonConverterAdapter());
    }

    public static string GetString([CallerMemberName] string? key = null)
    {
        if (key == null)
        {
            return string.Empty;
        }

        return s_resourceManager.GetString(key) ?? key;
    }

    public static Uri GetHelpUrl()
    {
        return new Uri(GetString("HelpUrl"));
    }

    public static string GetCancelText(this Company company)
    {
        return string.Format(GetString("CancelText"), company.Name ?? Resources.DefaultCompanyName);
    }

    public static string GetNotSupportedText(string extension)
    {
        if (extension.Length == 0)
        {
            return GetString("NotSupportedText_0");
        }

        return string.Format(GetString("NotSupportedText_1"), extension.Substring(1).ToUpper());
    }
}
