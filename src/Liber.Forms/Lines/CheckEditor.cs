// CheckEditor.cs
// Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Liber.Forms.Lines;

internal sealed class CheckEditor : UITypeEditor
{
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext? context)
    {
        return UITypeEditorEditStyle.Modal;
    }

    public override object? EditValue(ITypeDescriptorContext? context, IServiceProvider provider, object? value)
    {
        if (provider.GetService(typeof(IWindowsFormsEditorService)) is IWindowsFormsEditorService service && value is CheckView view)
        {
            using CheckDialog form = new CheckDialog(view);

            if (service.ShowDialog(form) == DialogResult.OK)
            {
                return form.Value;
            }
        }

        return value;
    }
}
