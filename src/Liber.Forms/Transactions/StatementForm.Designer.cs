// ReconciliationContextForm.Designer.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.Forms.Transactions;

partial class StatementForm
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatementForm));
        label1 = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        label3 = new System.Windows.Forms.Label();
        reconciledDateTimePicker = new System.Windows.Forms.DateTimePicker();
        openingBalanceNumericUpDown = new System.Windows.Forms.NumericUpDown();
        endingBalanceNumericUpDown = new System.Windows.Forms.NumericUpDown();
        acceptButton = new System.Windows.Forms.Button();
        label4 = new System.Windows.Forms.Label();
        accountComboBox = new System.Windows.Forms.ComboBox();
        ((System.ComponentModel.ISupportInitialize)openingBalanceNumericUpDown).BeginInit();
        ((System.ComponentModel.ISupportInitialize)endingBalanceNumericUpDown).BeginInit();
        SuspendLayout();
        // 
        // label1
        // 
        resources.ApplyResources(label1, "label1");
        label1.Name = "label1";
        // 
        // label2
        // 
        resources.ApplyResources(label2, "label2");
        label2.Name = "label2";
        // 
        // label3
        // 
        resources.ApplyResources(label3, "label3");
        label3.Name = "label3";
        // 
        // reconciledDateTimePicker
        // 
        resources.ApplyResources(reconciledDateTimePicker, "reconciledDateTimePicker");
        reconciledDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
        reconciledDateTimePicker.Name = "reconciledDateTimePicker";
        // 
        // openingBalanceNumericUpDown
        // 
        resources.ApplyResources(openingBalanceNumericUpDown, "openingBalanceNumericUpDown");
        openingBalanceNumericUpDown.DecimalPlaces = 2;
        openingBalanceNumericUpDown.Name = "openingBalanceNumericUpDown";
        // 
        // endingBalanceNumericUpDown
        // 
        resources.ApplyResources(endingBalanceNumericUpDown, "endingBalanceNumericUpDown");
        endingBalanceNumericUpDown.DecimalPlaces = 2;
        endingBalanceNumericUpDown.Name = "endingBalanceNumericUpDown";
        // 
        // acceptButton
        // 
        resources.ApplyResources(acceptButton, "acceptButton");
        acceptButton.Name = "acceptButton";
        acceptButton.UseVisualStyleBackColor = true;
        acceptButton.Click += OnAcceptButtonClick;
        // 
        // label4
        // 
        resources.ApplyResources(label4, "label4");
        label4.Name = "label4";
        // 
        // accountComboBox
        // 
        resources.ApplyResources(accountComboBox, "accountComboBox");
        accountComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        accountComboBox.FormattingEnabled = true;
        accountComboBox.Name = "accountComboBox";
        // 
        // ReconciliationContextForm
        // 
        AcceptButton = acceptButton;
        resources.ApplyResources(this, "$this");
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        Controls.Add(accountComboBox);
        Controls.Add(label4);
        Controls.Add(acceptButton);
        Controls.Add(endingBalanceNumericUpDown);
        Controls.Add(openingBalanceNumericUpDown);
        Controls.Add(reconciledDateTimePicker);
        Controls.Add(label3);
        Controls.Add(label2);
        Controls.Add(label1);
        MaximizeBox = false;
        Name = "ReconciliationContextForm";
        ((System.ComponentModel.ISupportInitialize)openingBalanceNumericUpDown).EndInit();
        ((System.ComponentModel.ISupportInitialize)endingBalanceNumericUpDown).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.DateTimePicker reconciledDateTimePicker;
    private System.Windows.Forms.NumericUpDown openingBalanceNumericUpDown;
    private System.Windows.Forms.NumericUpDown endingBalanceNumericUpDown;
    private System.Windows.Forms.Button acceptButton;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.ComboBox accountComboBox;
}