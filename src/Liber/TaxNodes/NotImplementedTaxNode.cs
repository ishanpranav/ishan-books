// NotImplementedTaxNode.cs
// Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.TaxNodes;

public class NotImplementedTaxNode : TaxNode
{
    public NotImplementedTaxNode(string name, string description) : base(name, description) { }

    protected override decimal EvaluateCore()
    {
        return 0;
    }
}
