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
        _dataGridView = new System.Windows.Forms.DataGridViewEx();
        saveButton = new System.Windows.Forms.Button();
        closeButton = new System.Windows.Forms.Button();
        basicRefreshButton = new System.Windows.Forms.Button();
        premiumRefreshButton = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)_dataGridView).BeginInit();
        SuspendLayout();
        // 
        // _dataGridView
        // 
        _dataGridView.AllowUserToOrderColumns = true;
        dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(242, 242, 242);
        dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(224, 220, 228);
        dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
        _dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
        _dataGridView.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        _dataGridView.BackgroundColor = System.Drawing.Color.FromArgb(248, 249, 250);
        dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
        dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
        dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        _dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
        _dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
        dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
        dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
        dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(224, 220, 228);
        dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
        dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
        _dataGridView.DefaultCellStyle = dataGridViewCellStyle3;
        _dataGridView.GridColor = System.Drawing.Color.FromArgb(33, 37, 41);
        _dataGridView.Location = new System.Drawing.Point(12, 12);
        _dataGridView.Name = "_dataGridView";
        _dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        _dataGridView.Size = new System.Drawing.Size(581, 322);
        _dataGridView.TabIndex = 0;
        _dataGridView.DataError += OnDataGridViewDataError;
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
        // SecuritiesForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(605, 375);
        Controls.Add(premiumRefreshButton);
        Controls.Add(basicRefreshButton);
        Controls.Add(closeButton);
        Controls.Add(saveButton);
        Controls.Add(_dataGridView);
        Name = "SecuritiesForm";
        Text = "Securities";
        ((System.ComponentModel.ISupportInitialize)_dataGridView).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.DataGridViewEx _dataGridView;
    private System.Windows.Forms.Button saveButton;
    private System.Windows.Forms.Button closeButton;
    private System.Windows.Forms.Button basicRefreshButton;
    private System.Windows.Forms.Button premiumRefreshButton;
}
