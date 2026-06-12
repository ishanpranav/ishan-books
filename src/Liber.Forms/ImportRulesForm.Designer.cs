namespace Liber.Forms
{
    partial class ImportRulesForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportRulesForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            acceptButton = new System.Windows.Forms.Button();
            cancelButton = new System.Windows.Forms.Button();
            _helpProvider = new System.Windows.Forms.HelpProvider();
            resetButton = new System.Windows.Forms.Button();
            importRulesDataGridView = new System.Windows.Forms.DataGridViewEx();
            _tabControl = new System.Windows.Forms.TabControl();
            editorTabPage = new System.Windows.Forms.TabPage();
            jsonTabPage = new System.Windows.Forms.TabPage();
            _textBox = new System.Windows.Forms.TextBox();
            _errorProvider = new System.Windows.Forms.ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)importRulesDataGridView).BeginInit();
            _tabControl.SuspendLayout();
            editorTabPage.SuspendLayout();
            jsonTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_errorProvider).BeginInit();
            SuspendLayout();
            // 
            // acceptButton
            // 
            resources.ApplyResources(acceptButton, "acceptButton");
            _helpProvider.SetHelpString(acceptButton, resources.GetString("acceptButton.HelpString"));
            acceptButton.Name = "acceptButton";
            _helpProvider.SetShowHelp(acceptButton, (bool)resources.GetObject("acceptButton.ShowHelp"));
            acceptButton.UseVisualStyleBackColor = true;
            acceptButton.Click += OnAcceptButtonClick;
            // 
            // cancelButton
            // 
            resources.ApplyResources(cancelButton, "cancelButton");
            _helpProvider.SetHelpString(cancelButton, resources.GetString("cancelButton.HelpString"));
            cancelButton.Name = "cancelButton";
            _helpProvider.SetShowHelp(cancelButton, (bool)resources.GetObject("cancelButton.ShowHelp"));
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += OnCancelButtonClick;
            // 
            // resetButton
            // 
            resources.ApplyResources(resetButton, "resetButton");
            _helpProvider.SetHelpString(resetButton, resources.GetString("resetButton.HelpString"));
            resetButton.Name = "resetButton";
            _helpProvider.SetShowHelp(resetButton, (bool)resources.GetObject("resetButton.ShowHelp"));
            resetButton.UseVisualStyleBackColor = true;
            resetButton.Click += OnResetButtonClick;
            // 
            // importRulesDataGridView
            // 
            importRulesDataGridView.AllowUserToOrderColumns = true;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(242, 242, 242);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(224, 220, 228);
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            importRulesDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            importRulesDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            importRulesDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(248, 249, 250);
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            importRulesDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            importRulesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(224, 220, 228);
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            importRulesDataGridView.DefaultCellStyle = dataGridViewCellStyle6;
            resources.ApplyResources(importRulesDataGridView, "importRulesDataGridView");
            importRulesDataGridView.GridColor = System.Drawing.Color.FromArgb(33, 37, 41);
            importRulesDataGridView.Name = "importRulesDataGridView";
            // 
            // _tabControl
            // 
            resources.ApplyResources(_tabControl, "_tabControl");
            _tabControl.Controls.Add(editorTabPage);
            _tabControl.Controls.Add(jsonTabPage);
            _tabControl.Name = "_tabControl";
            _tabControl.SelectedIndex = 0;
            _tabControl.SelectedIndexChanged += OnTabControlSelectedIndexChanged;
            _tabControl.Selecting += OnTabControlSelecting;
            // 
            // editorTabPage
            // 
            editorTabPage.Controls.Add(importRulesDataGridView);
            resources.ApplyResources(editorTabPage, "editorTabPage");
            editorTabPage.Name = "editorTabPage";
            editorTabPage.UseVisualStyleBackColor = true;
            // 
            // jsonTabPage
            // 
            jsonTabPage.Controls.Add(_textBox);
            resources.ApplyResources(jsonTabPage, "jsonTabPage");
            jsonTabPage.Name = "jsonTabPage";
            jsonTabPage.UseVisualStyleBackColor = true;
            // 
            // _textBox
            // 
            _textBox.AcceptsReturn = true;
            _textBox.AcceptsTab = true;
            resources.ApplyResources(_textBox, "_textBox");
            _textBox.Name = "_textBox";
            // 
            // _errorProvider
            // 
            _errorProvider.ContainerControl = this;
            // 
            // ImportRulesForm
            // 
            AcceptButton = acceptButton;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = cancelButton;
            Controls.Add(_tabControl);
            Controls.Add(resetButton);
            Controls.Add(acceptButton);
            Controls.Add(cancelButton);
            Name = "ImportRulesForm";
            _helpProvider.SetShowHelp(this, (bool)resources.GetObject("$this.ShowHelp"));
            ShowIcon = false;
            WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)importRulesDataGridView).EndInit();
            _tabControl.ResumeLayout(false);
            editorTabPage.ResumeLayout(false);
            jsonTabPage.ResumeLayout(false);
            jsonTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_errorProvider).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.HelpProvider _helpProvider;
        private System.Windows.Forms.DataGridViewEx importRulesDataGridView;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.TabControl _tabControl;
        private System.Windows.Forms.TabPage editorTabPage;
        private System.Windows.Forms.TabPage jsonTabPage;
        private System.Windows.Forms.TextBox _textBox;
        private System.Windows.Forms.ErrorProvider _errorProvider;
    }
}
