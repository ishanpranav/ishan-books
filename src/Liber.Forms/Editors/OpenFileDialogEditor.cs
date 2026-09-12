// OpenFileDialogEditor.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Windows.Forms;

namespace Liber.Forms.Editors;

internal class OpenFileDialogEditor : UITypeEditor
{
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext? context)
    {
        return UITypeEditorEditStyle.Modal;
    }

    public override object? EditValue(ITypeDescriptorContext? context, IServiceProvider provider, object? value)
    {
        string? path = value?.ToString();

        using OpenFileDialog dialog = new OpenFileDialog()
        {
            RestoreDirectory = true,
            FileName = path
        };

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            return dialog.FileName;
        }

        return value;
    }
}
