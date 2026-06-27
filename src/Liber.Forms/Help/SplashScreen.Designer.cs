// SplashScreen.Designer.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.Forms.Help;

partial class SplashScreen
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
        companyLabel = new System.Windows.Forms.Label();
        applicationNameLabel = new System.Windows.Forms.Label();
        SuspendLayout();
        // 
        // companyLabel
        // 
        companyLabel.BackColor = System.Drawing.Color.Transparent;
        companyLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        resources.ApplyResources(companyLabel, "companyLabel");
        companyLabel.Name = "companyLabel";
        // 
        // applicationNameLabel
        // 
        resources.ApplyResources(applicationNameLabel, "applicationNameLabel");
        applicationNameLabel.BackColor = System.Drawing.Color.Transparent;
        applicationNameLabel.Name = "applicationNameLabel";
        // 
        // SplashScreen
        // 
        resources.ApplyResources(this, "$this");
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ControlBox = false;
        Controls.Add(companyLabel);
        Controls.Add(applicationNameLabel);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "SplashScreen";
        ShowInTaskbar = false;
        TopMost = true;
        ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.Label companyLabel;
    private System.Windows.Forms.Label applicationNameLabel;
}