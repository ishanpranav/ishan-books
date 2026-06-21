// AboutBox1.Designer.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.Forms.Help;

partial class AboutBox
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox));
        descriptionTextBox = new System.Windows.Forms.TextBox();
        companyLabel = new System.Windows.Forms.Label();
        copyrightLabel = new System.Windows.Forms.Label();
        versionLabel = new System.Windows.Forms.Label();
        applicationNameLabel = new System.Windows.Forms.Label();
        tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
        panel1 = new System.Windows.Forms.Panel();
        thirdPartyNoticesLinkLabel = new System.Windows.Forms.LinkLabel();
        licenseLinkLabel = new System.Windows.Forms.LinkLabel();
        acceptButton = new System.Windows.Forms.Button();
        tableLayoutPanel.SuspendLayout();
        panel1.SuspendLayout();
        SuspendLayout();
        // 
        // descriptionTextBox
        // 
        resources.ApplyResources(descriptionTextBox, "descriptionTextBox");
        descriptionTextBox.Name = "descriptionTextBox";
        descriptionTextBox.ReadOnly = true;
        descriptionTextBox.TabStop = false;
        // 
        // companyLabel
        // 
        resources.ApplyResources(companyLabel, "companyLabel");
        companyLabel.Name = "companyLabel";
        // 
        // copyrightLabel
        // 
        resources.ApplyResources(copyrightLabel, "copyrightLabel");
        copyrightLabel.Name = "copyrightLabel";
        // 
        // versionLabel
        // 
        resources.ApplyResources(versionLabel, "versionLabel");
        versionLabel.Name = "versionLabel";
        // 
        // applicationNameLabel
        // 
        resources.ApplyResources(applicationNameLabel, "applicationNameLabel");
        applicationNameLabel.Name = "applicationNameLabel";
        // 
        // tableLayoutPanel
        // 
        resources.ApplyResources(tableLayoutPanel, "tableLayoutPanel");
        tableLayoutPanel.Controls.Add(applicationNameLabel, 1, 0);
        tableLayoutPanel.Controls.Add(panel1, 0, 5);
        tableLayoutPanel.Controls.Add(versionLabel, 1, 1);
        tableLayoutPanel.Controls.Add(copyrightLabel, 1, 2);
        tableLayoutPanel.Controls.Add(companyLabel, 1, 3);
        tableLayoutPanel.Controls.Add(descriptionTextBox, 1, 4);
        tableLayoutPanel.Name = "tableLayoutPanel";
        // 
        // panel1
        // 
        panel1.Controls.Add(thirdPartyNoticesLinkLabel);
        panel1.Controls.Add(licenseLinkLabel);
        panel1.Controls.Add(acceptButton);
        resources.ApplyResources(panel1, "panel1");
        panel1.Name = "panel1";
        // 
        // thirdPartyNoticesLinkLabel
        // 
        resources.ApplyResources(thirdPartyNoticesLinkLabel, "thirdPartyNoticesLinkLabel");
        thirdPartyNoticesLinkLabel.Name = "thirdPartyNoticesLinkLabel";
        thirdPartyNoticesLinkLabel.TabStop = true;
        thirdPartyNoticesLinkLabel.LinkClicked += OnThirdPartyNoticesLinkLabelLinkClicked;
        // 
        // licenseLinkLabel
        // 
        resources.ApplyResources(licenseLinkLabel, "licenseLinkLabel");
        licenseLinkLabel.Name = "licenseLinkLabel";
        licenseLinkLabel.TabStop = true;
        licenseLinkLabel.LinkClicked += OnLicenseLinkLabelLinkClicked;
        // 
        // acceptButton
        // 
        resources.ApplyResources(acceptButton, "acceptButton");
        acceptButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        acceptButton.Name = "acceptButton";
        // 
        // AboutBox
        // 
        resources.ApplyResources(this, "$this");
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        Controls.Add(tableLayoutPanel);
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "AboutBox";
        ShowIcon = false;
        ShowInTaskbar = false;
        tableLayoutPanel.ResumeLayout(false);
        tableLayoutPanel.PerformLayout();
        panel1.ResumeLayout(false);
        panel1.PerformLayout();
        ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.TextBox descriptionTextBox;
    private System.Windows.Forms.Label companyLabel;
    private System.Windows.Forms.Label copyrightLabel;
    private System.Windows.Forms.Label versionLabel;
    private System.Windows.Forms.Label applicationNameLabel;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.LinkLabel thirdPartyNoticesLinkLabel;
    private System.Windows.Forms.LinkLabel licenseLinkLabel;
    private System.Windows.Forms.Button acceptButton;
}
