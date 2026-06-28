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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindForm));
        splitContainer1 = new System.Windows.Forms.SplitContainer();
        tabControl1 = new System.Windows.Forms.TabControl();
        tabPage1 = new System.Windows.Forms.TabPage();
        tabPage2 = new System.Windows.Forms.TabPage();
        _filterControl = new FilterControl();
        _listView = new Liber.Forms.Lines.LineListView();
        findButton = new System.Windows.Forms.Button();
        closeButton = new System.Windows.Forms.Button();
        goToButton = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
        splitContainer1.Panel1.SuspendLayout();
        splitContainer1.Panel2.SuspendLayout();
        splitContainer1.SuspendLayout();
        tabControl1.SuspendLayout();
        tabPage2.SuspendLayout();
        SuspendLayout();
        // 
        // splitContainer1
        // 
        resources.ApplyResources(splitContainer1, "splitContainer1");
        splitContainer1.Name = "splitContainer1";
        // 
        // splitContainer1.Panel1
        // 
        resources.ApplyResources(splitContainer1.Panel1, "splitContainer1.Panel1");
        splitContainer1.Panel1.Controls.Add(tabControl1);
        // 
        // splitContainer1.Panel2
        // 
        resources.ApplyResources(splitContainer1.Panel2, "splitContainer1.Panel2");
        splitContainer1.Panel2.Controls.Add(_listView);
        // 
        // tabControl1
        // 
        resources.ApplyResources(tabControl1, "tabControl1");
        tabControl1.Controls.Add(tabPage1);
        tabControl1.Controls.Add(tabPage2);
        tabControl1.Name = "tabControl1";
        tabControl1.SelectedIndex = 0;
        // 
        // tabPage1
        // 
        resources.ApplyResources(tabPage1, "tabPage1");
        tabPage1.Name = "tabPage1";
        tabPage1.UseVisualStyleBackColor = true;
        // 
        // tabPage2
        // 
        resources.ApplyResources(tabPage2, "tabPage2");
        tabPage2.Controls.Add(_filterControl);
        tabPage2.Name = "tabPage2";
        tabPage2.UseVisualStyleBackColor = true;
        // 
        // _filterControl
        // 
        resources.ApplyResources(_filterControl, "_filterControl");
        _filterControl.Name = "_filterControl";
        // 
        // _listView
        // 
        resources.ApplyResources(_listView, "_listView");
        _listView.AllowColumnReorder = true;
        _listView.FullRowSelect = true;
        _listView.GridLines = true;
        _listView.Name = "_listView";
        _listView.UseCompatibleStateImageBehavior = false;
        _listView.View = System.Windows.Forms.View.Details;
        _listView.ItemActivate += OnListViewItemActivate;
        // 
        // findButton
        // 
        resources.ApplyResources(findButton, "findButton");
        findButton.Name = "findButton";
        findButton.UseVisualStyleBackColor = true;
        findButton.Click += OnFindButtonClick;
        // 
        // closeButton
        // 
        resources.ApplyResources(closeButton, "closeButton");
        closeButton.Name = "closeButton";
        closeButton.UseVisualStyleBackColor = true;
        closeButton.Click += OnCloseButtonClick;
        // 
        // goToButton
        // 
        resources.ApplyResources(goToButton, "goToButton");
        goToButton.Name = "goToButton";
        goToButton.UseVisualStyleBackColor = true;
        goToButton.Click += OnListViewItemActivate;
        // 
        // FindForm
        // 
        AcceptButton = findButton;
        resources.ApplyResources(this, "$this");
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        CancelButton = closeButton;
        Controls.Add(goToButton);
        Controls.Add(splitContainer1);
        Controls.Add(findButton);
        Controls.Add(closeButton);
        Name = "FindForm";
        splitContainer1.Panel1.ResumeLayout(false);
        splitContainer1.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
        splitContainer1.ResumeLayout(false);
        tabControl1.ResumeLayout(false);
        tabPage2.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion
    private Lines.LineListView _listView;
    private FilterControl _filterControl;
    private System.Windows.Forms.Button findButton;
    private System.Windows.Forms.Button closeButton;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.Button goToButton;
}
