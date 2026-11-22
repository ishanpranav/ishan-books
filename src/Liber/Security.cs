// Security.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Liber;

public class Security
{
    internal int references;

    public Security(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
    public string Symbol { get; set; } = string.Empty;
    public string? Name { get; set; }
}
