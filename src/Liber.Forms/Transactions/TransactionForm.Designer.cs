namespace Liber.Forms.Transactions
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            postedDateTimePicker = new System.Windows.Forms.DateTimePicker();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            numberNumericUpDown = new System.Windows.Forms.NumericUpDown();
            toolStrip1 = new System.Windows.Forms.ToolStrip();
            newToolStripButton = new System.Windows.Forms.ToolStripButton();
            saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            copyToolStripButton = new System.Windows.Forms.ToolStripButton();
            printToolStripButton = new System.Windows.Forms.ToolStripButton();
            cancelButton = new System.Windows.Forms.Button();
            applyButton = new System.Windows.Forms.Button();
            acceptButton = new System.Windows.Forms.Button();
            _dataGridView = new System.Windows.Forms.DataGridView();
            previousButton = new System.Windows.Forms.Button();
            nextButton = new System.Windows.Forms.Button();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            nameComboBox = new System.Windows.Forms.ComboBox();
            memoTextBox = new System.Windows.Forms.TextBox();
            _helpProvider = new System.Windows.Forms.HelpProvider();
            accountColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            debitColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            creditColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            descriptionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)numberNumericUpDown).BeginInit();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dataGridView).BeginInit();
            SuspendLayout();
            // 
            // postedDateTimePicker
            // 
            postedDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            _helpProvider.SetHelpString(postedDateTimePicker, resources.GetString("postedDateTimePicker.HelpString"));
            resources.ApplyResources(postedDateTimePicker, "postedDateTimePicker");
            postedDateTimePicker.Name = "postedDateTimePicker";
            _helpProvider.SetShowHelp(postedDateTimePicker, (bool)resources.GetObject("postedDateTimePicker.ShowHelp"));
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            _helpProvider.SetShowHelp(label1, (bool)resources.GetObject("label1.ShowHelp"));
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            _helpProvider.SetShowHelp(label2, (bool)resources.GetObject("label2.ShowHelp"));
            // 
            // numberNumericUpDown
            // 
            _helpProvider.SetHelpString(numberNumericUpDown, resources.GetString("numberNumericUpDown.HelpString"));
            resources.ApplyResources(numberNumericUpDown, "numberNumericUpDown");
            numberNumericUpDown.Name = "numberNumericUpDown";
            _helpProvider.SetShowHelp(numberNumericUpDown, (bool)resources.GetObject("numberNumericUpDown.ShowHelp"));
            numberNumericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { newToolStripButton, saveToolStripButton, toolStripSeparator3, copyToolStripButton, printToolStripButton });
            resources.ApplyResources(toolStrip1, "toolStrip1");
            toolStrip1.Name = "toolStrip1";
            _helpProvider.SetShowHelp(toolStrip1, (bool)resources.GetObject("toolStrip1.ShowHelp"));
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
            // printToolStripButton
            // 
            printToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(printToolStripButton, "printToolStripButton");
            printToolStripButton.Name = "printToolStripButton";
            printToolStripButton.Click += OnPrintToolStripButtonClick;
            // 
            // cancelButton
            // 
            resources.ApplyResources(cancelButton, "cancelButton");
            cancelButton.Name = "cancelButton";
            _helpProvider.SetShowHelp(cancelButton, (bool)resources.GetObject("cancelButton.ShowHelp"));
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += OnCancelButtonClick;
            // 
            // applyButton
            // 
            resources.ApplyResources(applyButton, "applyButton");
            applyButton.Name = "applyButton";
            _helpProvider.SetShowHelp(applyButton, (bool)resources.GetObject("applyButton.ShowHelp"));
            applyButton.UseVisualStyleBackColor = true;
            applyButton.Click += OnApplyButtonClick;
            // 
            // acceptButton
            // 
            resources.ApplyResources(acceptButton, "acceptButton");
            acceptButton.Name = "acceptButton";
            _helpProvider.SetShowHelp(acceptButton, (bool)resources.GetObject("acceptButton.ShowHelp"));
            acceptButton.UseVisualStyleBackColor = true;
            acceptButton.Click += OnAcceptButtonClick;
            // 
            // _dataGridView
            // 
            _dataGridView.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Info;
            _dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(_dataGridView, "_dataGridView");
            _dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            _dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { accountColumn, debitColumn, creditColumn, descriptionColumn });
            _dataGridView.MultiSelect = false;
            _dataGridView.Name = "_dataGridView";
            _dataGridView.RowTemplate.Height = 25;
            _dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            _helpProvider.SetShowHelp(_dataGridView, (bool)resources.GetObject("_dataGridView.ShowHelp"));
            // 
            // previousButton
            // 
            resources.ApplyResources(previousButton, "previousButton");
            previousButton.Name = "previousButton";
            _helpProvider.SetShowHelp(previousButton, (bool)resources.GetObject("previousButton.ShowHelp"));
            previousButton.UseVisualStyleBackColor = true;
            previousButton.Click += OnPreviousButtonClick;
            // 
            // nextButton
            // 
            resources.ApplyResources(nextButton, "nextButton");
            nextButton.Name = "nextButton";
            _helpProvider.SetShowHelp(nextButton, (bool)resources.GetObject("nextButton.ShowHelp"));
            nextButton.UseVisualStyleBackColor = true;
            nextButton.Click += OnNextButtonClick;
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            _helpProvider.SetShowHelp(label3, (bool)resources.GetObject("label3.ShowHelp"));
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            _helpProvider.SetShowHelp(label4, (bool)resources.GetObject("label4.ShowHelp"));
            // 
            // nameComboBox
            // 
            nameComboBox.FormattingEnabled = true;
            _helpProvider.SetHelpString(nameComboBox, resources.GetString("nameComboBox.HelpString"));
            resources.ApplyResources(nameComboBox, "nameComboBox");
            nameComboBox.Name = "nameComboBox";
            _helpProvider.SetShowHelp(nameComboBox, (bool)resources.GetObject("nameComboBox.ShowHelp"));
            // 
            // memoTextBox
            // 
            resources.ApplyResources(memoTextBox, "memoTextBox");
            _helpProvider.SetHelpString(memoTextBox, resources.GetString("memoTextBox.HelpString"));
            memoTextBox.Name = "memoTextBox";
            _helpProvider.SetShowHelp(memoTextBox, (bool)resources.GetObject("memoTextBox.ShowHelp"));
            // 
            // accountColumn
            // 
            resources.ApplyResources(accountColumn, "accountColumn");
            accountColumn.Name = "accountColumn";
            accountColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
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
            // TransactionForm
            // 
            AcceptButton = acceptButton;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(memoTextBox);
            Controls.Add(nameComboBox);
            Controls.Add(label4);
            Controls.Add(label3);
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
            _helpProvider.SetShowHelp(this, (bool)resources.GetObject("$this.ShowHelp"));
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
        private System.Windows.Forms.Button previousButton;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox nameComboBox;
        private System.Windows.Forms.TextBox memoTextBox;
        private System.Windows.Forms.HelpProvider _helpProvider;
        private System.Windows.Forms.ToolStripButton printToolStripButton;
        private System.Windows.Forms.DataGridViewComboBoxColumn accountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn debitColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn creditColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionColumn;
    }
}
