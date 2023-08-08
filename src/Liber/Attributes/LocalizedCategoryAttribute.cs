// LocalizedCategoryAttribute.cs
// Copyright (c) 2019-2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using Liber;

namespace System.ComponentModel;

public sealed class LocalizedCategoryAttribute : CategoryAttribute
{
    public LocalizedCategoryAttribute(string category) : base(LocalizedResources.GetString("_c_" + category)) { }
}
