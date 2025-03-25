// AccountsDialog.Designer.cs
// Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.Forms.Accounts;

partial class AccountsDialog
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountsDialog));
        acceptButton = new System.Windows.Forms.Button();
        cancelButton = new System.Windows.Forms.Button();
        selectAllButton = new System.Windows.Forms.Button();
        deselectAllButton = new System.Windows.Forms.Button();
        toggleAllButton = new System.Windows.Forms.Button();
        nameColumn = new System.Windows.Forms.ColumnHeader();
        numberColumn = new System.Windows.Forms.ColumnHeader();
        _accountListView = new AccountListView();
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
        // selectAllButton
        // 
        resources.ApplyResources(selectAllButton, "selectAllButton");
        selectAllButton.Name = "selectAllButton";
        selectAllButton.UseVisualStyleBackColor = true;
        selectAllButton.Click += OnSelectAllButtonClick;
        // 
        // deselectAllButton
        // 
        resources.ApplyResources(deselectAllButton, "deselectAllButton");
        deselectAllButton.Name = "deselectAllButton";
        deselectAllButton.UseVisualStyleBackColor = true;
        deselectAllButton.Click += OnDeselectAllButtonClick;
        // 
        // toggleAllButton
        // 
        resources.ApplyResources(toggleAllButton, "toggleAllButton");
        toggleAllButton.Name = "toggleAllButton";
        toggleAllButton.UseVisualStyleBackColor = true;
        toggleAllButton.Click += OnToggleAllButtonClick;
        // 
        // nameColumn
        // 
        resources.ApplyResources(nameColumn, "nameColumn");
        // 
        // numberColumn
        // 
        resources.ApplyResources(numberColumn, "numberColumn");
        // 
        // _accountListView
        // 
        _accountListView.AllowColumnReorder = true;
        resources.ApplyResources(_accountListView, "_accountListView");
        _accountListView.CheckBoxes = true;
        _accountListView.FullRowSelect = true;
        _accountListView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] { (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups1"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups2"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups3"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups4"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups5"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups6"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups7"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups8"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups9"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups10"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups11"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups12"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups13") });
        _accountListView.Name = "_accountListView";
        _accountListView.SortColumn = 0;
        _accountListView.SortOrder = System.Windows.Forms.SortOrder.None;
        _accountListView.UseCompatibleStateImageBehavior = false;
        _accountListView.View = System.Windows.Forms.View.Details;
        // 
        // AccountsDialog
        // 
        AcceptButton = acceptButton;
        resources.ApplyResources(this, "$this");
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        CancelButton = cancelButton;
        Controls.Add(_accountListView);
        Controls.Add(toggleAllButton);
        Controls.Add(deselectAllButton);
        Controls.Add(selectAllButton);
        Controls.Add(acceptButton);
        Controls.Add(cancelButton);
        Name = "AccountsDialog";
        ResumeLayout(false);
    }

    #endregion
    private System.Windows.Forms.Button acceptButton;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Button selectAllButton;
    private System.Windows.Forms.Button deselectAllButton;
    private System.Windows.Forms.Button toggleAllButton;
    private System.Windows.Forms.ColumnHeader nameColumn;
    private System.Windows.Forms.ColumnHeader numberColumn;
    private AccountListView _accountListView;
}
