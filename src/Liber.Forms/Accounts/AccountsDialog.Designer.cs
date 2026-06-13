// AccountsDialog.Designer.cs
// Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
// Licensed under the MIT License.

namespace Liber.Forms.Accounts;

partial class AccountsDialog
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountsDialog));
        acceptButton = new System.Windows.Forms.Button();
        cancelButton = new System.Windows.Forms.Button();
        selectAllButton = new System.Windows.Forms.Button();
        deselectAllButton = new System.Windows.Forms.Button();
        toggleAllButton = new System.Windows.Forms.Button();
        nameColumn = new System.Windows.Forms.ColumnHeader();
        numberColumn = new System.Windows.Forms.ColumnHeader();
        _accountListView = new AccountListView();
        _comboBox = new System.Windows.Forms.ComboBox();
        SuspendLayout();
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
        // selectAllButton
        // 
        resources.ApplyResources(selectAllButton, "selectAllButton");
        selectAllButton.Name = "selectAllButton";
        selectAllButton.UseVisualStyleBackColor = true;
        selectAllButton.Click += OnSelectAllButtonClick;
        // 
        // deselectAllButton
        // 
        resources.ApplyResources(deselectAllButton, "deselectAllButton");
        deselectAllButton.Name = "deselectAllButton";
        deselectAllButton.UseVisualStyleBackColor = true;
        deselectAllButton.Click += OnDeselectAllButtonClick;
        // 
        // toggleAllButton
        // 
        resources.ApplyResources(toggleAllButton, "toggleAllButton");
        toggleAllButton.Name = "toggleAllButton";
        toggleAllButton.UseVisualStyleBackColor = true;
        toggleAllButton.Click += OnToggleAllButtonClick;
        // 
        // nameColumn
        // 
        resources.ApplyResources(nameColumn, "nameColumn");
        // 
        // numberColumn
        // 
        resources.ApplyResources(numberColumn, "numberColumn");
        // 
        // _accountListView
        // 
        resources.ApplyResources(_accountListView, "_accountListView");
        _accountListView.AllowColumnReorder = true;
        _accountListView.CheckBoxes = true;
        _accountListView.FullRowSelect = true;
        _accountListView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] { (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups1"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups2"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups3"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups4"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups5"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups6"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups7"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups8"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups9"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups10"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups11"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups12"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups13"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups14"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups15"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups16"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups17"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups18"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups19"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups20"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups21"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups22"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups23"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups24"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups25"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups26"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups27"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups28"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups29"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups30"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups31"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups32"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups33"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups34"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups35"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups36"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups37"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups38"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups39"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups40"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups41"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups42"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups43"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups44"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups45"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups46"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups47"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups48"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups49"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups50"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups51"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups52"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups53"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups54"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups55"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups56"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups57"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups58"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups59"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups60"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups61"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups62"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups63"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups64"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups65"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups66"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups67"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups68"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups69"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups70"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups71"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups72"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups73"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups74"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups75"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups76"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups77"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups78"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups79"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups80"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups81"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups82"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups83"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups84"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups85"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups86"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups87"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups88"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups89"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups90"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups91"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups92"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups93"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups94"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups95"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups96"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups97"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups98"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups99"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups100"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups101"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups102"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups103"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups104"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups105"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups106"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups107"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups108"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups109"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups110"), (System.Windows.Forms.ListViewGroup)resources.GetObject("_accountListView.Groups111") });
        _accountListView.Name = "_accountListView";
        _accountListView.SortColumn = 0;
        _accountListView.UseCompatibleStateImageBehavior = false;
        _accountListView.View = System.Windows.Forms.View.Details;
        // 
        // _comboBox
        // 
        resources.ApplyResources(_comboBox, "_comboBox");
        _comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        _comboBox.DropDownWidth = 250;
        _comboBox.FormattingEnabled = true;
        _comboBox.Name = "_comboBox";
        _comboBox.SelectedIndexChanged += OnComboBoxSelectedIndexChanged;
        _comboBox.Format += OnComboBoxFormat;
        // 
        // AccountsDialog
        // 
        AcceptButton = acceptButton;
        resources.ApplyResources(this, "$this");
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        CancelButton = cancelButton;
        Controls.Add(_comboBox);
        Controls.Add(_accountListView);
        Controls.Add(toggleAllButton);
        Controls.Add(deselectAllButton);
        Controls.Add(selectAllButton);
        Controls.Add(acceptButton);
        Controls.Add(cancelButton);
        Name = "AccountsDialog";
        ResumeLayout(false);
    }

    #endregion
    private System.Windows.Forms.Button acceptButton;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Button selectAllButton;
    private System.Windows.Forms.Button deselectAllButton;
    private System.Windows.Forms.Button toggleAllButton;
    private System.Windows.Forms.ColumnHeader nameColumn;
    private System.Windows.Forms.ColumnHeader numberColumn;
    private AccountListView _accountListView;
    private System.Windows.Forms.ComboBox _comboBox;
}
