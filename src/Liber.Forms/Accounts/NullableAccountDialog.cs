// NullableAccountDialog.cs
// Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Windows.Forms;
using Liber.Forms.Properties;

namespace Liber.Forms.Accounts;

internal sealed class NullableAccountDialog : AccountDialog
{
    public NullableAccountDialog(EditableAccountView value) : base(value)
    {
        ListViewItem nullAccount = AccountListView.Items.Add(Resources.NoAccount);

        nullAccount.Tag = Guid.Empty;
        nullAccount.Selected = true;
    }
}
