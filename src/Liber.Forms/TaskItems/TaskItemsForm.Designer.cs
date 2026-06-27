// TaskListForm.Designer.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.Forms.TaskItems;

partial class TaskItemsForm
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskItemsForm));
        _listView = new System.Windows.Forms.ListViewEx();
        descriptionColumn = new System.Windows.Forms.ColumnHeader();
        priorityColumn = new System.Windows.Forms.ColumnHeader();
        statusStrip1 = new System.Windows.Forms.StatusStrip();
        countStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
        toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
        refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        statusStrip1.SuspendLayout();
        SuspendLayout();
        // 
        // _listView
        // 
        resources.ApplyResources(_listView, "_listView");
        _listView.AllowColumnReorder = true;
        _listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { descriptionColumn, priorityColumn });
        _listView.FullRowSelect = true;
        _listView.GridLines = true;
        _listView.HoverSelection = true;
        _listView.MultiSelect = false;
        _listView.Name = "_listView";
        _listView.SortColumn = 1;
        _listView.Sorting = System.Windows.Forms.SortOrder.Ascending;
        _listView.UseCompatibleStateImageBehavior = false;
        _listView.View = System.Windows.Forms.View.Details;
        _listView.ItemActivate += OnListViewItemActivate;
        // 
        // descriptionColumn
        // 
        resources.ApplyResources(descriptionColumn, "descriptionColumn");
        // 
        // priorityColumn
        // 
        resources.ApplyResources(priorityColumn, "priorityColumn");
        // 
        // statusStrip1
        // 
        resources.ApplyResources(statusStrip1, "statusStrip1");
        statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { countStatusLabel, toolStripDropDownButton1 });
        statusStrip1.Name = "statusStrip1";
        // 
        // countStatusLabel
        // 
        resources.ApplyResources(countStatusLabel, "countStatusLabel");
        countStatusLabel.Name = "countStatusLabel";
        // 
        // toolStripDropDownButton1
        // 
        resources.ApplyResources(toolStripDropDownButton1, "toolStripDropDownButton1");
        toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
        toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { refreshToolStripMenuItem });
        toolStripDropDownButton1.Name = "toolStripDropDownButton1";
        // 
        // refreshToolStripMenuItem
        // 
        resources.ApplyResources(refreshToolStripMenuItem, "refreshToolStripMenuItem");
        refreshToolStripMenuItem.Image = VisualStudioImageLibrary.Refresh;
        refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
        refreshToolStripMenuItem.Click += OnRefreshToolStripMenuItemClick;
        // 
        // TaskItemsForm
        // 
        resources.ApplyResources(this, "$this");
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        Controls.Add(_listView);
        Controls.Add(statusStrip1);
        Name = "TaskItemsForm";
        statusStrip1.ResumeLayout(false);
        statusStrip1.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion


    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (components != null)
            {
                components.Dispose();
            }
        }

        base.Dispose(disposing);
    }
    private System.Windows.Forms.ListViewEx _listView;
    private System.Windows.Forms.ColumnHeader descriptionColumn;
    private System.Windows.Forms.ColumnHeader priorityColumn;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripStatusLabel countStatusLabel;
    private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
    private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
}
