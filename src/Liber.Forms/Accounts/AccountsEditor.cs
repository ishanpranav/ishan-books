// AccountsEditor.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Liber.Forms.Accounts;

internal class AccountsEditor : UITypeEditor
{
    public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext? context)
    {
        return UITypeEditorEditStyle.Modal;
    }

    public override object? EditValue(ITypeDescriptorContext? context, IServiceProvider provider, object? value)
    {
        if (provider.GetService(typeof(IWindowsFormsEditorService)) is IWindowsFormsEditorService service && value is AccountsView accounts)
        {
            using AccountsDialog form = new AccountsDialog(accounts);

            if (service.ShowDialog(form) == DialogResult.OK)
            {
                return form.Value;
            }
        }

        return value;
    }
}
