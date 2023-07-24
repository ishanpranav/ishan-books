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
        _colorButton = new System.Windows.Forms.ColorButton();
        label2 = new System.Windows.Forms.Label();
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
        // _colorButton
        // 
        resources.ApplyResources(_colorButton, "_colorButton");
        _colorButton.Name = "_colorButton";
        _colorButton.UseVisualStyleBackColor = true;
        // 
        // label2
        // 
        resources.ApplyResources(label2, "label2");
        label2.Name = "label2";
        // 
        // CompanyForm
        // 
        AcceptButton = acceptButton;
        resources.ApplyResources(this, "$this");
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        CancelButton = cancelButton;
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
}
