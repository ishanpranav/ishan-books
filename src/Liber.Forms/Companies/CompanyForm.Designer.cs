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
        SuspendLayout();
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
        // _helpProvider
        // 
        resources.ApplyResources(_helpProvider, "_helpProvider");
        // 
        // passwordTextBox
        // 
        resources.ApplyResources(passwordTextBox, "passwordTextBox");
        _helpProvider.SetHelpKeyword(passwordTextBox, resources.GetString("passwordTextBox.HelpKeyword"));
        _helpProvider.SetHelpNavigator(passwordTextBox, (System.Windows.Forms.HelpNavigator)resources.GetObject("passwordTextBox.HelpNavigator"));
        _helpProvider.SetHelpString(passwordTextBox, resources.GetString("passwordTextBox.HelpString"));
        passwordTextBox.Name = "passwordTextBox";
        _helpProvider.SetShowHelp(passwordTextBox, (bool)resources.GetObject("passwordTextBox.ShowHelp"));
        passwordTextBox.UseSystemPasswordChar = true;
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
        // label2
        // 
        resources.ApplyResources(label2, "label2");
        _helpProvider.SetHelpKeyword(label2, resources.GetString("label2.HelpKeyword"));
        _helpProvider.SetHelpNavigator(label2, (System.Windows.Forms.HelpNavigator)resources.GetObject("label2.HelpNavigator"));
        _helpProvider.SetHelpString(label2, resources.GetString("label2.HelpString"));
        label2.Name = "label2";
        _helpProvider.SetShowHelp(label2, (bool)resources.GetObject("label2.ShowHelp"));
        // 
        // CompanyForm
        // 
        AcceptButton = acceptButton;
        resources.ApplyResources(this, "$this");
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        CancelButton = cancelButton;
        Controls.Add(passwordTextBox);
        Controls.Add(label3);
        Controls.Add(label2);
        Controls.Add(_colorButton);
        Controls.Add(acceptButton);
        Controls.Add(cancelButton);
        Controls.Add(nameTextBox);
        Controls.Add(label1);
        _helpProvider.SetHelpKeyword(this, resources.GetString("$this.HelpKeyword"));
        _helpProvider.SetHelpNavigator(this, (System.Windows.Forms.HelpNavigator)resources.GetObject("$this.HelpNavigator"));
        _helpProvider.SetHelpString(this, resources.GetString("$this.HelpString"));
        MaximizeBox = false;
        Name = "CompanyForm";
        _helpProvider.SetShowHelp(this, (bool)resources.GetObject("$this.ShowHelp"));
        ShowIcon = false;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox nameTextBox;
    private System.Windows.Forms.Button acceptButton;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.HelpProvider _helpProvider;
    private System.Windows.Forms.ColorButton _colorButton;
    private System.Windows.Forms.Label label2;
    protected System.Windows.Forms.TextBox passwordTextBox;
    private System.Windows.Forms.Label label3;
}
