// TransactionsForm.Designer.cs
// Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransactionsForm));
        _dataGridView = new System.Windows.Forms.DataGridView();
        postedColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        numberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        accountColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
        nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        debitColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        creditColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        balanceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        transactionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        ((System.ComponentModel.ISupportInitialize)_dataGridView).BeginInit();
        SuspendLayout();
        // 
        // _dataGridView
        // 
        _dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
        _dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        _dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { postedColumn, numberColumn, accountColumn, nameColumn, debitColumn, creditColumn, balanceColumn, transactionColumn });
        resources.ApplyResources(_dataGridView, "_dataGridView");
        _dataGridView.Name = "_dataGridView";
        _dataGridView.RowTemplate.Height = 29;
        _dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        _dataGridView.CellDoubleClick += OnDataGridViewCellDoubleClick;
        _dataGridView.EditingControlShowing += OnDataGridViewEditingControlShowing;
        _dataGridView.UserAddedRow += OnDataGridViewUserAddedRow;
        _dataGridView.UserDeletedRow += OnDataGridViewUserDeletedRow;
        // 
        // postedColumn
        // 
        postedColumn.Frozen = true;
        resources.ApplyResources(postedColumn, "postedColumn");
        postedColumn.Name = "postedColumn";
        postedColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
        // 
        // numberColumn
        // 
        numberColumn.Frozen = true;
        resources.ApplyResources(numberColumn, "numberColumn");
        numberColumn.Name = "numberColumn";
        numberColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
        // 
        // accountColumn
        // 
        accountColumn.Frozen = true;
        resources.ApplyResources(accountColumn, "accountColumn");
        accountColumn.Name = "accountColumn";
        // 
        // nameColumn
        // 
        nameColumn.Frozen = true;
        resources.ApplyResources(nameColumn, "nameColumn");
        nameColumn.Name = "nameColumn";
        nameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
        // 
        // debitColumn
        // 
        debitColumn.Frozen = true;
        resources.ApplyResources(debitColumn, "debitColumn");
        debitColumn.Name = "debitColumn";
        debitColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
        // 
        // creditColumn
        // 
        creditColumn.Frozen = true;
        resources.ApplyResources(creditColumn, "creditColumn");
        creditColumn.Name = "creditColumn";
        creditColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
        // 
        // balanceColumn
        // 
        balanceColumn.Frozen = true;
        resources.ApplyResources(balanceColumn, "balanceColumn");
        balanceColumn.Name = "balanceColumn";
        balanceColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
        // 
        // transactionColumn
        // 
        resources.ApplyResources(transactionColumn, "transactionColumn");
        transactionColumn.Name = "transactionColumn";
        // 
        // TransactionsForm
        // 
        resources.ApplyResources(this, "$this");
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        Controls.Add(_dataGridView);
        Name = "TransactionsForm";
        ((System.ComponentModel.ISupportInitialize)_dataGridView).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.DataGridView _dataGridView;
    private System.Windows.Forms.DataGridViewTextBoxColumn postedColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn numberColumn;
    private System.Windows.Forms.DataGridViewComboBoxColumn accountColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn nameColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn debitColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn creditColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn balanceColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn transactionColumn;
}
