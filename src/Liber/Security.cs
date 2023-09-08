// Security.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Liber;

public class Security
{
    public Security(string symbol)
    {
        Symbol = symbol;
    }

    public string Symbol { get; }
    public decimal Cost { get; set; }
    public decimal Quantity { get; set; }
    public Guid Account { get; set; }
}
