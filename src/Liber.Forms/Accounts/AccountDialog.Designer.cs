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
        _accountListView.FullRowSelect = true;
        _accountListView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] { (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups1"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups2"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups3"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups4"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups5"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups6"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups7"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups8"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups9"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups10"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups11"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups12"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups13"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups14"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups15"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups16"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups17"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups18"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups19"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups20"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups21"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups22"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups23"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups24"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups25"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups26"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups27"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups28"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups29"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups30"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups31"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups32"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups33"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups34"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups35"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups36"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups37"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups38"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups39"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups40"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups41") });
        _accountListView.Name = "_accountListView";
        _accountListView.SortColumn = 0;
        _accountListView.SortOrder = System.Windows.Forms.SortOrder.None;
        _accountListView.UseCompatibleStateImageBehavior = false;
        _accountListView.View = System.Windows.Forms.View.Details;
        _accountListView.ItemActivate += OnAcceptButtonClick;
        // 
        // AccountDialog
        // 
        AcceptButton = acceptButton;
        resources.ApplyResources(this, "$this");
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        CancelButton = cancelButton;
        Controls.Add(_accountListView);
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
    private AccountListView _accountListView;
}
