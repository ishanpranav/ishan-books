// SkiaCheckReportView.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using Liber.Skia;

namespace Liber.Forms.Reports;

internal sealed class SkiaCheckReportView : SkiaReportView
{
    private static readonly Dictionary<string, DrawableCheck> s_checks = new Dictionary<string, DrawableCheck>();

    public SkiaCheckReportView(string path) : base(Lookup(path)) { }

    private static DrawableReport Lookup(string path)
    {
        if (!s_checks.TryGetValue(path, out DrawableCheck? result))
        {
            result = new DrawableCheck(path);
            s_checks[path] = result;
        }

        return result;
    }
}
