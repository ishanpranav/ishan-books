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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            postedDateTimePicker = new DateTimePicker();
            label1 = new Label();
            label2 = new Label();
            numberNumericUpDown = new NumericUpDown();
            toolStrip1 = new ToolStrip();
            closeToolStripButton = new ToolStripButton();
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
            ((System.ComponentModel.ISupportInitialize)numberNumericUpDown).BeginInit();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dataGridView).BeginInit();
            SuspendLayout();
            // 
            // postedDateTimePicker
            // 
            resources.ApplyResources(postedDateTimePicker, "postedDateTimePicker");
            postedDateTimePicker.Format = DateTimePickerFormat.Short;
            _helpProvider.SetHelpKeyword(postedDateTimePicker, resources.GetString("postedDateTimePicker.HelpKeyword"));
            _helpProvider.SetHelpNavigator(postedDateTimePicker, (HelpNavigator)resources.GetObject("postedDateTimePicker.HelpNavigator"));
            _helpProvider.SetHelpString(postedDateTimePicker, resources.GetString("postedDateTimePicker.HelpString"));
            postedDateTimePicker.Name = "postedDateTimePicker";
            _helpProvider.SetShowHelp(postedDateTimePicker, (bool)resources.GetObject("postedDateTimePicker.ShowHelp"));
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            _helpProvider.SetHelpKeyword(label1, resources.GetString("label1.HelpKeyword"));
            _helpProvider.SetHelpNavigator(label1, (HelpNavigator)resources.GetObject("label1.HelpNavigator"));
            _helpProvider.SetHelpString(label1, resources.GetString("label1.HelpString"));
            label1.Name = "label1";
            _helpProvider.SetShowHelp(label1, (bool)resources.GetObject("label1.ShowHelp"));
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            _helpProvider.SetHelpKeyword(label2, resources.GetString("label2.HelpKeyword"));
            _helpProvider.SetHelpNavigator(label2, (HelpNavigator)resources.GetObject("label2.HelpNavigator"));
            _helpProvider.SetHelpString(label2, resources.GetString("label2.HelpString"));
            label2.Name = "label2";
            _helpProvider.SetShowHelp(label2, (bool)resources.GetObject("label2.ShowHelp"));
            // 
            // numberNumericUpDown
            // 
            resources.ApplyResources(numberNumericUpDown, "numberNumericUpDown");
            _helpProvider.SetHelpKeyword(numberNumericUpDown, resources.GetString("numberNumericUpDown.HelpKeyword"));
            _helpProvider.SetHelpNavigator(numberNumericUpDown, (HelpNavigator)resources.GetObject("numberNumericUpDown.HelpNavigator"));
            _helpProvider.SetHelpString(numberNumericUpDown, resources.GetString("numberNumericUpDown.HelpString"));
            numberNumericUpDown.Name = "numberNumericUpDown";
            _helpProvider.SetShowHelp(numberNumericUpDown, (bool)resources.GetObject("numberNumericUpDown.ShowHelp"));
            numberNumericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // toolStrip1
            // 
            resources.ApplyResources(toolStrip1, "toolStrip1");
            _helpProvider.SetHelpKeyword(toolStrip1, resources.GetString("toolStrip1.HelpKeyword"));
            _helpProvider.SetHelpNavigator(toolStrip1, (HelpNavigator)resources.GetObject("toolStrip1.HelpNavigator"));
            _helpProvider.SetHelpString(toolStrip1, resources.GetString("toolStrip1.HelpString"));
            toolStrip1.Items.AddRange(new ToolStripItem[] { closeToolStripButton, newToolStripButton, saveToolStripButton, saveCloseToolStripButton, toolStripSeparator3, copyToolStripButton, removeToolStripButton, toolStripSeparator1, firstToolStripButton, previousToolStripButton, nextToolStripButton, lastToolStripButton });
            toolStrip1.Name = "toolStrip1";
            _helpProvider.SetShowHelp(toolStrip1, (bool)resources.GetObject("toolStrip1.ShowHelp"));
            // 
            // closeToolStripButton
            // 
            resources.ApplyResources(closeToolStripButton, "closeToolStripButton");
            closeToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            closeToolStripButton.Image = VisualStudioImageLibrary.CloseLog;
            closeToolStripButton.Name = "closeToolStripButton";
            closeToolStripButton.Click += OnCloseToolStripButtonClick;
            // 
            // newToolStripButton
            // 
            resources.ApplyResources(newToolStripButton, "newToolStripButton");
            newToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            newToolStripButton.Image = VisualStudioImageLibrary.NewLog;
            newToolStripButton.Name = "newToolStripButton";
            newToolStripButton.Click += OnNewToolStripButtonClick;
            // 
            // saveToolStripButton
            // 
            resources.ApplyResources(saveToolStripButton, "saveToolStripButton");
            saveToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            saveToolStripButton.Image = VisualStudioImageLibrary.Save;
            saveToolStripButton.Name = "saveToolStripButton";
            saveToolStripButton.Click += OnSaveToolStripButtonClick;
            // 
            // saveCloseToolStripButton
            // 
            resources.ApplyResources(saveCloseToolStripButton, "saveCloseToolStripButton");
            saveCloseToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            saveCloseToolStripButton.Image = VisualStudioImageLibrary.SaveAndClose;
            saveCloseToolStripButton.Name = "saveCloseToolStripButton";
            saveCloseToolStripButton.Click += OnAcceptButtonClick;
            // 
            // toolStripSeparator3
            // 
            resources.ApplyResources(toolStripSeparator3, "toolStripSeparator3");
            toolStripSeparator3.Name = "toolStripSeparator3";
            // 
            // copyToolStripButton
            // 
            resources.ApplyResources(copyToolStripButton, "copyToolStripButton");
            copyToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            copyToolStripButton.Image = VisualStudioImageLibrary.Duplicate;
            copyToolStripButton.Name = "copyToolStripButton";
            copyToolStripButton.Click += OnCopyToolStripButtonClick;
            // 
            // removeToolStripButton
            // 
            resources.ApplyResources(removeToolStripButton, "removeToolStripButton");
            removeToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            removeToolStripButton.Image = VisualStudioImageLibrary.Delete;
            removeToolStripButton.Name = "removeToolStripButton";
            removeToolStripButton.Click += OnRemoveToolStripButton;
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(toolStripSeparator1, "toolStripSeparator1");
            toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // firstToolStripButton
            // 
            resources.ApplyResources(firstToolStripButton, "firstToolStripButton");
            firstToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            firstToolStripButton.Image = VisualStudioImageLibrary.GoToTop;
            firstToolStripButton.Name = "firstToolStripButton";
            firstToolStripButton.Click += OnFirstButtonClick;
            // 
            // previousToolStripButton
            // 
            resources.ApplyResources(previousToolStripButton, "previousToolStripButton");
            previousToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            previousToolStripButton.Image = VisualStudioImageLibrary.GoToPrevious;
            previousToolStripButton.Name = "previousToolStripButton";
            previousToolStripButton.Click += OnPreviousButtonClick;
            // 
            // nextToolStripButton
            // 
            resources.ApplyResources(nextToolStripButton, "nextToolStripButton");
            nextToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            nextToolStripButton.Image = VisualStudioImageLibrary.GoToNext;
            nextToolStripButton.Name = "nextToolStripButton";
            nextToolStripButton.Click += OnNextButtonClick;
            // 
            // lastToolStripButton
            // 
            resources.ApplyResources(lastToolStripButton, "lastToolStripButton");
            lastToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            lastToolStripButton.Image = VisualStudioImageLibrary.GoToBottom;
            lastToolStripButton.Name = "lastToolStripButton";
            lastToolStripButton.Click += OnLastButtonClick;
            // 
            // cancelButton
            // 
            resources.ApplyResources(cancelButton, "cancelButton");
            _helpProvider.SetHelpKeyword(cancelButton, resources.GetString("cancelButton.HelpKeyword"));
            _helpProvider.SetHelpNavigator(cancelButton, (HelpNavigator)resources.GetObject("cancelButton.HelpNavigator"));
            _helpProvider.SetHelpString(cancelButton, resources.GetString("cancelButton.HelpString"));
            cancelButton.Name = "cancelButton";
            _helpProvider.SetShowHelp(cancelButton, (bool)resources.GetObject("cancelButton.ShowHelp"));
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += OnCancelButtonClick;
            // 
            // applyButton
            // 
            resources.ApplyResources(applyButton, "applyButton");
            _helpProvider.SetHelpKeyword(applyButton, resources.GetString("applyButton.HelpKeyword"));
            _helpProvider.SetHelpNavigator(applyButton, (HelpNavigator)resources.GetObject("applyButton.HelpNavigator"));
            _helpProvider.SetHelpString(applyButton, resources.GetString("applyButton.HelpString"));
            applyButton.Name = "applyButton";
            _helpProvider.SetShowHelp(applyButton, (bool)resources.GetObject("applyButton.ShowHelp"));
            applyButton.UseVisualStyleBackColor = true;
            applyButton.Click += OnApplyButtonClick;
            // 
            // acceptButton
            // 
            resources.ApplyResources(acceptButton, "acceptButton");
            _helpProvider.SetHelpKeyword(acceptButton, resources.GetString("acceptButton.HelpKeyword"));
            _helpProvider.SetHelpNavigator(acceptButton, (HelpNavigator)resources.GetObject("acceptButton.HelpNavigator"));
            _helpProvider.SetHelpString(acceptButton, resources.GetString("acceptButton.HelpString"));
            acceptButton.Name = "acceptButton";
            _helpProvider.SetShowHelp(acceptButton, (bool)resources.GetObject("acceptButton.ShowHelp"));
            acceptButton.UseVisualStyleBackColor = true;
            acceptButton.Click += OnAcceptButtonClick;
            // 
            // _dataGridView
            // 
            resources.ApplyResources(_dataGridView, "_dataGridView");
            _dataGridView.AllowUserToOrderColumns = true;
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
            _dataGridView.Columns.AddRange(new DataGridViewColumn[] { accountColumn, debitColumn, creditColumn, descriptionColumn });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(224, 220, 228);
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            _dataGridView.DefaultCellStyle = dataGridViewCellStyle3;
            _dataGridView.GridColor = System.Drawing.Color.FromArgb(33, 37, 41);
            _helpProvider.SetHelpKeyword(_dataGridView, resources.GetString("_dataGridView.HelpKeyword"));
            _helpProvider.SetHelpNavigator(_dataGridView, (HelpNavigator)resources.GetObject("_dataGridView.HelpNavigator"));
            _helpProvider.SetHelpString(_dataGridView, resources.GetString("_dataGridView.HelpString"));
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
            _helpProvider.SetHelpKeyword(previousButton, resources.GetString("previousButton.HelpKeyword"));
            _helpProvider.SetHelpNavigator(previousButton, (HelpNavigator)resources.GetObject("previousButton.HelpNavigator"));
            _helpProvider.SetHelpString(previousButton, resources.GetString("previousButton.HelpString"));
            previousButton.Name = "previousButton";
            _helpProvider.SetShowHelp(previousButton, (bool)resources.GetObject("previousButton.ShowHelp"));
            previousButton.UseVisualStyleBackColor = true;
            previousButton.Click += OnPreviousButtonClick;
            // 
            // nextButton
            // 
            resources.ApplyResources(nextButton, "nextButton");
            _helpProvider.SetHelpKeyword(nextButton, resources.GetString("nextButton.HelpKeyword"));
            _helpProvider.SetHelpNavigator(nextButton, (HelpNavigator)resources.GetObject("nextButton.HelpNavigator"));
            _helpProvider.SetHelpString(nextButton, resources.GetString("nextButton.HelpString"));
            nextButton.Name = "nextButton";
            _helpProvider.SetShowHelp(nextButton, (bool)resources.GetObject("nextButton.ShowHelp"));
            nextButton.UseVisualStyleBackColor = true;
            nextButton.Click += OnNextButtonClick;
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            _helpProvider.SetHelpKeyword(label3, resources.GetString("label3.HelpKeyword"));
            _helpProvider.SetHelpNavigator(label3, (HelpNavigator)resources.GetObject("label3.HelpNavigator"));
            _helpProvider.SetHelpString(label3, resources.GetString("label3.HelpString"));
            label3.Name = "label3";
            _helpProvider.SetShowHelp(label3, (bool)resources.GetObject("label3.ShowHelp"));
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            _helpProvider.SetHelpKeyword(label4, resources.GetString("label4.HelpKeyword"));
            _helpProvider.SetHelpNavigator(label4, (HelpNavigator)resources.GetObject("label4.HelpNavigator"));
            _helpProvider.SetHelpString(label4, resources.GetString("label4.HelpString"));
            label4.Name = "label4";
            _helpProvider.SetShowHelp(label4, (bool)resources.GetObject("label4.ShowHelp"));
            // 
            // nameComboBox
            // 
            resources.ApplyResources(nameComboBox, "nameComboBox");
            nameComboBox.FormattingEnabled = true;
            _helpProvider.SetHelpKeyword(nameComboBox, resources.GetString("nameComboBox.HelpKeyword"));
            _helpProvider.SetHelpNavigator(nameComboBox, (HelpNavigator)resources.GetObject("nameComboBox.HelpNavigator"));
            _helpProvider.SetHelpString(nameComboBox, resources.GetString("nameComboBox.HelpString"));
            nameComboBox.Name = "nameComboBox";
            _helpProvider.SetShowHelp(nameComboBox, (bool)resources.GetObject("nameComboBox.ShowHelp"));
            // 
            // memoTextBox
            // 
            resources.ApplyResources(memoTextBox, "memoTextBox");
            _helpProvider.SetHelpKeyword(memoTextBox, resources.GetString("memoTextBox.HelpKeyword"));
            _helpProvider.SetHelpNavigator(memoTextBox, (HelpNavigator)resources.GetObject("memoTextBox.HelpNavigator"));
            _helpProvider.SetHelpString(memoTextBox, resources.GetString("memoTextBox.HelpString"));
            memoTextBox.Name = "memoTextBox";
            _helpProvider.SetShowHelp(memoTextBox, (bool)resources.GetObject("memoTextBox.ShowHelp"));
            // 
            // _helpProvider
            // 
            resources.ApplyResources(_helpProvider, "_helpProvider");
            // 
            // lastButton
            // 
            resources.ApplyResources(lastButton, "lastButton");
            _helpProvider.SetHelpKeyword(lastButton, resources.GetString("lastButton.HelpKeyword"));
            _helpProvider.SetHelpNavigator(lastButton, (HelpNavigator)resources.GetObject("lastButton.HelpNavigator"));
            _helpProvider.SetHelpString(lastButton, resources.GetString("lastButton.HelpString"));
            lastButton.Name = "lastButton";
            _helpProvider.SetShowHelp(lastButton, (bool)resources.GetObject("lastButton.ShowHelp"));
            lastButton.UseVisualStyleBackColor = true;
            lastButton.Click += OnLastButtonClick;
            // 
            // firstButton
            // 
            resources.ApplyResources(firstButton, "firstButton");
            _helpProvider.SetHelpKeyword(firstButton, resources.GetString("firstButton.HelpKeyword"));
            _helpProvider.SetHelpNavigator(firstButton, (HelpNavigator)resources.GetObject("firstButton.HelpNavigator"));
            _helpProvider.SetHelpString(firstButton, resources.GetString("firstButton.HelpString"));
            firstButton.Name = "firstButton";
            _helpProvider.SetShowHelp(firstButton, (bool)resources.GetObject("firstButton.ShowHelp"));
            firstButton.UseVisualStyleBackColor = true;
            firstButton.Click += OnFirstButtonClick;
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
            _helpProvider.SetHelpKeyword(this, resources.GetString("$this.HelpKeyword"));
            _helpProvider.SetHelpNavigator(this, (HelpNavigator)resources.GetObject("$this.HelpNavigator"));
            _helpProvider.SetHelpString(this, resources.GetString("$this.HelpString"));
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
