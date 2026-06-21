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
        companyLabel = new System.Windows.Forms.Label();
        applicationNameLabel = new System.Windows.Forms.Label();
        SuspendLayout();
        // 
        // companyLabel
        // 
        companyLabel.BackColor = System.Drawing.Color.Transparent;
        companyLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        companyLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
        companyLabel.Location = new System.Drawing.Point(12, 9);
        companyLabel.Name = "companyLabel";
        companyLabel.Padding = new System.Windows.Forms.Padding(10);
        companyLabel.Size = new System.Drawing.Size(110, 38);
        companyLabel.TabIndex = 23;
        // 
        // applicationNameLabel
        // 
        applicationNameLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        applicationNameLabel.BackColor = System.Drawing.Color.Transparent;
        applicationNameLabel.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
        applicationNameLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
        applicationNameLabel.Location = new System.Drawing.Point(16, 101);
        applicationNameLabel.Margin = new System.Windows.Forms.Padding(7, 0, 4, 0);
        applicationNameLabel.Name = "applicationNameLabel";
        applicationNameLabel.Size = new System.Drawing.Size(406, 56);
        applicationNameLabel.TabIndex = 22;
        applicationNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // SplashScreen
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(435, 246);
        ControlBox = false;
        Controls.Add(companyLabel);
        Controls.Add(applicationNameLabel);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "SplashScreen";
        ShowInTaskbar = false;
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "SplashScreen";
        TopMost = true;
        ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.Label companyLabel;
    private System.Windows.Forms.Label applicationNameLabel;
}