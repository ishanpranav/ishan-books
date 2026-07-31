// Security.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber;

public class Security
{
    public Security(string symbol)
    {
        Symbol = symbol;
    }

    public string Symbol { get; set; }
    public string? Description { get; set; }
}
