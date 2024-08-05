// PageSettingsEditor.cs
// Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace System.Drawing.Printing.Design;

internal sealed class PageSettingsEditor : UITypeEditor
{
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext? context)
    {
        return UITypeEditorEditStyle.Modal;
    }

    public override object? EditValue(ITypeDescriptorContext? context, IServiceProvider provider, object? value)
    {
        if (value is PageSettings settings)
        {
            using PageSetupDialog dialog = new PageSetupDialog()
            {
                AllowPrinter = false,
                ShowNetwork = false,
                PageSettings = settings
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.PageSettings.Clone();
            }
        }

        return value;
    }
}
