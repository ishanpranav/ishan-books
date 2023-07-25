﻿// ClickOnce.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using Liber.Forms.Properties;

namespace Liber.Forms;

internal static class ClickOnce
{
    public static bool NetworkDeployed
    {
        get
        {
            return bool.TryParse(Environment.GetEnvironmentVariable("CLICKONCE_ISNETWORKDEPLOYED"), out bool result) && result;
        }
    }

    public static Version? CurrentVersion
    {
        get
        {
            Version.TryParse("CLICKONCE_CURRENTVERSION", out Version? result);

            return result;
        }
    }

    public static Version? UpdatedVersion
    {
        get
        {
            Version.TryParse("CLICKONCE_UPDATEDVERSION", out Version? result);

            return result;
        }
    }

    public static Uri? UpdateLocation
    {
        get
        {
            Uri.TryCreate("CLICKONCE_UPDATELOCATION", UriKind.RelativeOrAbsolute, out Uri? result);

            return result;
        }
    }

    public static string? ApplicationName
    {
        get
        {
            return Environment.GetEnvironmentVariable("CLICKONCE_UPDATEDAPPLICATIONFULLNAME");
        }
    }

    public static DateTime LastUpdateCheck
    {
        get
        {
            DateTime.TryParse(Environment.GetEnvironmentVariable("CLICKONCE_TIMEOFLASTUPDATECHECK"), provider: null, DateTimeStyles.RoundtripKind, out DateTime result);

            return result;
        }
    }

    public static Uri? ActivationUri
    {
        get
        {
            Uri.TryCreate("CLICKONCE_ACTIVATIONURI", UriKind.RelativeOrAbsolute, out Uri? result);

            return result;
        }
    }

    public static string? DataDirectory
    {
        get
        {
            return Environment.GetEnvironmentVariable("CLICKONCE_DATADIRECTORY");
        }
    }

    public static IReadOnlyList<string> GetArguments()
    {
        List<string> results = new List<string>();
        int index = 1;
        string? result;

        do
        {
            result = Environment.GetEnvironmentVariable($"CLICKONCE_ACTIVATIONDATA_{index}");

            if (result != null)
            {
                results.Add(result);
            }
        }
        while (result != null);

        return results;
    }

    public static void Initialize(Form value)
    {
        value.Icon = Resources.Icon;
    }
}