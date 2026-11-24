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
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
        securityDataGridView = new System.Windows.Forms.DataGridViewEx();
        securitySymbolColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        colorColumn = new System.Windows.Forms.DataGridViewColorColumn();
        saveButton = new System.Windows.Forms.Button();
        closeButton = new System.Windows.Forms.Button();
        basicRefreshButton = new System.Windows.Forms.Button();
        premiumRefreshButton = new System.Windows.Forms.Button();
        _tabControl = new System.Windows.Forms.TabControl();
        securityTabPage = new System.Windows.Forms.TabPage();
        tradeTabPage = new System.Windows.Forms.TabPage();
        tradeDataGridView = new System.Windows.Forms.DataGridViewEx();
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
        valueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        tradeSymbolColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        postedColumn = new System.Windows.Forms.CalendarColumn();
        typeColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
        costColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        quantityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        commissionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        ((System.ComponentModel.ISupportInitialize)securityDataGridView).BeginInit();
        _tabControl.SuspendLayout();
        securityTabPage.SuspendLayout();
        tradeTabPage.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)tradeDataGridView).BeginInit();
        priceTabPage.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)priceDataGridView).BeginInit();
        dividendTabPage.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dividendDataGridView).BeginInit();
        SuspendLayout();
        // 
        // securityDataGridView
        // 
        securityDataGridView.AllowUserToOrderColumns = true;
        dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(242, 242, 242);
        dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(224, 220, 228);
        dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
        securityDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
        securityDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(248, 249, 250);
        dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
        dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
        dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        securityDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
        securityDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        securityDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { securitySymbolColumn, nameColumn, colorColumn });
        dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
        dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
        dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
        dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(224, 220, 228);
        dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
        dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
        securityDataGridView.DefaultCellStyle = dataGridViewCellStyle3;
        securityDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
        securityDataGridView.GridColor = System.Drawing.Color.FromArgb(33, 37, 41);
        securityDataGridView.Location = new System.Drawing.Point(3, 3);
        securityDataGridView.Name = "securityDataGridView";
        securityDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        securityDataGridView.Size = new System.Drawing.Size(567, 288);
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
        // saveButton
        // 
        saveButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        saveButton.Location = new System.Drawing.Point(437, 340);
        saveButton.Name = "saveButton";
        saveButton.Size = new System.Drawing.Size(75, 23);
        saveButton.TabIndex = 1;
        saveButton.Text = "&Save";
        saveButton.UseVisualStyleBackColor = true;
        saveButton.Click += OnSaveButtonClick;
        // 
        // closeButton
        // 
        closeButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        closeButton.Location = new System.Drawing.Point(518, 340);
        closeButton.Name = "closeButton";
        closeButton.Size = new System.Drawing.Size(75, 23);
        closeButton.TabIndex = 2;
        closeButton.Text = "&Close";
        closeButton.UseVisualStyleBackColor = true;
        closeButton.Click += OnCloseButtonClick;
        // 
        // basicRefreshButton
        // 
        basicRefreshButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        basicRefreshButton.Location = new System.Drawing.Point(12, 340);
        basicRefreshButton.Name = "basicRefreshButton";
        basicRefreshButton.Size = new System.Drawing.Size(115, 23);
        basicRefreshButton.TabIndex = 3;
        basicRefreshButton.Text = "&Refresh (Basic)";
        basicRefreshButton.UseVisualStyleBackColor = true;
        basicRefreshButton.Click += OnBasicRefreshButtonClick;
        // 
        // premiumRefreshButton
        // 
        premiumRefreshButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        premiumRefreshButton.Location = new System.Drawing.Point(133, 340);
        premiumRefreshButton.Name = "premiumRefreshButton";
        premiumRefreshButton.Size = new System.Drawing.Size(115, 23);
        premiumRefreshButton.TabIndex = 4;
        premiumRefreshButton.Text = "&Refresh (Premium)";
        premiumRefreshButton.UseVisualStyleBackColor = true;
        premiumRefreshButton.Click += OnPremiumRefreshButtonClick;
        // 
        // _tabControl
        // 
        _tabControl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        _tabControl.Controls.Add(securityTabPage);
        _tabControl.Controls.Add(tradeTabPage);
        _tabControl.Controls.Add(priceTabPage);
        _tabControl.Controls.Add(dividendTabPage);
        _tabControl.Location = new System.Drawing.Point(12, 12);
        _tabControl.Name = "_tabControl";
        _tabControl.SelectedIndex = 0;
        _tabControl.Size = new System.Drawing.Size(581, 322);
        _tabControl.TabIndex = 5;
        // 
        // securityTabPage
        // 
        securityTabPage.Controls.Add(securityDataGridView);
        securityTabPage.Location = new System.Drawing.Point(4, 24);
        securityTabPage.Name = "securityTabPage";
        securityTabPage.Padding = new System.Windows.Forms.Padding(3);
        securityTabPage.Size = new System.Drawing.Size(573, 294);
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
        dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(242, 242, 242);
        dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(224, 220, 228);
        dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
        tradeDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
        tradeDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(248, 249, 250);
        dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
        dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
        dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        tradeDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
        tradeDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        tradeDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { tradeSymbolColumn, postedColumn, typeColumn, costColumn, quantityColumn, commissionColumn });
        dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
        dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 9F);
        dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
        dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(224, 220, 228);
        dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
        dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
        tradeDataGridView.DefaultCellStyle = dataGridViewCellStyle9;
        tradeDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
        tradeDataGridView.GridColor = System.Drawing.Color.FromArgb(33, 37, 41);
        tradeDataGridView.Location = new System.Drawing.Point(3, 3);
        tradeDataGridView.Name = "tradeDataGridView";
        tradeDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        tradeDataGridView.Size = new System.Drawing.Size(567, 288);
        tradeDataGridView.TabIndex = 1;
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
        dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(242, 242, 242);
        dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(224, 220, 228);
        dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black;
        priceDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
        priceDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(248, 249, 250);
        dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
        dataGridViewCellStyle11.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
        dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        priceDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
        priceDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        priceDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { priceSymbolColumn, priceTimestampColumn, closeColumn, adjustedCloseColumn });
        dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
        dataGridViewCellStyle12.Font = new System.Drawing.Font("Segoe UI", 9F);
        dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
        dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(224, 220, 228);
        dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black;
        dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
        priceDataGridView.DefaultCellStyle = dataGridViewCellStyle12;
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
        dataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(242, 242, 242);
        dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.FromArgb(224, 220, 228);
        dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.Black;
        dividendDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle13;
        dividendDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(248, 249, 250);
        dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control;
        dataGridViewCellStyle14.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
        dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        dividendDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
        dividendDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dividendDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { dividendSymbolColumn, dividendTimestampColumn, valueColumn });
        dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window;
        dataGridViewCellStyle15.Font = new System.Drawing.Font("Segoe UI", 9F);
        dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText;
        dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.FromArgb(224, 220, 228);
        dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.Black;
        dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
        dividendDataGridView.DefaultCellStyle = dataGridViewCellStyle15;
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
        dividendTimestampColumn.HeaderText = "Date";
        dividendTimestampColumn.Name = "dividendTimestampColumn";
        dividendTimestampColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        // 
        // valueColumn
        // 
        valueColumn.HeaderText = "Value";
        valueColumn.Name = "valueColumn";
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
        dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
        costColumn.DefaultCellStyle = dataGridViewCellStyle6;
        costColumn.HeaderText = "Cost";
        costColumn.Name = "costColumn";
        // 
        // quantityColumn
        // 
        dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
        quantityColumn.DefaultCellStyle = dataGridViewCellStyle7;
        quantityColumn.HeaderText = "Quantity";
        quantityColumn.Name = "quantityColumn";
        // 
        // commissionColumn
        // 
        dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
        commissionColumn.DefaultCellStyle = dataGridViewCellStyle8;
        commissionColumn.HeaderText = "Commission";
        commissionColumn.Name = "commissionColumn";
        // 
        // SecuritiesForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(605, 375);
        Controls.Add(_tabControl);
        Controls.Add(premiumRefreshButton);
        Controls.Add(basicRefreshButton);
        Controls.Add(closeButton);
        Controls.Add(saveButton);
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
        ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.DataGridViewEx securityDataGridView;
    private System.Windows.Forms.Button saveButton;
    private System.Windows.Forms.Button closeButton;
    private System.Windows.Forms.Button basicRefreshButton;
    private System.Windows.Forms.Button premiumRefreshButton;
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
    private System.Windows.Forms.DataGridViewTextBoxColumn dividendSymbolColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn priceSymbolColumn;
    private System.Windows.Forms.CalendarColumn priceTimestampColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn closeColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn adjustedCloseColumn;
    private System.Windows.Forms.CalendarColumn dividendTimestampColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn valueColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn tradeSymbolColumn;
    private System.Windows.Forms.CalendarColumn postedColumn;
    private System.Windows.Forms.DataGridViewComboBoxColumn typeColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn costColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn quantityColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn commissionColumn;
}
