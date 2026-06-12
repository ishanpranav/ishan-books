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
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransactionsForm));
        DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
        _dataGridView = new TransactionGridView();
        postedColumn = new CalendarColumn();
        numberTypeColumn = new DataGridViewTextBoxColumn();
        nameMemoColumn = new DataGridViewTextBoxColumn();
        accountColumn = new DataGridViewComboBoxColumn();
        debitColumn = new DataGridViewTextBoxColumn();
        creditColumn = new DataGridViewTextBoxColumn();
        balanceColumn = new DataGridViewTextBoxColumn();
        _toolStrip = new ToolStrip();
        newToolStripButton = new ToolStripButton();
        saveToolStripButton = new ToolStripButton();
        printToolStripButton = new ToolStripButton();
        toolStripSeparator = new ToolStripSeparator();
        copyToolStripButton = new ToolStripButton();
        _contextMenuStrip = new ContextMenuStrip(components);
        openToolStripMenuItem = new ToolStripMenuItem();
        ((System.ComponentModel.ISupportInitialize)_dataGridView).BeginInit();
        _toolStrip.SuspendLayout();
        _contextMenuStrip.SuspendLayout();
        SuspendLayout();
        // 
        // _dataGridView
        // 
        _dataGridView.AllowUserToAddRows = false;
        _dataGridView.AllowUserToDeleteRows = false;
        _dataGridView.AllowUserToResizeRows = false;
        dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(33, 37, 41);
        dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(224, 220, 228);
        dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
        _dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
        _dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        _dataGridView.BackgroundColor = System.Drawing.Color.FromArgb(248, 249, 250);
        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
        dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
        dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
        _dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
        _dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        _dataGridView.Columns.AddRange(new DataGridViewColumn[] { postedColumn, numberTypeColumn, nameMemoColumn, accountColumn, debitColumn, creditColumn, balanceColumn });
        _dataGridView.ContextMenuStrip = _contextMenuStrip;
        dataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
        dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI", 9F);
        dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
        dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(224, 220, 228);
        dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black;
        dataGridViewCellStyle10.WrapMode = DataGridViewTriState.False;
        _dataGridView.DefaultCellStyle = dataGridViewCellStyle10;
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
        _dataGridView.Leave += OnDataGridViewLeave;
        // 
        // postedColumn
        // 
        postedColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
        postedColumn.DefaultCellStyle = dataGridViewCellStyle3;
        resources.ApplyResources(postedColumn, "postedColumn");
        postedColumn.Name = "postedColumn";
        postedColumn.Resizable = DataGridViewTriState.True;
        // 
        // numberTypeColumn
        // 
        numberTypeColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
        dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
        numberTypeColumn.DefaultCellStyle = dataGridViewCellStyle4;
        resources.ApplyResources(numberTypeColumn, "numberTypeColumn");
        numberTypeColumn.Name = "numberTypeColumn";
        numberTypeColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // nameMemoColumn
        // 
        nameMemoColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
        nameMemoColumn.DefaultCellStyle = dataGridViewCellStyle5;
        resources.ApplyResources(nameMemoColumn, "nameMemoColumn");
        nameMemoColumn.Name = "nameMemoColumn";
        nameMemoColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // accountColumn
        // 
        accountColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
        accountColumn.DefaultCellStyle = dataGridViewCellStyle6;
        accountColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
        resources.ApplyResources(accountColumn, "accountColumn");
        accountColumn.Name = "accountColumn";
        // 
        // debitColumn
        // 
        debitColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleRight;
        debitColumn.DefaultCellStyle = dataGridViewCellStyle7;
        resources.ApplyResources(debitColumn, "debitColumn");
        debitColumn.Name = "debitColumn";
        debitColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // creditColumn
        // 
        creditColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleRight;
        creditColumn.DefaultCellStyle = dataGridViewCellStyle8;
        resources.ApplyResources(creditColumn, "creditColumn");
        creditColumn.Name = "creditColumn";
        creditColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // balanceColumn
        // 
        balanceColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleRight;
        balanceColumn.DefaultCellStyle = dataGridViewCellStyle9;
        resources.ApplyResources(balanceColumn, "balanceColumn");
        balanceColumn.Name = "balanceColumn";
        balanceColumn.ReadOnly = true;
        balanceColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
        // 
        // _toolStrip
        // 
        _toolStrip.Items.AddRange(new ToolStripItem[] { newToolStripButton, saveToolStripButton, printToolStripButton, toolStripSeparator, copyToolStripButton });
        resources.ApplyResources(_toolStrip, "_toolStrip");
        _toolStrip.Name = "_toolStrip";
        // 
        // newToolStripButton
        // 
        newToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
        resources.ApplyResources(newToolStripButton, "newToolStripButton");
        newToolStripButton.Name = "newToolStripButton";
        // 
        // saveToolStripButton
        // 
        saveToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
        resources.ApplyResources(saveToolStripButton, "saveToolStripButton");
        saveToolStripButton.Name = "saveToolStripButton";
        saveToolStripButton.Click += OnSaveToolStripButtonClick;
        // 
        // printToolStripButton
        // 
        printToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
        resources.ApplyResources(printToolStripButton, "printToolStripButton");
        printToolStripButton.Name = "printToolStripButton";
        // 
        // toolStripSeparator
        // 
        toolStripSeparator.Name = "toolStripSeparator";
        resources.ApplyResources(toolStripSeparator, "toolStripSeparator");
        // 
        // copyToolStripButton
        // 
        copyToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
        resources.ApplyResources(copyToolStripButton, "copyToolStripButton");
        copyToolStripButton.Name = "copyToolStripButton";
        copyToolStripButton.Click += OnCopyToolStripButtonClick;
        // 
        // _contextMenuStrip
        // 
        _contextMenuStrip.Items.AddRange(new ToolStripItem[] { openToolStripMenuItem });
        _contextMenuStrip.Name = "_contextMenuStrip";
        resources.ApplyResources(_contextMenuStrip, "_contextMenuStrip");
        // 
        // openToolStripMenuItem
        // 
        openToolStripMenuItem.Name = "openToolStripMenuItem";
        resources.ApplyResources(openToolStripMenuItem, "openToolStripMenuItem");
        openToolStripMenuItem.Click += OnOpenToolStripMenuItem_Click;
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
        _toolStrip.ResumeLayout(false);
        _toolStrip.PerformLayout();
        _contextMenuStrip.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion


    private TransactionGridView _dataGridView;
    private System.Windows.Forms.ToolStrip _toolStrip;
    private System.Windows.Forms.ToolStripButton newToolStripButton;
    private System.Windows.Forms.ToolStripButton saveToolStripButton;
    private System.Windows.Forms.ToolStripButton printToolStripButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
    private System.Windows.Forms.ToolStripButton copyToolStripButton;
    private CalendarColumn postedColumn;
    private DataGridViewTextBoxColumn numberTypeColumn;
    private DataGridViewTextBoxColumn nameMemoColumn;
    private DataGridViewComboBoxColumn accountColumn;
    private DataGridViewTextBoxColumn debitColumn;
    private DataGridViewTextBoxColumn creditColumn;
    private DataGridViewTextBoxColumn balanceColumn;
    private ContextMenuStrip _contextMenuStrip;
    private ToolStripMenuItem openToolStripMenuItem;
}
