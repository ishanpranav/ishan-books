// AccountEditor.cs
// Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Liber.Forms.Accounts;

internal sealed class AccountEditor : UITypeEditor
{
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext? context)
    {
        return UITypeEditorEditStyle.Modal;
    }

    public override object? EditValue(ITypeDescriptorContext? context, IServiceProvider provider, object? value)
    {
        if (provider.GetService(typeof(IWindowsFormsEditorService)) is IWindowsFormsEditorService service && value is EditableAccountView account)
        {
            using NullableAccountDialog form = new NullableAccountDialog(account);

            if (service.ShowDialog(form) == DialogResult.OK)
            {
                return form.Value;
            }
        }

        return value;
    }
}
