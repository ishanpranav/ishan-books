// GdiReport.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Printing;
using System.Drawing.Printing.Design;
using System.Text.Json.Serialization;

namespace Liber.Forms.Reports.Gdi;

internal abstract class GdiReport : IReport
{
    [LocalizedCategory(nameof(Title))]
    [LocalizedDescription(nameof(Title))]
    [LocalizedDisplayName(nameof(Title))]
    public abstract string Title { get; set; }

    [Browsable(false)]
    public abstract string ImageKey { get; }

    [Editor(typeof(PageSettingsEditor), typeof(UITypeEditor))]
    [JsonIgnore]
    [LocalizedCategory(nameof(PageSettings))]
    [LocalizedDescription(nameof(PageSettings))]
    [LocalizedDisplayName(nameof(PageSettings))]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public PageSettings PageSettings { get; set; } = new PageSettings();

    public abstract void Draw(Graphics graphics);
    public abstract IReportView CreateView(Company company);
}
