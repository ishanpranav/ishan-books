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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
        menuStrip1 = new System.Windows.Forms.MenuStrip();
        fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        importAccountsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
        saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        exportAccountsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
        reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
        otherWindowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        newAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        indexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
        aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStrip1 = new System.Windows.Forms.ToolStrip();
        newToolStripButton = new System.Windows.Forms.ToolStripButton();
        openToolStripButton = new System.Windows.Forms.ToolStripButton();
        saveToolStripButton = new System.Windows.Forms.ToolStripButton();
        toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
        helpToolStripButton = new System.Windows.Forms.ToolStripButton();
        _openFileDialog = new System.Windows.Forms.OpenFileDialog();
        _saveFileDialog = new System.Windows.Forms.SaveFileDialog();
        _recentPathManager = new RecentPathManager();
        _factory = new FormFactory();
        menuStrip1.SuspendLayout();
        toolStrip1.SuspendLayout();
        SuspendLayout();
        // 
        // menuStrip1
        // 
        menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem1, viewToolStripMenuItem, helpToolStripMenuItem });
        resources.ApplyResources(menuStrip1, "menuStrip1");
        menuStrip1.Name = "menuStrip1";
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
        importToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { importAccountsToolStripMenuItem });
        importToolStripMenuItem.Name = "importToolStripMenuItem";
        resources.ApplyResources(importToolStripMenuItem, "importToolStripMenuItem");
        // 
        // importAccountsToolStripMenuItem
        // 
        importAccountsToolStripMenuItem.Name = "importAccountsToolStripMenuItem";
        resources.ApplyResources(importAccountsToolStripMenuItem, "importAccountsToolStripMenuItem");
        importAccountsToolStripMenuItem.Click += OnImportAccountsToolStripMenuItemClick;
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
        exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { exportAccountsToolStripMenuItem });
        exportToolStripMenuItem.Name = "exportToolStripMenuItem";
        resources.ApplyResources(exportToolStripMenuItem, "exportToolStripMenuItem");
        // 
        // exportAccountsToolStripMenuItem
        // 
        exportAccountsToolStripMenuItem.Name = "exportAccountsToolStripMenuItem";
        resources.ApplyResources(exportAccountsToolStripMenuItem, "exportAccountsToolStripMenuItem");
        exportAccountsToolStripMenuItem.Click += OnExportAccountsToolStripMenuItemClick;
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
        viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { accountsToolStripMenuItem, reportsToolStripMenuItem, toolStripSeparator10, otherWindowsToolStripMenuItem });
        viewToolStripMenuItem.Name = "viewToolStripMenuItem";
        resources.ApplyResources(viewToolStripMenuItem, "viewToolStripMenuItem");
        // 
        // accountsToolStripMenuItem
        // 
        accountsToolStripMenuItem.Name = "accountsToolStripMenuItem";
        resources.ApplyResources(accountsToolStripMenuItem, "accountsToolStripMenuItem");
        accountsToolStripMenuItem.Click += OnAccountsToolStripMenuItemClick;
        // 
        // reportsToolStripMenuItem
        // 
        reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
        resources.ApplyResources(reportsToolStripMenuItem, "reportsToolStripMenuItem");
        reportsToolStripMenuItem.Click += OnReportsToolStripMenuItemClick;
        // 
        // toolStripSeparator10
        // 
        toolStripSeparator10.Name = "toolStripSeparator10";
        resources.ApplyResources(toolStripSeparator10, "toolStripSeparator10");
        // 
        // otherWindowsToolStripMenuItem
        // 
        otherWindowsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { newAccountToolStripMenuItem });
        otherWindowsToolStripMenuItem.Name = "otherWindowsToolStripMenuItem";
        resources.ApplyResources(otherWindowsToolStripMenuItem, "otherWindowsToolStripMenuItem");
        // 
        // newAccountToolStripMenuItem
        // 
        newAccountToolStripMenuItem.Name = "newAccountToolStripMenuItem";
        resources.ApplyResources(newAccountToolStripMenuItem, "newAccountToolStripMenuItem");
        newAccountToolStripMenuItem.Click += OnNewAccountToolStripMenuItemClick;
        // 
        // helpToolStripMenuItem
        // 
        helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { contentsToolStripMenuItem, indexToolStripMenuItem, searchToolStripMenuItem, toolStripSeparator5, aboutToolStripMenuItem });
        helpToolStripMenuItem.Name = "helpToolStripMenuItem";
        resources.ApplyResources(helpToolStripMenuItem, "helpToolStripMenuItem");
        // 
        // contentsToolStripMenuItem
        // 
        contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
        resources.ApplyResources(contentsToolStripMenuItem, "contentsToolStripMenuItem");
        // 
        // indexToolStripMenuItem
        // 
        indexToolStripMenuItem.Name = "indexToolStripMenuItem";
        resources.ApplyResources(indexToolStripMenuItem, "indexToolStripMenuItem");
        // 
        // searchToolStripMenuItem
        // 
        searchToolStripMenuItem.Name = "searchToolStripMenuItem";
        resources.ApplyResources(searchToolStripMenuItem, "searchToolStripMenuItem");
        // 
        // toolStripSeparator5
        // 
        toolStripSeparator5.Name = "toolStripSeparator5";
        resources.ApplyResources(toolStripSeparator5, "toolStripSeparator5");
        // 
        // aboutToolStripMenuItem
        // 
        aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
        resources.ApplyResources(aboutToolStripMenuItem, "aboutToolStripMenuItem");
        aboutToolStripMenuItem.Click += OnAboutToolStripMenuItemClick;
        // 
        // toolStrip1
        // 
        toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { newToolStripButton, openToolStripButton, saveToolStripButton, toolStripSeparator6, helpToolStripButton });
        resources.ApplyResources(toolStrip1, "toolStrip1");
        toolStrip1.Name = "toolStrip1";
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
        _openFileDialog.DefaultExt = "liber";
        resources.ApplyResources(_openFileDialog, "_openFileDialog");
        _openFileDialog.FilterIndex = 2;
        _openFileDialog.RestoreDirectory = true;
        // 
        // _saveFileDialog
        // 
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
        Controls.Add(toolStrip1);
        Controls.Add(menuStrip1);
        IsMdiContainer = true;
        MainMenuStrip = menuStrip1;
        Name = "MainForm";
        WindowState = System.Windows.Forms.FormWindowState.Maximized;
        FormClosing += OnFormClosing;
        Load += OnLoad;
        DragDrop += OnDragDrop;
        DragOver += OnDragOver;
        menuStrip1.ResumeLayout(false);
        menuStrip1.PerformLayout();
        toolStrip1.ResumeLayout(false);
        toolStrip1.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStrip1;
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
    private System.Windows.Forms.ToolStripMenuItem contentsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem indexToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    private System.Windows.Forms.ToolStrip toolStrip1;
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
}