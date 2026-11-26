// Writer.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using Liber.CompanyWriters;

namespace Liber.Forms.CompanyWriters;

internal sealed class CompanyWriter
{
    public CompanyWriter(FilterIndex index, ICompanyWriter value)
    {
        Index = index;
        Value = value;
    }

    public FilterIndex Index { get; }
    public ICompanyWriter Value { get; }
}
