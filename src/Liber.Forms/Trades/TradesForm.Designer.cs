// TradesForm.Designer.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.Forms.Trades;

partial class TradesForm
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
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
        _dataGridView = new System.Windows.Forms.DataGridViewEx();
        saveButton = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)_dataGridView).BeginInit();
        SuspendLayout();
        // 
        // _dataGridView
        // 
        _dataGridView.AllowUserToOrderColumns = true;
        dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(242, 242, 242);
        dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(224, 220, 228);
        dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
        _dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
        _dataGridView.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        _dataGridView.BackgroundColor = System.Drawing.Color.FromArgb(248, 249, 250);
        dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
        dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
        dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        _dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
        _dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
        dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F);
        dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
        dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(224, 220, 228);
        dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
        dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
        _dataGridView.DefaultCellStyle = dataGridViewCellStyle6;
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
        saveButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        saveButton.Location = new System.Drawing.Point(12, 340);
        saveButton.Name = "saveButton";
        saveButton.Size = new System.Drawing.Size(75, 23);
        saveButton.TabIndex = 1;
        saveButton.Text = "&Save";
        saveButton.UseVisualStyleBackColor = true;
        saveButton.Click += OnSaveButtonClick;
        // 
        // TradesForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(605, 375);
        Controls.Add(saveButton);
        Controls.Add(_dataGridView);
        Name = "TradesForm";
        Text = "Trades";
        ((System.ComponentModel.ISupportInitialize)_dataGridView).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.DataGridViewEx _dataGridView;
    private System.Windows.Forms.Button saveButton;
}
