using System.Windows.Forms;

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
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            postedDateTimePicker = new DateTimePicker();
            label1 = new Label();
            label2 = new Label();
            numberNumericUpDown = new NumericUpDown();
            toolStrip1 = new ToolStrip();
            newToolStripButton = new ToolStripButton();
            saveToolStripButton = new ToolStripButton();
            saveCloseToolStripButton = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            copyToolStripButton = new ToolStripButton();
            removeToolStripButton = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            firstToolStripButton = new ToolStripButton();
            previousToolStripButton = new ToolStripButton();
            nextToolStripButton = new ToolStripButton();
            lastToolStripButton = new ToolStripButton();
            cancelButton = new Button();
            applyButton = new Button();
            acceptButton = new Button();
            _dataGridView = new TransactionGridView();
            accountColumn = new DataGridViewComboBoxColumn();
            debitColumn = new DataGridViewTextBoxColumn();
            creditColumn = new DataGridViewTextBoxColumn();
            descriptionColumn = new DataGridViewTextBoxColumn();
            previousButton = new Button();
            nextButton = new Button();
            label3 = new Label();
            label4 = new Label();
            nameComboBox = new ComboBox();
            memoTextBox = new TextBox();
            _helpProvider = new HelpProvider();
            lastButton = new Button();
            firstButton = new Button();
            closeToolStripButton = new ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)numberNumericUpDown).BeginInit();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dataGridView).BeginInit();
            SuspendLayout();
            // 
            // postedDateTimePicker
            // 
            postedDateTimePicker.Format = DateTimePickerFormat.Short;
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
            toolStrip1.Items.AddRange(new ToolStripItem[] { closeToolStripButton, newToolStripButton, saveToolStripButton, saveCloseToolStripButton, toolStripSeparator3, copyToolStripButton, removeToolStripButton, toolStripSeparator1, firstToolStripButton, previousToolStripButton, nextToolStripButton, lastToolStripButton });
            resources.ApplyResources(toolStrip1, "toolStrip1");
            toolStrip1.Name = "toolStrip1";
            _helpProvider.SetShowHelp(toolStrip1, (bool)resources.GetObject("toolStrip1.ShowHelp"));
            // 
            // newToolStripButton
            // 
            newToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            newToolStripButton.Image = VisualStudioImageLibrary.NewLog;
            resources.ApplyResources(newToolStripButton, "newToolStripButton");
            newToolStripButton.Name = "newToolStripButton";
            newToolStripButton.Click += OnNewToolStripButtonClick;
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
            saveCloseToolStripButton.Click += OnAcceptButtonClick;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(toolStripSeparator3, "toolStripSeparator3");
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
            removeToolStripButton.Click += OnRemoveToolStripButton;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(toolStripSeparator1, "toolStripSeparator1");
            // 
            // firstToolStripButton
            // 
            firstToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            firstToolStripButton.Image = VisualStudioImageLibrary.GoToFirst;
            resources.ApplyResources(firstToolStripButton, "firstToolStripButton");
            firstToolStripButton.Name = "firstToolStripButton";
            firstToolStripButton.Click += OnFirstButtonClick;
            // 
            // previousToolStripButton
            // 
            previousToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            previousToolStripButton.Image = VisualStudioImageLibrary.GoToPrevious;
            resources.ApplyResources(previousToolStripButton, "previousToolStripButton");
            previousToolStripButton.Name = "previousToolStripButton";
            previousToolStripButton.Click += OnPreviousButtonClick;
            // 
            // nextToolStripButton
            // 
            nextToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            nextToolStripButton.Image = VisualStudioImageLibrary.GoToNext;
            resources.ApplyResources(nextToolStripButton, "nextToolStripButton");
            nextToolStripButton.Name = "nextToolStripButton";
            nextToolStripButton.Click += OnNextButtonClick;
            // 
            // lastToolStripButton
            // 
            lastToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            lastToolStripButton.Image = VisualStudioImageLibrary.GoToLast;
            resources.ApplyResources(lastToolStripButton, "lastToolStripButton");
            lastToolStripButton.Name = "lastToolStripButton";
            lastToolStripButton.Click += OnLastButtonClick;
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
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(33, 37, 41);
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(224, 220, 228);
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            _dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            resources.ApplyResources(_dataGridView, "_dataGridView");
            _dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            _dataGridView.BackgroundColor = System.Drawing.Color.FromArgb(248, 249, 250);
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.True;
            _dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            _dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _dataGridView.Columns.AddRange(new DataGridViewColumn[] { accountColumn, debitColumn, creditColumn, descriptionColumn });
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(224, 220, 228);
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.WrapMode = DataGridViewTriState.False;
            _dataGridView.DefaultCellStyle = dataGridViewCellStyle9;
            _dataGridView.GridColor = System.Drawing.Color.FromArgb(33, 37, 41);
            _dataGridView.MultiSelect = false;
            _dataGridView.Name = "_dataGridView";
            _dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _helpProvider.SetShowHelp(_dataGridView, (bool)resources.GetObject("_dataGridView.ShowHelp"));
            _dataGridView.DefaultValuesNeeded += OnDataGridViewDefaultValuesNeeded;
            // 
            // accountColumn
            // 
            resources.ApplyResources(accountColumn, "accountColumn");
            accountColumn.Name = "accountColumn";
            accountColumn.Resizable = DataGridViewTriState.True;
            accountColumn.SortMode = DataGridViewColumnSortMode.Automatic;
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
            // lastButton
            // 
            resources.ApplyResources(lastButton, "lastButton");
            lastButton.Name = "lastButton";
            _helpProvider.SetShowHelp(lastButton, (bool)resources.GetObject("lastButton.ShowHelp"));
            lastButton.UseVisualStyleBackColor = true;
            lastButton.Click += OnLastButtonClick;
            // 
            // firstButton
            // 
            resources.ApplyResources(firstButton, "firstButton");
            firstButton.Name = "firstButton";
            _helpProvider.SetShowHelp(firstButton, (bool)resources.GetObject("firstButton.ShowHelp"));
            firstButton.UseVisualStyleBackColor = true;
            firstButton.Click += OnFirstButtonClick;
            // 
            // closeToolStripButton
            // 
            closeToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            closeToolStripButton.Image = VisualStudioImageLibrary.CloseLog;
            resources.ApplyResources(closeToolStripButton, "closeToolStripButton");
            closeToolStripButton.Name = "closeToolStripButton";
            closeToolStripButton.Click += OnCloseToolStripButtonClick;
            // 
            // TransactionForm
            // 
            AcceptButton = applyButton;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lastButton);
            Controls.Add(firstButton);
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
        private TransactionGridView _dataGridView;
        private System.Windows.Forms.Button previousButton;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox nameComboBox;
        private System.Windows.Forms.TextBox memoTextBox;
        private System.Windows.Forms.HelpProvider _helpProvider;
        private System.Windows.Forms.Button lastButton;
        private System.Windows.Forms.Button firstButton;
        private DataGridViewComboBoxColumn accountColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn debitColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn creditColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionColumn;
        private ToolStripButton saveCloseToolStripButton;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton firstToolStripButton;
        private ToolStripButton previousToolStripButton;
        private ToolStripButton nextToolStripButton;
        private ToolStripButton lastToolStripButton;
        private ToolStripButton removeToolStripButton;
        private ToolStripButton closeToolStripButton;
    }
}
