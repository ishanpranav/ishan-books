// FilterDialog.Designer.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.Forms.Filters;

partial class FilterDialog
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
        acceptButton = new System.Windows.Forms.Button();
        cancelButton = new System.Windows.Forms.Button();
        _filterControl = new FilterControl();
        SuspendLayout();
        // 
        // acceptButton
        // 
        acceptButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        acceptButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
        acceptButton.Location = new System.Drawing.Point(420, 311);
        acceptButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
        acceptButton.Name = "acceptButton";
        acceptButton.Size = new System.Drawing.Size(82, 23);
        acceptButton.TabIndex = 9;
        acceptButton.Text = "O&K";
        acceptButton.UseVisualStyleBackColor = true;
        acceptButton.Click += OnAcceptButtonClick;
        // 
        // cancelButton
        // 
        cancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        cancelButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
        cancelButton.Location = new System.Drawing.Point(508, 311);
        cancelButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new System.Drawing.Size(82, 23);
        cancelButton.TabIndex = 8;
        cancelButton.Text = "&Cancel";
        cancelButton.UseVisualStyleBackColor = true;
        cancelButton.Click += OnCancelButtonClick;
        // 
        // _filterControl
        // 
        _filterControl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        _filterControl.Location = new System.Drawing.Point(12, 12);
        _filterControl.Name = "_filterControl";
        _filterControl.Size = new System.Drawing.Size(578, 294);
        _filterControl.TabIndex = 10;
        // 
        // FilterDialog
        // 
        AcceptButton = acceptButton;
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        CancelButton = cancelButton;
        ClientSize = new System.Drawing.Size(602, 345);
        Controls.Add(_filterControl);
        Controls.Add(acceptButton);
        Controls.Add(cancelButton);
        Name = "FilterDialog";
        Text = "Filter";
        ResumeLayout(false);
    }

    #endregion
    private System.Windows.Forms.Button acceptButton;
    private System.Windows.Forms.Button cancelButton;
    private FilterControl _filterControl;
}