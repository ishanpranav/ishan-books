// LocalizedDescriptionAttribute.cs
// Copyright (c) 2019-2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using Liber;

namespace System.ComponentModel;

public sealed class LocalizedDescriptionAttribute : DescriptionAttribute
{
    public override string Description
    {
        get
        {
            return LocalizedResources.GetString("_d_" + DescriptionValue);
        }
    }

    public LocalizedDescriptionAttribute(string description) : base(description) { }
}
