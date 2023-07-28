// LocalizedDescriptionAttribute.cs
// Copyright (c) 2019-2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Resources;

namespace System.ComponentModel;

internal sealed class LocalizedDescriptionAttribute : DescriptionAttribute
{
    private static readonly ResourceManager s_resourceManager = new ResourceManager(typeof(LocalizedDescriptionAttribute));

    public LocalizedDescriptionAttribute(string description) : base(description)
    {
        DescriptionValue = s_resourceManager.GetString(description) ?? string.Empty;
    }
}
