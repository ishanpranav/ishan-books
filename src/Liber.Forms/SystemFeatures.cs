// SystemFeatures.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Liber.Forms.Properties;
using Microsoft.Win32;

namespace Liber.Forms;

internal static class SystemFeatures
{
    public static bool IsNetworkDeployed
    {
        get
        {
            return bool.TryParse(Environment.GetEnvironmentVariable("CLICKONCE_ISNETWORKDEPLOYED"), out bool result) && result;
        }
    }

    public static bool IsFirstRun
    {
        get
        {
            return bool.TryParse(Environment.GetEnvironmentVariable("CLICKONCE_ISFIRSTRUN"), out bool result) && result;
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

    public static string ApplicationName
    {
        get
        {
            AssemblyTitleAttribute? customAttribute = typeof(SystemFeatures).Assembly.GetCustomAttribute<AssemblyTitleAttribute>();

            if (customAttribute != null)
            {
                return customAttribute.Title;
            }

            return string.Empty;
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

    public static void Initialize()
    {
        if (!IsFirstRun)
        {
            return;
        }

        try
        {
            Assembly assembly = typeof(SystemFeatures).Assembly;

            string path = Path.Combine(Application.StartupPath, "favicon.ico");

            if (!File.Exists(path))
            {
                return;
            }

            RegistryKey? key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall");

            if (key == null)
            {
                return;
            }

            string[] subKeyNames = key.GetSubKeyNames();

            for (int i = 0; i < subKeyNames.Length; i++)
            {
                RegistryKey? subKey = key.OpenSubKey(subKeyNames[i], true);

                if (subKey == null)
                {
                    continue;
                }

                object? displayNameValue = subKey.GetValue("DisplayName");

                if (displayNameValue != null && displayNameValue.ToString() == ApplicationName)
                {
                    subKey.SetValue("DisplayIcon", path);

                    break;
                }
            }
        }
        catch { }
    }

    public static void Initialize(Form value)
    {
        value.Icon = Resources.Icon;
    }
}
