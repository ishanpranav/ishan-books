using System.Windows.Forms;

namespace Liber.Forms.Accounts
{
    partial class AccountsForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountsForm));
            _listView = new ListViewEx();
            nameColumn = new ColumnHeader();
            numberColumn = new ColumnHeader();
            typeColumn = new ColumnHeader();
            balanceColumn = new ColumnHeader();
            _contextMenuStrip = new ContextMenuStrip(components);
            newAccountToolStripMenuItem = new ToolStripMenuItem();
            editAccountToolStripMenuItem = new ToolStripMenuItem();
            renameAccountToolStripMenuItem = new ToolStripMenuItem();
            removeAccountToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            makeGeneralJournalEntriesToolStripMenuItem1 = new ToolStripMenuItem();
            reconcileToolStripMenuItem1 = new ToolStripMenuItem();
            useRegisterToolStripMenuItem1 = new ToolStripMenuItem();
            _statusStrip1 = new StatusStrip();
            toolStripDropDownButton1 = new ToolStripDropDownButton();
            newToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            renameToolStripMenuItem = new ToolStripMenuItem();
            removeToolStripMenuItem = new ToolStripMenuItem();
            toolStripDropDownButton2 = new ToolStripDropDownButton();
            transactionlToolStripMenuItem = new ToolStripMenuItem();
            reconcileToolStripMenuItem = new ToolStripMenuItem();
            useRegisterToolStripMenuItem = new ToolStripMenuItem();
            _contextMenuStrip.SuspendLayout();
            _statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // _listView
            // 
            _listView.AllowColumnReorder = true;
            _listView.Columns.AddRange(new ColumnHeader[] { nameColumn, numberColumn, typeColumn, balanceColumn });
            _listView.ContextMenuStrip = _contextMenuStrip;
            resources.ApplyResources(_listView, "_listView");
            _listView.FullRowSelect = true;
            _listView.LabelEdit = true;
            _listView.MultiSelect = false;
            _listView.Name = "_listView";
            _listView.SortColumn = 0;
            _listView.SortOrder = SortOrder.None;
            _listView.UseCompatibleStateImageBehavior = false;
            _listView.View = View.Details;
            _listView.AfterLabelEdit += OnListViewAfterLabelEdit;
            _listView.ItemActivate += OnTransactionToolStripMenuItemClick;
            // 
            // nameColumn
            // 
            resources.ApplyResources(nameColumn, "nameColumn");
            // 
            // numberColumn
            // 
            resources.ApplyResources(numberColumn, "numberColumn");
            // 
            // typeColumn
            // 
            resources.ApplyResources(typeColumn, "typeColumn");
            // 
            // balanceColumn
            // 
            resources.ApplyResources(balanceColumn, "balanceColumn");
            // 
            // _contextMenuStrip
            // 
            _contextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            _contextMenuStrip.Items.AddRange(new ToolStripItem[] { newAccountToolStripMenuItem, editAccountToolStripMenuItem, renameAccountToolStripMenuItem, removeAccountToolStripMenuItem, toolStripSeparator2, makeGeneralJournalEntriesToolStripMenuItem1, reconcileToolStripMenuItem1, useRegisterToolStripMenuItem1 });
            _contextMenuStrip.Name = "contextMenuStrip1";
            resources.ApplyResources(_contextMenuStrip, "_contextMenuStrip");
            // 
            // newAccountToolStripMenuItem
            // 
            newAccountToolStripMenuItem.Name = "newAccountToolStripMenuItem";
            resources.ApplyResources(newAccountToolStripMenuItem, "newAccountToolStripMenuItem");
            newAccountToolStripMenuItem.Click += OnNewToolStripMenuItemClick;
            // 
            // editAccountToolStripMenuItem
            // 
            editAccountToolStripMenuItem.Name = "editAccountToolStripMenuItem";
            resources.ApplyResources(editAccountToolStripMenuItem, "editAccountToolStripMenuItem");
            editAccountToolStripMenuItem.Click += OnEditToolStripMenuItemClick;
            // 
            // renameAccountToolStripMenuItem
            // 
            renameAccountToolStripMenuItem.Name = "renameAccountToolStripMenuItem";
            resources.ApplyResources(renameAccountToolStripMenuItem, "renameAccountToolStripMenuItem");
            renameAccountToolStripMenuItem.Click += OnRenameToolStripMenuItemClick;
            // 
            // removeAccountToolStripMenuItem
            // 
            removeAccountToolStripMenuItem.Name = "removeAccountToolStripMenuItem";
            resources.ApplyResources(removeAccountToolStripMenuItem, "removeAccountToolStripMenuItem");
            removeAccountToolStripMenuItem.Click += OnRemoveToolStripMenuItem;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(toolStripSeparator2, "toolStripSeparator2");
            // 
            // makeGeneralJournalEntriesToolStripMenuItem1
            // 
            makeGeneralJournalEntriesToolStripMenuItem1.Name = "makeGeneralJournalEntriesToolStripMenuItem1";
            resources.ApplyResources(makeGeneralJournalEntriesToolStripMenuItem1, "makeGeneralJournalEntriesToolStripMenuItem1");
            makeGeneralJournalEntriesToolStripMenuItem1.Click += OnTransactionToolStripMenuItemClick;
            // 
            // reconcileToolStripMenuItem1
            // 
            reconcileToolStripMenuItem1.Name = "reconcileToolStripMenuItem1";
            resources.ApplyResources(reconcileToolStripMenuItem1, "reconcileToolStripMenuItem1");
            // 
            // useRegisterToolStripMenuItem1
            // 
            useRegisterToolStripMenuItem1.Name = "useRegisterToolStripMenuItem1";
            resources.ApplyResources(useRegisterToolStripMenuItem1, "useRegisterToolStripMenuItem1");
            // 
            // _statusStrip1
            // 
            _statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            _statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripDropDownButton1, toolStripDropDownButton2 });
            resources.ApplyResources(_statusStrip1, "_statusStrip1");
            _statusStrip1.Name = "_statusStrip1";
            // 
            // toolStripDropDownButton1
            // 
            toolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, editToolStripMenuItem, renameToolStripMenuItem, removeToolStripMenuItem });
            resources.ApplyResources(toolStripDropDownButton1, "toolStripDropDownButton1");
            toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            resources.ApplyResources(newToolStripMenuItem, "newToolStripMenuItem");
            newToolStripMenuItem.Click += OnNewToolStripMenuItemClick;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            resources.ApplyResources(editToolStripMenuItem, "editToolStripMenuItem");
            editToolStripMenuItem.Click += OnEditToolStripMenuItemClick;
            // 
            // renameToolStripMenuItem
            // 
            renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            resources.ApplyResources(renameToolStripMenuItem, "renameToolStripMenuItem");
            renameToolStripMenuItem.Click += OnRenameToolStripMenuItemClick;
            // 
            // removeToolStripMenuItem
            // 
            removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            resources.ApplyResources(removeToolStripMenuItem, "removeToolStripMenuItem");
            removeToolStripMenuItem.Click += OnRemoveToolStripMenuItem;
            // 
            // toolStripDropDownButton2
            // 
            toolStripDropDownButton2.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButton2.DropDownItems.AddRange(new ToolStripItem[] { transactionlToolStripMenuItem, reconcileToolStripMenuItem, useRegisterToolStripMenuItem });
            resources.ApplyResources(toolStripDropDownButton2, "toolStripDropDownButton2");
            toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            // 
            // transactionlToolStripMenuItem
            // 
            transactionlToolStripMenuItem.Name = "transactionlToolStripMenuItem";
            resources.ApplyResources(transactionlToolStripMenuItem, "transactionlToolStripMenuItem");
            transactionlToolStripMenuItem.Click += OnTransactionToolStripMenuItemClick;
            // 
            // reconcileToolStripMenuItem
            // 
            reconcileToolStripMenuItem.Name = "reconcileToolStripMenuItem";
            resources.ApplyResources(reconcileToolStripMenuItem, "reconcileToolStripMenuItem");
            // 
            // useRegisterToolStripMenuItem
            // 
            useRegisterToolStripMenuItem.Name = "useRegisterToolStripMenuItem";
            resources.ApplyResources(useRegisterToolStripMenuItem, "useRegisterToolStripMenuItem");
            // 
            // AccountsForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(_listView);
            Controls.Add(_statusStrip1);
            Name = "AccountsForm";
            ShowIcon = false;
            WindowState = FormWindowState.Maximized;
            _contextMenuStrip.ResumeLayout(false);
            _statusStrip1.ResumeLayout(false);
            _statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListViewEx _listView;
        private System.Windows.Forms.StatusStrip _statusStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ContextMenuStrip _contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader numberColumn;
        private System.Windows.Forms.ColumnHeader nameColumn;
        private System.Windows.Forms.ColumnHeader typeColumn;
        private System.Windows.Forms.ColumnHeader balanceColumn;
        private System.Windows.Forms.ToolStripMenuItem editAccountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeAccountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameAccountToolStripMenuItem;
        private ToolStripDropDownButton toolStripDropDownButton2;
        private ToolStripMenuItem transactionlToolStripMenuItem;
        private ToolStripMenuItem reconcileToolStripMenuItem;
        private ToolStripMenuItem useRegisterToolStripMenuItem;
        private ToolStripMenuItem newAccountToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem makeGeneralJournalEntriesToolStripMenuItem1;
        private ToolStripMenuItem reconcileToolStripMenuItem1;
        private ToolStripMenuItem useRegisterToolStripMenuItem1;
    }
}