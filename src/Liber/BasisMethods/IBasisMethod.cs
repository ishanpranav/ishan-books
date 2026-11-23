// IBasisMethod.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.BasisMethods;

public interface IBasisMethod
{
    decimal Quantity { get; }
    decimal CostBasis { get; }
    decimal RealizedGain { get; }

    void Apply(Trade trade);
}
