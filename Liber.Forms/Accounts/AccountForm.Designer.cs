namespace Liber.Forms.Accounts
{
    partial class AccountForm
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
            parentComboBox = new System.Windows.Forms.ComboBox();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            taxTypeComboBox = new System.Windows.Forms.ComboBox();
            label8 = new System.Windows.Forms.Label();
            notesTextBox = new System.Windows.Forms.TextBox();
            colorButton = new System.Windows.Forms.Button();
            _helpProvider = new System.Windows.Forms.HelpProvider();
            _colorDialog = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)numberNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // cancelButton
            // 
            resources.ApplyResources(cancelButton, "cancelButton");
            cancelButton.Name = "cancelButton";
            _helpProvider.SetShowHelp(cancelButton, (bool)resources.GetObject("cancelButton.ShowHelp"));
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += OnCancelButtonClick;
            // 
            // acceptButton
            // 
            resources.ApplyResources(acceptButton, "acceptButton");
            acceptButton.Name = "acceptButton";
            _helpProvider.SetShowHelp(acceptButton, (bool)resources.GetObject("acceptButton.ShowHelp"));
            acceptButton.UseVisualStyleBackColor = true;
            acceptButton.Click += OnAcceptButtonClick;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            _helpProvider.SetShowHelp(label1, (bool)resources.GetObject("label1.ShowHelp"));
            // 
            // nameTextBox
            // 
            resources.ApplyResources(nameTextBox, "nameTextBox");
            _helpProvider.SetHelpString(nameTextBox, resources.GetString("nameTextBox.HelpString"));
            nameTextBox.Name = "nameTextBox";
            _helpProvider.SetShowHelp(nameTextBox, (bool)resources.GetObject("nameTextBox.ShowHelp"));
            // 
            // numberNumericUpDown
            // 
            resources.ApplyResources(numberNumericUpDown, "numberNumericUpDown");
            numberNumericUpDown.DecimalPlaces = 2;
            _helpProvider.SetHelpString(numberNumericUpDown, resources.GetString("numberNumericUpDown.HelpString"));
            numberNumericUpDown.Maximum = new decimal(new int[] { 9999999, 0, 0, 0 });
            numberNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numberNumericUpDown.Name = "numberNumericUpDown";
            _helpProvider.SetShowHelp(numberNumericUpDown, (bool)resources.GetObject("numberNumericUpDown.ShowHelp"));
            numberNumericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // typeComboBox
            // 
            resources.ApplyResources(typeComboBox, "typeComboBox");
            typeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            typeComboBox.FormattingEnabled = true;
            _helpProvider.SetHelpString(typeComboBox, resources.GetString("typeComboBox.HelpString"));
            typeComboBox.Name = "typeComboBox";
            _helpProvider.SetShowHelp(typeComboBox, (bool)resources.GetObject("typeComboBox.ShowHelp"));
            typeComboBox.Format += OnTypeComboBoxFormat;
            // 
            // descriptionTextBox
            // 
            resources.ApplyResources(descriptionTextBox, "descriptionTextBox");
            _helpProvider.SetHelpString(descriptionTextBox, resources.GetString("descriptionTextBox.HelpString"));
            descriptionTextBox.Name = "descriptionTextBox";
            _helpProvider.SetShowHelp(descriptionTextBox, (bool)resources.GetObject("descriptionTextBox.ShowHelp"));
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            _helpProvider.SetShowHelp(label2, (bool)resources.GetObject("label2.ShowHelp"));
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
            // placeholderCheckBox
            // 
            resources.ApplyResources(placeholderCheckBox, "placeholderCheckBox");
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
            _helpProvider.SetHelpString(parentComboBox, resources.GetString("parentComboBox.HelpString"));
            parentComboBox.Name = "parentComboBox";
            _helpProvider.SetShowHelp(parentComboBox, (bool)resources.GetObject("parentComboBox.ShowHelp"));
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            _helpProvider.SetShowHelp(label5, (bool)resources.GetObject("label5.ShowHelp"));
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            label6.Name = "label6";
            _helpProvider.SetShowHelp(label6, (bool)resources.GetObject("label6.ShowHelp"));
            // 
            // label7
            // 
            resources.ApplyResources(label7, "label7");
            label7.Name = "label7";
            _helpProvider.SetShowHelp(label7, (bool)resources.GetObject("label7.ShowHelp"));
            // 
            // taxTypeComboBox
            // 
            resources.ApplyResources(taxTypeComboBox, "taxTypeComboBox");
            taxTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            taxTypeComboBox.FormattingEnabled = true;
            _helpProvider.SetHelpString(taxTypeComboBox, resources.GetString("taxTypeComboBox.HelpString"));
            taxTypeComboBox.Name = "taxTypeComboBox";
            _helpProvider.SetShowHelp(taxTypeComboBox, (bool)resources.GetObject("taxTypeComboBox.ShowHelp"));
            taxTypeComboBox.Format += OnTaxTypeComboBoxFormat;
            // 
            // label8
            // 
            resources.ApplyResources(label8, "label8");
            label8.Name = "label8";
            _helpProvider.SetShowHelp(label8, (bool)resources.GetObject("label8.ShowHelp"));
            // 
            // notesTextBox
            // 
            resources.ApplyResources(notesTextBox, "notesTextBox");
            notesTextBox.Name = "notesTextBox";
            _helpProvider.SetShowHelp(notesTextBox, (bool)resources.GetObject("notesTextBox.ShowHelp"));
            // 
            // colorButton
            // 
            resources.ApplyResources(colorButton, "colorButton");
            colorButton.Name = "colorButton";
            _helpProvider.SetShowHelp(colorButton, (bool)resources.GetObject("colorButton.ShowHelp"));
            colorButton.UseVisualStyleBackColor = true;
            colorButton.Click += OnColorButtonClick;
            // 
            // _colorDialog
            // 
            _colorDialog.AnyColor = true;
            _colorDialog.FullOpen = true;
            _colorDialog.SolidColorOnly = true;
            // 
            // AccountForm
            // 
            AcceptButton = acceptButton;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = cancelButton;
            Controls.Add(taxTypeComboBox);
            Controls.Add(label8);
            Controls.Add(colorButton);
            Controls.Add(label7);
            Controls.Add(notesTextBox);
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
        private System.Windows.Forms.ComboBox parentComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        protected System.Windows.Forms.TextBox nameTextBox;
        protected System.Windows.Forms.NumericUpDown numberNumericUpDown;
        protected System.Windows.Forms.CheckBox placeholderCheckBox;
        protected System.Windows.Forms.TextBox notesTextBox;
        protected System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.ComboBox typeComboBox;
        private System.Windows.Forms.Button colorButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ColorDialog _colorDialog;
        private System.Windows.Forms.ComboBox taxTypeComboBox;
        private System.Windows.Forms.Label label8;
    }
}