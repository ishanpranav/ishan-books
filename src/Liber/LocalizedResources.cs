// LocalizedResources.cs
// Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Resources;

namespace Liber;

internal static class LocalizedResources
{
    private static readonly ResourceManager s_resourceManager = new ResourceManager(typeof(LocalizedResources));

    public static string GetString(string key)
    {
        return s_resourceManager.GetString(key) ?? key;
    }
}
