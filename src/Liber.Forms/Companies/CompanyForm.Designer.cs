namespace Liber.Forms.Companies;

partial class CompanyForm
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CompanyForm));
        label1 = new System.Windows.Forms.Label();
        nameTextBox = new System.Windows.Forms.TextBox();
        cancelButton = new System.Windows.Forms.Button();
        acceptButton = new System.Windows.Forms.Button();
        _helpProvider = new System.Windows.Forms.HelpProvider();
        passwordTextBox = new System.Windows.Forms.TextBox();
        label3 = new System.Windows.Forms.Label();
        _colorButton = new System.Windows.Forms.ColorButton();
        label2 = new System.Windows.Forms.Label();
        equityAccountComboBox = new Accounts.AccountComboBox();
        label5 = new System.Windows.Forms.Label();
        otherEquityAccountComboBox = new Accounts.AccountComboBox();
        closingAccountsGroupBox = new System.Windows.Forms.GroupBox();
        label4 = new System.Windows.Forms.Label();
        label6 = new System.Windows.Forms.Label();
        typeComboBox = new System.Windows.Forms.ComboBox();
        closingAccountsGroupBox.SuspendLayout();
        SuspendLayout();
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
        // cancelButton
        // 
        resources.ApplyResources(cancelButton, "cancelButton");
        _helpProvider.SetHelpString(cancelButton, resources.GetString("cancelButton.HelpString"));
        cancelButton.Name = "cancelButton";
        _helpProvider.SetShowHelp(cancelButton, (bool)resources.GetObject("cancelButton.ShowHelp"));
        cancelButton.UseVisualStyleBackColor = true;
        cancelButton.Click += OnCancelButtonClick;
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
        // passwordTextBox
        // 
        resources.ApplyResources(passwordTextBox, "passwordTextBox");
        _helpProvider.SetHelpString(passwordTextBox, resources.GetString("passwordTextBox.HelpString"));
        passwordTextBox.Name = "passwordTextBox";
        _helpProvider.SetShowHelp(passwordTextBox, (bool)resources.GetObject("passwordTextBox.ShowHelp"));
        passwordTextBox.UseSystemPasswordChar = true;
        // 
        // label3
        // 
        resources.ApplyResources(label3, "label3");
        label3.Name = "label3";
        _helpProvider.SetShowHelp(label3, (bool)resources.GetObject("label3.ShowHelp"));
        // 
        // _colorButton
        // 
        resources.ApplyResources(_colorButton, "_colorButton");
        _helpProvider.SetHelpString(_colorButton, resources.GetString("_colorButton.HelpString"));
        _colorButton.Name = "_colorButton";
        _helpProvider.SetShowHelp(_colorButton, (bool)resources.GetObject("_colorButton.ShowHelp"));
        _colorButton.UseVisualStyleBackColor = true;
        // 
        // label2
        // 
        resources.ApplyResources(label2, "label2");
        label2.Name = "label2";
        _helpProvider.SetShowHelp(label2, (bool)resources.GetObject("label2.ShowHelp"));
        // 
        // equityAccountComboBox
        // 
        resources.ApplyResources(equityAccountComboBox, "equityAccountComboBox");
        equityAccountComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        equityAccountComboBox.FormattingEnabled = true;
        _helpProvider.SetHelpString(equityAccountComboBox, resources.GetString("equityAccountComboBox.HelpString"));
        equityAccountComboBox.Name = "equityAccountComboBox";
        _helpProvider.SetShowHelp(equityAccountComboBox, (bool)resources.GetObject("equityAccountComboBox.ShowHelp"));
        // 
        // label5
        // 
        resources.ApplyResources(label5, "label5");
        label5.Name = "label5";
        _helpProvider.SetShowHelp(label5, (bool)resources.GetObject("label5.ShowHelp"));
        // 
        // otherEquityAccountComboBox
        // 
        resources.ApplyResources(otherEquityAccountComboBox, "otherEquityAccountComboBox");
        otherEquityAccountComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        otherEquityAccountComboBox.FormattingEnabled = true;
        _helpProvider.SetHelpString(otherEquityAccountComboBox, resources.GetString("otherEquityAccountComboBox.HelpString"));
        otherEquityAccountComboBox.Name = "otherEquityAccountComboBox";
        _helpProvider.SetShowHelp(otherEquityAccountComboBox, (bool)resources.GetObject("otherEquityAccountComboBox.ShowHelp"));
        // 
        // closingAccountsGroupBox
        // 
        resources.ApplyResources(closingAccountsGroupBox, "closingAccountsGroupBox");
        closingAccountsGroupBox.Controls.Add(label5);
        closingAccountsGroupBox.Controls.Add(otherEquityAccountComboBox);
        closingAccountsGroupBox.Controls.Add(label4);
        closingAccountsGroupBox.Controls.Add(equityAccountComboBox);
        closingAccountsGroupBox.Name = "closingAccountsGroupBox";
        _helpProvider.SetShowHelp(closingAccountsGroupBox, (bool)resources.GetObject("closingAccountsGroupBox.ShowHelp"));
        closingAccountsGroupBox.TabStop = false;
        // 
        // label4
        // 
        resources.ApplyResources(label4, "label4");
        label4.Name = "label4";
        _helpProvider.SetShowHelp(label4, (bool)resources.GetObject("label4.ShowHelp"));
        // 
        // label6
        // 
        resources.ApplyResources(label6, "label6");
        label6.Name = "label6";
        _helpProvider.SetShowHelp(label6, (bool)resources.GetObject("label6.ShowHelp"));
        // 
        // typeComboBox
        // 
        resources.ApplyResources(typeComboBox, "typeComboBox");
        typeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        typeComboBox.FormattingEnabled = true;
        typeComboBox.Name = "typeComboBox";
        typeComboBox.Format += OnTypeComboBoxFormat;
        // 
        // CompanyForm
        // 
        AcceptButton = acceptButton;
        resources.ApplyResources(this, "$this");
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        CancelButton = cancelButton;
        Controls.Add(label6);
        Controls.Add(typeComboBox);
        Controls.Add(closingAccountsGroupBox);
        Controls.Add(passwordTextBox);
        Controls.Add(label3);
        Controls.Add(label2);
        Controls.Add(_colorButton);
        Controls.Add(acceptButton);
        Controls.Add(cancelButton);
        Controls.Add(nameTextBox);
        Controls.Add(label1);
        MaximizeBox = false;
        Name = "CompanyForm";
        _helpProvider.SetShowHelp(this, (bool)resources.GetObject("$this.ShowHelp"));
        ShowIcon = false;
        closingAccountsGroupBox.ResumeLayout(false);
        closingAccountsGroupBox.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox nameTextBox;
    private System.Windows.Forms.Button acceptButton;
    private System.Windows.Forms.HelpProvider _helpProvider;
    private System.Windows.Forms.ColorButton _colorButton;
    private System.Windows.Forms.Label label2;
    protected System.Windows.Forms.TextBox passwordTextBox;
    private System.Windows.Forms.Label label3;
    private Accounts.AccountComboBox equityAccountComboBox;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;
    private Accounts.AccountComboBox otherEquityAccountComboBox;
    protected System.Windows.Forms.GroupBox closingAccountsGroupBox;
    private System.Windows.Forms.ComboBox typeComboBox;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Button cancelButton;
}
