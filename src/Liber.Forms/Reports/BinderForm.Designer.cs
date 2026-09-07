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
        components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BinderForm));
        _customReportListView = new ReportListView();
        _customReportContextMenu = new System.Windows.Forms.ContextMenuStrip(components);
        editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
        moveUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        moveDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
        customReportSelectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        splitContainer1 = new System.Windows.Forms.SplitContainer();
        _listView = new ReportListView();
        _contextMenu = new System.Windows.Forms.ContextMenuStrip(components);
        addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
        selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        statusStrip1 = new System.Windows.Forms.StatusStrip();
        toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
        addToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
        editToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        renameToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        duplicateToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        removeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
        moveUpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        moveDownToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        toolStrip1 = new System.Windows.Forms.ToolStrip();
        newToolStripButton = new System.Windows.Forms.ToolStripButton();
        openToolStripButton = new System.Windows.Forms.ToolStripButton();
        saveToolStripButton = new System.Windows.Forms.ToolStripButton();
        printToolStripButton = new System.Windows.Forms.ToolStripButton();
        toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
        cutToolStripButton = new System.Windows.Forms.ToolStripButton();
        copyToolStripButton = new System.Windows.Forms.ToolStripButton();
        pasteToolStripButton = new System.Windows.Forms.ToolStripButton();
        toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
        helpToolStripButton = new System.Windows.Forms.ToolStripButton();
        _customReportContextMenu.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
        splitContainer1.Panel1.SuspendLayout();
        splitContainer1.Panel2.SuspendLayout();
        splitContainer1.SuspendLayout();
        _contextMenu.SuspendLayout();
        statusStrip1.SuspendLayout();
        toolStrip1.SuspendLayout();
        SuspendLayout();
        // 
        // _customReportListView
        // 
        _customReportListView.AllowColumnReorder = true;
        _customReportListView.ContextMenuStrip = _customReportContextMenu;
        resources.ApplyResources(_customReportListView, "_customReportListView");
        _customReportListView.FullRowSelect = true;
        _customReportListView.GridLines = true;
        _customReportListView.LabelEdit = true;
        _customReportListView.Name = "_customReportListView";
        _customReportListView.UseCompatibleStateImageBehavior = false;
        _customReportListView.View = System.Windows.Forms.View.Details;
        _customReportListView.AfterLabelEdit += OnCustomReportListViewAfterLabelEdit;
        _customReportListView.ItemActivate += OnEditToolStripMenuItemClick;
        // 
        // _customReportContextMenu
        // 
        _customReportContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { editToolStripMenuItem, renameToolStripMenuItem, duplicateToolStripMenuItem, removeToolStripMenuItem, toolStripSeparator2, moveUpToolStripMenuItem, moveDownToolStripMenuItem, toolStripSeparator1, customReportSelectAllToolStripMenuItem });
        _customReportContextMenu.Name = "_customReportContextMenu";
        resources.ApplyResources(_customReportContextMenu, "_customReportContextMenu");
        // 
        // editToolStripMenuItem
        // 
        editToolStripMenuItem.Image = VisualStudioImageLibrary.Edit;
        editToolStripMenuItem.Name = "editToolStripMenuItem";
        resources.ApplyResources(editToolStripMenuItem, "editToolStripMenuItem");
        editToolStripMenuItem.Click += OnEditToolStripMenuItemClick;
        // 
        // renameToolStripMenuItem
        // 
        renameToolStripMenuItem.Image = VisualStudioImageLibrary.Rename;
        renameToolStripMenuItem.Name = "renameToolStripMenuItem";
        resources.ApplyResources(renameToolStripMenuItem, "renameToolStripMenuItem");
        renameToolStripMenuItem.Click += OnRenameToolStripMenuItemClick;
        // 
        // duplicateToolStripMenuItem
        // 
        duplicateToolStripMenuItem.Image = VisualStudioImageLibrary.Duplicate;
        duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
        resources.ApplyResources(duplicateToolStripMenuItem, "duplicateToolStripMenuItem");
        duplicateToolStripMenuItem.Click += OnDuplicateToolStripMenuItemClick;
        // 
        // removeToolStripMenuItem
        // 
        removeToolStripMenuItem.Image = VisualStudioImageLibrary.Remove;
        removeToolStripMenuItem.Name = "removeToolStripMenuItem";
        resources.ApplyResources(removeToolStripMenuItem, "removeToolStripMenuItem");
        removeToolStripMenuItem.Click += OnRemoveToolStripMenuItemClick;
        // 
        // toolStripSeparator2
        // 
        toolStripSeparator2.Name = "toolStripSeparator2";
        resources.ApplyResources(toolStripSeparator2, "toolStripSeparator2");
        // 
        // moveUpToolStripMenuItem
        // 
        moveUpToolStripMenuItem.Image = VisualStudioImageLibrary.MoveUp;
        moveUpToolStripMenuItem.Name = "moveUpToolStripMenuItem";
        resources.ApplyResources(moveUpToolStripMenuItem, "moveUpToolStripMenuItem");
        moveUpToolStripMenuItem.Click += OnMoveUpToolStripMenuItemClick;
        // 
        // moveDownToolStripMenuItem
        // 
        moveDownToolStripMenuItem.Image = VisualStudioImageLibrary.MoveDown;
        moveDownToolStripMenuItem.Name = "moveDownToolStripMenuItem";
        resources.ApplyResources(moveDownToolStripMenuItem, "moveDownToolStripMenuItem");
        moveDownToolStripMenuItem.Click += OnMoveDownToolStripMenuItemClick;
        // 
        // toolStripSeparator1
        // 
        toolStripSeparator1.Name = "toolStripSeparator1";
        resources.ApplyResources(toolStripSeparator1, "toolStripSeparator1");
        // 
        // customReportSelectAllToolStripMenuItem
        // 
        customReportSelectAllToolStripMenuItem.Name = "customReportSelectAllToolStripMenuItem";
        resources.ApplyResources(customReportSelectAllToolStripMenuItem, "customReportSelectAllToolStripMenuItem");
        customReportSelectAllToolStripMenuItem.Click += OnCustomReportSelectAllToolStripMenuItemClick;
        // 
        // splitContainer1
        // 
        resources.ApplyResources(splitContainer1, "splitContainer1");
        splitContainer1.Name = "splitContainer1";
        // 
        // splitContainer1.Panel1
        // 
        splitContainer1.Panel1.Controls.Add(_customReportListView);
        // 
        // splitContainer1.Panel2
        // 
        splitContainer1.Panel2.Controls.Add(_listView);
        // 
        // _listView
        // 
        _listView.AllowColumnReorder = true;
        _listView.ContextMenuStrip = _contextMenu;
        resources.ApplyResources(_listView, "_listView");
        _listView.FullRowSelect = true;
        _listView.GridLines = true;
        _listView.Name = "_listView";
        _listView.UseCompatibleStateImageBehavior = false;
        _listView.View = System.Windows.Forms.View.Details;
        _listView.ItemActivate += OnAddToolStripMenuItemClick;
        // 
        // _contextMenu
        // 
        _contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { addToolStripMenuItem, toolStripSeparator3, selectAllToolStripMenuItem });
        _contextMenu.Name = "_contextMenu";
        resources.ApplyResources(_contextMenu, "_contextMenu");
        // 
        // addToolStripMenuItem
        // 
        addToolStripMenuItem.Image = VisualStudioImageLibrary.New;
        addToolStripMenuItem.Name = "addToolStripMenuItem";
        resources.ApplyResources(addToolStripMenuItem, "addToolStripMenuItem");
        addToolStripMenuItem.Click += OnAddToolStripMenuItemClick;
        // 
        // toolStripSeparator3
        // 
        toolStripSeparator3.Name = "toolStripSeparator3";
        resources.ApplyResources(toolStripSeparator3, "toolStripSeparator3");
        // 
        // selectAllToolStripMenuItem
        // 
        selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
        resources.ApplyResources(selectAllToolStripMenuItem, "selectAllToolStripMenuItem");
        selectAllToolStripMenuItem.Click += OnSelectAllToolStripMenuItemClick;
        // 
        // statusStrip1
        // 
        statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripDropDownButton1 });
        resources.ApplyResources(statusStrip1, "statusStrip1");
        statusStrip1.Name = "statusStrip1";
        // 
        // toolStripDropDownButton1
        // 
        toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
        toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { addToolStripMenuItem1, toolStripSeparator4, editToolStripMenuItem1, renameToolStripMenuItem1, removeToolStripMenuItem1, duplicateToolStripMenuItem1, toolStripSeparator5, moveDownToolStripMenuItem1, moveUpToolStripMenuItem1 });
        resources.ApplyResources(toolStripDropDownButton1, "toolStripDropDownButton1");
        toolStripDropDownButton1.Name = "toolStripDropDownButton1";
        // 
        // addToolStripMenuItem1
        // 
        addToolStripMenuItem1.Image = VisualStudioImageLibrary.New;
        addToolStripMenuItem1.Name = "addToolStripMenuItem1";
        resources.ApplyResources(addToolStripMenuItem1, "addToolStripMenuItem1");
        addToolStripMenuItem1.Click += OnAddToolStripMenuItemClick;
        // 
        // toolStripSeparator4
        // 
        toolStripSeparator4.Name = "toolStripSeparator4";
        resources.ApplyResources(toolStripSeparator4, "toolStripSeparator4");
        // 
        // editToolStripMenuItem1
        // 
        editToolStripMenuItem1.Image = VisualStudioImageLibrary.Edit;
        editToolStripMenuItem1.Name = "editToolStripMenuItem1";
        resources.ApplyResources(editToolStripMenuItem1, "editToolStripMenuItem1");
        editToolStripMenuItem1.Click += OnEditToolStripMenuItemClick;
        // 
        // renameToolStripMenuItem1
        // 
        renameToolStripMenuItem1.Image = VisualStudioImageLibrary.Rename;
        renameToolStripMenuItem1.Name = "renameToolStripMenuItem1";
        resources.ApplyResources(renameToolStripMenuItem1, "renameToolStripMenuItem1");
        renameToolStripMenuItem1.Click += OnRenameToolStripMenuItemClick;
        // 
        // duplicateToolStripMenuItem1
        // 
        duplicateToolStripMenuItem1.Image = VisualStudioImageLibrary.Duplicate;
        duplicateToolStripMenuItem1.Name = "duplicateToolStripMenuItem1";
        resources.ApplyResources(duplicateToolStripMenuItem1, "duplicateToolStripMenuItem1");
        duplicateToolStripMenuItem1.Click += OnDuplicateToolStripMenuItemClick;
        // 
        // removeToolStripMenuItem1
        // 
        removeToolStripMenuItem1.Image = VisualStudioImageLibrary.Remove;
        removeToolStripMenuItem1.Name = "removeToolStripMenuItem1";
        resources.ApplyResources(removeToolStripMenuItem1, "removeToolStripMenuItem1");
        removeToolStripMenuItem1.Click += OnRemoveToolStripMenuItemClick;
        // 
        // toolStripSeparator5
        // 
        toolStripSeparator5.Name = "toolStripSeparator5";
        resources.ApplyResources(toolStripSeparator5, "toolStripSeparator5");
        // 
        // moveUpToolStripMenuItem1
        // 
        moveUpToolStripMenuItem1.Image = VisualStudioImageLibrary.MoveUp;
        moveUpToolStripMenuItem1.Name = "moveUpToolStripMenuItem1";
        resources.ApplyResources(moveUpToolStripMenuItem1, "moveUpToolStripMenuItem1");
        moveUpToolStripMenuItem1.Click += OnMoveDownToolStripMenuItemClick;
        // 
        // moveDownToolStripMenuItem1
        // 
        moveDownToolStripMenuItem1.Image = VisualStudioImageLibrary.MoveDown;
        moveDownToolStripMenuItem1.Name = "moveDownToolStripMenuItem1";
        resources.ApplyResources(moveDownToolStripMenuItem1, "moveDownToolStripMenuItem1");
        moveDownToolStripMenuItem1.Click += OnMoveUpToolStripMenuItemClick;
        // 
        // toolStrip1
        // 
        toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { newToolStripButton, openToolStripButton, saveToolStripButton, printToolStripButton, toolStripSeparator, cutToolStripButton, copyToolStripButton, pasteToolStripButton, toolStripSeparator6, helpToolStripButton });
        resources.ApplyResources(toolStrip1, "toolStrip1");
        toolStrip1.Name = "toolStrip1";
        // 
        // newToolStripButton
        // 
        newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        resources.ApplyResources(newToolStripButton, "newToolStripButton");
        newToolStripButton.Name = "newToolStripButton";
        // 
        // openToolStripButton
        // 
        openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        resources.ApplyResources(openToolStripButton, "openToolStripButton");
        openToolStripButton.Name = "openToolStripButton";
        // 
        // saveToolStripButton
        // 
        saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        resources.ApplyResources(saveToolStripButton, "saveToolStripButton");
        saveToolStripButton.Name = "saveToolStripButton";
        // 
        // printToolStripButton
        // 
        printToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        resources.ApplyResources(printToolStripButton, "printToolStripButton");
        printToolStripButton.Name = "printToolStripButton";
        // 
        // toolStripSeparator
        // 
        toolStripSeparator.Name = "toolStripSeparator";
        resources.ApplyResources(toolStripSeparator, "toolStripSeparator");
        // 
        // cutToolStripButton
        // 
        cutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        resources.ApplyResources(cutToolStripButton, "cutToolStripButton");
        cutToolStripButton.Name = "cutToolStripButton";
        // 
        // copyToolStripButton
        // 
        copyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        resources.ApplyResources(copyToolStripButton, "copyToolStripButton");
        copyToolStripButton.Name = "copyToolStripButton";
        // 
        // pasteToolStripButton
        // 
        pasteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        resources.ApplyResources(pasteToolStripButton, "pasteToolStripButton");
        pasteToolStripButton.Name = "pasteToolStripButton";
        // 
        // toolStripSeparator6
        // 
        toolStripSeparator6.Name = "toolStripSeparator6";
        resources.ApplyResources(toolStripSeparator6, "toolStripSeparator6");
        // 
        // helpToolStripButton
        // 
        helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        resources.ApplyResources(helpToolStripButton, "helpToolStripButton");
        helpToolStripButton.Name = "helpToolStripButton";
        // 
        // BinderForm
        // 
        resources.ApplyResources(this, "$this");
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        Controls.Add(statusStrip1);
        Controls.Add(splitContainer1);
        Controls.Add(toolStrip1);
        Name = "BinderForm";
        _customReportContextMenu.ResumeLayout(false);
        splitContainer1.Panel1.ResumeLayout(false);
        splitContainer1.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
        splitContainer1.ResumeLayout(false);
        _contextMenu.ResumeLayout(false);
        statusStrip1.ResumeLayout(false);
        statusStrip1.PerformLayout();
        toolStrip1.ResumeLayout(false);
        toolStrip1.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private ReportListView _customReportListView;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private ReportListView _listView;
    private System.Windows.Forms.ContextMenuStrip _customReportContextMenu;
    private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripMenuItem moveUpToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem moveDownToolStripMenuItem;
    private System.Windows.Forms.ContextMenuStrip _contextMenu;
    private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem duplicateToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem customReportSelectAllToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
    private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem duplicateToolStripMenuItem1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    private System.Windows.Forms.ToolStripMenuItem moveDownToolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem moveUpToolStripMenuItem1;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton newToolStripButton;
    private System.Windows.Forms.ToolStripButton openToolStripButton;
    private System.Windows.Forms.ToolStripButton saveToolStripButton;
    private System.Windows.Forms.ToolStripButton printToolStripButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
    private System.Windows.Forms.ToolStripButton cutToolStripButton;
    private System.Windows.Forms.ToolStripButton copyToolStripButton;
    private System.Windows.Forms.ToolStripButton pasteToolStripButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
    private System.Windows.Forms.ToolStripButton helpToolStripButton;
}
