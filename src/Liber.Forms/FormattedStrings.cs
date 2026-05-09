// FormattedStrings.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using Humanizer;
using Liber.Forms.Properties;

namespace Liber.Forms;

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

    static FormattedStrings()
    {
        JsonOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: true));
        JsonOptions.Converters.Add(new JsonColorConverter());
        JsonOptions.Converters.Add(new TypeConverterJsonConverterAdapter());
    }

    public static string AboutText
    {
        get
        {
            return string.Format(GetString("AboutText{0}"), SystemFeatures.ApplicationName);
        }
    }

    public static Uri AboutUrl
    {
        get
        {
            return new Uri(GetString());
        }
    }

    public static ResourceManager ResourceManager { get; } = new ResourceManager(typeof(FormattedStrings));

    public static string GetString([CallerMemberName] string? key = null)
    {
        if (key == null)
        {
            return string.Empty;
        }

        return ResourceManager.GetString(key) ?? key;
    }

    public static bool TryGetString(string key, [NotNullWhen(true)] out string? value)
    {
        value = ResourceManager.GetString(key);

        return value != null;
    }

    public static IEnumerable<string> GetStringsBySuffix(string suffix)
    {
        ResourceSet? resources = ResourceManager.GetResourceSet(
            CultureInfo.CurrentUICulture,
            createIfNotExists: true,
            tryParents: true);

        if (resources == null)
        {
            yield break;
        }

        foreach (DictionaryEntry entry in resources)
        {
            string? key = entry.Key.ToString();

            if (key == null || !key.EndsWith(suffix) || entry.Value == null)
            {
                continue;
            }

            string? value = entry.Value.ToString();

            if (value == null)
            {
                continue;
            }

            yield return value;
        }
    }

    public static string GetCancelText(this Company company)
    {
        return string.Format(GetString("CancelText{0}"), company.DisplayName);
    }

    public static string GetCheckWords(decimal amount)
    {
        decimal integral = decimal.Floor(amount);
        decimal fractional = decimal.Round((amount - integral), 0) * 100;

        if (integral > int.MaxValue)
        {
            return string.Format(GetString("CheckWords{0}{1}"), integral, fractional);
        }

        return string.Format(GetString("CheckWords{0}{1}"), ((int)integral).ToWords(), fractional);
    }

    public static string GetMultipleWords(decimal multiple)
    {
        switch (multiple)
        {
            case 0:
            case 1:
            case 0.01m:
                return string.Empty;
        }

        if (multiple > int.MaxValue || !multiple.IsPow10() || multiple < 1)
        {
            return string.Format(GetString("MultipleWords2{0}"), multiple);
        }

        int integral = (int)multiple;
        string words = integral
            .ToWords()
            .Replace(1.ToWords(), string.Empty)
            .Trim()
            .Pluralize();

        return string.Format(GetString("MultipleWords1{0}"), words);
    }

    public static void ShowNotSupportedMessage(string extension)
    {
        string text;

        if (extension.Length == 0)
        {
            text = GetString("NotSupportedText");
        }
        else
        {
            text = string.Format(GetString("NotSupportedText{0}"), extension.Substring(1).ToUpper());
        }

        MessageBox.Show(Resources.ExceptionCaption, text, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
