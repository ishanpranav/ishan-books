// NotImplementedTaxNode.cs
// Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Liber.TaxNodes;

public class NotImplementedTaxNode : TaxNode
{
    protected override decimal EvaluateCore(DateTime started, DateTime posted)
    {
        return 0;
    }
}
