// TaxComponent.cs
// Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Text.Json.Serialization;
using Liber.TaxNodes;

namespace Liber;

public class TaxComponent
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public IReadOnlyCollection<TaxNode> Lines { get; set; } = new List<TaxNode>();

    [JsonIgnore]
    public string FullName
    {
        get
        {
            return $"{Name}: {Description}";
        }
    }
}
