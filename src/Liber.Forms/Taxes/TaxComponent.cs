// TaxComponent.cs
// Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace Liber.Forms.Taxes;

internal sealed class TaxComponent
{
    public TaxComponent(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; }
    public string Description { get; }
    public IReadOnlyCollection<TaxNode> Lines { get; } = new List<TaxNode>();
    public string FullName
    {
        get
        {
            return $"{Name}: {Description}";
        }
    }
}
