namespace Liber.Forms.Accounts
{
    partial class AccountForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                    components = null;
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountForm));
            cancelButton = new System.Windows.Forms.Button();
            acceptButton = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            nameTextBox = new System.Windows.Forms.TextBox();
            numberNumericUpDown = new System.Windows.Forms.NumericUpDown();
            typeComboBox = new System.Windows.Forms.ComboBox();
            descriptionTextBox = new System.Windows.Forms.TextBox();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            placeholderCheckBox = new System.Windows.Forms.CheckBox();
            parentComboBox = new AccountComboBox();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            taxTypeComboBox = new System.Windows.Forms.ComboBox();
            label8 = new System.Windows.Forms.Label();
            memoTextBox = new System.Windows.Forms.TextBox();
            _colorButton = new System.Windows.Forms.ColorButton();
            _helpProvider = new System.Windows.Forms.HelpProvider();
            adjustmentCheckBox = new System.Windows.Forms.CheckBox();
            inactiveCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)numberNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // cancelButton
            // 
            resources.ApplyResources(cancelButton, "cancelButton");
            _helpProvider.SetHelpKeyword(cancelButton, resources.GetString("cancelButton.HelpKeyword"));
            _helpProvider.SetHelpNavigator(cancelButton, (System.Windows.Forms.HelpNavigator)resources.GetObject("cancelButton.HelpNavigator"));
            _helpProvider.SetHelpString(cancelButton, resources.GetString("cancelButton.HelpString"));
            cancelButton.Name = "cancelButton";
            _helpProvider.SetShowHelp(cancelButton, (bool)resources.GetObject("cancelButton.ShowHelp"));
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += OnCancelButtonClick;
            // 
            // acceptButton
            // 
            resources.ApplyResources(acceptButton, "acceptButton");
            _helpProvider.SetHelpKeyword(acceptButton, resources.GetString("acceptButton.HelpKeyword"));
            _helpProvider.SetHelpNavigator(acceptButton, (System.Windows.Forms.HelpNavigator)resources.GetObject("acceptButton.HelpNavigator"));
            _helpProvider.SetHelpString(acceptButton, resources.GetString("acceptButton.HelpString"));
            acceptButton.Name = "acceptButton";
            _helpProvider.SetShowHelp(acceptButton, (bool)resources.GetObject("acceptButton.ShowHelp"));
            acceptButton.UseVisualStyleBackColor = true;
            acceptButton.Click += OnAcceptButtonClick;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            _helpProvider.SetHelpKeyword(label1, resources.GetString("label1.HelpKeyword"));
            _helpProvider.SetHelpNavigator(label1, (System.Windows.Forms.HelpNavigator)resources.GetObject("label1.HelpNavigator"));
            _helpProvider.SetHelpString(label1, resources.GetString("label1.HelpString"));
            label1.Name = "label1";
            _helpProvider.SetShowHelp(label1, (bool)resources.GetObject("label1.ShowHelp"));
            // 
            // nameTextBox
            // 
            resources.ApplyResources(nameTextBox, "nameTextBox");
            _helpProvider.SetHelpKeyword(nameTextBox, resources.GetString("nameTextBox.HelpKeyword"));
            _helpProvider.SetHelpNavigator(nameTextBox, (System.Windows.Forms.HelpNavigator)resources.GetObject("nameTextBox.HelpNavigator"));
            _helpProvider.SetHelpString(nameTextBox, resources.GetString("nameTextBox.HelpString"));
            nameTextBox.Name = "nameTextBox";
            _helpProvider.SetShowHelp(nameTextBox, (bool)resources.GetObject("nameTextBox.ShowHelp"));
            // 
            // numberNumericUpDown
            // 
            resources.ApplyResources(numberNumericUpDown, "numberNumericUpDown");
            numberNumericUpDown.DecimalPlaces = 2;
            _helpProvider.SetHelpKeyword(numberNumericUpDown, resources.GetString("numberNumericUpDown.HelpKeyword"));
            _helpProvider.SetHelpNavigator(numberNumericUpDown, (System.Windows.Forms.HelpNavigator)resources.GetObject("numberNumericUpDown.HelpNavigator"));
            _helpProvider.SetHelpString(numberNumericUpDown, resources.GetString("numberNumericUpDown.HelpString"));
            numberNumericUpDown.Name = "numberNumericUpDown";
            _helpProvider.SetShowHelp(numberNumericUpDown, (bool)resources.GetObject("numberNumericUpDown.ShowHelp"));
            numberNumericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // typeComboBox
            // 
            resources.ApplyResources(typeComboBox, "typeComboBox");
            typeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            typeComboBox.FormattingEnabled = true;
            _helpProvider.SetHelpKeyword(typeComboBox, resources.GetString("typeComboBox.HelpKeyword"));
            _helpProvider.SetHelpNavigator(typeComboBox, (System.Windows.Forms.HelpNavigator)resources.GetObject("typeComboBox.HelpNavigator"));
            _helpProvider.SetHelpString(typeComboBox, resources.GetString("typeComboBox.HelpString"));
            typeComboBox.Name = "typeComboBox";
            _helpProvider.SetShowHelp(typeComboBox, (bool)resources.GetObject("typeComboBox.ShowHelp"));
            typeComboBox.Format += OnTypeComboBoxFormat;
            // 
            // descriptionTextBox
            // 
            resources.ApplyResources(descriptionTextBox, "descriptionTextBox");
            _helpProvider.SetHelpKeyword(descriptionTextBox, resources.GetString("descriptionTextBox.HelpKeyword"));
            _helpProvider.SetHelpNavigator(descriptionTextBox, (System.Windows.Forms.HelpNavigator)resources.GetObject("descriptionTextBox.HelpNavigator"));
            _helpProvider.SetHelpString(descriptionTextBox, resources.GetString("descriptionTextBox.HelpString"));
            descriptionTextBox.Name = "descriptionTextBox";
            _helpProvider.SetShowHelp(descriptionTextBox, (bool)resources.GetObject("descriptionTextBox.ShowHelp"));
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            _helpProvider.SetHelpKeyword(label2, resources.GetString("label2.HelpKeyword"));
            _helpProvider.SetHelpNavigator(label2, (System.Windows.Forms.HelpNavigator)resources.GetObject("label2.HelpNavigator"));
            _helpProvider.SetHelpString(label2, resources.GetString("label2.HelpString"));
            label2.Name = "label2";
            _helpProvider.SetShowHelp(label2, (bool)resources.GetObject("label2.ShowHelp"));
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            _helpProvider.SetHelpKeyword(label3, resources.GetString("label3.HelpKeyword"));
            _helpProvider.SetHelpNavigator(label3, (System.Windows.Forms.HelpNavigator)resources.GetObject("label3.HelpNavigator"));
            _helpProvider.SetHelpString(label3, resources.GetString("label3.HelpString"));
            label3.Name = "label3";
            _helpProvider.SetShowHelp(label3, (bool)resources.GetObject("label3.ShowHelp"));
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            _helpProvider.SetHelpKeyword(label4, resources.GetString("label4.HelpKeyword"));
            _helpProvider.SetHelpNavigator(label4, (System.Windows.Forms.HelpNavigator)resources.GetObject("label4.HelpNavigator"));
            _helpProvider.SetHelpString(label4, resources.GetString("label4.HelpString"));
            label4.Name = "label4";
            _helpProvider.SetShowHelp(label4, (bool)resources.GetObject("label4.ShowHelp"));
            // 
            // placeholderCheckBox
            // 
            resources.ApplyResources(placeholderCheckBox, "placeholderCheckBox");
            _helpProvider.SetHelpKeyword(placeholderCheckBox, resources.GetString("placeholderCheckBox.HelpKeyword"));
            _helpProvider.SetHelpNavigator(placeholderCheckBox, (System.Windows.Forms.HelpNavigator)resources.GetObject("placeholderCheckBox.HelpNavigator"));
            _helpProvider.SetHelpString(placeholderCheckBox, resources.GetString("placeholderCheckBox.HelpString"));
            placeholderCheckBox.Name = "placeholderCheckBox";
            _helpProvider.SetShowHelp(placeholderCheckBox, (bool)resources.GetObject("placeholderCheckBox.ShowHelp"));
            placeholderCheckBox.UseVisualStyleBackColor = true;
            // 
            // parentComboBox
            // 
            resources.ApplyResources(parentComboBox, "parentComboBox");
            parentComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            parentComboBox.FormattingEnabled = true;
            _helpProvider.SetHelpKeyword(parentComboBox, resources.GetString("parentComboBox.HelpKeyword"));
            _helpProvider.SetHelpNavigator(parentComboBox, (System.Windows.Forms.HelpNavigator)resources.GetObject("parentComboBox.HelpNavigator"));
            _helpProvider.SetHelpString(parentComboBox, resources.GetString("parentComboBox.HelpString"));
            parentComboBox.Name = "parentComboBox";
            _helpProvider.SetShowHelp(parentComboBox, (bool)resources.GetObject("parentComboBox.ShowHelp"));
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            _helpProvider.SetHelpKeyword(label5, resources.GetString("label5.HelpKeyword"));
            _helpProvider.SetHelpNavigator(label5, (System.Windows.Forms.HelpNavigator)resources.GetObject("label5.HelpNavigator"));
            _helpProvider.SetHelpString(label5, resources.GetString("label5.HelpString"));
            label5.Name = "label5";
            _helpProvider.SetShowHelp(label5, (bool)resources.GetObject("label5.ShowHelp"));
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            _helpProvider.SetHelpKeyword(label6, resources.GetString("label6.HelpKeyword"));
            _helpProvider.SetHelpNavigator(label6, (System.Windows.Forms.HelpNavigator)resources.GetObject("label6.HelpNavigator"));
            _helpProvider.SetHelpString(label6, resources.GetString("label6.HelpString"));
            label6.Name = "label6";
            _helpProvider.SetShowHelp(label6, (bool)resources.GetObject("label6.ShowHelp"));
            // 
            // label7
            // 
            resources.ApplyResources(label7, "label7");
            _helpProvider.SetHelpKeyword(label7, resources.GetString("label7.HelpKeyword"));
            _helpProvider.SetHelpNavigator(label7, (System.Windows.Forms.HelpNavigator)resources.GetObject("label7.HelpNavigator"));
            _helpProvider.SetHelpString(label7, resources.GetString("label7.HelpString"));
            label7.Name = "label7";
            _helpProvider.SetShowHelp(label7, (bool)resources.GetObject("label7.ShowHelp"));
            // 
            // taxTypeComboBox
            // 
            resources.ApplyResources(taxTypeComboBox, "taxTypeComboBox");
            taxTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            taxTypeComboBox.FormattingEnabled = true;
            _helpProvider.SetHelpKeyword(taxTypeComboBox, resources.GetString("taxTypeComboBox.HelpKeyword"));
            _helpProvider.SetHelpNavigator(taxTypeComboBox, (System.Windows.Forms.HelpNavigator)resources.GetObject("taxTypeComboBox.HelpNavigator"));
            _helpProvider.SetHelpString(taxTypeComboBox, resources.GetString("taxTypeComboBox.HelpString"));
            taxTypeComboBox.Name = "taxTypeComboBox";
            _helpProvider.SetShowHelp(taxTypeComboBox, (bool)resources.GetObject("taxTypeComboBox.ShowHelp"));
            taxTypeComboBox.Format += OnTaxTypeComboBoxFormat;
            // 
            // label8
            // 
            resources.ApplyResources(label8, "label8");
            _helpProvider.SetHelpKeyword(label8, resources.GetString("label8.HelpKeyword"));
            _helpProvider.SetHelpNavigator(label8, (System.Windows.Forms.HelpNavigator)resources.GetObject("label8.HelpNavigator"));
            _helpProvider.SetHelpString(label8, resources.GetString("label8.HelpString"));
            label8.Name = "label8";
            _helpProvider.SetShowHelp(label8, (bool)resources.GetObject("label8.ShowHelp"));
            // 
            // memoTextBox
            // 
            resources.ApplyResources(memoTextBox, "memoTextBox");
            _helpProvider.SetHelpKeyword(memoTextBox, resources.GetString("memoTextBox.HelpKeyword"));
            _helpProvider.SetHelpNavigator(memoTextBox, (System.Windows.Forms.HelpNavigator)resources.GetObject("memoTextBox.HelpNavigator"));
            _helpProvider.SetHelpString(memoTextBox, resources.GetString("memoTextBox.HelpString"));
            memoTextBox.Name = "memoTextBox";
            _helpProvider.SetShowHelp(memoTextBox, (bool)resources.GetObject("memoTextBox.ShowHelp"));
            // 
            // _colorButton
            // 
            resources.ApplyResources(_colorButton, "_colorButton");
            _helpProvider.SetHelpKeyword(_colorButton, resources.GetString("_colorButton.HelpKeyword"));
            _helpProvider.SetHelpNavigator(_colorButton, (System.Windows.Forms.HelpNavigator)resources.GetObject("_colorButton.HelpNavigator"));
            _helpProvider.SetHelpString(_colorButton, resources.GetString("_colorButton.HelpString"));
            _colorButton.Name = "_colorButton";
            _helpProvider.SetShowHelp(_colorButton, (bool)resources.GetObject("_colorButton.ShowHelp"));
            _colorButton.UseVisualStyleBackColor = true;
            // 
            // _helpProvider
            // 
            resources.ApplyResources(_helpProvider, "_helpProvider");
            // 
            // adjustmentCheckBox
            // 
            resources.ApplyResources(adjustmentCheckBox, "adjustmentCheckBox");
            _helpProvider.SetHelpKeyword(adjustmentCheckBox, resources.GetString("adjustmentCheckBox.HelpKeyword"));
            _helpProvider.SetHelpNavigator(adjustmentCheckBox, (System.Windows.Forms.HelpNavigator)resources.GetObject("adjustmentCheckBox.HelpNavigator"));
            _helpProvider.SetHelpString(adjustmentCheckBox, resources.GetString("adjustmentCheckBox.HelpString"));
            adjustmentCheckBox.Name = "adjustmentCheckBox";
            _helpProvider.SetShowHelp(adjustmentCheckBox, (bool)resources.GetObject("adjustmentCheckBox.ShowHelp"));
            adjustmentCheckBox.UseVisualStyleBackColor = true;
            // 
            // inactiveCheckBox
            // 
            resources.ApplyResources(inactiveCheckBox, "inactiveCheckBox");
            _helpProvider.SetHelpKeyword(inactiveCheckBox, resources.GetString("inactiveCheckBox.HelpKeyword"));
            _helpProvider.SetHelpNavigator(inactiveCheckBox, (System.Windows.Forms.HelpNavigator)resources.GetObject("inactiveCheckBox.HelpNavigator"));
            _helpProvider.SetHelpString(inactiveCheckBox, resources.GetString("inactiveCheckBox.HelpString"));
            inactiveCheckBox.Name = "inactiveCheckBox";
            _helpProvider.SetShowHelp(inactiveCheckBox, (bool)resources.GetObject("inactiveCheckBox.ShowHelp"));
            inactiveCheckBox.UseVisualStyleBackColor = true;
            // 
            // AccountForm
            // 
            AcceptButton = acceptButton;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = cancelButton;
            Controls.Add(inactiveCheckBox);
            Controls.Add(adjustmentCheckBox);
            Controls.Add(taxTypeComboBox);
            Controls.Add(label8);
            Controls.Add(_colorButton);
            Controls.Add(label7);
            Controls.Add(memoTextBox);
            Controls.Add(label6);
            Controls.Add(descriptionTextBox);
            Controls.Add(label5);
            Controls.Add(parentComboBox);
            Controls.Add(label4);
            Controls.Add(placeholderCheckBox);
            Controls.Add(typeComboBox);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(numberNumericUpDown);
            Controls.Add(nameTextBox);
            Controls.Add(label1);
            Controls.Add(acceptButton);
            Controls.Add(cancelButton);
            _helpProvider.SetHelpKeyword(this, resources.GetString("$this.HelpKeyword"));
            _helpProvider.SetHelpNavigator(this, (System.Windows.Forms.HelpNavigator)resources.GetObject("$this.HelpNavigator"));
            _helpProvider.SetHelpString(this, resources.GetString("$this.HelpString"));
            MaximizeBox = false;
            Name = "AccountForm";
            _helpProvider.SetShowHelp(this, (bool)resources.GetObject("$this.ShowHelp"));
            ShowIcon = false;
            ((System.ComponentModel.ISupportInitialize)numberNumericUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.HelpProvider _helpProvider;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        protected System.Windows.Forms.TextBox nameTextBox;
        protected System.Windows.Forms.NumericUpDown numberNumericUpDown;
        protected System.Windows.Forms.CheckBox placeholderCheckBox;
        protected System.Windows.Forms.TextBox memoTextBox;
        protected System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.ComboBox typeComboBox;
        protected System.Windows.Forms.ColorButton _colorButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox taxTypeComboBox;
        private System.Windows.Forms.Label label8;
        protected AccountComboBox parentComboBox;
        protected System.Windows.Forms.CheckBox adjustmentCheckBox;
        protected System.Windows.Forms.CheckBox inactiveCheckBox;
    }
}
