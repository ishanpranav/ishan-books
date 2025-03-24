// SavingForm.Designer.cs
// Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.Forms.Saving;

partial class SavingForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SavingForm));
        progressBar1 = new System.Windows.Forms.ProgressBar();
        SuspendLayout();
        // 
        // progressBar1
        // 
        resources.ApplyResources(progressBar1, "progressBar1");
        progressBar1.Name = "progressBar1";
        // 
        // SavingForm
        // 
        resources.ApplyResources(this, "$this");
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        Controls.Add(progressBar1);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "SavingForm";
        ShowIcon = false;
        ShowInTaskbar = false;
        ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.ProgressBar progressBar1;
}
