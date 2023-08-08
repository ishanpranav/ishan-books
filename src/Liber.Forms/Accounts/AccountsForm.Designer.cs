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
            _contextMenu = new ContextMenuStrip(components);
            newAccountToolStripMenuItem = new ToolStripMenuItem();
            editAccountToolStripMenuItem = new ToolStripMenuItem();
            renameAccountToolStripMenuItem = new ToolStripMenuItem();
            removeAccountToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            transactionToolStripMenuItem1 = new ToolStripMenuItem();
            reconcileToolStripMenuItem1 = new ToolStripMenuItem();
            transactionsToolStripMenuItem1 = new ToolStripMenuItem();
            _statusStrip = new StatusStrip();
            toolStripDropDownButton1 = new ToolStripDropDownButton();
            newToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            renameToolStripMenuItem = new ToolStripMenuItem();
            removeToolStripMenuItem = new ToolStripMenuItem();
            toolStripDropDownButton2 = new ToolStripDropDownButton();
            transactionToolStripMenuItem = new ToolStripMenuItem();
            reconcileToolStripMenuItem = new ToolStripMenuItem();
            transactionsToolStripMenuItem = new ToolStripMenuItem();
            toolStripDropDownButton3 = new ToolStripDropDownButton();
            refreshToolStripMenuItem = new ToolStripMenuItem();
            _contextMenu.SuspendLayout();
            _statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // _listView
            // 
            resources.ApplyResources(_listView, "_listView");
            _listView.AllowColumnReorder = true;
            _listView.Columns.AddRange(new ColumnHeader[] { nameColumn, numberColumn, typeColumn, balanceColumn });
            _listView.ContextMenuStrip = _contextMenu;
            _listView.FullRowSelect = true;
            _listView.LabelEdit = true;
            _listView.MultiSelect = false;
            _listView.Name = "_listView";
            _listView.SortColumn = 0;
            _listView.SortOrder = SortOrder.None;
            _listView.UseCompatibleStateImageBehavior = false;
            _listView.View = View.Details;
            _listView.AfterLabelEdit += OnListViewAfterLabelEdit;
            _listView.ItemActivate += OnListViewItemActivate;
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
            // _contextMenu
            // 
            resources.ApplyResources(_contextMenu, "_contextMenu");
            _contextMenu.Items.AddRange(new ToolStripItem[] { newAccountToolStripMenuItem, editAccountToolStripMenuItem, renameAccountToolStripMenuItem, removeAccountToolStripMenuItem, toolStripSeparator2, transactionToolStripMenuItem1, reconcileToolStripMenuItem1, transactionsToolStripMenuItem1 });
            _contextMenu.Name = "contextMenuStrip1";
            // 
            // newAccountToolStripMenuItem
            // 
            resources.ApplyResources(newAccountToolStripMenuItem, "newAccountToolStripMenuItem");
            newAccountToolStripMenuItem.Name = "newAccountToolStripMenuItem";
            newAccountToolStripMenuItem.Click += OnNewToolStripMenuItemClick;
            // 
            // editAccountToolStripMenuItem
            // 
            resources.ApplyResources(editAccountToolStripMenuItem, "editAccountToolStripMenuItem");
            editAccountToolStripMenuItem.Name = "editAccountToolStripMenuItem";
            editAccountToolStripMenuItem.Click += OnEditToolStripMenuItemClick;
            // 
            // renameAccountToolStripMenuItem
            // 
            resources.ApplyResources(renameAccountToolStripMenuItem, "renameAccountToolStripMenuItem");
            renameAccountToolStripMenuItem.Name = "renameAccountToolStripMenuItem";
            renameAccountToolStripMenuItem.Click += OnRenameToolStripMenuItemClick;
            // 
            // removeAccountToolStripMenuItem
            // 
            resources.ApplyResources(removeAccountToolStripMenuItem, "removeAccountToolStripMenuItem");
            removeAccountToolStripMenuItem.Name = "removeAccountToolStripMenuItem";
            removeAccountToolStripMenuItem.Click += OnRemoveToolStripMenuItem;
            // 
            // toolStripSeparator2
            // 
            resources.ApplyResources(toolStripSeparator2, "toolStripSeparator2");
            toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // transactionToolStripMenuItem1
            // 
            resources.ApplyResources(transactionToolStripMenuItem1, "transactionToolStripMenuItem1");
            transactionToolStripMenuItem1.Name = "transactionToolStripMenuItem1";
            transactionToolStripMenuItem1.Click += OnTransactionToolStripMenuItemClick;
            // 
            // reconcileToolStripMenuItem1
            // 
            resources.ApplyResources(reconcileToolStripMenuItem1, "reconcileToolStripMenuItem1");
            reconcileToolStripMenuItem1.Name = "reconcileToolStripMenuItem1";
            // 
            // transactionsToolStripMenuItem1
            // 
            resources.ApplyResources(transactionsToolStripMenuItem1, "transactionsToolStripMenuItem1");
            transactionsToolStripMenuItem1.Name = "transactionsToolStripMenuItem1";
            transactionsToolStripMenuItem1.Click += OnTransactionsToolStripMenuItemClick;
            // 
            // _statusStrip
            // 
            resources.ApplyResources(_statusStrip, "_statusStrip");
            _statusStrip.Items.AddRange(new ToolStripItem[] { toolStripDropDownButton1, toolStripDropDownButton2, toolStripDropDownButton3 });
            _statusStrip.Name = "_statusStrip";
            // 
            // toolStripDropDownButton1
            // 
            resources.ApplyResources(toolStripDropDownButton1, "toolStripDropDownButton1");
            toolStripDropDownButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButton1.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, editToolStripMenuItem, renameToolStripMenuItem, removeToolStripMenuItem });
            toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            // 
            // newToolStripMenuItem
            // 
            resources.ApplyResources(newToolStripMenuItem, "newToolStripMenuItem");
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.Click += OnNewToolStripMenuItemClick;
            // 
            // editToolStripMenuItem
            // 
            resources.ApplyResources(editToolStripMenuItem, "editToolStripMenuItem");
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Click += OnEditToolStripMenuItemClick;
            // 
            // renameToolStripMenuItem
            // 
            resources.ApplyResources(renameToolStripMenuItem, "renameToolStripMenuItem");
            renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            renameToolStripMenuItem.Click += OnRenameToolStripMenuItemClick;
            // 
            // removeToolStripMenuItem
            // 
            resources.ApplyResources(removeToolStripMenuItem, "removeToolStripMenuItem");
            removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            removeToolStripMenuItem.Click += OnRemoveToolStripMenuItem;
            // 
            // toolStripDropDownButton2
            // 
            resources.ApplyResources(toolStripDropDownButton2, "toolStripDropDownButton2");
            toolStripDropDownButton2.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButton2.DropDownItems.AddRange(new ToolStripItem[] { transactionToolStripMenuItem, reconcileToolStripMenuItem, transactionsToolStripMenuItem });
            toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            // 
            // transactionToolStripMenuItem
            // 
            resources.ApplyResources(transactionToolStripMenuItem, "transactionToolStripMenuItem");
            transactionToolStripMenuItem.Name = "transactionToolStripMenuItem";
            transactionToolStripMenuItem.Click += OnTransactionToolStripMenuItemClick;
            // 
            // reconcileToolStripMenuItem
            // 
            resources.ApplyResources(reconcileToolStripMenuItem, "reconcileToolStripMenuItem");
            reconcileToolStripMenuItem.Name = "reconcileToolStripMenuItem";
            // 
            // transactionsToolStripMenuItem
            // 
            resources.ApplyResources(transactionsToolStripMenuItem, "transactionsToolStripMenuItem");
            transactionsToolStripMenuItem.Name = "transactionsToolStripMenuItem";
            transactionsToolStripMenuItem.Click += OnTransactionsToolStripMenuItemClick;
            // 
            // toolStripDropDownButton3
            // 
            resources.ApplyResources(toolStripDropDownButton3, "toolStripDropDownButton3");
            toolStripDropDownButton3.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButton3.DropDownItems.AddRange(new ToolStripItem[] { refreshToolStripMenuItem });
            toolStripDropDownButton3.Name = "toolStripDropDownButton3";
            // 
            // refreshToolStripMenuItem
            // 
            resources.ApplyResources(refreshToolStripMenuItem, "refreshToolStripMenuItem");
            refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            refreshToolStripMenuItem.Click += OnRefreshToolStripMenuItemClick;
            // 
            // AccountsForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(_listView);
            Controls.Add(_statusStrip);
            Name = "AccountsForm";
            ShowIcon = false;
            _contextMenu.ResumeLayout(false);
            _statusStrip.ResumeLayout(false);
            _statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListViewEx _listView;
        private StatusStrip _statusStrip;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ContextMenuStrip _contextMenu;
        private ToolStripMenuItem newToolStripMenuItem;
        private ColumnHeader numberColumn;
        private ColumnHeader nameColumn;
        private ColumnHeader typeColumn;
        private ColumnHeader balanceColumn;
        private ToolStripMenuItem editAccountToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem removeAccountToolStripMenuItem;
        private ToolStripMenuItem removeToolStripMenuItem;
        private ToolStripMenuItem renameToolStripMenuItem;
        private ToolStripMenuItem renameAccountToolStripMenuItem;
        private ToolStripDropDownButton toolStripDropDownButton2;
        private ToolStripMenuItem transactionToolStripMenuItem;
        private ToolStripMenuItem reconcileToolStripMenuItem;
        private ToolStripMenuItem transactionsToolStripMenuItem;
        private ToolStripMenuItem newAccountToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem transactionToolStripMenuItem1;
        private ToolStripMenuItem reconcileToolStripMenuItem1;
        private ToolStripMenuItem transactionsToolStripMenuItem1;
        private ToolStripDropDownButton toolStripDropDownButton3;
        private ToolStripMenuItem refreshToolStripMenuItem;
    }
}
