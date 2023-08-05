// GdiReport.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Printing;
using System.Drawing.Printing.Design;

namespace Liber.Forms.Reports.Gdi;

internal abstract class GdiReport
{   
    [LocalizedCategory(nameof(Title))]
    [LocalizedDescription(nameof(Title))]
    [LocalizedDisplayName(nameof(Title))]
    public abstract string Title { get; set; }

    [Editor(typeof(PageSettingsEditor), typeof(UITypeEditor))]
    [LocalizedCategory(nameof(PageSettings))]
    [LocalizedDescription(nameof(PageSettings))]
    [LocalizedDisplayName(nameof(PageSettings))]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public PageSettings PageSettings { get; set; } = new PageSettings();

    public abstract void Draw(Graphics graphics);
}
