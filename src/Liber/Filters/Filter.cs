// Filter.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text.Json.Serialization;

namespace Liber.Filters;

[JsonDerivedType(typeof(BalanceFilter), "balance")]
[JsonDerivedType(typeof(ConjunctionFilter), "conjunction")]
[JsonDerivedType(typeof(DescriptionFilter), "description")]
[JsonDerivedType(typeof(ReconciledFilter), "reconciled")]
[JsonDerivedType(typeof(TransactionMemoFilter), "transaction-memo")]
[JsonDerivedType(typeof(TransactionNameFilter), "transaction-name")]
[JsonDerivedType(typeof(TransactionNumberFilter), "transaction-number")]
[JsonDerivedType(typeof(TransactionPostedFilter), "transaction-posted")]
public abstract class Filter : ICloneable
{
    public abstract string Name { get; }

    public abstract bool IsMatch(Line value);

    public abstract Filter Clone();

    object ICloneable.Clone()
    {
        return Clone();
    }

    public override string ToString()
    {
        return Name;
    }
}
