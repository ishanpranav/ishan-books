// BinderReportEditor.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace Liber.Forms.Reports.Binder;

internal class BinderReportEditor : UITypeEditor
{
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext? context)
    {
        return UITypeEditorEditStyle.Modal;
    }

    public override object? EditValue(ITypeDescriptorContext? context, IServiceProvider provider, object? value)
    {
        if (provider.GetService(typeof(IWindowsFormsEditorService)) is IWindowsFormsEditorService service && value is Binder report)
        {
            using BinderForm form = new BinderForm(report.Engine, report.Views);

            service.ShowDialog(form);

            return new Binder(report.Engine, new List<IReportView>(form.Sections));
        }

        return value;
    }
}
