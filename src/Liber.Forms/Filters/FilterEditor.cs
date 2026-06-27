// FilterEditor.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Liber.Filters;

namespace Liber.Forms.Filters;

internal sealed class FilterEditor : UITypeEditor
{
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext? context)
    {
        return UITypeEditorEditStyle.Modal;
    }

    public override object? EditValue(ITypeDescriptorContext? context, IServiceProvider provider, object? value)
    {
        if (provider.GetService(typeof(IWindowsFormsEditorService)) is IWindowsFormsEditorService service && value is Filter filter)
        {
            using FilterDialog form = new FilterDialog(filter);

            if (service.ShowDialog(form) == DialogResult.OK)
            {
                return form.Value.Clone();
            }
        }

        return value;
    }
}
