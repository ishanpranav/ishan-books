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
        cultureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        importSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
        combinePdfDocumentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        accountsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        transactionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
        checkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        transactionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
        reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
        otherWindowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        newAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        removeAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        reportsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        otherReportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        formToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        formsToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
        formsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        indexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
        aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        _toolStrip = new System.Windows.Forms.ToolStrip();
        newToolStripButton = new System.Windows.Forms.ToolStripButton();
        openToolStripButton = new System.Windows.Forms.ToolStripButton();
        saveToolStripButton = new System.Windows.Forms.ToolStripButton();
        toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
        accountsToolStripButton = new System.Windows.Forms.ToolStripButton();
        transactionToolStripButton = new System.Windows.Forms.ToolStripButton();
        toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
        transactionsToolStripButton = new System.Windows.Forms.ToolStripButton();
        toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
        reportsToolStripButton = new System.Windows.Forms.ToolStripButton();
        _openFileDialog = new System.Windows.Forms.OpenFileDialog();
        _saveFileDialog = new System.Windows.Forms.SaveFileDialog();
        _recentPathManager = new RecentPathManager(components);
        _factory = new FormFactory(components);
        _timer = new System.Windows.Forms.Timer(components);
        _menuStrip.SuspendLayout();
        _toolStrip.SuspendLayout();
        SuspendLayout();
        // 
        // _menuStrip
        // 
        resources.ApplyResources(_menuStrip, "_menuStrip");
        _menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem1, viewToolStripMenuItem, reportsToolStripMenuItem1, formToolStripMenuItem, helpToolStripMenuItem1 });
        _menuStrip.Name = "_menuStrip";
        // 
        // fileToolStripMenuItem
        // 
        resources.ApplyResources(fileToolStripMenuItem, "fileToolStripMenuItem");
        fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { newToolStripMenuItem, openToolStripMenuItem, importToolStripMenuItem, toolStripSeparator, saveToolStripMenuItem, saveAsToolStripMenuItem, exportToolStripMenuItem, toolStripSeparator1, editToolStripMenuItem, toolStripSeparator8, recentPathsToolStripMenuItem, recentPathsToolStripSeparator, exitToolStripMenuItem });
        fileToolStripMenuItem.Name = "fileToolStripMenuItem";
        // 
        // newToolStripMenuItem
        // 
        resources.ApplyResources(newToolStripMenuItem, "newToolStripMenuItem");
        newToolStripMenuItem.Image = VisualStudioImageLibrary.AddDocument;
        newToolStripMenuItem.Name = "newToolStripMenuItem";
        newToolStripMenuItem.Click += OnNewToolStripMenuItemClick;
        // 
        // openToolStripMenuItem
        // 
        resources.ApplyResources(openToolStripMenuItem, "openToolStripMenuItem");
        openToolStripMenuItem.Image = VisualStudioImageLibrary.OpenFile;
        openToolStripMenuItem.Name = "openToolStripMenuItem";
        openToolStripMenuItem.Click += OnOpenToolStripMenuItemClick;
        // 
        // importToolStripMenuItem
        // 
        resources.ApplyResources(importToolStripMenuItem, "importToolStripMenuItem");
        importToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { importAccountsToolStripMenuItem, importTransactionsToolStripMenuItem });
        importToolStripMenuItem.Name = "importToolStripMenuItem";
        // 
        // importAccountsToolStripMenuItem
        // 
        resources.ApplyResources(importAccountsToolStripMenuItem, "importAccountsToolStripMenuItem");
        importAccountsToolStripMenuItem.Image = VisualStudioImageLibrary.Import;
        importAccountsToolStripMenuItem.Name = "importAccountsToolStripMenuItem";
        importAccountsToolStripMenuItem.Click += OnImportAccountsToolStripMenuItemClick;
        // 
        // importTransactionsToolStripMenuItem
        // 
        resources.ApplyResources(importTransactionsToolStripMenuItem, "importTransactionsToolStripMenuItem");
        importTransactionsToolStripMenuItem.Image = VisualStudioImageLibrary.Import;
        importTransactionsToolStripMenuItem.Name = "importTransactionsToolStripMenuItem";
        importTransactionsToolStripMenuItem.Click += OnImportTransactionsToolStripMenuItemClick;
        // 
        // toolStripSeparator
        // 
        resources.ApplyResources(toolStripSeparator, "toolStripSeparator");
        toolStripSeparator.Name = "toolStripSeparator";
        // 
        // saveToolStripMenuItem
        // 
        resources.ApplyResources(saveToolStripMenuItem, "saveToolStripMenuItem");
        saveToolStripMenuItem.Image = VisualStudioImageLibrary.Save;
        saveToolStripMenuItem.Name = "saveToolStripMenuItem";
        saveToolStripMenuItem.Click += OnSaveToolStripMenuItemClick;
        // 
        // saveAsToolStripMenuItem
        // 
        resources.ApplyResources(saveAsToolStripMenuItem, "saveAsToolStripMenuItem");
        saveAsToolStripMenuItem.Image = VisualStudioImageLibrary.SaveAs;
        saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
        saveAsToolStripMenuItem.Click += OnSaveAsToolStripMenuItemClick;
        // 
        // exportToolStripMenuItem
        // 
        resources.ApplyResources(exportToolStripMenuItem, "exportToolStripMenuItem");
        exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { exportCompanyXmlToolStripMenuItem, toolStripSeparator3, exportAccountsToolStripMenuItem, exportTransactionsToolStripMenuItem, toolStripSeparator5, exportAccountsIifToolStripMenuItem });
        exportToolStripMenuItem.Name = "exportToolStripMenuItem";
        // 
        // exportCompanyXmlToolStripMenuItem
        // 
        resources.ApplyResources(exportCompanyXmlToolStripMenuItem, "exportCompanyXmlToolStripMenuItem");
        exportCompanyXmlToolStripMenuItem.Image = VisualStudioImageLibrary.Export;
        exportCompanyXmlToolStripMenuItem.Name = "exportCompanyXmlToolStripMenuItem";
        exportCompanyXmlToolStripMenuItem.Click += OnExportToolStripMenuItemClick;
        // 
        // toolStripSeparator3
        // 
        resources.ApplyResources(toolStripSeparator3, "toolStripSeparator3");
        toolStripSeparator3.Name = "toolStripSeparator3";
        // 
        // exportAccountsToolStripMenuItem
        // 
        resources.ApplyResources(exportAccountsToolStripMenuItem, "exportAccountsToolStripMenuItem");
        exportAccountsToolStripMenuItem.Image = VisualStudioImageLibrary.Export;
        exportAccountsToolStripMenuItem.Name = "exportAccountsToolStripMenuItem";
        exportAccountsToolStripMenuItem.Click += OnExportToolStripMenuItemClick;
        // 
        // exportTransactionsToolStripMenuItem
        // 
        resources.ApplyResources(exportTransactionsToolStripMenuItem, "exportTransactionsToolStripMenuItem");
        exportTransactionsToolStripMenuItem.Image = VisualStudioImageLibrary.Export;
        exportTransactionsToolStripMenuItem.Name = "exportTransactionsToolStripMenuItem";
        exportTransactionsToolStripMenuItem.Click += OnExportToolStripMenuItemClick;
        // 
        // toolStripSeparator5
        // 
        resources.ApplyResources(toolStripSeparator5, "toolStripSeparator5");
        toolStripSeparator5.Name = "toolStripSeparator5";
        // 
        // exportAccountsIifToolStripMenuItem
        // 
        resources.ApplyResources(exportAccountsIifToolStripMenuItem, "exportAccountsIifToolStripMenuItem");
        exportAccountsIifToolStripMenuItem.Image = VisualStudioImageLibrary.Export;
        exportAccountsIifToolStripMenuItem.Name = "exportAccountsIifToolStripMenuItem";
        exportAccountsIifToolStripMenuItem.Click += OnExportToolStripMenuItemClick;
        // 
        // toolStripSeparator1
        // 
        resources.ApplyResources(toolStripSeparator1, "toolStripSeparator1");
        toolStripSeparator1.Name = "toolStripSeparator1";
        // 
        // editToolStripMenuItem
        // 
        resources.ApplyResources(editToolStripMenuItem, "editToolStripMenuItem");
        editToolStripMenuItem.Image = VisualStudioImageLibrary.Settings;
        editToolStripMenuItem.Name = "editToolStripMenuItem";
        editToolStripMenuItem.Click += OnEditToolStripMenuItemClick;
        // 
        // toolStripSeparator8
        // 
        resources.ApplyResources(toolStripSeparator8, "toolStripSeparator8");
        toolStripSeparator8.Name = "toolStripSeparator8";
        // 
        // recentPathsToolStripMenuItem
        // 
        resources.ApplyResources(recentPathsToolStripMenuItem, "recentPathsToolStripMenuItem");
        recentPathsToolStripMenuItem.Name = "recentPathsToolStripMenuItem";
        // 
        // recentPathsToolStripSeparator
        // 
        resources.ApplyResources(recentPathsToolStripSeparator, "recentPathsToolStripSeparator");
        recentPathsToolStripSeparator.Name = "recentPathsToolStripSeparator";
        // 
        // exitToolStripMenuItem
        // 
        resources.ApplyResources(exitToolStripMenuItem, "exitToolStripMenuItem");
        exitToolStripMenuItem.Name = "exitToolStripMenuItem";
        exitToolStripMenuItem.Click += OnExitToolStripMenuItemClick;
        // 
        // editToolStripMenuItem1
        // 
        resources.ApplyResources(editToolStripMenuItem1, "editToolStripMenuItem1");
        editToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { cultureToolStripMenuItem, importSettingsToolStripMenuItem, toolStripSeparator7, combinePdfDocumentsToolStripMenuItem });
        editToolStripMenuItem1.Name = "editToolStripMenuItem1";
        // 
        // cultureToolStripMenuItem
        // 
        resources.ApplyResources(cultureToolStripMenuItem, "cultureToolStripMenuItem");
        cultureToolStripMenuItem.Image = VisualStudioImageLibrary.SetLanguage;
        cultureToolStripMenuItem.Name = "cultureToolStripMenuItem";
        cultureToolStripMenuItem.Click += OnCultureToolStripMenuItemClick;
        // 
        // importSettingsToolStripMenuItem
        // 
        resources.ApplyResources(importSettingsToolStripMenuItem, "importSettingsToolStripMenuItem");
        importSettingsToolStripMenuItem.Image = VisualStudioImageLibrary.ImportSettings;
        importSettingsToolStripMenuItem.Name = "importSettingsToolStripMenuItem";
        importSettingsToolStripMenuItem.Click += OnImportSettingsToolStripMenuItemClick;
        // 
        // toolStripSeparator7
        // 
        resources.ApplyResources(toolStripSeparator7, "toolStripSeparator7");
        toolStripSeparator7.Name = "toolStripSeparator7";
        // 
        // combinePdfDocumentsToolStripMenuItem
        // 
        resources.ApplyResources(combinePdfDocumentsToolStripMenuItem, "combinePdfDocumentsToolStripMenuItem");
        combinePdfDocumentsToolStripMenuItem.Name = "combinePdfDocumentsToolStripMenuItem";
        combinePdfDocumentsToolStripMenuItem.Click += OnCombinePdfDocumentsToolStripMenuItemClick;
        // 
        // viewToolStripMenuItem
        // 
        resources.ApplyResources(viewToolStripMenuItem, "viewToolStripMenuItem");
        viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { accountsToolStripMenuItem, transactionToolStripMenuItem, toolStripSeparator2, checkToolStripMenuItem, transactionsToolStripMenuItem, toolStripSeparator4, reportsToolStripMenuItem, toolStripSeparator9, otherWindowsToolStripMenuItem });
        viewToolStripMenuItem.Name = "viewToolStripMenuItem";
        // 
        // accountsToolStripMenuItem
        // 
        resources.ApplyResources(accountsToolStripMenuItem, "accountsToolStripMenuItem");
        accountsToolStripMenuItem.Image = VisualStudioImageLibrary.TableGroup;
        accountsToolStripMenuItem.Name = "accountsToolStripMenuItem";
        accountsToolStripMenuItem.Click += OnAccountsToolStripMenuItemClick;
        // 
        // transactionToolStripMenuItem
        // 
        resources.ApplyResources(transactionToolStripMenuItem, "transactionToolStripMenuItem");
        transactionToolStripMenuItem.Image = VisualStudioImageLibrary.JournalMessage;
        transactionToolStripMenuItem.Name = "transactionToolStripMenuItem";
        transactionToolStripMenuItem.Click += OnTransactionToolStripMenuItemClick;
        // 
        // toolStripSeparator2
        // 
        resources.ApplyResources(toolStripSeparator2, "toolStripSeparator2");
        toolStripSeparator2.Name = "toolStripSeparator2";
        // 
        // checkToolStripMenuItem
        // 
        resources.ApplyResources(checkToolStripMenuItem, "checkToolStripMenuItem");
        checkToolStripMenuItem.Name = "checkToolStripMenuItem";
        checkToolStripMenuItem.Click += OnCheckToolStripMenuItemClick;
        // 
        // transactionsToolStripMenuItem
        // 
        resources.ApplyResources(transactionsToolStripMenuItem, "transactionsToolStripMenuItem");
        transactionsToolStripMenuItem.Image = VisualStudioImageLibrary.Log;
        transactionsToolStripMenuItem.Name = "transactionsToolStripMenuItem";
        transactionsToolStripMenuItem.Click += OnTransactionsToolStripMenuItemClick;
        // 
        // toolStripSeparator4
        // 
        resources.ApplyResources(toolStripSeparator4, "toolStripSeparator4");
        toolStripSeparator4.Name = "toolStripSeparator4";
        // 
        // reportsToolStripMenuItem
        // 
        resources.ApplyResources(reportsToolStripMenuItem, "reportsToolStripMenuItem");
        reportsToolStripMenuItem.Image = VisualStudioImageLibrary.Report;
        reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
        reportsToolStripMenuItem.Click += OnReportsToolStripMenuItemClick;
        // 
        // toolStripSeparator9
        // 
        resources.ApplyResources(toolStripSeparator9, "toolStripSeparator9");
        toolStripSeparator9.Name = "toolStripSeparator9";
        // 
        // otherWindowsToolStripMenuItem
        // 
        resources.ApplyResources(otherWindowsToolStripMenuItem, "otherWindowsToolStripMenuItem");
        otherWindowsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { newAccountToolStripMenuItem, removeAccountToolStripMenuItem });
        otherWindowsToolStripMenuItem.Name = "otherWindowsToolStripMenuItem";
        // 
        // newAccountToolStripMenuItem
        // 
        resources.ApplyResources(newAccountToolStripMenuItem, "newAccountToolStripMenuItem");
        newAccountToolStripMenuItem.Image = VisualStudioImageLibrary.AddTable;
        newAccountToolStripMenuItem.Name = "newAccountToolStripMenuItem";
        newAccountToolStripMenuItem.Click += OnNewAccountToolStripMenuItemClick;
        // 
        // removeAccountToolStripMenuItem
        // 
        resources.ApplyResources(removeAccountToolStripMenuItem, "removeAccountToolStripMenuItem");
        removeAccountToolStripMenuItem.Image = VisualStudioImageLibrary.DeleteTable;
        removeAccountToolStripMenuItem.Name = "removeAccountToolStripMenuItem";
        removeAccountToolStripMenuItem.Click += OnRemoveAccountToolStripMenuItemClick;
        // 
        // reportsToolStripMenuItem1
        // 
        resources.ApplyResources(reportsToolStripMenuItem1, "reportsToolStripMenuItem1");
        reportsToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { otherReportsToolStripMenuItem });
        reportsToolStripMenuItem1.Name = "reportsToolStripMenuItem1";
        // 
        // otherReportsToolStripMenuItem
        // 
        resources.ApplyResources(otherReportsToolStripMenuItem, "otherReportsToolStripMenuItem");
        otherReportsToolStripMenuItem.Name = "otherReportsToolStripMenuItem";
        // 
        // formToolStripMenuItem
        // 
        resources.ApplyResources(formToolStripMenuItem, "formToolStripMenuItem");
        formToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { closeAllToolStripMenuItem, formsToolStripSeparator, formsToolStripMenuItem });
        formToolStripMenuItem.Name = "formToolStripMenuItem";
        // 
        // closeAllToolStripMenuItem
        // 
        resources.ApplyResources(closeAllToolStripMenuItem, "closeAllToolStripMenuItem");
        closeAllToolStripMenuItem.Image = VisualStudioImageLibrary.CloseAll;
        closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
        closeAllToolStripMenuItem.Click += OnCloseAllToolStripMenuItem_Click;
        // 
        // formsToolStripSeparator
        // 
        resources.ApplyResources(formsToolStripSeparator, "formsToolStripSeparator");
        formsToolStripSeparator.Name = "formsToolStripSeparator";
        // 
        // formsToolStripMenuItem
        // 
        resources.ApplyResources(formsToolStripMenuItem, "formsToolStripMenuItem");
        formsToolStripMenuItem.Name = "formsToolStripMenuItem";
        // 
        // helpToolStripMenuItem1
        // 
        resources.ApplyResources(helpToolStripMenuItem1, "helpToolStripMenuItem1");
        helpToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { contentsToolStripMenuItem, indexToolStripMenuItem, searchToolStripMenuItem, toolStripSeparator16, aboutToolStripMenuItem });
        helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
        // 
        // contentsToolStripMenuItem
        // 
        resources.ApplyResources(contentsToolStripMenuItem, "contentsToolStripMenuItem");
        contentsToolStripMenuItem.Image = VisualStudioImageLibrary.HelpTableOfContents;
        contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
        // 
        // indexToolStripMenuItem
        // 
        resources.ApplyResources(indexToolStripMenuItem, "indexToolStripMenuItem");
        indexToolStripMenuItem.Image = VisualStudioImageLibrary.HelpIndexFile;
        indexToolStripMenuItem.Name = "indexToolStripMenuItem";
        // 
        // searchToolStripMenuItem
        // 
        resources.ApplyResources(searchToolStripMenuItem, "searchToolStripMenuItem");
        searchToolStripMenuItem.Image = VisualStudioImageLibrary.Search;
        searchToolStripMenuItem.Name = "searchToolStripMenuItem";
        // 
        // toolStripSeparator16
        // 
        resources.ApplyResources(toolStripSeparator16, "toolStripSeparator16");
        toolStripSeparator16.Name = "toolStripSeparator16";
        // 
        // aboutToolStripMenuItem
        // 
        resources.ApplyResources(aboutToolStripMenuItem, "aboutToolStripMenuItem");
        aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
        aboutToolStripMenuItem.Click += OnAboutToolStripMenuItemClick;
        // 
        // _toolStrip
        // 
        resources.ApplyResources(_toolStrip, "_toolStrip");
        _toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { newToolStripButton, openToolStripButton, saveToolStripButton, toolStripSeparator6, accountsToolStripButton, transactionToolStripButton, toolStripSeparator10, transactionsToolStripButton, toolStripSeparator11, reportsToolStripButton });
        _toolStrip.Name = "_toolStrip";
        // 
        // newToolStripButton
        // 
        resources.ApplyResources(newToolStripButton, "newToolStripButton");
        newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        newToolStripButton.Image = VisualStudioImageLibrary.AddDocument;
        newToolStripButton.Name = "newToolStripButton";
        newToolStripButton.Click += OnNewToolStripMenuItemClick;
        // 
        // openToolStripButton
        // 
        resources.ApplyResources(openToolStripButton, "openToolStripButton");
        openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        openToolStripButton.Image = VisualStudioImageLibrary.OpenFile;
        openToolStripButton.Name = "openToolStripButton";
        openToolStripButton.Click += OnOpenToolStripMenuItemClick;
        // 
        // saveToolStripButton
        // 
        resources.ApplyResources(saveToolStripButton, "saveToolStripButton");
        saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        saveToolStripButton.Image = VisualStudioImageLibrary.Save;
        saveToolStripButton.Name = "saveToolStripButton";
        saveToolStripButton.Click += OnSaveToolStripMenuItemClick;
        // 
        // toolStripSeparator6
        // 
        resources.ApplyResources(toolStripSeparator6, "toolStripSeparator6");
        toolStripSeparator6.Name = "toolStripSeparator6";
        // 
        // accountsToolStripButton
        // 
        resources.ApplyResources(accountsToolStripButton, "accountsToolStripButton");
        accountsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        accountsToolStripButton.Image = VisualStudioImageLibrary.TableGroup;
        accountsToolStripButton.Name = "accountsToolStripButton";
        accountsToolStripButton.Click += OnAccountsToolStripMenuItemClick;
        // 
        // transactionToolStripButton
        // 
        resources.ApplyResources(transactionToolStripButton, "transactionToolStripButton");
        transactionToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        transactionToolStripButton.Image = VisualStudioImageLibrary.JournalMessage;
        transactionToolStripButton.Name = "transactionToolStripButton";
        transactionToolStripButton.Click += OnTransactionToolStripMenuItemClick;
        // 
        // toolStripSeparator10
        // 
        resources.ApplyResources(toolStripSeparator10, "toolStripSeparator10");
        toolStripSeparator10.Name = "toolStripSeparator10";
        // 
        // transactionsToolStripButton
        // 
        resources.ApplyResources(transactionsToolStripButton, "transactionsToolStripButton");
        transactionsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        transactionsToolStripButton.Image = VisualStudioImageLibrary.Log;
        transactionsToolStripButton.Name = "transactionsToolStripButton";
        transactionsToolStripButton.Click += OnTransactionsToolStripMenuItemClick;
        // 
        // toolStripSeparator11
        // 
        resources.ApplyResources(toolStripSeparator11, "toolStripSeparator11");
        toolStripSeparator11.Name = "toolStripSeparator11";
        // 
        // reportsToolStripButton
        // 
        resources.ApplyResources(reportsToolStripButton, "reportsToolStripButton");
        reportsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        reportsToolStripButton.Image = VisualStudioImageLibrary.Report;
        reportsToolStripButton.Name = "reportsToolStripButton";
        reportsToolStripButton.Click += OnReportsToolStripMenuItemClick;
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
        _recentPathManager.MaxPaths = 1;
        _recentPathManager.Invalidated += OnRecentPathManagerInvalidated;
        // 
        // _factory
        // 
        _factory.Invalidated += OnFactoryInvalidated;
        // 
        // _timer
        // 
        _timer.Interval = 30000;
        _timer.Tick += OnTimerTick;
        // 
        // MainForm
        // 
        resources.ApplyResources(this, "$this");
        AllowDrop = true;
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        Controls.Add(_toolStrip);
        Controls.Add(_menuStrip);
        IsMdiContainer = true;
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
    private System.Windows.Forms.ToolStrip _toolStrip;
    private System.Windows.Forms.ToolStripButton newToolStripButton;
    private System.Windows.Forms.ToolStripButton openToolStripButton;
    private System.Windows.Forms.ToolStripButton saveToolStripButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
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
    private System.Windows.Forms.ToolStripMenuItem cultureToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem exportAccountsToolStripMenuItem;
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
    private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
    private System.Windows.Forms.ToolStripMenuItem combinePdfDocumentsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem otherReportsToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
    private System.Windows.Forms.Timer _timer;
    private System.Windows.Forms.ToolStripMenuItem transactionsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem formToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator formsToolStripSeparator;
    private System.Windows.Forms.ToolStripMenuItem formsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem importSettingsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem removeAccountToolStripMenuItem;
    private System.Windows.Forms.ToolStripButton accountsToolStripButton;
    private System.Windows.Forms.ToolStripButton transactionToolStripButton;
    private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem contentsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem indexToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
    private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    private System.Windows.Forms.ToolStripButton transactionsToolStripButton;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
    private System.Windows.Forms.ToolStripButton reportsToolStripButton;
}
