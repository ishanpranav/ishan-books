// Binder.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;

namespace Liber.Forms.Reports.Binder;

[Editor(typeof(BinderReportEditor), typeof(UITypeEditor))]
internal sealed class Binder
{
    public Binder(ReportEngine engine, IReadOnlyCollection<IReportView> views)
    {
        Engine = engine;
        Views = views;
    }

    public ReportEngine Engine { get; }
    public IReadOnlyCollection<IReportView> Views { get; }

    public override string ToString()
    {
        return string.Join(", ", Views.Select(x => x.GenericTitle));
    }
}
