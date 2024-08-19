// AccountDialog.Designer.cs
// Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.Forms.Accounts;

partial class AccountDialog
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountDialog));
        acceptButton = new System.Windows.Forms.Button();
        cancelButton = new System.Windows.Forms.Button();
        AccountListView = new System.Windows.Forms.ListViewEx();
        nameColumn = new System.Windows.Forms.ColumnHeader();
        numberColumn = new System.Windows.Forms.ColumnHeader();
        SuspendLayout();
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
        // AccountListView
        // 
        AccountListView.AllowColumnReorder = true;
        resources.ApplyResources(AccountListView, "AccountListView");
        AccountListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { nameColumn, numberColumn });
        AccountListView.FullRowSelect = true;
        AccountListView.MultiSelect = false;
        AccountListView.Name = "AccountListView";
        AccountListView.SortColumn = 0;
        AccountListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
        AccountListView.SortOrder = System.Windows.Forms.SortOrder.None;
        AccountListView.UseCompatibleStateImageBehavior = false;
        AccountListView.View = System.Windows.Forms.View.Details;
        AccountListView.ItemActivate += OnAcceptButtonClick;
        // 
        // nameColumn
        // 
        resources.ApplyResources(nameColumn, "nameColumn");
        // 
        // numberColumn
        // 
        resources.ApplyResources(numberColumn, "numberColumn");
        // 
        // AccountDialog
        // 
        AcceptButton = acceptButton;
        resources.ApplyResources(this, "$this");
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        CancelButton = cancelButton;
        Controls.Add(AccountListView);
        Controls.Add(acceptButton);
        Controls.Add(cancelButton);
        Name = "AccountDialog";
        ResumeLayout(false);
    }

    #endregion
    private System.Windows.Forms.Button acceptButton;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.ColumnHeader numberColumn;
    private System.Windows.Forms.ColumnHeader nameColumn;
    protected internal System.Windows.Forms.ListViewEx AccountListView;
}