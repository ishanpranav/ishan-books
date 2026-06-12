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
            // resetButton
            // 
            resources.ApplyResources(resetButton, "resetButton");
            _helpProvider.SetHelpString(resetButton, resources.GetString("resetButton.HelpString"));
            resetButton.Name = "resetButton";
            _helpProvider.SetShowHelp(resetButton, (bool)resources.GetObject("resetButton.ShowHelp"));
            resetButton.UseVisualStyleBackColor = true;
            resetButton.Click += OnResetButtonClick;
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.AutoEllipsis = true;
            label2.Name = "label2";
            _helpProvider.SetShowHelp(label2, (bool)resources.GetObject("label2.ShowHelp"));
            // 
            // _errorProvider
            // 
            _errorProvider.ContainerControl = this;
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
