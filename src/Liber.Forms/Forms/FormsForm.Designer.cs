// FormsForm.Designer.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.Forms.Forms;

partial class FormsForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormsForm));
        _listView = new System.Windows.Forms.ListViewEx();
        nameColumn = new System.Windows.Forms.ColumnHeader();
        activateFormButton = new System.Windows.Forms.Button();
        closeFormButton = new System.Windows.Forms.Button();
        acceptButton = new System.Windows.Forms.Button();
        SuspendLayout();
        // 
        // _listView
        // 
        resources.ApplyResources(_listView, "_listView");
        _listView.AllowColumnReorder = true;
        _listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { nameColumn });
        _listView.FullRowSelect = true;
        _listView.GridLines = true;
        _listView.Name = "_listView";
        _listView.SortColumn = 0;
        _listView.Sorting = System.Windows.Forms.SortOrder.Ascending;
        _listView.UseCompatibleStateImageBehavior = false;
        _listView.View = System.Windows.Forms.View.Details;
        _listView.ItemActivate += OnListViewItemActivate;
        // 
        // nameColumn
        // 
        resources.ApplyResources(nameColumn, "nameColumn");
        // 
        // activateFormButton
        // 
        resources.ApplyResources(activateFormButton, "activateFormButton");
        activateFormButton.Name = "activateFormButton";
        activateFormButton.UseVisualStyleBackColor = true;
        activateFormButton.Click += OnListViewItemActivate;
        // 
        // closeFormButton
        // 
        resources.ApplyResources(closeFormButton, "closeFormButton");
        closeFormButton.Name = "closeFormButton";
        closeFormButton.UseVisualStyleBackColor = true;
        closeFormButton.Click += OnCloseFormButtonClick;
        // 
        // acceptButton
        // 
        resources.ApplyResources(acceptButton, "acceptButton");
        acceptButton.Name = "acceptButton";
        acceptButton.UseVisualStyleBackColor = true;
        acceptButton.Click += OnAcceptButtonClick;
        // 
        // FormsForm
        // 
        AcceptButton = acceptButton;
        resources.ApplyResources(this, "$this");
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        Controls.Add(acceptButton);
        Controls.Add(closeFormButton);
        Controls.Add(activateFormButton);
        Controls.Add(_listView);
        Name = "FormsForm";
        ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.ListViewEx _listView;
    private System.Windows.Forms.Button activateFormButton;
    private System.Windows.Forms.Button closeFormButton;
    private System.Windows.Forms.Button acceptButton;
    private System.Windows.Forms.ColumnHeader nameColumn;
}
