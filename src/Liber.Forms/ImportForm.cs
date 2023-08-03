// ImportForm.cs
// Copyright (c) 2023 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;
using System.Windows.Forms;

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
