// BinderForm.Designer.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.Forms.Reports;

partial class BinderForm
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
        listViewEx1 = new System.Windows.Forms.ListViewEx();
        SuspendLayout();
        // 
        // listViewEx1
        // 
        listViewEx1.AllowColumnReorder = true;
        listViewEx1.GridLines = true;
        listViewEx1.Location = new System.Drawing.Point(12, 12);
        listViewEx1.Name = "listViewEx1";
        listViewEx1.Size = new System.Drawing.Size(211, 353);
        listViewEx1.TabIndex = 0;
        listViewEx1.UseCompatibleStateImageBehavior = false;
        // 
        // BinderForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(577, 377);
        Controls.Add(listViewEx1);
        Name = "BinderForm";
        Text = "BinderForm";
        ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.ListViewEx listViewEx1;
}