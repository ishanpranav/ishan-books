// LocalizedCategoryAttribute.cs
// Copyright (c) 2019-2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Resources;

namespace System.ComponentModel;

internal sealed class LocalizedCategoryAttribute : CategoryAttribute
{
    private static readonly ResourceManager s_resourceManager = new ResourceManager(typeof(LocalizedCategoryAttribute));

    public LocalizedCategoryAttribute(string category) : base(s_resourceManager.GetString(category) ?? category) { }
}
