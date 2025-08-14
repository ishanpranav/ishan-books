// TaxNode.cs
// Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace Liber.Forms.Taxes;

internal abstract class TaxNode
{
    public IReadOnlyCollection<TaxNode> Dependencies { get; } = new List<TaxNode>();

    public abstract decimal Evaluate();
}
