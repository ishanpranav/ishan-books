// SecurityView.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;

namespace Liber.Forms.Securities;

internal sealed class SecurityView
{
    public SecurityView(Guid id)
    {
        Id = id;
    }

    [Browsable(false)]
    public Guid Id { get; }

    [LocalizedDisplayName(nameof(Symbol))]
    public string Symbol { get; set; } = string.Empty;

    [LocalizedDisplayName(nameof(Name))]
    public string? Name { get; set; }

    [LocalizedDisplayName(nameof(Price))]
    [ReadOnly(true)]
    public decimal? Price { get; set; }
}
