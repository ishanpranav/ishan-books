// ParentKey.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Liber.Forms.Reports.Xsl;

internal sealed class ParentKey : IEquatable<ParentKey>
{
    public ParentKey(AccountType type, CashFlow cashFlow)
    {
        Type = type;
        CashFlow = cashFlow;
    }

    public AccountType Type { get; }
    public CashFlow CashFlow { get; }

    public bool Equals(ParentKey? other)
    {
        return other != null && other.Type == Type && other.CashFlow == CashFlow;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as ParentKey);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Type, CashFlow);
    }
}
