// CalendarIterator.cs
// Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.ComponentModel;

namespace Liber.Forms.Reports.Html;

[TypeConverter(typeof(LocalizedEnumConverter))]
public enum Periodicity
{
    [LocalizedDescription(nameof(Daily))]
    Daily,

    [LocalizedDescription(nameof(Weekly))]
    Weekly,

    [LocalizedDescription(nameof(Biweekly))]
    Biweekly,

    [LocalizedDescription(nameof(Monthly))]
    Monthly,

    [LocalizedDescription(nameof(Annually))]
    Annually
}
