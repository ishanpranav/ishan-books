// SecuritiesForm.Designer.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.Forms.Securities;

partial class SecuritiesForm
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
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SecuritiesForm));
        securityDataGridView = new System.Windows.Forms.DataGridViewEx();
        securitySymbolColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        colorColumn = new System.Windows.Forms.DataGridViewColorColumn();
        priceRefreshButton = new System.Windows.Forms.Button();
        _tabControl = new System.Windows.Forms.TabControl();
        securityTabPage = new System.Windows.Forms.TabPage();
        tradeTabPage = new System.Windows.Forms.TabPage();
        tradeDataGridView = new System.Windows.Forms.DataGridViewEx();
        tradeSymbolColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        postedColumn = new System.Windows.Forms.CalendarColumn();
        typeColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
        costColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        quantityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        commissionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        priceTabPage = new System.Windows.Forms.TabPage();
        priceDataGridView = new System.Windows.Forms.DataGridViewEx();
        priceSymbolColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        priceTimestampColumn = new System.Windows.Forms.CalendarColumn();
        closeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        adjustedCloseColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        dividendTabPage = new System.Windows.Forms.TabPage();
        dividendDataGridView = new System.Windows.Forms.DataGridViewEx();
        dividendSymbolColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        dividendTimestampColumn = new System.Windows.Forms.CalendarColumn();
        dividendPaidColumn = new System.Windows.Forms.CalendarColumn();
        valueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        dividendRefreshButton = new System.Windows.Forms.Button();
        statusStrip1 = new System.Windows.Forms.StatusStrip();
        toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
        newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
        saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
        printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
        addToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
        editToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        renameToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        removeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        duplicateToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
        toolStrip1 = new System.Windows.Forms.ToolStrip();
        newToolStripButton = new System.Windows.Forms.ToolStripButton();
        openToolStripButton = new System.Windows.Forms.ToolStripButton();
        saveToolStripButton = new System.Windows.Forms.ToolStripButton();
        saveAsToolStripButton = new System.Windows.Forms.ToolStripButton();
        toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
        printToolStripButton = new System.Windows.Forms.ToolStripButton();
        addToolStripButton = new System.Windows.Forms.ToolStripButton();
        toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
        toolStripButton2 = new System.Windows.Forms.ToolStripButton();
        toolStripButton3 = new System.Windows.Forms.ToolStripButton();
        toolStripButton4 = new System.Windows.Forms.ToolStripButton();
        toolStripButton5 = new System.Windows.Forms.ToolStripButton();
        toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
        toolStripButton6 = new System.Windows.Forms.ToolStripButton();
        toolStripButton7 = new System.Windows.Forms.ToolStripButton();
        refreshPricesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        refreshDividendsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        ((System.ComponentModel.ISupportInitialize)securityDataGridView).BeginInit();
        _tabControl.SuspendLayout();
        securityTabPage.SuspendLayout();
        tradeTabPage.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)tradeDataGridView).BeginInit();
        priceTabPage.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)priceDataGridView).BeginInit();
        dividendTabPage.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dividendDataGridView).BeginInit();
        statusStrip1.SuspendLayout();
        toolStrip1.SuspendLayout();
        SuspendLayout();
        // 
        // securityDataGridView
        // 
        securityDataGridView.AllowUserToOrderColumns = true;
        dataGridViewCellStyle16.BackColor = System.Drawing.Color.FromArgb(242, 242, 242);
        dataGridViewCellStyle16.SelectionBackColor = System.Drawing.Color.FromArgb(224, 220, 228);
        dataGridViewCellStyle16.SelectionForeColor = System.Drawing.Color.Black;
        securityDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle16;
        securityDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(248, 249, 250);
        dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.Control;
        dataGridViewCellStyle17.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.WindowText;
        dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        securityDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle17;
        securityDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        securityDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { securitySymbolColumn, nameColumn, colorColumn });
        dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Window;
        dataGridViewCellStyle18.Font = new System.Drawing.Font("Segoe UI", 9F);
        dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.ControlText;
        dataGridViewCellStyle18.SelectionBackColor = System.Drawing.Color.FromArgb(224, 220, 228);
        dataGridViewCellStyle18.SelectionForeColor = System.Drawing.Color.Black;
        dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
        securityDataGridView.DefaultCellStyle = dataGridViewCellStyle18;
        securityDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
        securityDataGridView.GridColor = System.Drawing.Color.FromArgb(33, 37, 41);
        securityDataGridView.Location = new System.Drawing.Point(3, 3);
        securityDataGridView.Name = "securityDataGridView";
        securityDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        securityDataGridView.Size = new System.Drawing.Size(567, 259);
        securityDataGridView.TabIndex = 0;
        securityDataGridView.DataError += OnDataGridViewDataError;
        // 
        // securitySymbolColumn
        // 
        securitySymbolColumn.HeaderText = "Symbol";
        securitySymbolColumn.Name = "securitySymbolColumn";
        // 
        // nameColumn
        // 
        nameColumn.HeaderText = "Name";
        nameColumn.Name = "nameColumn";
        // 
        // colorColumn
        // 
        colorColumn.HeaderText = "Color";
        colorColumn.Name = "colorColumn";
        colorColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        // 
        // priceRefreshButton
        // 
        priceRefreshButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        priceRefreshButton.Location = new System.Drawing.Point(16, 323);
        priceRefreshButton.Name = "priceRefreshButton";
        priceRefreshButton.Size = new System.Drawing.Size(122, 23);
        priceRefreshButton.TabIndex = 4;
        priceRefreshButton.Text = "&Refresh Prices";
        priceRefreshButton.UseVisualStyleBackColor = true;
        priceRefreshButton.Click += OnPriceRefreshButtonClick;
        // 
        // _tabControl
        // 
        _tabControl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        _tabControl.Controls.Add(securityTabPage);
        _tabControl.Controls.Add(tradeTabPage);
        _tabControl.Controls.Add(priceTabPage);
        _tabControl.Controls.Add(dividendTabPage);
        _tabControl.Location = new System.Drawing.Point(12, 28);
        _tabControl.Name = "_tabControl";
        _tabControl.SelectedIndex = 0;
        _tabControl.Size = new System.Drawing.Size(581, 293);
        _tabControl.TabIndex = 5;
        // 
        // securityTabPage
        // 
        securityTabPage.Controls.Add(securityDataGridView);
        securityTabPage.Location = new System.Drawing.Point(4, 24);
        securityTabPage.Name = "securityTabPage";
        securityTabPage.Padding = new System.Windows.Forms.Padding(3);
        securityTabPage.Size = new System.Drawing.Size(573, 265);
        securityTabPage.TabIndex = 0;
        securityTabPage.Text = "Securities";
        securityTabPage.UseVisualStyleBackColor = true;
        // 
        // tradeTabPage
        // 
        tradeTabPage.Controls.Add(tradeDataGridView);
        tradeTabPage.Location = new System.Drawing.Point(4, 24);
        tradeTabPage.Name = "tradeTabPage";
        tradeTabPage.Padding = new System.Windows.Forms.Padding(3);
        tradeTabPage.Size = new System.Drawing.Size(573, 294);
        tradeTabPage.TabIndex = 1;
        tradeTabPage.Text = "Trades";
        tradeTabPage.UseVisualStyleBackColor = true;
        // 
        // tradeDataGridView
        // 
        tradeDataGridView.AllowUserToOrderColumns = true;
        dataGridViewCellStyle19.BackColor = System.Drawing.Color.FromArgb(242, 242, 242);
        dataGridViewCellStyle19.SelectionBackColor = System.Drawing.Color.FromArgb(224, 220, 228);
        dataGridViewCellStyle19.SelectionForeColor = System.Drawing.Color.Black;
        tradeDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle19;
        tradeDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(248, 249, 250);
        dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.Control;
        dataGridViewCellStyle20.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.WindowText;
        dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        tradeDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle20;
        tradeDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        tradeDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { tradeSymbolColumn, postedColumn, typeColumn, costColumn, quantityColumn, commissionColumn });
        dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle24.BackColor = System.Drawing.SystemColors.Window;
        dataGridViewCellStyle24.Font = new System.Drawing.Font("Segoe UI", 9F);
        dataGridViewCellStyle24.ForeColor = System.Drawing.SystemColors.ControlText;
        dataGridViewCellStyle24.SelectionBackColor = System.Drawing.Color.FromArgb(224, 220, 228);
        dataGridViewCellStyle24.SelectionForeColor = System.Drawing.Color.Black;
        dataGridViewCellStyle24.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
        tradeDataGridView.DefaultCellStyle = dataGridViewCellStyle24;
        tradeDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
        tradeDataGridView.GridColor = System.Drawing.Color.FromArgb(33, 37, 41);
        tradeDataGridView.Location = new System.Drawing.Point(3, 3);
        tradeDataGridView.Name = "tradeDataGridView";
        tradeDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        tradeDataGridView.Size = new System.Drawing.Size(567, 288);
        tradeDataGridView.TabIndex = 1;
        // 
        // tradeSymbolColumn
        // 
        tradeSymbolColumn.HeaderText = "Symbol";
        tradeSymbolColumn.Name = "tradeSymbolColumn";
        // 
        // postedColumn
        // 
        postedColumn.HeaderText = "Date";
        postedColumn.Name = "postedColumn";
        postedColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        // 
        // typeColumn
        // 
        typeColumn.HeaderText = "Type";
        typeColumn.Name = "typeColumn";
        typeColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
        typeColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        // 
        // costColumn
        // 
        dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
        costColumn.DefaultCellStyle = dataGridViewCellStyle21;
        costColumn.HeaderText = "Cost";
        costColumn.Name = "costColumn";
        // 
        // quantityColumn
        // 
        dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
        quantityColumn.DefaultCellStyle = dataGridViewCellStyle22;
        quantityColumn.HeaderText = "Quantity";
        quantityColumn.Name = "quantityColumn";
        // 
        // commissionColumn
        // 
        dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
        commissionColumn.DefaultCellStyle = dataGridViewCellStyle23;
        commissionColumn.HeaderText = "Commission";
        commissionColumn.Name = "commissionColumn";
        // 
        // priceTabPage
        // 
        priceTabPage.Controls.Add(priceDataGridView);
        priceTabPage.Location = new System.Drawing.Point(4, 24);
        priceTabPage.Name = "priceTabPage";
        priceTabPage.Padding = new System.Windows.Forms.Padding(3);
        priceTabPage.Size = new System.Drawing.Size(573, 294);
        priceTabPage.TabIndex = 2;
        priceTabPage.Text = "Prices";
        priceTabPage.UseVisualStyleBackColor = true;
        // 
        // priceDataGridView
        // 
        priceDataGridView.AllowUserToOrderColumns = true;
        dataGridViewCellStyle25.BackColor = System.Drawing.Color.FromArgb(242, 242, 242);
        dataGridViewCellStyle25.SelectionBackColor = System.Drawing.Color.FromArgb(224, 220, 228);
        dataGridViewCellStyle25.SelectionForeColor = System.Drawing.Color.Black;
        priceDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle25;
        priceDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(248, 249, 250);
        dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle26.BackColor = System.Drawing.SystemColors.Control;
        dataGridViewCellStyle26.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        dataGridViewCellStyle26.ForeColor = System.Drawing.SystemColors.WindowText;
        dataGridViewCellStyle26.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle26.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle26.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        priceDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle26;
        priceDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        priceDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { priceSymbolColumn, priceTimestampColumn, closeColumn, adjustedCloseColumn });
        dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle27.BackColor = System.Drawing.SystemColors.Window;
        dataGridViewCellStyle27.Font = new System.Drawing.Font("Segoe UI", 9F);
        dataGridViewCellStyle27.ForeColor = System.Drawing.SystemColors.ControlText;
        dataGridViewCellStyle27.SelectionBackColor = System.Drawing.Color.FromArgb(224, 220, 228);
        dataGridViewCellStyle27.SelectionForeColor = System.Drawing.Color.Black;
        dataGridViewCellStyle27.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
        priceDataGridView.DefaultCellStyle = dataGridViewCellStyle27;
        priceDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
        priceDataGridView.GridColor = System.Drawing.Color.FromArgb(33, 37, 41);
        priceDataGridView.Location = new System.Drawing.Point(3, 3);
        priceDataGridView.Name = "priceDataGridView";
        priceDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        priceDataGridView.Size = new System.Drawing.Size(567, 288);
        priceDataGridView.TabIndex = 1;
        // 
        // priceSymbolColumn
        // 
        priceSymbolColumn.HeaderText = "Symbol";
        priceSymbolColumn.Name = "priceSymbolColumn";
        // 
        // priceTimestampColumn
        // 
        priceTimestampColumn.HeaderText = "Date";
        priceTimestampColumn.Name = "priceTimestampColumn";
        priceTimestampColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
        priceTimestampColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        // 
        // closeColumn
        // 
        closeColumn.HeaderText = "Close";
        closeColumn.Name = "closeColumn";
        // 
        // adjustedCloseColumn
        // 
        adjustedCloseColumn.HeaderText = "Adjusted Close";
        adjustedCloseColumn.Name = "adjustedCloseColumn";
        // 
        // dividendTabPage
        // 
        dividendTabPage.Controls.Add(dividendDataGridView);
        dividendTabPage.Location = new System.Drawing.Point(4, 24);
        dividendTabPage.Name = "dividendTabPage";
        dividendTabPage.Padding = new System.Windows.Forms.Padding(3);
        dividendTabPage.Size = new System.Drawing.Size(573, 294);
        dividendTabPage.TabIndex = 3;
        dividendTabPage.Text = "Dividends";
        dividendTabPage.UseVisualStyleBackColor = true;
        // 
        // dividendDataGridView
        // 
        dividendDataGridView.AllowUserToOrderColumns = true;
        dataGridViewCellStyle28.BackColor = System.Drawing.Color.FromArgb(242, 242, 242);
        dataGridViewCellStyle28.SelectionBackColor = System.Drawing.Color.FromArgb(224, 220, 228);
        dataGridViewCellStyle28.SelectionForeColor = System.Drawing.Color.Black;
        dividendDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle28;
        dividendDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(248, 249, 250);
        dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle29.BackColor = System.Drawing.SystemColors.Control;
        dataGridViewCellStyle29.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        dataGridViewCellStyle29.ForeColor = System.Drawing.SystemColors.WindowText;
        dataGridViewCellStyle29.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle29.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle29.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        dividendDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle29;
        dividendDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dividendDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { dividendSymbolColumn, dividendTimestampColumn, dividendPaidColumn, valueColumn });
        dataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle30.BackColor = System.Drawing.SystemColors.Window;
        dataGridViewCellStyle30.Font = new System.Drawing.Font("Segoe UI", 9F);
        dataGridViewCellStyle30.ForeColor = System.Drawing.SystemColors.ControlText;
        dataGridViewCellStyle30.SelectionBackColor = System.Drawing.Color.FromArgb(224, 220, 228);
        dataGridViewCellStyle30.SelectionForeColor = System.Drawing.Color.Black;
        dataGridViewCellStyle30.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
        dividendDataGridView.DefaultCellStyle = dataGridViewCellStyle30;
        dividendDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
        dividendDataGridView.GridColor = System.Drawing.Color.FromArgb(33, 37, 41);
        dividendDataGridView.Location = new System.Drawing.Point(3, 3);
        dividendDataGridView.Name = "dividendDataGridView";
        dividendDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        dividendDataGridView.Size = new System.Drawing.Size(567, 288);
        dividendDataGridView.TabIndex = 2;
        // 
        // dividendSymbolColumn
        // 
        dividendSymbolColumn.HeaderText = "Symbol";
        dividendSymbolColumn.Name = "dividendSymbolColumn";
        // 
        // dividendTimestampColumn
        // 
        dividendTimestampColumn.HeaderText = "Ex-Date";
        dividendTimestampColumn.Name = "dividendTimestampColumn";
        dividendTimestampColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        // 
        // dividendPaidColumn
        // 
        dividendPaidColumn.HeaderText = "Payment Date";
        dividendPaidColumn.Name = "dividendPaidColumn";
        dividendPaidColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        // 
        // valueColumn
        // 
        valueColumn.HeaderText = "Value";
        valueColumn.Name = "valueColumn";
        // 
        // dividendRefreshButton
        // 
        dividendRefreshButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        dividendRefreshButton.Location = new System.Drawing.Point(144, 323);
        dividendRefreshButton.Name = "dividendRefreshButton";
        dividendRefreshButton.Size = new System.Drawing.Size(122, 23);
        dividendRefreshButton.TabIndex = 6;
        dividendRefreshButton.Text = "&Refresh Dividends";
        dividendRefreshButton.UseVisualStyleBackColor = true;
        dividendRefreshButton.Click += OnDividendRefreshButtonClick;
        // 
        // statusStrip1
        // 
        statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripDropDownButton2, toolStripDropDownButton1 });
        statusStrip1.Location = new System.Drawing.Point(0, 353);
        statusStrip1.Name = "statusStrip1";
        statusStrip1.Size = new System.Drawing.Size(605, 22);
        statusStrip1.TabIndex = 7;
        statusStrip1.Text = "statusStrip1";
        // 
        // toolStripDropDownButton2
        // 
        toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
        toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { newToolStripMenuItem, openToolStripMenuItem, toolStripSeparator, saveToolStripMenuItem, saveAsToolStripMenuItem, toolStripSeparator9, printToolStripMenuItem });
        toolStripDropDownButton2.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButton2.Image");
        toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
        toolStripDropDownButton2.Name = "toolStripDropDownButton2";
        toolStripDropDownButton2.Size = new System.Drawing.Size(55, 20);
        toolStripDropDownButton2.Text = "&Report";
        // 
        // newToolStripMenuItem
        // 
        newToolStripMenuItem.Image = VisualStudioImageLibrary.AddDocument;
        newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
        newToolStripMenuItem.Name = "newToolStripMenuItem";
        newToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N;
        newToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
        newToolStripMenuItem.Text = "&New";
        // 
        // openToolStripMenuItem
        // 
        openToolStripMenuItem.Image = VisualStudioImageLibrary.OpenFile;
        openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
        openToolStripMenuItem.Name = "openToolStripMenuItem";
        openToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O;
        openToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
        openToolStripMenuItem.Text = "&Open";
        // 
        // toolStripSeparator
        // 
        toolStripSeparator.Name = "toolStripSeparator";
        toolStripSeparator.Size = new System.Drawing.Size(177, 6);
        // 
        // saveToolStripMenuItem
        // 
        saveToolStripMenuItem.Image = VisualStudioImageLibrary.Save;
        saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
        saveToolStripMenuItem.Name = "saveToolStripMenuItem";
        saveToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S;
        saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
        saveToolStripMenuItem.Text = "&Save";
        // 
        // saveAsToolStripMenuItem
        // 
        saveAsToolStripMenuItem.Image = VisualStudioImageLibrary.SaveAs;
        saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
        saveAsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
        saveAsToolStripMenuItem.Text = "Save &As";
        // 
        // toolStripSeparator9
        // 
        toolStripSeparator9.Name = "toolStripSeparator9";
        toolStripSeparator9.Size = new System.Drawing.Size(177, 6);
        // 
        // printToolStripMenuItem
        // 
        printToolStripMenuItem.Image = VisualStudioImageLibrary.Print;
        printToolStripMenuItem.Name = "printToolStripMenuItem";
        printToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P;
        printToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
        printToolStripMenuItem.Text = "&Print";
        // 
        // toolStripDropDownButton1
        // 
        toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
        toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { addToolStripMenuItem1, toolStripSeparator4, editToolStripMenuItem1, renameToolStripMenuItem1, removeToolStripMenuItem1, duplicateToolStripMenuItem1, toolStripSeparator5, refreshPricesToolStripMenuItem, refreshDividendsToolStripMenuItem });
        toolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButton1.Image");
        toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
        toolStripDropDownButton1.Name = "toolStripDropDownButton1";
        toolStripDropDownButton1.Size = new System.Drawing.Size(59, 20);
        toolStripDropDownButton1.Text = "&Refresh";
        // 
        // addToolStripMenuItem1
        // 
        addToolStripMenuItem1.Image = VisualStudioImageLibrary.New;
        addToolStripMenuItem1.Name = "addToolStripMenuItem1";
        addToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
        addToolStripMenuItem1.Text = "&Add";
        // 
        // toolStripSeparator4
        // 
        toolStripSeparator4.Name = "toolStripSeparator4";
        toolStripSeparator4.Size = new System.Drawing.Size(177, 6);
        // 
        // editToolStripMenuItem1
        // 
        editToolStripMenuItem1.Image = VisualStudioImageLibrary.Edit;
        editToolStripMenuItem1.Name = "editToolStripMenuItem1";
        editToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
        editToolStripMenuItem1.Text = "&Edit";
        // 
        // renameToolStripMenuItem1
        // 
        renameToolStripMenuItem1.Image = VisualStudioImageLibrary.Rename;
        renameToolStripMenuItem1.Name = "renameToolStripMenuItem1";
        renameToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F2;
        renameToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
        renameToolStripMenuItem1.Text = "Re&name";
        // 
        // removeToolStripMenuItem1
        // 
        removeToolStripMenuItem1.Image = VisualStudioImageLibrary.Remove;
        removeToolStripMenuItem1.Name = "removeToolStripMenuItem1";
        removeToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.Delete;
        removeToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
        removeToolStripMenuItem1.Text = "&Remove";
        // 
        // duplicateToolStripMenuItem1
        // 
        duplicateToolStripMenuItem1.Image = VisualStudioImageLibrary.Duplicate;
        duplicateToolStripMenuItem1.Name = "duplicateToolStripMenuItem1";
        duplicateToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
        duplicateToolStripMenuItem1.Text = "&Duplicate";
        // 
        // toolStripSeparator5
        // 
        toolStripSeparator5.Name = "toolStripSeparator5";
        toolStripSeparator5.Size = new System.Drawing.Size(177, 6);
        // 
        // toolStrip1
        // 
        toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { newToolStripButton, openToolStripButton, saveToolStripButton, saveAsToolStripButton, toolStripSeparator6, printToolStripButton, addToolStripButton, toolStripSeparator7, toolStripButton2, toolStripButton3, toolStripButton4, toolStripButton5, toolStripSeparator8, toolStripButton6, toolStripButton7 });
        toolStrip1.Location = new System.Drawing.Point(0, 0);
        toolStrip1.Name = "toolStrip1";
        toolStrip1.Size = new System.Drawing.Size(605, 25);
        toolStrip1.TabIndex = 8;
        toolStrip1.Text = "toolStrip1";
        // 
        // newToolStripButton
        // 
        newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        newToolStripButton.Image = VisualStudioImageLibrary.AddDocument;
        newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
        newToolStripButton.Name = "newToolStripButton";
        newToolStripButton.Size = new System.Drawing.Size(23, 22);
        newToolStripButton.Text = "&New";
        // 
        // openToolStripButton
        // 
        openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        openToolStripButton.Image = VisualStudioImageLibrary.OpenFile;
        openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
        openToolStripButton.Name = "openToolStripButton";
        openToolStripButton.Size = new System.Drawing.Size(23, 22);
        openToolStripButton.Text = "&Open";
        // 
        // saveToolStripButton
        // 
        saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        saveToolStripButton.Image = VisualStudioImageLibrary.Save;
        saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
        saveToolStripButton.Name = "saveToolStripButton";
        saveToolStripButton.Size = new System.Drawing.Size(23, 22);
        saveToolStripButton.Text = "&Save";
        // 
        // saveAsToolStripButton
        // 
        saveAsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        saveAsToolStripButton.Image = VisualStudioImageLibrary.SaveAs;
        saveAsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
        saveAsToolStripButton.Name = "saveAsToolStripButton";
        saveAsToolStripButton.Size = new System.Drawing.Size(23, 22);
        saveAsToolStripButton.Text = "Save As";
        // 
        // toolStripSeparator6
        // 
        toolStripSeparator6.Name = "toolStripSeparator6";
        toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
        // 
        // printToolStripButton
        // 
        printToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        printToolStripButton.Image = VisualStudioImageLibrary.Print;
        printToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
        printToolStripButton.Name = "printToolStripButton";
        printToolStripButton.Size = new System.Drawing.Size(23, 22);
        printToolStripButton.Text = "Print";
        // 
        // addToolStripButton
        // 
        addToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        addToolStripButton.Image = VisualStudioImageLibrary.New;
        addToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
        addToolStripButton.Name = "addToolStripButton";
        addToolStripButton.Size = new System.Drawing.Size(23, 22);
        addToolStripButton.Text = "Add";
        // 
        // toolStripSeparator7
        // 
        toolStripSeparator7.Name = "toolStripSeparator7";
        toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
        // 
        // toolStripButton2
        // 
        toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        toolStripButton2.Image = VisualStudioImageLibrary.Edit;
        toolStripButton2.Name = "toolStripButton2";
        toolStripButton2.Size = new System.Drawing.Size(23, 22);
        toolStripButton2.Text = "Edit";
        // 
        // toolStripButton3
        // 
        toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        toolStripButton3.Image = VisualStudioImageLibrary.Rename;
        toolStripButton3.Name = "toolStripButton3";
        toolStripButton3.Size = new System.Drawing.Size(23, 22);
        toolStripButton3.Text = "Rename";
        // 
        // toolStripButton4
        // 
        toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        toolStripButton4.Image = VisualStudioImageLibrary.Duplicate;
        toolStripButton4.Name = "toolStripButton4";
        toolStripButton4.Size = new System.Drawing.Size(23, 22);
        toolStripButton4.Text = "Duplicate";
        // 
        // toolStripButton5
        // 
        toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        toolStripButton5.Image = VisualStudioImageLibrary.Remove;
        toolStripButton5.Name = "toolStripButton5";
        toolStripButton5.Size = new System.Drawing.Size(23, 22);
        toolStripButton5.Text = "Remove";
        // 
        // toolStripSeparator8
        // 
        toolStripSeparator8.Name = "toolStripSeparator8";
        toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
        // 
        // toolStripButton6
        // 
        toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        toolStripButton6.Image = VisualStudioImageLibrary.MoveUp;
        toolStripButton6.Name = "toolStripButton6";
        toolStripButton6.Size = new System.Drawing.Size(23, 22);
        toolStripButton6.Text = "Move Up";
        // 
        // toolStripButton7
        // 
        toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        toolStripButton7.Image = VisualStudioImageLibrary.MoveDown;
        toolStripButton7.Name = "toolStripButton7";
        toolStripButton7.Size = new System.Drawing.Size(23, 22);
        toolStripButton7.Text = "Move Down";
        // 
        // refreshPricesToolStripMenuItem
        // 
        refreshPricesToolStripMenuItem.Name = "refreshPricesToolStripMenuItem";
        refreshPricesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
        refreshPricesToolStripMenuItem.Text = "&Prices";
        refreshPricesToolStripMenuItem.Click += OnPriceRefreshButtonClick;
        // 
        // refreshDividendsToolStripMenuItem
        // 
        refreshDividendsToolStripMenuItem.Name = "refreshDividendsToolStripMenuItem";
        refreshDividendsToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
        refreshDividendsToolStripMenuItem.Text = "&Dividends";
        refreshDividendsToolStripMenuItem.Click += OnDividendRefreshButtonClick;
        // 
        // SecuritiesForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(605, 375);
        Controls.Add(statusStrip1);
        Controls.Add(toolStrip1);
        Controls.Add(dividendRefreshButton);
        Controls.Add(_tabControl);
        Controls.Add(priceRefreshButton);
        Name = "SecuritiesForm";
        Text = "Portfolio";
        Load += OnLoad;
        ((System.ComponentModel.ISupportInitialize)securityDataGridView).EndInit();
        _tabControl.ResumeLayout(false);
        securityTabPage.ResumeLayout(false);
        tradeTabPage.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)tradeDataGridView).EndInit();
        priceTabPage.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)priceDataGridView).EndInit();
        dividendTabPage.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dividendDataGridView).EndInit();
        statusStrip1.ResumeLayout(false);
        statusStrip1.PerformLayout();
        toolStrip1.ResumeLayout(false);
        toolStrip1.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private System.Windows.Forms.DataGridViewEx securityDataGridView;
    private System.Windows.Forms.Button priceRefreshButton;
    private System.Windows.Forms.TabControl _tabControl;
    private System.Windows.Forms.TabPage securityTabPage;
    private System.Windows.Forms.TabPage tradeTabPage;
    private System.Windows.Forms.DataGridViewEx tradeDataGridView;
    private System.Windows.Forms.TabPage priceTabPage;
    private System.Windows.Forms.DataGridViewEx priceDataGridView;
    private System.Windows.Forms.TabPage dividendTabPage;
    private System.Windows.Forms.DataGridViewEx dividendDataGridView;
    private System.Windows.Forms.DataGridViewTextBoxColumn securitySymbolColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn nameColumn;
    private System.Windows.Forms.DataGridViewColorColumn colorColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn priceSymbolColumn;
    private System.Windows.Forms.CalendarColumn priceTimestampColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn closeColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn adjustedCloseColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn tradeSymbolColumn;
    private System.Windows.Forms.CalendarColumn postedColumn;
    private System.Windows.Forms.DataGridViewComboBoxColumn typeColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn costColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn quantityColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn commissionColumn;
    private System.Windows.Forms.Button dividendRefreshButton;
    private System.Windows.Forms.DataGridViewTextBoxColumn dividendSymbolColumn;
    private System.Windows.Forms.CalendarColumn dividendTimestampColumn;
    private System.Windows.Forms.CalendarColumn dividendPaidColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn valueColumn;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
    private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
    private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
    private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
    private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
    private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem duplicateToolStripMenuItem1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    private System.Windows.Forms.ToolStripMenuItem refreshPricesToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem refreshDividendsToolStripMenuItem;
    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripButton newToolStripButton;
    private System.Windows.Forms.ToolStripButton openToolStripButton;
    private System.Windows.Forms.ToolStripButton saveToolStripButton;
    private System.Windows.Forms.ToolStripButton saveAsToolStripButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
    private System.Windows.Forms.ToolStripButton printToolStripButton;
    private System.Windows.Forms.ToolStripButton addToolStripButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
    private System.Windows.Forms.ToolStripButton toolStripButton2;
    private System.Windows.Forms.ToolStripButton toolStripButton3;
    private System.Windows.Forms.ToolStripButton toolStripButton4;
    private System.Windows.Forms.ToolStripButton toolStripButton5;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
    private System.Windows.Forms.ToolStripButton toolStripButton6;
    private System.Windows.Forms.ToolStripButton toolStripButton7;
}
