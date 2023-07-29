// LocalizedDisplayNameAttribute.cs
// Copyright (c) 2019-2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Resources;

namespace System.ComponentModel;

public sealed class LocalizedDisplayNameAttribute : DisplayNameAttribute
{
    private static readonly ResourceManager s_resourceManager = new ResourceManager(typeof(LocalizedDisplayNameAttribute));

    public override string DisplayName
    {
        get
        {
            return s_resourceManager.GetString(DisplayNameValue) ?? DisplayNameValue;
        }
    }

    public LocalizedDisplayNameAttribute(string displayName) : base(displayName) { }
}
