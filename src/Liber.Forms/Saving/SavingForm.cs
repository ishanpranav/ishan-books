// SavingForm.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Windows.Forms;
using Liber.Forms.Properties;
using System.ComponentModel;

namespace Liber.Forms.Saving;

public partial class SavingForm : Form
{
    public SavingForm()
    {
        InitializeComponent();
        SystemFeatures.Initialize(this);

        Text = Resources.CancelCaption;
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int Progress
    {
        get
        {
            return progressBar1.Value;
        }
        set
        {
            progressBar1.Value = value;
        }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public int MaxProgress
    {
        get
        {
            return progressBar1.Maximum;
        }
        set
        {
            progressBar1.Maximum = value;
        }
    }
}
