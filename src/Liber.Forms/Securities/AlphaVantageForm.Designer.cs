// AlphaVantageForm.Designer.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.Forms.Securities;

partial class AlphaVantageForm
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
        cancelButton = new System.Windows.Forms.Button();
        acceptButton = new System.Windows.Forms.Button();
        label1 = new System.Windows.Forms.Label();
        apiKeyTextBox = new System.Windows.Forms.TextBox();
        testButton = new System.Windows.Forms.Button();
        label2 = new System.Windows.Forms.Label();
        statusTextBox = new System.Windows.Forms.TextBox();
        hostnameTextBox = new System.Windows.Forms.TextBox();
        label3 = new System.Windows.Forms.Label();
        symbolTextBox = new System.Windows.Forms.TextBox();
        label4 = new System.Windows.Forms.Label();
        SuspendLayout();
        // 
        // cancelButton
        // 
        cancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        cancelButton.Location = new System.Drawing.Point(290, 223);
        cancelButton.Name = "cancelButton";
        cancelButton.Size = new System.Drawing.Size(75, 23);
        cancelButton.TabIndex = 0;
        cancelButton.Text = "&Cancel";
        cancelButton.UseVisualStyleBackColor = true;
        cancelButton.Click += OnCancelButtonClick;
        // 
        // acceptButton
        // 
        acceptButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        acceptButton.Location = new System.Drawing.Point(209, 223);
        acceptButton.Name = "acceptButton";
        acceptButton.Size = new System.Drawing.Size(75, 23);
        acceptButton.TabIndex = 1;
        acceptButton.Text = "&OK";
        acceptButton.UseVisualStyleBackColor = true;
        acceptButton.Click += OnAcceptButtonClick;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(12, 15);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(62, 15);
        label1.TabIndex = 2;
        label1.Text = "&Hostname";
        // 
        // apiKeyTextBox
        // 
        apiKeyTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        apiKeyTextBox.Location = new System.Drawing.Point(80, 41);
        apiKeyTextBox.Name = "apiKeyTextBox";
        apiKeyTextBox.Size = new System.Drawing.Size(285, 23);
        apiKeyTextBox.TabIndex = 3;
        // 
        // testButton
        // 
        testButton.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        testButton.Location = new System.Drawing.Point(290, 69);
        testButton.Name = "testButton";
        testButton.Size = new System.Drawing.Size(75, 23);
        testButton.TabIndex = 4;
        testButton.Text = "&Test";
        testButton.UseVisualStyleBackColor = true;
        testButton.Click += OnTestButtonClick;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(12, 110);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(39, 15);
        label2.TabIndex = 6;
        label2.Text = "Status";
        // 
        // statusTextBox
        // 
        statusTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        statusTextBox.Font = new System.Drawing.Font("Consolas", 9F);
        statusTextBox.Location = new System.Drawing.Point(12, 128);
        statusTextBox.Multiline = true;
        statusTextBox.Name = "statusTextBox";
        statusTextBox.ReadOnly = true;
        statusTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        statusTextBox.Size = new System.Drawing.Size(353, 89);
        statusTextBox.TabIndex = 7;
        // 
        // hostnameTextBox
        // 
        hostnameTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        hostnameTextBox.Location = new System.Drawing.Point(80, 12);
        hostnameTextBox.Name = "hostnameTextBox";
        hostnameTextBox.ReadOnly = true;
        hostnameTextBox.Size = new System.Drawing.Size(285, 23);
        hostnameTextBox.TabIndex = 9;
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(12, 44);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(47, 15);
        label3.TabIndex = 8;
        label3.Text = "API &Key";
        // 
        // symbolTextBox
        // 
        symbolTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        symbolTextBox.Location = new System.Drawing.Point(132, 69);
        symbolTextBox.Name = "symbolTextBox";
        symbolTextBox.Size = new System.Drawing.Size(152, 23);
        symbolTextBox.TabIndex = 11;
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Location = new System.Drawing.Point(12, 73);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(47, 15);
        label4.TabIndex = 10;
        label4.Text = "S&ymbol";
        // 
        // AlphaVantageForm
        // 
        AcceptButton = acceptButton;
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        CancelButton = cancelButton;
        ClientSize = new System.Drawing.Size(377, 258);
        Controls.Add(symbolTextBox);
        Controls.Add(label4);
        Controls.Add(hostnameTextBox);
        Controls.Add(label3);
        Controls.Add(statusTextBox);
        Controls.Add(label2);
        Controls.Add(testButton);
        Controls.Add(apiKeyTextBox);
        Controls.Add(label1);
        Controls.Add(acceptButton);
        Controls.Add(cancelButton);
        MaximizeBox = false;
        MinimumSize = new System.Drawing.Size(279, 232);
        Name = "AlphaVantageForm";
        Text = "Alpha Vantage API Settings";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Button acceptButton;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox apiKeyTextBox;
    private System.Windows.Forms.Button testButton;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox statusTextBox;
    private System.Windows.Forms.TextBox hostnameTextBox;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox symbolTextBox;
    private System.Windows.Forms.Label label4;
}
