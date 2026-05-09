// ParentKey.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Liber.Forms.Reports.Xsl;

internal sealed class ParentKey : IEquatable<ParentKey>
{
    public ParentKey(Account value)
    {
        Type = value.Type;
        CashFlow = value.CashFlow;
        TaxType = value.TaxType;
    }

    public AccountType Type { get; }
    public CashFlow CashFlow { get; }
    public bool TaxType { get; }

    public bool Equals(ParentKey? other)
    {
        return other != null &&
               other.Type == Type &&
               other.CashFlow == CashFlow &&
               other.TaxType == TaxType;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as ParentKey);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Type, CashFlow, TaxType);
    }
}
