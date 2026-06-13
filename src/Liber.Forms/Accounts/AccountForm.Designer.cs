using System.Windows.Forms;

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
            cancelButton = new Button();
            acceptButton = new Button();
            label1 = new Label();
            nameTextBox = new TextBox();
            numberNumericUpDown = new NumericUpDown();
            descriptionTextBox = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            placeholderCheckBox = new CheckBox();
            parentComboBox = new ComboBox();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            memoTextBox = new TextBox();
            _colorButton = new ColorButton();
            _helpProvider = new HelpProvider();
            inactiveCheckBox = new CheckBox();
            cashFlowComboBox = new ComboBox();
            label9 = new Label();
            typeComboBox = new ComboBox();
            taxTypeCheckBox = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)numberNumericUpDown).BeginInit();
            SuspendLayout();
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
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            _helpProvider.SetHelpKeyword(label1, resources.GetString("label1.HelpKeyword"));
            _helpProvider.SetHelpNavigator(label1, (HelpNavigator)resources.GetObject("label1.HelpNavigator"));
            _helpProvider.SetHelpString(label1, resources.GetString("label1.HelpString"));
            label1.Name = "label1";
            _helpProvider.SetShowHelp(label1, (bool)resources.GetObject("label1.ShowHelp"));
            // 
            // nameTextBox
            // 
            resources.ApplyResources(nameTextBox, "nameTextBox");
            _helpProvider.SetHelpKeyword(nameTextBox, resources.GetString("nameTextBox.HelpKeyword"));
            _helpProvider.SetHelpNavigator(nameTextBox, (HelpNavigator)resources.GetObject("nameTextBox.HelpNavigator"));
            _helpProvider.SetHelpString(nameTextBox, resources.GetString("nameTextBox.HelpString"));
            nameTextBox.Name = "nameTextBox";
            _helpProvider.SetShowHelp(nameTextBox, (bool)resources.GetObject("nameTextBox.ShowHelp"));
            // 
            // numberNumericUpDown
            // 
            resources.ApplyResources(numberNumericUpDown, "numberNumericUpDown");
            numberNumericUpDown.DecimalPlaces = 2;
            _helpProvider.SetHelpKeyword(numberNumericUpDown, resources.GetString("numberNumericUpDown.HelpKeyword"));
            _helpProvider.SetHelpNavigator(numberNumericUpDown, (HelpNavigator)resources.GetObject("numberNumericUpDown.HelpNavigator"));
            _helpProvider.SetHelpString(numberNumericUpDown, resources.GetString("numberNumericUpDown.HelpString"));
            numberNumericUpDown.Name = "numberNumericUpDown";
            _helpProvider.SetShowHelp(numberNumericUpDown, (bool)resources.GetObject("numberNumericUpDown.ShowHelp"));
            numberNumericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // descriptionTextBox
            // 
            resources.ApplyResources(descriptionTextBox, "descriptionTextBox");
            _helpProvider.SetHelpKeyword(descriptionTextBox, resources.GetString("descriptionTextBox.HelpKeyword"));
            _helpProvider.SetHelpNavigator(descriptionTextBox, (HelpNavigator)resources.GetObject("descriptionTextBox.HelpNavigator"));
            _helpProvider.SetHelpString(descriptionTextBox, resources.GetString("descriptionTextBox.HelpString"));
            descriptionTextBox.Name = "descriptionTextBox";
            _helpProvider.SetShowHelp(descriptionTextBox, (bool)resources.GetObject("descriptionTextBox.ShowHelp"));
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
            // placeholderCheckBox
            // 
            resources.ApplyResources(placeholderCheckBox, "placeholderCheckBox");
            _helpProvider.SetHelpKeyword(placeholderCheckBox, resources.GetString("placeholderCheckBox.HelpKeyword"));
            _helpProvider.SetHelpNavigator(placeholderCheckBox, (HelpNavigator)resources.GetObject("placeholderCheckBox.HelpNavigator"));
            _helpProvider.SetHelpString(placeholderCheckBox, resources.GetString("placeholderCheckBox.HelpString"));
            placeholderCheckBox.Name = "placeholderCheckBox";
            _helpProvider.SetShowHelp(placeholderCheckBox, (bool)resources.GetObject("placeholderCheckBox.ShowHelp"));
            placeholderCheckBox.UseVisualStyleBackColor = true;
            // 
            // parentComboBox
            // 
            resources.ApplyResources(parentComboBox, "parentComboBox");
            parentComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            parentComboBox.FormattingEnabled = true;
            _helpProvider.SetHelpKeyword(parentComboBox, resources.GetString("parentComboBox.HelpKeyword"));
            _helpProvider.SetHelpNavigator(parentComboBox, (HelpNavigator)resources.GetObject("parentComboBox.HelpNavigator"));
            _helpProvider.SetHelpString(parentComboBox, resources.GetString("parentComboBox.HelpString"));
            parentComboBox.Name = "parentComboBox";
            _helpProvider.SetShowHelp(parentComboBox, (bool)resources.GetObject("parentComboBox.ShowHelp"));
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            _helpProvider.SetHelpKeyword(label5, resources.GetString("label5.HelpKeyword"));
            _helpProvider.SetHelpNavigator(label5, (HelpNavigator)resources.GetObject("label5.HelpNavigator"));
            _helpProvider.SetHelpString(label5, resources.GetString("label5.HelpString"));
            label5.Name = "label5";
            _helpProvider.SetShowHelp(label5, (bool)resources.GetObject("label5.ShowHelp"));
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            _helpProvider.SetHelpKeyword(label6, resources.GetString("label6.HelpKeyword"));
            _helpProvider.SetHelpNavigator(label6, (HelpNavigator)resources.GetObject("label6.HelpNavigator"));
            _helpProvider.SetHelpString(label6, resources.GetString("label6.HelpString"));
            label6.Name = "label6";
            _helpProvider.SetShowHelp(label6, (bool)resources.GetObject("label6.ShowHelp"));
            // 
            // label7
            // 
            resources.ApplyResources(label7, "label7");
            _helpProvider.SetHelpKeyword(label7, resources.GetString("label7.HelpKeyword"));
            _helpProvider.SetHelpNavigator(label7, (HelpNavigator)resources.GetObject("label7.HelpNavigator"));
            _helpProvider.SetHelpString(label7, resources.GetString("label7.HelpString"));
            label7.Name = "label7";
            _helpProvider.SetShowHelp(label7, (bool)resources.GetObject("label7.ShowHelp"));
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
            // _colorButton
            // 
            resources.ApplyResources(_colorButton, "_colorButton");
            _helpProvider.SetHelpKeyword(_colorButton, resources.GetString("_colorButton.HelpKeyword"));
            _helpProvider.SetHelpNavigator(_colorButton, (HelpNavigator)resources.GetObject("_colorButton.HelpNavigator"));
            _helpProvider.SetHelpString(_colorButton, resources.GetString("_colorButton.HelpString"));
            _colorButton.Name = "_colorButton";
            _helpProvider.SetShowHelp(_colorButton, (bool)resources.GetObject("_colorButton.ShowHelp"));
            _colorButton.UseVisualStyleBackColor = true;
            // 
            // _helpProvider
            // 
            resources.ApplyResources(_helpProvider, "_helpProvider");
            // 
            // inactiveCheckBox
            // 
            resources.ApplyResources(inactiveCheckBox, "inactiveCheckBox");
            _helpProvider.SetHelpKeyword(inactiveCheckBox, resources.GetString("inactiveCheckBox.HelpKeyword"));
            _helpProvider.SetHelpNavigator(inactiveCheckBox, (HelpNavigator)resources.GetObject("inactiveCheckBox.HelpNavigator"));
            _helpProvider.SetHelpString(inactiveCheckBox, resources.GetString("inactiveCheckBox.HelpString"));
            inactiveCheckBox.Name = "inactiveCheckBox";
            _helpProvider.SetShowHelp(inactiveCheckBox, (bool)resources.GetObject("inactiveCheckBox.ShowHelp"));
            inactiveCheckBox.UseVisualStyleBackColor = true;
            // 
            // cashFlowComboBox
            // 
            resources.ApplyResources(cashFlowComboBox, "cashFlowComboBox");
            cashFlowComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            cashFlowComboBox.FormattingEnabled = true;
            _helpProvider.SetHelpKeyword(cashFlowComboBox, resources.GetString("cashFlowComboBox.HelpKeyword"));
            _helpProvider.SetHelpNavigator(cashFlowComboBox, (HelpNavigator)resources.GetObject("cashFlowComboBox.HelpNavigator"));
            _helpProvider.SetHelpString(cashFlowComboBox, resources.GetString("cashFlowComboBox.HelpString"));
            cashFlowComboBox.Name = "cashFlowComboBox";
            _helpProvider.SetShowHelp(cashFlowComboBox, (bool)resources.GetObject("cashFlowComboBox.ShowHelp"));
            cashFlowComboBox.Format += OnCashFlowComboBoxFormat;
            // 
            // label9
            // 
            resources.ApplyResources(label9, "label9");
            _helpProvider.SetHelpKeyword(label9, resources.GetString("label9.HelpKeyword"));
            _helpProvider.SetHelpNavigator(label9, (HelpNavigator)resources.GetObject("label9.HelpNavigator"));
            _helpProvider.SetHelpString(label9, resources.GetString("label9.HelpString"));
            label9.Name = "label9";
            _helpProvider.SetShowHelp(label9, (bool)resources.GetObject("label9.ShowHelp"));
            // 
            // typeComboBox
            // 
            resources.ApplyResources(typeComboBox, "typeComboBox");
            typeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            typeComboBox.FormattingEnabled = true;
            _helpProvider.SetHelpKeyword(typeComboBox, resources.GetString("typeComboBox.HelpKeyword"));
            _helpProvider.SetHelpNavigator(typeComboBox, (HelpNavigator)resources.GetObject("typeComboBox.HelpNavigator"));
            _helpProvider.SetHelpString(typeComboBox, resources.GetString("typeComboBox.HelpString"));
            typeComboBox.Name = "typeComboBox";
            _helpProvider.SetShowHelp(typeComboBox, (bool)resources.GetObject("typeComboBox.ShowHelp"));
            typeComboBox.SelectedIndexChanged += OnTypeComboBoxSelectedIndexChanged;
            typeComboBox.Format += OnTypeComboBoxFormat;
            // 
            // taxTypeCheckBox
            // 
            resources.ApplyResources(taxTypeCheckBox, "taxTypeCheckBox");
            _helpProvider.SetHelpKeyword(taxTypeCheckBox, resources.GetString("taxTypeCheckBox.HelpKeyword"));
            _helpProvider.SetHelpNavigator(taxTypeCheckBox, (HelpNavigator)resources.GetObject("taxTypeCheckBox.HelpNavigator"));
            _helpProvider.SetHelpString(taxTypeCheckBox, resources.GetString("taxTypeCheckBox.HelpString"));
            taxTypeCheckBox.Name = "taxTypeCheckBox";
            _helpProvider.SetShowHelp(taxTypeCheckBox, (bool)resources.GetObject("taxTypeCheckBox.ShowHelp"));
            taxTypeCheckBox.UseVisualStyleBackColor = true;
            // 
            // AccountForm
            // 
            AcceptButton = acceptButton;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = cancelButton;
            Controls.Add(taxTypeCheckBox);
            Controls.Add(cashFlowComboBox);
            Controls.Add(label9);
            Controls.Add(inactiveCheckBox);
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
            _helpProvider.SetHelpNavigator(this, (HelpNavigator)resources.GetObject("$this.HelpNavigator"));
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
        protected System.Windows.Forms.ColorButton _colorButton;
        private System.Windows.Forms.Label label7;
        protected ComboBox parentComboBox;
        protected System.Windows.Forms.CheckBox inactiveCheckBox;
        private System.Windows.Forms.Label label9;
        protected System.Windows.Forms.CheckBox taxTypeCheckBox;
        protected System.Windows.Forms.ComboBox cashFlowComboBox;
        protected System.Windows.Forms.ComboBox typeComboBox;
    }
}
