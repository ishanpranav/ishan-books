// FindForm.Designer.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.Forms.Filters;

partial class FindForm
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
        _listView = new Liber.Forms.Lines.LineListView();
        _filterControl = new FilterControl();
        findButton = new System.Windows.Forms.Button();
        closeButton = new System.Windows.Forms.Button();
        splitContainer1 = new System.Windows.Forms.SplitContainer();
        ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
        splitContainer1.Panel1.SuspendLayout();
        splitContainer1.Panel2.SuspendLayout();
        splitContainer1.SuspendLayout();
        SuspendLayout();
        // 
        // _listView
        // 
        _listView.AllowColumnReorder = true;
        _listView.Dock = System.Windows.Forms.DockStyle.Fill;
        _listView.FullRowSelect = true;
        _listView.GridLines = true;
        _listView.Location = new System.Drawing.Point(0, 0);
        _listView.Name = "_listView";
        _listView.Size = new System.Drawing.Size(589, 209);
        _listView.TabIndex = 0;
        _listView.UseCompatibleStateImageBehavior = false;
        _listView.View = System.Windows.Forms.View.Details;
        // 
        // _filterControl
        // 
        _filterControl.Dock = System.Windows.Forms.DockStyle.Fill;
        _filterControl.Location = new System.Drawing.Point(0, 0);
        _filterControl.Name = "_filterControl";
        _filterControl.Size = new System.Drawing.Size(589, 170);
        _filterControl.TabIndex = 1;
        // 
        // findButton
        // 
        findButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        findButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
        findButton.Location = new System.Drawing.Point(431, 400);
        findButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
        findButton.Name = "findButton";
        findButton.Size = new System.Drawing.Size(82, 23);
        findButton.TabIndex = 11;
        findButton.Text = "&Find";
        findButton.UseVisualStyleBackColor = true;
        findButton.Click += OnFindButtonClick;
        // 
        // closeButton
        // 
        closeButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        closeButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
        closeButton.Location = new System.Drawing.Point(519, 400);
        closeButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
        closeButton.Name = "closeButton";
        closeButton.Size = new System.Drawing.Size(82, 23);
        closeButton.TabIndex = 10;
        closeButton.Text = "&Close";
        closeButton.UseVisualStyleBackColor = true;
        closeButton.Click += OnCloseButtonClick;
        // 
        // splitContainer1
        // 
        splitContainer1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        splitContainer1.Location = new System.Drawing.Point(12, 12);
        splitContainer1.Name = "splitContainer1";
        splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
        // 
        // splitContainer1.Panel1
        // 
        splitContainer1.Panel1.Controls.Add(_filterControl);
        // 
        // splitContainer1.Panel2
        // 
        splitContainer1.Panel2.Controls.Add(_listView);
        splitContainer1.Size = new System.Drawing.Size(589, 383);
        splitContainer1.SplitterDistance = 170;
        splitContainer1.TabIndex = 12;
        // 
        // FindForm
        // 
        AcceptButton = findButton;
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        CancelButton = closeButton;
        ClientSize = new System.Drawing.Size(613, 434);
        Controls.Add(splitContainer1);
        Controls.Add(findButton);
        Controls.Add(closeButton);
        Name = "FindForm";
        Text = "Find";
        splitContainer1.Panel1.ResumeLayout(false);
        splitContainer1.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
        splitContainer1.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion
    private Lines.LineListView _listView;
    private FilterControl _filterControl;
    private System.Windows.Forms.Button findButton;
    private System.Windows.Forms.Button closeButton;
    private System.Windows.Forms.SplitContainer splitContainer1;
}