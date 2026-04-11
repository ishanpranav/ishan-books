// ReportLevel.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.ComponentModel;

namespace Liber.Forms.Reports.Xsl;

public enum ReportLevel
{
    [LocalizedDescription(nameof(ByType))]
    ByType,

    [LocalizedDescription(nameof(ByAccount))]
    ByAccount,

    [LocalizedDescription(nameof(Detailed))]
    Detailed
}
