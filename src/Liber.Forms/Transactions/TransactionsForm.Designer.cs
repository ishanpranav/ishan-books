// TransactionsForm.Designer.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

using System.Windows.Forms;
using Liber.Forms.Controls;

namespace Liber.Forms.Transactions;

partial class TransactionsForm
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
        DataGridViewCellStyle dataGridViewCellStyle11 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle12 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle20 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle13 = new DataGridViewCellStyle();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransactionsForm));
        DataGridViewCellStyle dataGridViewCellStyle14 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle15 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle16 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle17 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle18 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle19 = new DataGridViewCellStyle();
        _dataGridView = new TransactionGridView();
        postedColumn = new CalendarColumn();
        numberTypeColumn = new DataGridViewTextBoxColumn();
        nameMemoColumn = new DataGridViewTextBoxColumn();
        accountColumn = new DataGridViewComboBoxColumn();
        reconciledColumn = new DataGridViewCheckBoxColumn();
        debitColumn = new DataGridViewTextBoxColumn();
        creditColumn = new DataGridViewTextBoxColumn();
        balanceColumn = new DataGridViewTextBoxColumn();
        _contextMenu = new ContextMenuStrip(components);
        openToolStripMenuItem = new ToolStripMenuItem();
        toolStripSeparator4 = new ToolStripSeparator();
        duplicateToolStripMenuItem = new ToolStripMenuItem();
        deleteToolStripMenuItem = new ToolStripMenuItem();
        toolStripSeparator5 = new ToolStripSeparator();
        goToSelectedToolStripMenuItem = new ToolStripMenuItem();
        goToReferenceToolStripMenuItem = new ToolStripMenuItem();
        toolStripSeparator6 = new ToolStripSeparator();
        saveToolStripMenuItem = new ToolStripMenuItem();
        revertToolStripMenuItem = new ToolStripMenuItem();
        _toolStrip = new ToolStrip();
        closeToolStripButton = new ToolStripButton();
        newToolStripButton = new ToolStripButton();
        toolStripSeparator = new ToolStripSeparator();
        saveToolStripButton = new ToolStripButton();
        saveCloseToolStripButton = new ToolStripButton();
        toolStripSeparator1 = new ToolStripSeparator();
        copyToolStripButton = new ToolStripButton();
        removeToolStripButton = new ToolStripButton();
        toolStripSeparator2 = new ToolStripSeparator();
        firstToolStripButton = new ToolStripButton();
        previousToolStripButton = new ToolStripButton();
        nextToolStripButton = new ToolStripButton();
        lastToolStripButton = new ToolStripButton();
        toolStripSeparator3 = new ToolStripSeparator();
        goToSelectedToolStripButton = new ToolStripButton();
        goToSiblingToolStripButton = new ToolStripButton();
        toolStripSeparator7 = new ToolStripSeparator();
        transactionToolStripButton = new ToolStripButton();
        ((System.ComponentModel.ISupportInitialize)_dataGridView).BeginInit();
        _contextMenu.SuspendLayout();
        _toolStrip.SuspendLayout();
        SuspendLayout();
        // 
        // _dataGridView
        // 
        _dataGridView.AllowUserToAddRows = false;
        _dataGridView.AllowUserToDeleteRows = false;
        _dataGridView.AllowUserToResizeRows = false;
        dataGridViewCellStyle11.ForeColor = System.Drawing.Color.FromArgb(33, 37, 41);
        dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(224, 220, 228);
        dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
        _dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle11;
        _dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        _dataGridView.BackgroundColor = System.Drawing.Color.FromArgb(248, 249, 250);
        _dataGridView.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
        dataGridViewCellStyle12.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
        dataGridViewCellStyle12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
        dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle12.WrapMode = DataGridViewTriState.True;
        _dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
        _dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        _dataGridView.Columns.AddRange(new DataGridViewColumn[] { postedColumn, numberTypeColumn, nameMemoColumn, accountColumn, reconciledColumn, debitColumn, creditColumn, balanceColumn });
        _dataGridView.ContextMenuStrip = _contextMenu;
        dataGridViewCellStyle20.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Window;
        dataGridViewCellStyle20.Font = new System.Drawing.Font("Segoe UI", 9F);
        dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.ControlText;
        dataGridViewCellStyle20.SelectionBackColor = System.Drawing.Color.FromArgb(224, 220, 228);
        dataGridViewCellStyle20.SelectionForeColor = System.Drawing.Color.Black;
        dataGridViewCellStyle20.WrapMode = DataGridViewTriState.False;
        _dataGridView.DefaultCellStyle = dataGridViewCellStyle20;
        resources.ApplyResources(_dataGridView, "_dataGridView");
        _dataGridView.GridColor = System.Drawing.Color.FromArgb(33, 37, 41);
        _dataGridView.MultiSelect = false;
        _dataGridView.Name = "_dataGridView";
        _dataGridView.RowTemplate.Height = 29;
        _dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        _dataGridView.CellClick += OnDataGridViewCellClick;
        _dataGridView.CellDoubleClick += OnDataGridViewCellDoubleClick;
        _dataGridView.CellEndEdit += OnDataGridViewCellEndEdit;
        _dataGridView.CellPainting += OnDataGridViewCellPainting;
        _dataGridView.CurrentCellChanged += OnDataGridViewCurrentCellChanged;
        _dataGridView.EditingControlShowing += OnDataGridViewEditingControlShowing;
        _dataGridView.SelectionChanged += OnDataGridViewSelectionChanged;
        _dataGridView.KeyDown += OnDataGridViewKeyDown;
        _dataGridView.Leave += OnDataGridViewLeave;
        // 
        // postedColumn
        // 
        postedColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        dataGridViewCellStyle13.Alignment = DataGridViewContentAlignment.MiddleLeft;
        postedColumn.DefaultCellStyle = dataGridViewCellStyle13;
        resources.ApplyResources(postedColumn, "postedColumn");
        postedColumn.Name = "postedColumn";
        postedColumn.Resizable = DataGridViewTriState.True;
        // 
        // numberTypeColumn
        // 
        numberTypeColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        dataGridViewCellStyle14.Alignment = DataGridViewContentAlignment.MiddleLeft;
        numberTypeColumn.DefaultCellStyle = dataGridViewCellStyle14;
        resources.ApplyResources(numberTypeColumn, "numberTypeColumn");
        numberTypeColumn.Name = "numberTypeColumn";
        numberTypeColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // nameMemoColumn
        // 
        nameMemoColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        dataGridViewCellStyle15.Alignment = DataGridViewContentAlignment.MiddleLeft;
        nameMemoColumn.DefaultCellStyle = dataGridViewCellStyle15;
        resources.ApplyResources(nameMemoColumn, "nameMemoColumn");
        nameMemoColumn.Name = "nameMemoColumn";
        nameMemoColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // accountColumn
        // 
        accountColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        dataGridViewCellStyle16.Alignment = DataGridViewContentAlignment.MiddleLeft;
        accountColumn.DefaultCellStyle = dataGridViewCellStyle16;
        accountColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
        resources.ApplyResources(accountColumn, "accountColumn");
        accountColumn.Name = "accountColumn";
        // 
        // reconciledColumn
        // 
        resources.ApplyResources(reconciledColumn, "reconciledColumn");
        reconciledColumn.Name = "reconciledColumn";
        reconciledColumn.ReadOnly = true;
        reconciledColumn.Resizable = DataGridViewTriState.False;
        // 
        // debitColumn
        // 
        debitColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        dataGridViewCellStyle17.Alignment = DataGridViewContentAlignment.MiddleRight;
        debitColumn.DefaultCellStyle = dataGridViewCellStyle17;
        resources.ApplyResources(debitColumn, "debitColumn");
        debitColumn.Name = "debitColumn";
        debitColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // creditColumn
        // 
        creditColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        dataGridViewCellStyle18.Alignment = DataGridViewContentAlignment.MiddleRight;
        creditColumn.DefaultCellStyle = dataGridViewCellStyle18;
        resources.ApplyResources(creditColumn, "creditColumn");
        creditColumn.Name = "creditColumn";
        creditColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // balanceColumn
        // 
        balanceColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        dataGridViewCellStyle19.Alignment = DataGridViewContentAlignment.MiddleRight;
        balanceColumn.DefaultCellStyle = dataGridViewCellStyle19;
        resources.ApplyResources(balanceColumn, "balanceColumn");
        balanceColumn.Name = "balanceColumn";
        balanceColumn.ReadOnly = true;
        balanceColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // _contextMenu
        // 
        _contextMenu.Items.AddRange(new ToolStripItem[] { openToolStripMenuItem, toolStripSeparator4, duplicateToolStripMenuItem, deleteToolStripMenuItem, toolStripSeparator5, goToSelectedToolStripMenuItem, goToReferenceToolStripMenuItem, toolStripSeparator6, saveToolStripMenuItem, revertToolStripMenuItem });
        _contextMenu.Name = "_contextMenuStrip";
        resources.ApplyResources(_contextMenu, "_contextMenu");
        // 
        // openToolStripMenuItem
        // 
        openToolStripMenuItem.Image = VisualStudioImageLibrary.Open;
        openToolStripMenuItem.Name = "openToolStripMenuItem";
        resources.ApplyResources(openToolStripMenuItem, "openToolStripMenuItem");
        openToolStripMenuItem.Click += OnTransactionToolStripButtonClick;
        // 
        // toolStripSeparator4
        // 
        toolStripSeparator4.Name = "toolStripSeparator4";
        resources.ApplyResources(toolStripSeparator4, "toolStripSeparator4");
        // 
        // duplicateToolStripMenuItem
        // 
        duplicateToolStripMenuItem.Image = VisualStudioImageLibrary.Duplicate;
        duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
        resources.ApplyResources(duplicateToolStripMenuItem, "duplicateToolStripMenuItem");
        // 
        // deleteToolStripMenuItem
        // 
        deleteToolStripMenuItem.Image = VisualStudioImageLibrary.Delete;
        deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
        resources.ApplyResources(deleteToolStripMenuItem, "deleteToolStripMenuItem");
        // 
        // toolStripSeparator5
        // 
        toolStripSeparator5.Name = "toolStripSeparator5";
        resources.ApplyResources(toolStripSeparator5, "toolStripSeparator5");
        // 
        // goToSelectedToolStripMenuItem
        // 
        goToSelectedToolStripMenuItem.Image = VisualStudioImageLibrary.GoToCurrentLine;
        goToSelectedToolStripMenuItem.Name = "goToSelectedToolStripMenuItem";
        resources.ApplyResources(goToSelectedToolStripMenuItem, "goToSelectedToolStripMenuItem");
        goToSelectedToolStripMenuItem.Click += OnGoToSelectedToolStripButtonClick;
        // 
        // goToReferenceToolStripMenuItem
        // 
        goToReferenceToolStripMenuItem.Image = VisualStudioImageLibrary.GoToReference;
        goToReferenceToolStripMenuItem.Name = "goToReferenceToolStripMenuItem";
        resources.ApplyResources(goToReferenceToolStripMenuItem, "goToReferenceToolStripMenuItem");
        goToReferenceToolStripMenuItem.Click += OnGoToSiblingToolStripButtonClick;
        // 
        // toolStripSeparator6
        // 
        toolStripSeparator6.Name = "toolStripSeparator6";
        resources.ApplyResources(toolStripSeparator6, "toolStripSeparator6");
        // 
        // saveToolStripMenuItem
        // 
        saveToolStripMenuItem.Image = VisualStudioImageLibrary.Save;
        saveToolStripMenuItem.Name = "saveToolStripMenuItem";
        resources.ApplyResources(saveToolStripMenuItem, "saveToolStripMenuItem");
        saveToolStripMenuItem.Click += OnSaveToolStripButtonClick;
        // 
        // revertToolStripMenuItem
        // 
        revertToolStripMenuItem.Name = "revertToolStripMenuItem";
        resources.ApplyResources(revertToolStripMenuItem, "revertToolStripMenuItem");
        revertToolStripMenuItem.Click += OnRevertToolStripMenuItem;
        // 
        // _toolStrip
        // 
        _toolStrip.Items.AddRange(new ToolStripItem[] { closeToolStripButton, newToolStripButton, toolStripSeparator, saveToolStripButton, saveCloseToolStripButton, toolStripSeparator1, copyToolStripButton, removeToolStripButton, toolStripSeparator2, firstToolStripButton, previousToolStripButton, nextToolStripButton, lastToolStripButton, toolStripSeparator3, goToSelectedToolStripButton, goToSiblingToolStripButton, toolStripSeparator7, transactionToolStripButton });
        resources.ApplyResources(_toolStrip, "_toolStrip");
        _toolStrip.Name = "_toolStrip";
        // 
        // closeToolStripButton
        // 
        closeToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
        closeToolStripButton.Image = VisualStudioImageLibrary.CloseLog;
        resources.ApplyResources(closeToolStripButton, "closeToolStripButton");
        closeToolStripButton.Name = "closeToolStripButton";
        closeToolStripButton.Click += OnCloseToolStripButtonClick;
        // 
        // newToolStripButton
        // 
        newToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
        newToolStripButton.Image = VisualStudioImageLibrary.NewLog;
        resources.ApplyResources(newToolStripButton, "newToolStripButton");
        newToolStripButton.Name = "newToolStripButton";
        newToolStripButton.Click += OnNewToolStripButtonClick;
        // 
        // toolStripSeparator
        // 
        toolStripSeparator.Name = "toolStripSeparator";
        resources.ApplyResources(toolStripSeparator, "toolStripSeparator");
        // 
        // saveToolStripButton
        // 
        saveToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
        saveToolStripButton.Image = VisualStudioImageLibrary.Save;
        resources.ApplyResources(saveToolStripButton, "saveToolStripButton");
        saveToolStripButton.Name = "saveToolStripButton";
        saveToolStripButton.Click += OnSaveToolStripButtonClick;
        // 
        // saveCloseToolStripButton
        // 
        saveCloseToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
        saveCloseToolStripButton.Image = VisualStudioImageLibrary.SaveAndClose;
        resources.ApplyResources(saveCloseToolStripButton, "saveCloseToolStripButton");
        saveCloseToolStripButton.Name = "saveCloseToolStripButton";
        saveCloseToolStripButton.Click += OnSaveCloseToolStripButtonClick;
        // 
        // toolStripSeparator1
        // 
        toolStripSeparator1.Name = "toolStripSeparator1";
        resources.ApplyResources(toolStripSeparator1, "toolStripSeparator1");
        // 
        // copyToolStripButton
        // 
        copyToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
        copyToolStripButton.Image = VisualStudioImageLibrary.Duplicate;
        resources.ApplyResources(copyToolStripButton, "copyToolStripButton");
        copyToolStripButton.Name = "copyToolStripButton";
        copyToolStripButton.Click += OnCopyToolStripButtonClick;
        // 
        // removeToolStripButton
        // 
        removeToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
        removeToolStripButton.Image = VisualStudioImageLibrary.Delete;
        resources.ApplyResources(removeToolStripButton, "removeToolStripButton");
        removeToolStripButton.Name = "removeToolStripButton";
        removeToolStripButton.Click += OnRemoveToolStripButtonClick;
        // 
        // toolStripSeparator2
        // 
        toolStripSeparator2.Name = "toolStripSeparator2";
        resources.ApplyResources(toolStripSeparator2, "toolStripSeparator2");
        // 
        // firstToolStripButton
        // 
        firstToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
        firstToolStripButton.Image = VisualStudioImageLibrary.GoToTop;
        resources.ApplyResources(firstToolStripButton, "firstToolStripButton");
        firstToolStripButton.Name = "firstToolStripButton";
        firstToolStripButton.Click += OnFirstToolStripButtonClick;
        // 
        // previousToolStripButton
        // 
        previousToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
        previousToolStripButton.Image = VisualStudioImageLibrary.GoToPrevious;
        resources.ApplyResources(previousToolStripButton, "previousToolStripButton");
        previousToolStripButton.Name = "previousToolStripButton";
        previousToolStripButton.Click += OnPreviousToolStripButtonClick;
        // 
        // nextToolStripButton
        // 
        nextToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
        nextToolStripButton.Image = VisualStudioImageLibrary.GoToNext;
        resources.ApplyResources(nextToolStripButton, "nextToolStripButton");
        nextToolStripButton.Name = "nextToolStripButton";
        nextToolStripButton.Click += OnNextToolStripButtonClick;
        // 
        // lastToolStripButton
        // 
        lastToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
        lastToolStripButton.Image = VisualStudioImageLibrary.GoToBottom;
        resources.ApplyResources(lastToolStripButton, "lastToolStripButton");
        lastToolStripButton.Name = "lastToolStripButton";
        lastToolStripButton.Click += OnLastToolStripButtonClick;
        // 
        // toolStripSeparator3
        // 
        toolStripSeparator3.Name = "toolStripSeparator3";
        resources.ApplyResources(toolStripSeparator3, "toolStripSeparator3");
        // 
        // goToSelectedToolStripButton
        // 
        goToSelectedToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
        goToSelectedToolStripButton.Image = VisualStudioImageLibrary.GoToCurrentLine;
        resources.ApplyResources(goToSelectedToolStripButton, "goToSelectedToolStripButton");
        goToSelectedToolStripButton.Name = "goToSelectedToolStripButton";
        goToSelectedToolStripButton.Click += OnGoToSelectedToolStripButtonClick;
        // 
        // goToSiblingToolStripButton
        // 
        goToSiblingToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
        goToSiblingToolStripButton.Image = VisualStudioImageLibrary.GoToReference;
        resources.ApplyResources(goToSiblingToolStripButton, "goToSiblingToolStripButton");
        goToSiblingToolStripButton.Name = "goToSiblingToolStripButton";
        goToSiblingToolStripButton.Click += OnGoToSiblingToolStripButtonClick;
        // 
        // toolStripSeparator7
        // 
        toolStripSeparator7.Name = "toolStripSeparator7";
        resources.ApplyResources(toolStripSeparator7, "toolStripSeparator7");
        // 
        // transactionToolStripButton
        // 
        transactionToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
        transactionToolStripButton.Image = VisualStudioImageLibrary.JournalMessage;
        resources.ApplyResources(transactionToolStripButton, "transactionToolStripButton");
        transactionToolStripButton.Name = "transactionToolStripButton";
        transactionToolStripButton.Click += OnTransactionToolStripButtonClick;
        // 
        // TransactionsForm
        // 
        resources.ApplyResources(this, "$this");
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(_dataGridView);
        Controls.Add(_toolStrip);
        Name = "TransactionsForm";
        WindowState = FormWindowState.Maximized;
        ((System.ComponentModel.ISupportInitialize)_dataGridView).EndInit();
        _contextMenu.ResumeLayout(false);
        _toolStrip.ResumeLayout(false);
        _toolStrip.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion


    private TransactionGridView _dataGridView;
    private System.Windows.Forms.ToolStrip _toolStrip;
    private System.Windows.Forms.ToolStripButton newToolStripButton;
    private System.Windows.Forms.ToolStripButton saveToolStripButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
    private System.Windows.Forms.ToolStripButton copyToolStripButton;
    private ContextMenuStrip _contextMenu;
    private ToolStripMenuItem openToolStripMenuItem;
    private ToolStripButton closeToolStripButton;
    private ToolStripButton saveCloseToolStripButton;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripButton removeToolStripButton;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripButton firstToolStripButton;
    private ToolStripButton previousToolStripButton;
    private ToolStripButton nextToolStripButton;
    private ToolStripButton lastToolStripButton;
    private ToolStripSeparator toolStripSeparator3;
    private ToolStripButton goToSelectedToolStripButton;
    private ToolStripButton goToSiblingToolStripButton;
    private ToolStripButton transactionToolStripButton;
    private ToolStripSeparator toolStripSeparator4;
    private ToolStripMenuItem duplicateToolStripMenuItem;
    private ToolStripMenuItem deleteToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator5;
    private ToolStripMenuItem goToReferenceToolStripMenuItem;
    private ToolStripMenuItem goToSelectedToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator7;
    private ToolStripSeparator toolStripSeparator6;
    private ToolStripMenuItem saveToolStripMenuItem;
    private ToolStripMenuItem revertToolStripMenuItem;
    private CalendarColumn postedColumn;
    private DataGridViewTextBoxColumn numberTypeColumn;
    private DataGridViewTextBoxColumn nameMemoColumn;
    private DataGridViewComboBoxColumn accountColumn;
    private DataGridViewCheckBoxColumn reconciledColumn;
    private DataGridViewTextBoxColumn debitColumn;
    private DataGridViewTextBoxColumn creditColumn;
    private DataGridViewTextBoxColumn balanceColumn;
}
