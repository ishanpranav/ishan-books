﻿// FormattedStrings.cs
// Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using Humanizer;
using Liber.Forms;
using Liber.Forms.Properties;

namespace Liber;

internal static class FormattedStrings
{
    public static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions()
    {
        AllowTrailingCommas = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false
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

    public static string GetCancelText(this Company company)
    {
        return string.Format(GetString("CancelText{0}"), company.DisplayName);
    }

    public static string GetCheckWords(decimal amount)
    {
        int integral = (int)amount;
        int fractional = (int)((amount - integral) * 100);

        return string.Format(GetString("CheckWords{0}{1}"), integral.ToWords(), fractional);
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
