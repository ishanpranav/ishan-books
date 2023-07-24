// PasswordForm.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.IO;
using System.Windows.Forms;
using Liber.Sqlite;

namespace Liber.Forms;

internal sealed partial class PasswordForm : Form
{
    public string Password { get; private set; } = string.Empty;

    public PasswordForm()
    {
        InitializeComponent();

        DialogResult = DialogResult.Cancel;
    }

    private void OnAcceptButtonClick(object sender, EventArgs e)
    {
        Password = passwordTextBox.Text;
        DialogResult = DialogResult.OK;

        Close();
    }
}
