// LocalizedCategoryAttribute.cs
// Copyright (c) 2019-2023-2024 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using Liber;

namespace System.ComponentModel;

/// <summary>
/// Represents a localized version of a category attribute.
/// </summary>
/// <remarks>
/// This attribute is used to provide localized display names for property categories in user interfaces.
/// </remarks>
public sealed class LocalizedCategoryAttribute : CategoryAttribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LocalizedCategoryAttribute"/> class with the specified category name.
    /// </summary>
    /// <param name="category">The name of the category.</param>
    public LocalizedCategoryAttribute(string category) : base(LocalizedResources.GetString("_c_" + category)) { }
}
