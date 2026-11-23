// Security.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Drawing;
using System.Text.Json.Serialization;

namespace Liber;

public class Security
{
    public Security() { }

    [JsonConstructor]
    public Security(Guid id, string symbol)
    {
        Id = id;
        Symbol = symbol;
    }

    public Guid Id { get; }
    public string Symbol { get; internal set; } = string.Empty;
    public string? Name { get; set; }
    public Color Color { get; set; }

    public override string ToString()
    {
        if (Name != null)
        {
            return $"{Name} ({Symbol})";
        }

        return Symbol;
    }
}
