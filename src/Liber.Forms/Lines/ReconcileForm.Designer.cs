// ReconcileForm.Designer.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.Forms.Transactions;

partial class ReconcileForm
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReconcileForm));
        debitListView = new Liber.Forms.Lines.LineListView();
        creditListView = new Liber.Forms.Lines.LineListView();
        tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
        label3 = new System.Windows.Forms.Label();
        label4 = new System.Windows.Forms.Label();
        groupBox1 = new System.Windows.Forms.GroupBox();
        differenceNumericUpDown = new System.Windows.Forms.NumericUpDown();
        label7 = new System.Windows.Forms.Label();
        nextReconciledBalanceNumericUpDown = new System.Windows.Forms.NumericUpDown();
        label6 = new System.Windows.Forms.Label();
        endingBalanceNumericUpDown = new System.Windows.Forms.NumericUpDown();
        label5 = new System.Windows.Forms.Label();
        acceptButton = new System.Windows.Forms.Button();
        cancelButton = new System.Windows.Forms.Button();
        label2 = new System.Windows.Forms.Label();
        reconciledDateTimePicker = new System.Windows.Forms.DateTimePicker();
        groupBox2 = new System.Windows.Forms.GroupBox();
        creditLabel = new System.Windows.Forms.Label();
        debitLabel = new System.Windows.Forms.Label();
        creditNumericUpDown = new System.Windows.Forms.NumericUpDown();
        debitNumericUpDown = new System.Windows.Forms.NumericUpDown();
        reconciledBalanceNumericUpDown = new System.Windows.Forms.NumericUpDown();
        label11 = new System.Windows.Forms.Label();
        tableLayoutPanel1.SuspendLayout();
        groupBox1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)differenceNumericUpDown).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nextReconciledBalanceNumericUpDown).BeginInit();
        ((System.ComponentModel.ISupportInitialize)endingBalanceNumericUpDown).BeginInit();
        groupBox2.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)creditNumericUpDown).BeginInit();
        ((System.ComponentModel.ISupportInitialize)debitNumericUpDown).BeginInit();
        ((System.ComponentModel.ISupportInitialize)reconciledBalanceNumericUpDown).BeginInit();
        SuspendLayout();
        // 
        // debitListView
        // 
        resources.ApplyResources(debitListView, "debitListView");
        debitListView.Activation = System.Windows.Forms.ItemActivation.OneClick;
        debitListView.AllowColumnReorder = true;
        debitListView.CheckBoxes = true;
        debitListView.FullRowSelect = true;
        debitListView.GridLines = true;
        debitListView.Name = "debitListView";
        debitListView.UseCompatibleStateImageBehavior = false;
        debitListView.View = System.Windows.Forms.View.Details;
        debitListView.ItemCheck += OnDebitListViewItemCheck;
        // 
        // creditListView
        // 
        resources.ApplyResources(creditListView, "creditListView");
        creditListView.Activation = System.Windows.Forms.ItemActivation.OneClick;
        creditListView.AllowColumnReorder = true;
        creditListView.CheckBoxes = true;
        creditListView.FullRowSelect = true;
        creditListView.GridLines = true;
        creditListView.Name = "creditListView";
        creditListView.UseCompatibleStateImageBehavior = false;
        creditListView.View = System.Windows.Forms.View.Details;
        creditListView.ItemCheck += OnCreditListViewItemCheck;
        // 
        // tableLayoutPanel1
        // 
        resources.ApplyResources(tableLayoutPanel1, "tableLayoutPanel1");
        tableLayoutPanel1.Controls.Add(debitListView, 0, 1);
        tableLayoutPanel1.Controls.Add(creditListView, 1, 1);
        tableLayoutPanel1.Controls.Add(label3, 0, 0);
        tableLayoutPanel1.Controls.Add(label4, 1, 0);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        // 
        // label3
        // 
        resources.ApplyResources(label3, "label3");
        label3.AutoEllipsis = true;
        label3.Name = "label3";
        // 
        // label4
        // 
        resources.ApplyResources(label4, "label4");
        label4.AutoEllipsis = true;
        label4.Name = "label4";
        // 
        // groupBox1
        // 
        resources.ApplyResources(groupBox1, "groupBox1");
        groupBox1.Controls.Add(differenceNumericUpDown);
        groupBox1.Controls.Add(label7);
        groupBox1.Controls.Add(nextReconciledBalanceNumericUpDown);
        groupBox1.Controls.Add(label6);
        groupBox1.Controls.Add(endingBalanceNumericUpDown);
        groupBox1.Controls.Add(label5);
        groupBox1.Name = "groupBox1";
        groupBox1.TabStop = false;
        // 
        // differenceNumericUpDown
        // 
        resources.ApplyResources(differenceNumericUpDown, "differenceNumericUpDown");
        differenceNumericUpDown.DecimalPlaces = 2;
        differenceNumericUpDown.Name = "differenceNumericUpDown";
        differenceNumericUpDown.ReadOnly = true;
        // 
        // label7
        // 
        resources.ApplyResources(label7, "label7");
        label7.Name = "label7";
        // 
        // nextReconciledBalanceNumericUpDown
        // 
        resources.ApplyResources(nextReconciledBalanceNumericUpDown, "nextReconciledBalanceNumericUpDown");
        nextReconciledBalanceNumericUpDown.DecimalPlaces = 2;
        nextReconciledBalanceNumericUpDown.Name = "nextReconciledBalanceNumericUpDown";
        nextReconciledBalanceNumericUpDown.ReadOnly = true;
        // 
        // label6
        // 
        resources.ApplyResources(label6, "label6");
        label6.Name = "label6";
        // 
        // endingBalanceNumericUpDown
        // 
        resources.ApplyResources(endingBalanceNumericUpDown, "endingBalanceNumericUpDown");
        endingBalanceNumericUpDown.DecimalPlaces = 2;
        endingBalanceNumericUpDown.Name = "endingBalanceNumericUpDown";
        endingBalanceNumericUpDown.ReadOnly = true;
        // 
        // label5
        // 
        resources.ApplyResources(label5, "label5");
        label5.Name = "label5";
        // 
        // acceptButton
        // 
        resources.ApplyResources(acceptButton, "acceptButton");
        acceptButton.Name = "acceptButton";
        acceptButton.UseVisualStyleBackColor = true;
        acceptButton.Click += OnAcceptButtonClick;
        // 
        // cancelButton
        // 
        resources.ApplyResources(cancelButton, "cancelButton");
        cancelButton.Name = "cancelButton";
        cancelButton.UseVisualStyleBackColor = true;
        cancelButton.Click += OnCancelButtonClick;
        // 
        // label2
        // 
        resources.ApplyResources(label2, "label2");
        label2.Name = "label2";
        // 
        // reconciledDateTimePicker
        // 
        resources.ApplyResources(reconciledDateTimePicker, "reconciledDateTimePicker");
        reconciledDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
        reconciledDateTimePicker.Name = "reconciledDateTimePicker";
        // 
        // groupBox2
        // 
        resources.ApplyResources(groupBox2, "groupBox2");
        groupBox2.Controls.Add(creditLabel);
        groupBox2.Controls.Add(debitLabel);
        groupBox2.Controls.Add(creditNumericUpDown);
        groupBox2.Controls.Add(debitNumericUpDown);
        groupBox2.Controls.Add(reconciledBalanceNumericUpDown);
        groupBox2.Controls.Add(label11);
        groupBox2.Name = "groupBox2";
        groupBox2.TabStop = false;
        // 
        // creditLabel
        // 
        resources.ApplyResources(creditLabel, "creditLabel");
        creditLabel.AutoEllipsis = true;
        creditLabel.Name = "creditLabel";
        // 
        // debitLabel
        // 
        resources.ApplyResources(debitLabel, "debitLabel");
        debitLabel.AutoEllipsis = true;
        debitLabel.Name = "debitLabel";
        // 
        // creditNumericUpDown
        // 
        resources.ApplyResources(creditNumericUpDown, "creditNumericUpDown");
        creditNumericUpDown.DecimalPlaces = 2;
        creditNumericUpDown.Name = "creditNumericUpDown";
        creditNumericUpDown.ReadOnly = true;
        // 
        // debitNumericUpDown
        // 
        resources.ApplyResources(debitNumericUpDown, "debitNumericUpDown");
        debitNumericUpDown.DecimalPlaces = 2;
        debitNumericUpDown.Name = "debitNumericUpDown";
        debitNumericUpDown.ReadOnly = true;
        // 
        // reconciledBalanceNumericUpDown
        // 
        resources.ApplyResources(reconciledBalanceNumericUpDown, "reconciledBalanceNumericUpDown");
        reconciledBalanceNumericUpDown.DecimalPlaces = 2;
        reconciledBalanceNumericUpDown.Name = "reconciledBalanceNumericUpDown";
        reconciledBalanceNumericUpDown.ReadOnly = true;
        // 
        // label11
        // 
        resources.ApplyResources(label11, "label11");
        label11.Name = "label11";
        // 
        // ReconcileForm
        // 
        AcceptButton = acceptButton;
        resources.ApplyResources(this, "$this");
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        CancelButton = cancelButton;
        Controls.Add(groupBox2);
        Controls.Add(reconciledDateTimePicker);
        Controls.Add(label2);
        Controls.Add(acceptButton);
        Controls.Add(cancelButton);
        Controls.Add(groupBox1);
        Controls.Add(tableLayoutPanel1);
        Name = "ReconcileForm";
        tableLayoutPanel1.ResumeLayout(false);
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)differenceNumericUpDown).EndInit();
        ((System.ComponentModel.ISupportInitialize)nextReconciledBalanceNumericUpDown).EndInit();
        ((System.ComponentModel.ISupportInitialize)endingBalanceNumericUpDown).EndInit();
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)creditNumericUpDown).EndInit();
        ((System.ComponentModel.ISupportInitialize)debitNumericUpDown).EndInit();
        ((System.ComponentModel.ISupportInitialize)reconciledBalanceNumericUpDown).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Lines.LineListView debitListView;
    private Lines.LineListView creditListView;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Button acceptButton;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.DateTimePicker reconciledDateTimePicker;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.NumericUpDown differenceNumericUpDown;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.NumericUpDown nextReconciledBalanceNumericUpDown;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.NumericUpDown endingBalanceNumericUpDown;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.NumericUpDown creditNumericUpDown;
    private System.Windows.Forms.NumericUpDown debitNumericUpDown;
    private System.Windows.Forms.NumericUpDown reconciledBalanceNumericUpDown;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Label creditLabel;
    private System.Windows.Forms.Label debitLabel;
}
