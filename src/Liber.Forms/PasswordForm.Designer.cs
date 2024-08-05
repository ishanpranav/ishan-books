// PasswordForm.Designer.cs
// Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.Forms;

partial class PasswordForm
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PasswordForm));
        acceptButton = new System.Windows.Forms.Button();
        passwordTextBox = new System.Windows.Forms.TextBox();
        label1 = new System.Windows.Forms.Label();
        SuspendLayout();
        // 
        // acceptButton
        // 
        resources.ApplyResources(acceptButton, "acceptButton");
        acceptButton.Name = "acceptButton";
        acceptButton.UseVisualStyleBackColor = true;
        acceptButton.Click += OnAcceptButtonClick;
        // 
        // passwordTextBox
        // 
        resources.ApplyResources(passwordTextBox, "passwordTextBox");
        passwordTextBox.Name = "passwordTextBox";
        passwordTextBox.UseSystemPasswordChar = true;
        // 
        // label1
        // 
        resources.ApplyResources(label1, "label1");
        label1.AutoEllipsis = true;
        label1.Name = "label1";
        // 
        // PasswordForm
        // 
        AcceptButton = acceptButton;
        resources.ApplyResources(this, "$this");
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        Controls.Add(label1);
        Controls.Add(passwordTextBox);
        Controls.Add(acceptButton);
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "PasswordForm";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private System.Windows.Forms.Button acceptButton;
    private System.Windows.Forms.TextBox passwordTextBox;
    private System.Windows.Forms.Label label1;
}