// Conjunction.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.ComponentModel;

namespace Liber.Filters;

public enum Conjunction
{
    [LocalizedDescription(nameof(And))]
    And,

    [LocalizedDescription(nameof(Or))]
    Or
}
