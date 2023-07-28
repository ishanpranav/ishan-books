// AssemblyInfo.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Reflection;

namespace Liber.Forms.Properties;

internal static class AssemblyInfo
{
    public static string Title
    {
        get
        {
            AssemblyTitleAttribute? customAttribute = typeof(AssemblyInfo).Assembly.GetCustomAttribute<AssemblyTitleAttribute>();

            if (customAttribute == null)
            {
                return string.Empty;
            }

            return customAttribute.Title;
        }
    }
}
