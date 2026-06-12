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
            cashFlowColumn = new ColumnHeader();
            taxType = new ColumnHeader();
            balanceColumn = new ColumnHeader();
            _contextMenu = new ContextMenuStrip(components);
            openToolStripMenuItem1 = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            newAccountToolStripMenuItem = new ToolStripMenuItem();
            editAccountToolStripMenuItem = new ToolStripMenuItem();
            renameAccountToolStripMenuItem = new ToolStripMenuItem();
            removeAccountToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            transactionToolStripMenuItem1 = new ToolStripMenuItem();
            reconcileToolStripMenuItem1 = new ToolStripMenuItem();
            transactionsToolStripMenuItem1 = new ToolStripMenuItem();
            toolStripSeparator5 = new ToolStripSeparator();
            quickReportToolStripMenuItem = new ToolStripMenuItem();
            _imageList = new ImageList(components);
            _statusStrip = new StatusStrip();
            toolStripDropDownButton1 = new ToolStripDropDownButton();
            newToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            renameToolStripMenuItem = new ToolStripMenuItem();
            removeToolStripMenuItem = new ToolStripMenuItem();
            toolStripDropDownButton2 = new ToolStripDropDownButton();
            openToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            transactionToolStripMenuItem = new ToolStripMenuItem();
            reconcileToolStripMenuItem = new ToolStripMenuItem();
            transactionsToolStripMenuItem = new ToolStripMenuItem();
            quickReportToolStripSeparator = new ToolStripSeparator();
            quickReportToolStripMenuItem1 = new ToolStripMenuItem();
            toolStripDropDownButton3 = new ToolStripDropDownButton();
            refreshToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            inactiveToolStripMenuItem = new ToolStripMenuItem();
            _contextMenu.SuspendLayout();
            _statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // _listView
            // 
            _listView.AllowColumnReorder = true;
            _listView.Columns.AddRange(new ColumnHeader[] { nameColumn, numberColumn, typeColumn, cashFlowColumn, taxType, balanceColumn });
            _listView.ContextMenuStrip = _contextMenu;
            resources.ApplyResources(_listView, "_listView");
            _listView.FullRowSelect = true;
            _listView.LabelEdit = true;
            _listView.MultiSelect = false;
            _listView.Name = "_listView";
            _listView.SmallImageList = _imageList;
            _listView.SortColumn = 0;
            _listView.Sorting = SortOrder.Ascending;
            _listView.UseCompatibleStateImageBehavior = false;
            _listView.View = View.Details;
            _listView.AfterLabelEdit += OnListViewAfterLabelEdit;
            _listView.ItemActivate += OnListViewItemActivate;
            _listView.SelectedIndexChanged += OnListViewSelectedIndexChanged;
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
            // cashFlowColumn
            // 
            resources.ApplyResources(cashFlowColumn, "cashFlowColumn");
            // 
            // taxType
            // 
            resources.ApplyResources(taxType, "taxType");
            // 
            // balanceColumn
            // 
            resources.ApplyResources(balanceColumn, "balanceColumn");
            // 
            // _contextMenu
            // 
            _contextMenu.Items.AddRange(new ToolStripItem[] { openToolStripMenuItem1, toolStripSeparator3, newAccountToolStripMenuItem, editAccountToolStripMenuItem, renameAccountToolStripMenuItem, removeAccountToolStripMenuItem, toolStripSeparator2, transactionToolStripMenuItem1, reconcileToolStripMenuItem1, transactionsToolStripMenuItem1, toolStripSeparator5, quickReportToolStripMenuItem });
            _contextMenu.Name = "contextMenuStrip1";
            resources.ApplyResources(_contextMenu, "_contextMenu");
            // 
            // openToolStripMenuItem1
            // 
            openToolStripMenuItem1.Image = VisualStudioImageLibrary.Open;
            openToolStripMenuItem1.Name = "openToolStripMenuItem1";
            resources.ApplyResources(openToolStripMenuItem1, "openToolStripMenuItem1");
            openToolStripMenuItem1.Click += OnListViewItemActivate;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(toolStripSeparator3, "toolStripSeparator3");
            // 
            // newAccountToolStripMenuItem
            // 
            newAccountToolStripMenuItem.Image = VisualStudioImageLibrary.AddTable;
            newAccountToolStripMenuItem.Name = "newAccountToolStripMenuItem";
            resources.ApplyResources(newAccountToolStripMenuItem, "newAccountToolStripMenuItem");
            newAccountToolStripMenuItem.Click += OnNewToolStripMenuItemClick;
            // 
            // editAccountToolStripMenuItem
            // 
            editAccountToolStripMenuItem.Image = VisualStudioImageLibrary.Edit;
            editAccountToolStripMenuItem.Name = "editAccountToolStripMenuItem";
            resources.ApplyResources(editAccountToolStripMenuItem, "editAccountToolStripMenuItem");
            editAccountToolStripMenuItem.Click += OnEditToolStripMenuItemClick;
            // 
            // renameAccountToolStripMenuItem
            // 
            renameAccountToolStripMenuItem.Image = VisualStudioImageLibrary.Rename;
            renameAccountToolStripMenuItem.Name = "renameAccountToolStripMenuItem";
            resources.ApplyResources(renameAccountToolStripMenuItem, "renameAccountToolStripMenuItem");
            renameAccountToolStripMenuItem.Click += OnRenameToolStripMenuItemClick;
            // 
            // removeAccountToolStripMenuItem
            // 
            removeAccountToolStripMenuItem.Image = VisualStudioImageLibrary.DeleteTable;
            removeAccountToolStripMenuItem.Name = "removeAccountToolStripMenuItem";
            resources.ApplyResources(removeAccountToolStripMenuItem, "removeAccountToolStripMenuItem");
            removeAccountToolStripMenuItem.Click += OnRemoveToolStripMenuItemClick;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(toolStripSeparator2, "toolStripSeparator2");
            // 
            // transactionToolStripMenuItem1
            // 
            transactionToolStripMenuItem1.Image = VisualStudioImageLibrary.JournalMessage;
            transactionToolStripMenuItem1.Name = "transactionToolStripMenuItem1";
            resources.ApplyResources(transactionToolStripMenuItem1, "transactionToolStripMenuItem1");
            transactionToolStripMenuItem1.Click += OnTransactionToolStripMenuItemClick;
            // 
            // reconcileToolStripMenuItem1
            // 
            reconcileToolStripMenuItem1.Name = "reconcileToolStripMenuItem1";
            resources.ApplyResources(reconcileToolStripMenuItem1, "reconcileToolStripMenuItem1");
            // 
            // transactionsToolStripMenuItem1
            // 
            transactionsToolStripMenuItem1.Image = VisualStudioImageLibrary.Log;
            transactionsToolStripMenuItem1.Name = "transactionsToolStripMenuItem1";
            resources.ApplyResources(transactionsToolStripMenuItem1, "transactionsToolStripMenuItem1");
            transactionsToolStripMenuItem1.Click += OnTransactionsToolStripMenuItemClick;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(toolStripSeparator5, "toolStripSeparator5");
            // 
            // quickReportToolStripMenuItem
            // 
            quickReportToolStripMenuItem.Image = VisualStudioImageLibrary.Report;
            quickReportToolStripMenuItem.Name = "quickReportToolStripMenuItem";
            resources.ApplyResources(quickReportToolStripMenuItem, "quickReportToolStripMenuItem");
            quickReportToolStripMenuItem.Click += OnQuickReportToolStripMenuItemClick;
            // 
            // _imageList
            // 
            _imageList.ColorDepth = ColorDepth.Depth32Bit;
            resources.ApplyResources(_imageList, "_imageList");
            _imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // _statusStrip
            // 
            _statusStrip.Items.AddRange(new ToolStripItem[] { toolStripDropDownButton1, toolStripDropDownButton2, toolStripDropDownButton3 });
            resources.ApplyResources(_statusStrip, "_statusStrip");
            _statusStrip.Name = "_statusStrip";
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
            newToolStripMenuItem.Image = VisualStudioImageLibrary.AddTable;
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            resources.ApplyResources(newToolStripMenuItem, "newToolStripMenuItem");
            newToolStripMenuItem.Click += OnNewToolStripMenuItemClick;
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
            // removeToolStripMenuItem
            // 
            removeToolStripMenuItem.Image = VisualStudioImageLibrary.DeleteTable;
            removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            resources.ApplyResources(removeToolStripMenuItem, "removeToolStripMenuItem");
            removeToolStripMenuItem.Click += OnRemoveToolStripMenuItemClick;
            // 
            // toolStripDropDownButton2
            // 
            toolStripDropDownButton2.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButton2.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, toolStripSeparator1, transactionToolStripMenuItem, reconcileToolStripMenuItem, transactionsToolStripMenuItem, quickReportToolStripSeparator, quickReportToolStripMenuItem1 });
            resources.ApplyResources(toolStripDropDownButton2, "toolStripDropDownButton2");
            toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Image = VisualStudioImageLibrary.Open;
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            resources.ApplyResources(openToolStripMenuItem, "openToolStripMenuItem");
            openToolStripMenuItem.Click += OnListViewItemActivate;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(toolStripSeparator1, "toolStripSeparator1");
            // 
            // transactionToolStripMenuItem
            // 
            transactionToolStripMenuItem.Image = VisualStudioImageLibrary.JournalMessage;
            transactionToolStripMenuItem.Name = "transactionToolStripMenuItem";
            resources.ApplyResources(transactionToolStripMenuItem, "transactionToolStripMenuItem");
            transactionToolStripMenuItem.Click += OnTransactionToolStripMenuItemClick;
            // 
            // reconcileToolStripMenuItem
            // 
            reconcileToolStripMenuItem.Name = "reconcileToolStripMenuItem";
            resources.ApplyResources(reconcileToolStripMenuItem, "reconcileToolStripMenuItem");
            // 
            // transactionsToolStripMenuItem
            // 
            transactionsToolStripMenuItem.Image = VisualStudioImageLibrary.Log;
            transactionsToolStripMenuItem.Name = "transactionsToolStripMenuItem";
            resources.ApplyResources(transactionsToolStripMenuItem, "transactionsToolStripMenuItem");
            transactionsToolStripMenuItem.Click += OnTransactionsToolStripMenuItemClick;
            // 
            // quickReportToolStripSeparator
            // 
            quickReportToolStripSeparator.Name = "quickReportToolStripSeparator";
            resources.ApplyResources(quickReportToolStripSeparator, "quickReportToolStripSeparator");
            // 
            // quickReportToolStripMenuItem1
            // 
            quickReportToolStripMenuItem1.Image = VisualStudioImageLibrary.Report;
            quickReportToolStripMenuItem1.Name = "quickReportToolStripMenuItem1";
            resources.ApplyResources(quickReportToolStripMenuItem1, "quickReportToolStripMenuItem1");
            quickReportToolStripMenuItem1.Click += OnQuickReportToolStripMenuItemClick;
            // 
            // toolStripDropDownButton3
            // 
            toolStripDropDownButton3.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripDropDownButton3.DropDownItems.AddRange(new ToolStripItem[] { refreshToolStripMenuItem, toolStripSeparator4, inactiveToolStripMenuItem });
            resources.ApplyResources(toolStripDropDownButton3, "toolStripDropDownButton3");
            toolStripDropDownButton3.Name = "toolStripDropDownButton3";
            // 
            // refreshToolStripMenuItem
            // 
            refreshToolStripMenuItem.Image = VisualStudioImageLibrary.Refresh;
            refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            resources.ApplyResources(refreshToolStripMenuItem, "refreshToolStripMenuItem");
            refreshToolStripMenuItem.Click += OnRefreshToolStripMenuItemClick;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(toolStripSeparator4, "toolStripSeparator4");
            // 
            // inactiveToolStripMenuItem
            // 
            inactiveToolStripMenuItem.CheckOnClick = true;
            inactiveToolStripMenuItem.Name = "inactiveToolStripMenuItem";
            resources.ApplyResources(inactiveToolStripMenuItem, "inactiveToolStripMenuItem");
            inactiveToolStripMenuItem.CheckedChanged += OnInactiveToolStripMenuItemCheckedChanged;
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
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem openToolStripMenuItem1;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem inactiveToolStripMenuItem;
        private ColumnHeader cashFlowColumn;
        private ColumnHeader taxType;
        private ImageList _imageList;
        private ToolStripSeparator quickReportToolStripSeparator;
        private ToolStripMenuItem quickReportToolStripMenuItem1;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem quickReportToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
    }
}
