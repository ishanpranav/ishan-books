namespace Liber.Forms
{
    partial class TransactionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransactionForm));
            postedDateTimePicker = new System.Windows.Forms.DateTimePicker();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            numberNumericUpDown = new System.Windows.Forms.NumericUpDown();
            toolStrip1 = new System.Windows.Forms.ToolStrip();
            newToolStripButton = new System.Windows.Forms.ToolStripButton();
            saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            copyToolStripButton = new System.Windows.Forms.ToolStripButton();
            cancelButton = new System.Windows.Forms.Button();
            applyButton = new System.Windows.Forms.Button();
            acceptButton = new System.Windows.Forms.Button();
            _dataGridView = new System.Windows.Forms.DataGridView();
            accountColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            debitColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            creditColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            descriptionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            previousButton = new System.Windows.Forms.Button();
            nextButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)numberNumericUpDown).BeginInit();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dataGridView).BeginInit();
            SuspendLayout();
            // 
            // postedDateTimePicker
            // 
            postedDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            resources.ApplyResources(postedDateTimePicker, "postedDateTimePicker");
            postedDateTimePicker.Name = "postedDateTimePicker";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // numberNumericUpDown
            // 
            resources.ApplyResources(numberNumericUpDown, "numberNumericUpDown");
            numberNumericUpDown.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
            numberNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numberNumericUpDown.Name = "numberNumericUpDown";
            numberNumericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { newToolStripButton, saveToolStripButton, toolStripSeparator3, copyToolStripButton });
            resources.ApplyResources(toolStrip1, "toolStrip1");
            toolStrip1.Name = "toolStrip1";
            // 
            // newToolStripButton
            // 
            newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(newToolStripButton, "newToolStripButton");
            newToolStripButton.Name = "newToolStripButton";
            newToolStripButton.Click += OnNewToolStripButtonClick;
            // 
            // saveToolStripButton
            // 
            saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(saveToolStripButton, "saveToolStripButton");
            saveToolStripButton.Name = "saveToolStripButton";
            saveToolStripButton.Click += OnSaveToolStripButtonClick;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(toolStripSeparator3, "toolStripSeparator3");
            // 
            // copyToolStripButton
            // 
            copyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(copyToolStripButton, "copyToolStripButton");
            copyToolStripButton.Name = "copyToolStripButton";
            copyToolStripButton.Click += OnCopyToolStripButtonClick;
            // 
            // cancelButton
            // 
            resources.ApplyResources(cancelButton, "cancelButton");
            cancelButton.Name = "cancelButton";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += OnCancelButtonClick;
            // 
            // applyButton
            // 
            resources.ApplyResources(applyButton, "applyButton");
            applyButton.Name = "applyButton";
            applyButton.UseVisualStyleBackColor = true;
            applyButton.Click += OnApplyButtonClick;
            // 
            // acceptButton
            // 
            resources.ApplyResources(acceptButton, "acceptButton");
            acceptButton.Name = "acceptButton";
            acceptButton.UseVisualStyleBackColor = true;
            acceptButton.Click += OnAcceptButtonClick;
            // 
            // _dataGridView
            // 
            _dataGridView.AllowUserToOrderColumns = true;
            resources.ApplyResources(_dataGridView, "_dataGridView");
            _dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { accountColumn, debitColumn, creditColumn, descriptionColumn });
            _dataGridView.Name = "_dataGridView";
            _dataGridView.RowTemplate.Height = 25;
            // 
            // accountColumn
            // 
            resources.ApplyResources(accountColumn, "accountColumn");
            accountColumn.Name = "accountColumn";
            // 
            // debitColumn
            // 
            resources.ApplyResources(debitColumn, "debitColumn");
            debitColumn.Name = "debitColumn";
            // 
            // creditColumn
            // 
            resources.ApplyResources(creditColumn, "creditColumn");
            creditColumn.Name = "creditColumn";
            // 
            // descriptionColumn
            // 
            resources.ApplyResources(descriptionColumn, "descriptionColumn");
            descriptionColumn.Name = "descriptionColumn";
            // 
            // previousButton
            // 
            resources.ApplyResources(previousButton, "previousButton");
            previousButton.Name = "previousButton";
            previousButton.UseVisualStyleBackColor = true;
            previousButton.Click += OnPreviousButtonClick;
            // 
            // nextButton
            // 
            resources.ApplyResources(nextButton, "nextButton");
            nextButton.Name = "nextButton";
            nextButton.UseVisualStyleBackColor = true;
            nextButton.Click += OnNextButtonClick;
            // 
            // TransactionForm
            // 
            AcceptButton = acceptButton;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = cancelButton;
            Controls.Add(nextButton);
            Controls.Add(previousButton);
            Controls.Add(_dataGridView);
            Controls.Add(acceptButton);
            Controls.Add(applyButton);
            Controls.Add(cancelButton);
            Controls.Add(toolStrip1);
            Controls.Add(numberNumericUpDown);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(postedDateTimePicker);
            Name = "TransactionForm";
            ((System.ComponentModel.ISupportInitialize)numberNumericUpDown).EndInit();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_dataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DateTimePicker postedDateTimePicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numberNumericUpDown;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripButton copyToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.DataGridView _dataGridView;
        private System.Windows.Forms.DataGridViewComboBoxColumn accountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn debitColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn creditColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionColumn;
        private System.Windows.Forms.Button previousButton;
        private System.Windows.Forms.Button nextButton;
    }
}