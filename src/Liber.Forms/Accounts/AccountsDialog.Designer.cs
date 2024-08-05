// AccountsDialog.Designer.cs
// Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
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
        _checkedListBox = new System.Windows.Forms.CheckedListBox();
        acceptButton = new System.Windows.Forms.Button();
        cancelButton = new System.Windows.Forms.Button();
        selectAllButton = new System.Windows.Forms.Button();
        deselectAllButton = new System.Windows.Forms.Button();
        toggleAllButton = new System.Windows.Forms.Button();
        SuspendLayout();
        // 
        // _checkedListBox
        // 
        resources.ApplyResources(_checkedListBox, "_checkedListBox");
        _checkedListBox.CheckOnClick = true;
        _checkedListBox.Name = "_checkedListBox";
        _checkedListBox.Format += OnCheckedListBoxFormat;
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
        // AccountsDialog
        // 
        AcceptButton = acceptButton;
        resources.ApplyResources(this, "$this");
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        CancelButton = cancelButton;
        Controls.Add(toggleAllButton);
        Controls.Add(deselectAllButton);
        Controls.Add(selectAllButton);
        Controls.Add(_checkedListBox);
        Controls.Add(acceptButton);
        Controls.Add(cancelButton);
        Name = "AccountsDialog";
        ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.CheckedListBox _checkedListBox;
    private System.Windows.Forms.Button acceptButton;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Button selectAllButton;
    private System.Windows.Forms.Button deselectAllButton;
    private System.Windows.Forms.Button toggleAllButton;
}
