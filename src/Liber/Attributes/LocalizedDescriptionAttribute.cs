// LocalizedDescriptionAttribute.cs
// Copyright (c) 2019-2023-2024 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using Liber;

namespace System.ComponentModel;

/// <summary>
/// Represents a localized version of a description attribute.
/// </summary>
/// <remarks>
/// This attribute is used to provide localized descriptions for properties, events, or other members in user interfaces.
/// </remarks>
public sealed class LocalizedDescriptionAttribute : DescriptionAttribute
{
    /// <inheritdoc/>
    public override string Description
    {
        get
        {
            return LocalizedResources.GetString("_d_" + DescriptionValue);
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LocalizedDescriptionAttribute"/> class with the specified description.
    /// </summary>
    /// <param name="description">The description text for the attributed member.</param>
    public LocalizedDescriptionAttribute(string description) : base(description) { }
}
