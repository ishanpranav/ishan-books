// LocalizedDisplayNameAttribute.cs
// Copyright (c) 2019-2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Resources;

namespace System.ComponentModel;

internal sealed class LocalizedDisplayNameAttribute : DisplayNameAttribute
{
    private static readonly ResourceManager s_resourceManager = new ResourceManager(typeof(LocalizedDisplayNameAttribute));

    public LocalizedDisplayNameAttribute(string displayName) : base(displayName)
    {
        DisplayNameValue = s_resourceManager.GetString(displayName) ?? displayName;
    }
}
