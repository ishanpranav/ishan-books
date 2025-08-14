using Liber.Forms.Components;

namespace Liber.Forms;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
        _menuStrip = new System.Windows.Forms.MenuStrip();
        fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        importAccountsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        importTransactionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
        saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        exportCompanyXmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
        exportAccountsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        exportTransactionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
        exportAccountsIifToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
        editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
        recentPathsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        recentPathsToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
        exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        editToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        accountsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        transactionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
        checkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
        reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        taxesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
        otherWindowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        newAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        transactionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        reportsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        _toolStrip = new System.Windows.Forms.ToolStrip();
        newToolStripButton = new System.Windows.Forms.ToolStripButton();
        openToolStripButton = new System.Windows.Forms.ToolStripButton();
        saveToolStripButton = new System.Windows.Forms.ToolStripButton();
        toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
        helpToolStripButton = new System.Windows.Forms.ToolStripButton();
        _openFileDialog = new System.Windows.Forms.OpenFileDialog();
        _saveFileDialog = new System.Windows.Forms.SaveFileDialog();
        _recentPathManager = new RecentPathManager(components);
        _factory = new FormFactory(components);
        _menuStrip.SuspendLayout();
        _toolStrip.SuspendLayout();
        SuspendLayout();
        // 
        // _menuStrip
        // 
        _menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
        _menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem1, viewToolStripMenuItem, reportsToolStripMenuItem1, helpToolStripMenuItem });
        resources.ApplyResources(_menuStrip, "_menuStrip");
        _menuStrip.Name = "_menuStrip";
        // 
        // fileToolStripMenuItem
        // 
        fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { newToolStripMenuItem, openToolStripMenuItem, importToolStripMenuItem, toolStripSeparator, saveToolStripMenuItem, saveAsToolStripMenuItem, exportToolStripMenuItem, toolStripSeparator1, editToolStripMenuItem, toolStripSeparator8, recentPathsToolStripMenuItem, recentPathsToolStripSeparator, exitToolStripMenuItem });
        fileToolStripMenuItem.Name = "fileToolStripMenuItem";
        resources.ApplyResources(fileToolStripMenuItem, "fileToolStripMenuItem");
        // 
        // newToolStripMenuItem
        // 
        resources.ApplyResources(newToolStripMenuItem, "newToolStripMenuItem");
        newToolStripMenuItem.Name = "newToolStripMenuItem";
        newToolStripMenuItem.Click += OnNewToolStripMenuItemClick;
        // 
        // openToolStripMenuItem
        // 
        resources.ApplyResources(openToolStripMenuItem, "openToolStripMenuItem");
        openToolStripMenuItem.Name = "openToolStripMenuItem";
        openToolStripMenuItem.Click += OnOpenToolStripMenuItemClick;
        // 
        // importToolStripMenuItem
        // 
        importToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { importAccountsToolStripMenuItem, importTransactionsToolStripMenuItem });
        importToolStripMenuItem.Name = "importToolStripMenuItem";
        resources.ApplyResources(importToolStripMenuItem, "importToolStripMenuItem");
        // 
        // importAccountsToolStripMenuItem
        // 
        importAccountsToolStripMenuItem.Name = "importAccountsToolStripMenuItem";
        resources.ApplyResources(importAccountsToolStripMenuItem, "importAccountsToolStripMenuItem");
        importAccountsToolStripMenuItem.Click += OnImportAccountsToolStripMenuItemClick;
        // 
        // importTransactionsToolStripMenuItem
        // 
        importTransactionsToolStripMenuItem.Name = "importTransactionsToolStripMenuItem";
        resources.ApplyResources(importTransactionsToolStripMenuItem, "importTransactionsToolStripMenuItem");
        importTransactionsToolStripMenuItem.Click += OnImportTransactionsToolStripMenuItemClick;
        // 
        // toolStripSeparator
        // 
        toolStripSeparator.Name = "toolStripSeparator";
        resources.ApplyResources(toolStripSeparator, "toolStripSeparator");
        // 
        // saveToolStripMenuItem
        // 
        resources.ApplyResources(saveToolStripMenuItem, "saveToolStripMenuItem");
        saveToolStripMenuItem.Name = "saveToolStripMenuItem";
        saveToolStripMenuItem.Click += OnSaveToolStripMenuItemClick;
        // 
        // saveAsToolStripMenuItem
        // 
        saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
        resources.ApplyResources(saveAsToolStripMenuItem, "saveAsToolStripMenuItem");
        saveAsToolStripMenuItem.Click += OnSaveAsToolStripMenuItemClick;
        // 
        // exportToolStripMenuItem
        // 
        exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { exportCompanyXmlToolStripMenuItem, toolStripSeparator3, exportAccountsToolStripMenuItem, exportTransactionsToolStripMenuItem, toolStripSeparator5, exportAccountsIifToolStripMenuItem });
        exportToolStripMenuItem.Name = "exportToolStripMenuItem";
        resources.ApplyResources(exportToolStripMenuItem, "exportToolStripMenuItem");
        // 
        // exportCompanyXmlToolStripMenuItem
        // 
        exportCompanyXmlToolStripMenuItem.Name = "exportCompanyXmlToolStripMenuItem";
        resources.ApplyResources(exportCompanyXmlToolStripMenuItem, "exportCompanyXmlToolStripMenuItem");
        exportCompanyXmlToolStripMenuItem.Click += OnExportToolStripMenuItemClick;
        // 
        // toolStripSeparator3
        // 
        toolStripSeparator3.Name = "toolStripSeparator3";
        resources.ApplyResources(toolStripSeparator3, "toolStripSeparator3");
        // 
        // exportAccountsToolStripMenuItem
        // 
        exportAccountsToolStripMenuItem.Name = "exportAccountsToolStripMenuItem";
        resources.ApplyResources(exportAccountsToolStripMenuItem, "exportAccountsToolStripMenuItem");
        exportAccountsToolStripMenuItem.Click += OnExportToolStripMenuItemClick;
        // 
        // exportTransactionsToolStripMenuItem
        // 
        exportTransactionsToolStripMenuItem.Name = "exportTransactionsToolStripMenuItem";
        resources.ApplyResources(exportTransactionsToolStripMenuItem, "exportTransactionsToolStripMenuItem");
        exportTransactionsToolStripMenuItem.Click += OnExportToolStripMenuItemClick;
        // 
        // toolStripSeparator5
        // 
        toolStripSeparator5.Name = "toolStripSeparator5";
        resources.ApplyResources(toolStripSeparator5, "toolStripSeparator5");
        // 
        // exportAccountsIifToolStripMenuItem
        // 
        exportAccountsIifToolStripMenuItem.Name = "exportAccountsIifToolStripMenuItem";
        resources.ApplyResources(exportAccountsIifToolStripMenuItem, "exportAccountsIifToolStripMenuItem");
        exportAccountsIifToolStripMenuItem.Click += OnExportToolStripMenuItemClick;
        // 
        // toolStripSeparator1
        // 
        toolStripSeparator1.Name = "toolStripSeparator1";
        resources.ApplyResources(toolStripSeparator1, "toolStripSeparator1");
        // 
        // editToolStripMenuItem
        // 
        editToolStripMenuItem.Name = "editToolStripMenuItem";
        resources.ApplyResources(editToolStripMenuItem, "editToolStripMenuItem");
        editToolStripMenuItem.Click += OnEditToolStripMenuItemClick;
        // 
        // toolStripSeparator8
        // 
        toolStripSeparator8.Name = "toolStripSeparator8";
        resources.ApplyResources(toolStripSeparator8, "toolStripSeparator8");
        // 
        // recentPathsToolStripMenuItem
        // 
        recentPathsToolStripMenuItem.Name = "recentPathsToolStripMenuItem";
        resources.ApplyResources(recentPathsToolStripMenuItem, "recentPathsToolStripMenuItem");
        // 
        // recentPathsToolStripSeparator
        // 
        recentPathsToolStripSeparator.Name = "recentPathsToolStripSeparator";
        resources.ApplyResources(recentPathsToolStripSeparator, "recentPathsToolStripSeparator");
        // 
        // exitToolStripMenuItem
        // 
        exitToolStripMenuItem.Name = "exitToolStripMenuItem";
        resources.ApplyResources(exitToolStripMenuItem, "exitToolStripMenuItem");
        exitToolStripMenuItem.Click += OnExitToolStripMenuItemClick;
        // 
        // editToolStripMenuItem1
        // 
        editToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { settingsToolStripMenuItem });
        editToolStripMenuItem1.Name = "editToolStripMenuItem1";
        resources.ApplyResources(editToolStripMenuItem1, "editToolStripMenuItem1");
        // 
        // settingsToolStripMenuItem
        // 
        settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
        resources.ApplyResources(settingsToolStripMenuItem, "settingsToolStripMenuItem");
        settingsToolStripMenuItem.Click += OnSettingsToolStripMenuItemClick;
        // 
        // viewToolStripMenuItem
        // 
        viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { accountsToolStripMenuItem, transactionToolStripMenuItem, toolStripSeparator2, checkToolStripMenuItem, toolStripSeparator4, reportsToolStripMenuItem, taxesToolStripMenuItem, toolStripSeparator10, otherWindowsToolStripMenuItem });
        viewToolStripMenuItem.Name = "viewToolStripMenuItem";
        resources.ApplyResources(viewToolStripMenuItem, "viewToolStripMenuItem");
        // 
        // accountsToolStripMenuItem
        // 
        accountsToolStripMenuItem.Name = "accountsToolStripMenuItem";
        resources.ApplyResources(accountsToolStripMenuItem, "accountsToolStripMenuItem");
        accountsToolStripMenuItem.Click += OnAccountsToolStripMenuItemClick;
        // 
        // transactionToolStripMenuItem
        // 
        transactionToolStripMenuItem.Name = "transactionToolStripMenuItem";
        resources.ApplyResources(transactionToolStripMenuItem, "transactionToolStripMenuItem");
        transactionToolStripMenuItem.Click += OnTransactionToolStripMenuItemClick;
        // 
        // toolStripSeparator2
        // 
        toolStripSeparator2.Name = "toolStripSeparator2";
        resources.ApplyResources(toolStripSeparator2, "toolStripSeparator2");
        // 
        // checkToolStripMenuItem
        // 
        checkToolStripMenuItem.Name = "checkToolStripMenuItem";
        resources.ApplyResources(checkToolStripMenuItem, "checkToolStripMenuItem");
        checkToolStripMenuItem.Click += OnCheckToolStripMenuItemClick;
        // 
        // toolStripSeparator4
        // 
        toolStripSeparator4.Name = "toolStripSeparator4";
        resources.ApplyResources(toolStripSeparator4, "toolStripSeparator4");
        // 
        // reportsToolStripMenuItem
        // 
        reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
        resources.ApplyResources(reportsToolStripMenuItem, "reportsToolStripMenuItem");
        reportsToolStripMenuItem.Click += OnReportsToolStripMenuItemClick;
        // 
        // taxesToolStripMenuItem
        // 
        taxesToolStripMenuItem.Name = "taxesToolStripMenuItem";
        resources.ApplyResources(taxesToolStripMenuItem, "taxesToolStripMenuItem");
        taxesToolStripMenuItem.Click += OnTaxesToolStripMenuItemClick;
        // 
        // toolStripSeparator10
        // 
        toolStripSeparator10.Name = "toolStripSeparator10";
        resources.ApplyResources(toolStripSeparator10, "toolStripSeparator10");
        // 
        // otherWindowsToolStripMenuItem
        // 
        otherWindowsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { newAccountToolStripMenuItem, transactionsToolStripMenuItem });
        otherWindowsToolStripMenuItem.Name = "otherWindowsToolStripMenuItem";
        resources.ApplyResources(otherWindowsToolStripMenuItem, "otherWindowsToolStripMenuItem");
        // 
        // newAccountToolStripMenuItem
        // 
        newAccountToolStripMenuItem.Name = "newAccountToolStripMenuItem";
        resources.ApplyResources(newAccountToolStripMenuItem, "newAccountToolStripMenuItem");
        newAccountToolStripMenuItem.Click += OnNewAccountToolStripMenuItemClick;
        // 
        // transactionsToolStripMenuItem
        // 
        transactionsToolStripMenuItem.Name = "transactionsToolStripMenuItem";
        resources.ApplyResources(transactionsToolStripMenuItem, "transactionsToolStripMenuItem");
        transactionsToolStripMenuItem.Click += OnTransactionsToolStripMenuItemClick;
        // 
        // reportsToolStripMenuItem1
        // 
        reportsToolStripMenuItem1.Name = "reportsToolStripMenuItem1";
        resources.ApplyResources(reportsToolStripMenuItem1, "reportsToolStripMenuItem1");
        // 
        // helpToolStripMenuItem
        // 
        helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { aboutToolStripMenuItem });
        helpToolStripMenuItem.Name = "helpToolStripMenuItem";
        resources.ApplyResources(helpToolStripMenuItem, "helpToolStripMenuItem");
        // 
        // aboutToolStripMenuItem
        // 
        aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
        resources.ApplyResources(aboutToolStripMenuItem, "aboutToolStripMenuItem");
        aboutToolStripMenuItem.Click += OnAboutToolStripMenuItemClick;
        // 
        // _toolStrip
        // 
        _toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
        _toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { newToolStripButton, openToolStripButton, saveToolStripButton, toolStripSeparator6, helpToolStripButton });
        resources.ApplyResources(_toolStrip, "_toolStrip");
        _toolStrip.Name = "_toolStrip";
        // 
        // newToolStripButton
        // 
        newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        resources.ApplyResources(newToolStripButton, "newToolStripButton");
        newToolStripButton.Name = "newToolStripButton";
        newToolStripButton.Click += OnNewToolStripMenuItemClick;
        // 
        // openToolStripButton
        // 
        openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        resources.ApplyResources(openToolStripButton, "openToolStripButton");
        openToolStripButton.Name = "openToolStripButton";
        openToolStripButton.Click += OnOpenToolStripMenuItemClick;
        // 
        // saveToolStripButton
        // 
        saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        resources.ApplyResources(saveToolStripButton, "saveToolStripButton");
        saveToolStripButton.Name = "saveToolStripButton";
        saveToolStripButton.Click += OnSaveToolStripMenuItemClick;
        // 
        // toolStripSeparator6
        // 
        toolStripSeparator6.Name = "toolStripSeparator6";
        resources.ApplyResources(toolStripSeparator6, "toolStripSeparator6");
        // 
        // helpToolStripButton
        // 
        helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        resources.ApplyResources(helpToolStripButton, "helpToolStripButton");
        helpToolStripButton.Name = "helpToolStripButton";
        helpToolStripButton.Click += OnAboutToolStripMenuItemClick;
        // 
        // _openFileDialog
        // 
        _openFileDialog.DefaultExt = "shbk";
        resources.ApplyResources(_openFileDialog, "_openFileDialog");
        _openFileDialog.RestoreDirectory = true;
        _openFileDialog.Tag = "";
        // 
        // _saveFileDialog
        // 
        _saveFileDialog.DefaultExt = "shbk";
        resources.ApplyResources(_saveFileDialog, "_saveFileDialog");
        _saveFileDialog.FilterIndex = 3;
        _saveFileDialog.RestoreDirectory = true;
        // 
        // _recentPathManager
        // 
        _recentPathManager.Updated += OnRecentPathManagerUpdated;
        // 
        // _factory
        // 
        _factory.Parent = this;
        // 
        // MainForm
        // 
        AllowDrop = true;
        resources.ApplyResources(this, "$this");
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        Controls.Add(_toolStrip);
        Controls.Add(_menuStrip);
        MainMenuStrip = _menuStrip;
        Name = "MainForm";
        WindowState = System.Windows.Forms.FormWindowState.Maximized;
        FormClosing += OnFormClosing;
        Load += OnLoad;
        DragDrop += OnDragDrop;
        DragOver += OnDragOver;
        _menuStrip.ResumeLayout(false);
        _menuStrip.PerformLayout();
        _toolStrip.ResumeLayout(false);
        _toolStrip.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private System.Windows.Forms.MenuStrip _menuStrip;
    private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
    private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripSeparator recentPathsToolStripSeparator;
    private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    private System.Windows.Forms.ToolStrip _toolStrip;
    private System.Windows.Forms.ToolStripButton newToolStripButton;
    private System.Windows.Forms.ToolStripButton openToolStripButton;
    private System.Windows.Forms.ToolStripButton saveToolStripButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
    private System.Windows.Forms.ToolStripButton helpToolStripButton;
    private System.Windows.Forms.OpenFileDialog _openFileDialog;
    private System.Windows.Forms.SaveFileDialog _saveFileDialog;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
    private System.Windows.Forms.ToolStripMenuItem recentPathsToolStripMenuItem;
    private RecentPathManager _recentPathManager;
    private FormFactory _factory;
    private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem accountsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem otherWindowsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem newAccountToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem importAccountsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem exportAccountsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
    private System.Windows.Forms.ToolStripMenuItem transactionToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripMenuItem importTransactionsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem exportTransactionsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem checkToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private System.Windows.Forms.ToolStripMenuItem exportCompanyXmlToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    private System.Windows.Forms.ToolStripMenuItem exportAccountsIifToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem transactionsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem taxesToolStripMenuItem;
}
