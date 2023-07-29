// LocalizedDescriptionAttribute.cs
// Copyright (c) 2019-2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Resources;

namespace System.ComponentModel;

public sealed class LocalizedDescriptionAttribute : DescriptionAttribute
{
    private static readonly ResourceManager s_resourceManager = new ResourceManager(typeof(LocalizedDescriptionAttribute));

    public override string Description
    {
        get
        {
            return s_resourceManager.GetString(DescriptionValue) ?? string.Empty;
        }
    }

    public LocalizedDescriptionAttribute(string description) : base(description) { }
}
