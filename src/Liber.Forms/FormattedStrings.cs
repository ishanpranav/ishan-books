// FormattedStrings.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
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

    public static Uri GetHelpUrl()
    {
        return new Uri(GetString("HelpUrl"));
    }

    public static string GetCancelText(this Company company)
    {
        return string.Format(GetString("CancelText"), company.Name ?? GetString("DefaultCompanyName"));
    }
}
