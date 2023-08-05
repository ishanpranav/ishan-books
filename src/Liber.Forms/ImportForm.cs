﻿// ImportForm.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using System.Windows.Forms;
using Liber.Forms.Properties;

namespace Liber.Forms.Accounts;

internal abstract partial class ImportForm : Form
{
    protected ImportForm(Company company)
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        new ComponentResourceManager(GetType()).ApplyResources(this, "$this");

        Company = company;
    }

    protected Company Company { get; }

    protected abstract void CommitChanges();

    private void OnDataGridViewDataError(object sender, DataGridViewDataErrorEventArgs e)
    {
        if (e.Exception != null)
        {
            MessageBox.Show(e.Exception.Message, Resources.ExceptionCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void OnAcceptButtonClick(object sender, EventArgs e)
    {
        CommitChanges();
        Close();
    }

    private void OnCancelButtonClick(object sender, EventArgs e)
    {
        Close();
    }
}
