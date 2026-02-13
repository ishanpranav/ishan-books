namespace Liber.Forms
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            acceptButton = new System.Windows.Forms.Button();
            cancelButton = new System.Windows.Forms.Button();
            cultureComboBox = new System.Windows.Forms.ComboBox();
            label2 = new System.Windows.Forms.Label();
            groupBox1 = new System.Windows.Forms.GroupBox();
            _helpProvider = new System.Windows.Forms.HelpProvider();
            resetButton = new System.Windows.Forms.Button();
            importRulesDataGridView = new System.Windows.Forms.DataGridView();
            groupBox2 = new System.Windows.Forms.GroupBox();
            _tabControl = new System.Windows.Forms.TabControl();
            editorTabPage = new System.Windows.Forms.TabPage();
            jsonTabPage = new System.Windows.Forms.TabPage();
            _textBox = new System.Windows.Forms.TextBox();
            _errorProvider = new System.Windows.Forms.ErrorProvider(components);
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)importRulesDataGridView).BeginInit();
            groupBox2.SuspendLayout();
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
            // cultureComboBox
            // 
            resources.ApplyResources(cultureComboBox, "cultureComboBox");
            cultureComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cultureComboBox.FormattingEnabled = true;
            _helpProvider.SetHelpString(cultureComboBox, resources.GetString("cultureComboBox.HelpString"));
            cultureComboBox.Name = "cultureComboBox";
            _helpProvider.SetShowHelp(cultureComboBox, (bool)resources.GetObject("cultureComboBox.ShowHelp"));
            cultureComboBox.Format += OnCultureComboBoxFormat;
            // 
            // label2
            // 
            label2.AutoEllipsis = true;
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // groupBox1
            // 
            resources.ApplyResources(groupBox1, "groupBox1");
            groupBox1.Controls.Add(cultureComboBox);
            groupBox1.Controls.Add(label2);
            groupBox1.Name = "groupBox1";
            groupBox1.TabStop = false;
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
            importRulesDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            importRulesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(importRulesDataGridView, "importRulesDataGridView");
            importRulesDataGridView.Name = "importRulesDataGridView";
            // 
            // groupBox2
            // 
            resources.ApplyResources(groupBox2, "groupBox2");
            groupBox2.Controls.Add(_tabControl);
            groupBox2.Name = "groupBox2";
            groupBox2.TabStop = false;
            // 
            // _tabControl
            // 
            _tabControl.Controls.Add(editorTabPage);
            _tabControl.Controls.Add(jsonTabPage);
            resources.ApplyResources(_tabControl, "_tabControl");
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
            // SettingsForm
            // 
            AcceptButton = acceptButton;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = cancelButton;
            Controls.Add(resetButton);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(acceptButton);
            Controls.Add(cancelButton);
            Name = "SettingsForm";
            _helpProvider.SetShowHelp(this, (bool)resources.GetObject("$this.ShowHelp"));
            ShowIcon = false;
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)importRulesDataGridView).EndInit();
            groupBox2.ResumeLayout(false);
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
        private System.Windows.Forms.ComboBox cultureComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.HelpProvider _helpProvider;
        private System.Windows.Forms.DataGridView importRulesDataGridView;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.TabControl _tabControl;
        private System.Windows.Forms.TabPage editorTabPage;
        private System.Windows.Forms.TabPage jsonTabPage;
        private System.Windows.Forms.TextBox _textBox;
        private System.Windows.Forms.ErrorProvider _errorProvider;
    }
}
