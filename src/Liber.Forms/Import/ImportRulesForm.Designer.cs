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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
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
            _errorProvider.SetError(acceptButton, resources.GetString("acceptButton.Error"));
            _helpProvider.SetHelpKeyword(acceptButton, resources.GetString("acceptButton.HelpKeyword"));
            _helpProvider.SetHelpNavigator(acceptButton, (System.Windows.Forms.HelpNavigator)resources.GetObject("acceptButton.HelpNavigator"));
            _helpProvider.SetHelpString(acceptButton, resources.GetString("acceptButton.HelpString"));
            _errorProvider.SetIconAlignment(acceptButton, (System.Windows.Forms.ErrorIconAlignment)resources.GetObject("acceptButton.IconAlignment"));
            _errorProvider.SetIconPadding(acceptButton, (int)resources.GetObject("acceptButton.IconPadding"));
            acceptButton.Name = "acceptButton";
            _helpProvider.SetShowHelp(acceptButton, (bool)resources.GetObject("acceptButton.ShowHelp"));
            acceptButton.UseVisualStyleBackColor = true;
            acceptButton.Click += OnAcceptButtonClick;
            // 
            // cancelButton
            // 
            resources.ApplyResources(cancelButton, "cancelButton");
            _errorProvider.SetError(cancelButton, resources.GetString("cancelButton.Error"));
            _helpProvider.SetHelpKeyword(cancelButton, resources.GetString("cancelButton.HelpKeyword"));
            _helpProvider.SetHelpNavigator(cancelButton, (System.Windows.Forms.HelpNavigator)resources.GetObject("cancelButton.HelpNavigator"));
            _helpProvider.SetHelpString(cancelButton, resources.GetString("cancelButton.HelpString"));
            _errorProvider.SetIconAlignment(cancelButton, (System.Windows.Forms.ErrorIconAlignment)resources.GetObject("cancelButton.IconAlignment"));
            _errorProvider.SetIconPadding(cancelButton, (int)resources.GetObject("cancelButton.IconPadding"));
            cancelButton.Name = "cancelButton";
            _helpProvider.SetShowHelp(cancelButton, (bool)resources.GetObject("cancelButton.ShowHelp"));
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += OnCancelButtonClick;
            // 
            // _helpProvider
            // 
            resources.ApplyResources(_helpProvider, "_helpProvider");
            // 
            // resetButton
            // 
            resources.ApplyResources(resetButton, "resetButton");
            _errorProvider.SetError(resetButton, resources.GetString("resetButton.Error"));
            _helpProvider.SetHelpKeyword(resetButton, resources.GetString("resetButton.HelpKeyword"));
            _helpProvider.SetHelpNavigator(resetButton, (System.Windows.Forms.HelpNavigator)resources.GetObject("resetButton.HelpNavigator"));
            _helpProvider.SetHelpString(resetButton, resources.GetString("resetButton.HelpString"));
            _errorProvider.SetIconAlignment(resetButton, (System.Windows.Forms.ErrorIconAlignment)resources.GetObject("resetButton.IconAlignment"));
            _errorProvider.SetIconPadding(resetButton, (int)resources.GetObject("resetButton.IconPadding"));
            resetButton.Name = "resetButton";
            _helpProvider.SetShowHelp(resetButton, (bool)resources.GetObject("resetButton.ShowHelp"));
            resetButton.UseVisualStyleBackColor = true;
            resetButton.Click += OnResetButtonClick;
            // 
            // importRulesDataGridView
            // 
            resources.ApplyResources(importRulesDataGridView, "importRulesDataGridView");
            importRulesDataGridView.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(242, 242, 242);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(224, 220, 228);
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            importRulesDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            importRulesDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            importRulesDataGridView.BackgroundColor = System.Drawing.Color.FromArgb(248, 249, 250);
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            importRulesDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            importRulesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(224, 220, 228);
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            importRulesDataGridView.DefaultCellStyle = dataGridViewCellStyle3;
            _errorProvider.SetError(importRulesDataGridView, resources.GetString("importRulesDataGridView.Error"));
            importRulesDataGridView.GridColor = System.Drawing.Color.FromArgb(33, 37, 41);
            _helpProvider.SetHelpKeyword(importRulesDataGridView, resources.GetString("importRulesDataGridView.HelpKeyword"));
            _helpProvider.SetHelpNavigator(importRulesDataGridView, (System.Windows.Forms.HelpNavigator)resources.GetObject("importRulesDataGridView.HelpNavigator"));
            _helpProvider.SetHelpString(importRulesDataGridView, resources.GetString("importRulesDataGridView.HelpString"));
            _errorProvider.SetIconAlignment(importRulesDataGridView, (System.Windows.Forms.ErrorIconAlignment)resources.GetObject("importRulesDataGridView.IconAlignment"));
            _errorProvider.SetIconPadding(importRulesDataGridView, (int)resources.GetObject("importRulesDataGridView.IconPadding"));
            importRulesDataGridView.Name = "importRulesDataGridView";
            // 
            // _tabControl
            // 
            resources.ApplyResources(_tabControl, "_tabControl");
            _tabControl.Controls.Add(editorTabPage);
            _tabControl.Controls.Add(jsonTabPage);
            _errorProvider.SetError(_tabControl, resources.GetString("_tabControl.Error"));
            _helpProvider.SetHelpKeyword(_tabControl, resources.GetString("_tabControl.HelpKeyword"));
            _helpProvider.SetHelpNavigator(_tabControl, (System.Windows.Forms.HelpNavigator)resources.GetObject("_tabControl.HelpNavigator"));
            _helpProvider.SetHelpString(_tabControl, resources.GetString("_tabControl.HelpString"));
            _errorProvider.SetIconAlignment(_tabControl, (System.Windows.Forms.ErrorIconAlignment)resources.GetObject("_tabControl.IconAlignment"));
            _errorProvider.SetIconPadding(_tabControl, (int)resources.GetObject("_tabControl.IconPadding"));
            _tabControl.Name = "_tabControl";
            _tabControl.SelectedIndex = 0;
            _tabControl.SelectedIndexChanged += OnTabControlSelectedIndexChanged;
            _tabControl.Selecting += OnTabControlSelecting;
            // 
            // editorTabPage
            // 
            resources.ApplyResources(editorTabPage, "editorTabPage");
            editorTabPage.Controls.Add(importRulesDataGridView);
            _errorProvider.SetError(editorTabPage, resources.GetString("editorTabPage.Error"));
            _helpProvider.SetHelpKeyword(editorTabPage, resources.GetString("editorTabPage.HelpKeyword"));
            _helpProvider.SetHelpNavigator(editorTabPage, (System.Windows.Forms.HelpNavigator)resources.GetObject("editorTabPage.HelpNavigator"));
            _helpProvider.SetHelpString(editorTabPage, resources.GetString("editorTabPage.HelpString"));
            _errorProvider.SetIconAlignment(editorTabPage, (System.Windows.Forms.ErrorIconAlignment)resources.GetObject("editorTabPage.IconAlignment"));
            _errorProvider.SetIconPadding(editorTabPage, (int)resources.GetObject("editorTabPage.IconPadding"));
            editorTabPage.Name = "editorTabPage";
            editorTabPage.UseVisualStyleBackColor = true;
            // 
            // jsonTabPage
            // 
            resources.ApplyResources(jsonTabPage, "jsonTabPage");
            jsonTabPage.Controls.Add(_textBox);
            _errorProvider.SetError(jsonTabPage, resources.GetString("jsonTabPage.Error"));
            _helpProvider.SetHelpKeyword(jsonTabPage, resources.GetString("jsonTabPage.HelpKeyword"));
            _helpProvider.SetHelpNavigator(jsonTabPage, (System.Windows.Forms.HelpNavigator)resources.GetObject("jsonTabPage.HelpNavigator"));
            _helpProvider.SetHelpString(jsonTabPage, resources.GetString("jsonTabPage.HelpString"));
            _errorProvider.SetIconAlignment(jsonTabPage, (System.Windows.Forms.ErrorIconAlignment)resources.GetObject("jsonTabPage.IconAlignment"));
            _errorProvider.SetIconPadding(jsonTabPage, (int)resources.GetObject("jsonTabPage.IconPadding"));
            jsonTabPage.Name = "jsonTabPage";
            jsonTabPage.UseVisualStyleBackColor = true;
            // 
            // _textBox
            // 
            _textBox.AcceptsReturn = true;
            _textBox.AcceptsTab = true;
            resources.ApplyResources(_textBox, "_textBox");
            _errorProvider.SetError(_textBox, resources.GetString("_textBox.Error"));
            _helpProvider.SetHelpKeyword(_textBox, resources.GetString("_textBox.HelpKeyword"));
            _helpProvider.SetHelpNavigator(_textBox, (System.Windows.Forms.HelpNavigator)resources.GetObject("_textBox.HelpNavigator"));
            _helpProvider.SetHelpString(_textBox, resources.GetString("_textBox.HelpString"));
            _errorProvider.SetIconAlignment(_textBox, (System.Windows.Forms.ErrorIconAlignment)resources.GetObject("_textBox.IconAlignment"));
            _errorProvider.SetIconPadding(_textBox, (int)resources.GetObject("_textBox.IconPadding"));
            _textBox.Name = "_textBox";
            // 
            // _errorProvider
            // 
            _errorProvider.ContainerControl = this;
            resources.ApplyResources(_errorProvider, "_errorProvider");
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
            _helpProvider.SetHelpKeyword(this, resources.GetString("$this.HelpKeyword"));
            _helpProvider.SetHelpNavigator(this, (System.Windows.Forms.HelpNavigator)resources.GetObject("$this.HelpNavigator"));
            _helpProvider.SetHelpString(this, resources.GetString("$this.HelpString"));
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
