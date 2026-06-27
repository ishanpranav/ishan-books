// FilterControl.Designer.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.Forms.Filters;

partial class FilterControl
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        splitContainer1 = new System.Windows.Forms.SplitContainer();
        _treeView = new System.Windows.Forms.TreeView();
        _contextMenu = new System.Windows.Forms.ContextMenuStrip(components);
        addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        _propertyGrid = new System.Windows.Forms.PropertyGrid();
        ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
        splitContainer1.Panel1.SuspendLayout();
        splitContainer1.Panel2.SuspendLayout();
        splitContainer1.SuspendLayout();
        _contextMenu.SuspendLayout();
        SuspendLayout();
        // 
        // splitContainer1
        // 
        splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
        splitContainer1.Location = new System.Drawing.Point(0, 0);
        splitContainer1.Name = "splitContainer1";
        // 
        // splitContainer1.Panel1
        // 
        splitContainer1.Panel1.Controls.Add(_treeView);
        // 
        // splitContainer1.Panel2
        // 
        splitContainer1.Panel2.Controls.Add(_propertyGrid);
        splitContainer1.Size = new System.Drawing.Size(749, 450);
        splitContainer1.SplitterDistance = 387;
        splitContainer1.TabIndex = 1;
        // 
        // _treeView
        // 
        _treeView.ContextMenuStrip = _contextMenu;
        _treeView.Dock = System.Windows.Forms.DockStyle.Fill;
        _treeView.Location = new System.Drawing.Point(0, 0);
        _treeView.Name = "_treeView";
        _treeView.Size = new System.Drawing.Size(387, 450);
        _treeView.TabIndex = 0;
        _treeView.AfterSelect += OnTreeViewAfterSelect;
        _treeView.NodeMouseClick += OnTreeViewNodeMouseClick;
        // 
        // _contextMenu
        // 
        _contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { addToolStripMenuItem, editToolStripMenuItem, removeToolStripMenuItem });
        _contextMenu.Name = "_contextMenu";
        _contextMenu.Size = new System.Drawing.Size(147, 70);
        _contextMenu.Opening += OnContextMenuOpening;
        // 
        // addToolStripMenuItem
        // 
        addToolStripMenuItem.Image = VisualStudioImageLibrary.NewFilter;
        addToolStripMenuItem.Name = "addToolStripMenuItem";
        addToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
        addToolStripMenuItem.Text = "Add Filter";
        // 
        // editToolStripMenuItem
        // 
        editToolStripMenuItem.Image = VisualStudioImageLibrary.EditFilter;
        editToolStripMenuItem.Name = "editToolStripMenuItem";
        editToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
        editToolStripMenuItem.Text = "Edit Filter";
        editToolStripMenuItem.Click += OnEditToolStripMenuItemClick;
        // 
        // removeToolStripMenuItem
        // 
        removeToolStripMenuItem.Image = VisualStudioImageLibrary.DeleteFilter;
        removeToolStripMenuItem.Name = "removeToolStripMenuItem";
        removeToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
        removeToolStripMenuItem.Text = "Remove Filter";
        removeToolStripMenuItem.Click += OnRemoveToolStripMenuItemClick;
        // 
        // _propertyGrid
        // 
        _propertyGrid.BackColor = System.Drawing.SystemColors.Control;
        _propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
        _propertyGrid.Location = new System.Drawing.Point(0, 0);
        _propertyGrid.Name = "_propertyGrid";
        _propertyGrid.Size = new System.Drawing.Size(358, 450);
        _propertyGrid.TabIndex = 0;
        _propertyGrid.PropertyValueChanged += OnPropertyGridPropertyValueChanged;
        // 
        // FilterControl
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        Controls.Add(splitContainer1);
        Name = "FilterControl";
        Size = new System.Drawing.Size(749, 450);
        splitContainer1.Panel1.ResumeLayout(false);
        splitContainer1.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
        splitContainer1.ResumeLayout(false);
        _contextMenu.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.TreeView _treeView;
    private System.Windows.Forms.PropertyGrid _propertyGrid;
    private System.Windows.Forms.ContextMenuStrip _contextMenu;
    private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
}
