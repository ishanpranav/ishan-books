// LineForm.Designer.cs
// Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.Forms.Lines;

partial class CheckDialog
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckDialog));
        _listView = new System.Windows.Forms.ListViewEx();
        nameColumn = new System.Windows.Forms.ColumnHeader();
        postedColumn = new System.Windows.Forms.ColumnHeader();
        numberColumn = new System.Windows.Forms.ColumnHeader();
        balanceColumn = new System.Windows.Forms.ColumnHeader();
        acceptButton = new System.Windows.Forms.Button();
        cancelButton = new System.Windows.Forms.Button();
        SuspendLayout();
        // 
        // _listView
        // 
        _listView.AllowColumnReorder = true;
        resources.ApplyResources(_listView, "_listView");
        _listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { nameColumn, postedColumn, numberColumn, balanceColumn });
        _listView.FullRowSelect = true;
        _listView.MultiSelect = false;
        _listView.Name = "_listView";
        _listView.SortColumn = 0;
        _listView.SortOrder = System.Windows.Forms.SortOrder.None;
        _listView.UseCompatibleStateImageBehavior = false;
        _listView.View = System.Windows.Forms.View.Details;
        _listView.ItemActivate += OnAcceptButtonClick;
        // 
        // nameColumn
        // 
        resources.ApplyResources(nameColumn, "nameColumn");
        // 
        // postedColumn
        // 
        resources.ApplyResources(postedColumn, "postedColumn");
        // 
        // numberColumn
        // 
        resources.ApplyResources(numberColumn, "numberColumn");
        // 
        // balanceColumn
        // 
        resources.ApplyResources(balanceColumn, "balanceColumn");
        // 
        // acceptButton
        // 
        resources.ApplyResources(acceptButton, "acceptButton");
        acceptButton.Name = "acceptButton";
        acceptButton.UseVisualStyleBackColor = true;
        acceptButton.Click += OnAcceptButtonClick;
        // 
        // cancelButton
        // 
        resources.ApplyResources(cancelButton, "cancelButton");
        cancelButton.Name = "cancelButton";
        cancelButton.UseVisualStyleBackColor = true;
        cancelButton.Click += OnCancelButtonClick;
        // 
        // CheckForm
        // 
        AcceptButton = acceptButton;
        resources.ApplyResources(this, "$this");
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        CancelButton = cancelButton;
        Controls.Add(acceptButton);
        Controls.Add(cancelButton);
        Controls.Add(_listView);
        Name = "CheckForm";
        ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.ListViewEx _listView;
    private System.Windows.Forms.Button acceptButton;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.ColumnHeader postedColumn;
    private System.Windows.Forms.ColumnHeader numberColumn;
    private System.Windows.Forms.ColumnHeader nameColumn;
    private System.Windows.Forms.ColumnHeader balanceColumn;
}
