// LocalizedDisplayNameAttribute.cs
// Copyright (c) 2019-2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using Liber;

namespace System.ComponentModel;

public sealed class LocalizedDisplayNameAttribute : DisplayNameAttribute
{
    public override string DisplayName
    {
        get
        {
            return LocalizedResources.GetString(DisplayNameValue);
        }
    }

    public LocalizedDisplayNameAttribute(string displayName) : base(displayName) { }
}
