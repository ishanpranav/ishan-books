namespace Liber.Forms
{
    partial class CultureForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CultureForm));
            acceptButton = new System.Windows.Forms.Button();
            cancelButton = new System.Windows.Forms.Button();
            cultureComboBox = new System.Windows.Forms.ComboBox();
            _helpProvider = new System.Windows.Forms.HelpProvider();
            resetButton = new System.Windows.Forms.Button();
            label2 = new System.Windows.Forms.Label();
            _errorProvider = new System.Windows.Forms.ErrorProvider(components);
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
            // cultureComboBox
            // 
            resources.ApplyResources(cultureComboBox, "cultureComboBox");
            cultureComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            _errorProvider.SetError(cultureComboBox, resources.GetString("cultureComboBox.Error"));
            cultureComboBox.FormattingEnabled = true;
            _helpProvider.SetHelpKeyword(cultureComboBox, resources.GetString("cultureComboBox.HelpKeyword"));
            _helpProvider.SetHelpNavigator(cultureComboBox, (System.Windows.Forms.HelpNavigator)resources.GetObject("cultureComboBox.HelpNavigator"));
            _helpProvider.SetHelpString(cultureComboBox, resources.GetString("cultureComboBox.HelpString"));
            _errorProvider.SetIconAlignment(cultureComboBox, (System.Windows.Forms.ErrorIconAlignment)resources.GetObject("cultureComboBox.IconAlignment"));
            _errorProvider.SetIconPadding(cultureComboBox, (int)resources.GetObject("cultureComboBox.IconPadding"));
            cultureComboBox.Name = "cultureComboBox";
            _helpProvider.SetShowHelp(cultureComboBox, (bool)resources.GetObject("cultureComboBox.ShowHelp"));
            cultureComboBox.Format += OnCultureComboBoxFormat;
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
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.AutoEllipsis = true;
            _errorProvider.SetError(label2, resources.GetString("label2.Error"));
            _helpProvider.SetHelpKeyword(label2, resources.GetString("label2.HelpKeyword"));
            _helpProvider.SetHelpNavigator(label2, (System.Windows.Forms.HelpNavigator)resources.GetObject("label2.HelpNavigator"));
            _helpProvider.SetHelpString(label2, resources.GetString("label2.HelpString"));
            _errorProvider.SetIconAlignment(label2, (System.Windows.Forms.ErrorIconAlignment)resources.GetObject("label2.IconAlignment"));
            _errorProvider.SetIconPadding(label2, (int)resources.GetObject("label2.IconPadding"));
            label2.Name = "label2";
            _helpProvider.SetShowHelp(label2, (bool)resources.GetObject("label2.ShowHelp"));
            // 
            // _errorProvider
            // 
            _errorProvider.ContainerControl = this;
            resources.ApplyResources(_errorProvider, "_errorProvider");
            // 
            // CultureForm
            // 
            AcceptButton = acceptButton;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = cancelButton;
            Controls.Add(cultureComboBox);
            Controls.Add(resetButton);
            Controls.Add(label2);
            Controls.Add(acceptButton);
            Controls.Add(cancelButton);
            _helpProvider.SetHelpKeyword(this, resources.GetString("$this.HelpKeyword"));
            _helpProvider.SetHelpNavigator(this, (System.Windows.Forms.HelpNavigator)resources.GetObject("$this.HelpNavigator"));
            _helpProvider.SetHelpString(this, resources.GetString("$this.HelpString"));
            MaximizeBox = false;
            Name = "CultureForm";
            _helpProvider.SetShowHelp(this, (bool)resources.GetObject("$this.ShowHelp"));
            ShowIcon = false;
            ((System.ComponentModel.ISupportInitialize)_errorProvider).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ComboBox cultureComboBox;
        private System.Windows.Forms.HelpProvider _helpProvider;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.ErrorProvider _errorProvider;
        private System.Windows.Forms.Label label2;
    }
}
