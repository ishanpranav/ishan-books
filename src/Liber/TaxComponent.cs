// TaxComponent.cs
// Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using Liber.TaxNodes;

namespace Liber;

public class TaxComponent
{
    public TaxComponent(
        string name,
        string description,
        IReadOnlyCollection<TaxNode> lines)
    {
        Name = name;
        Description = description;
        Lines = lines;
    }

    public string Name { get; }
    public string Description { get; }
    public IReadOnlyCollection<TaxNode> Lines { get; } 
    public string FullName
    {
        get
        {
            return $"{Name}: {Description}";
        }
    }
}
