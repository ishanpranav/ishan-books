// CompanyType.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.ComponentModel;

namespace Liber;

public enum CompanyType : byte
{
    [LocalizedDescription(nameof(None))]
    None = 0,

    [LocalizedDescription(nameof(Individual))]
    Individual = 1,

    [LocalizedDescription(nameof(Partnership))]
    Partnership = 2,

    [LocalizedDescription(nameof(Corporation))]
    Corporation = 3,

    [LocalizedDescription(nameof(NonprofitCorporation))]
    NonprofitCorporation = 4
}
