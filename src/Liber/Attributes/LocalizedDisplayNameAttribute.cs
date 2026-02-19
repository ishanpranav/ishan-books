// LocalizedDisplayNameAttribute.cs
// Copyright (c) 2019-2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using Liber;

namespace System.ComponentModel;

/// <summary>
/// Represents a localized version of a display name attribute.
/// </summary>
/// <remarks>
/// This attribute is used to provide localized display names for properties, events, or other members in user interfaces.
/// </remarks>
public sealed class LocalizedDisplayNameAttribute : DisplayNameAttribute
{
    public override string DisplayName
    {
        get
        {
            return LocalizedResources.GetString(DisplayNameValue);
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LocalizedDisplayNameAttribute"/> class with the specified display name.
    /// </summary>
    /// <param name="displayName">The display name for the attributed member.</param>
    public LocalizedDisplayNameAttribute(string displayName) : base(displayName) { }
}
