using Liber.Forms.Forms;
using Liber.Forms.RecentPaths;

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
        exportCompanyJsonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        exportCompanyXmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
        exportAccountsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        exportTransactionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
        exportAccountsIifToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
        editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
        anonymizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
        recentPathsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        recentPathsToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
        exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        editToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        cultureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        importSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
        unreconcileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
        combinePdfDocumentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        accountsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        transactionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
        checkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        accountTransactionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
        reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
        taskItemsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
        otherWindowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        newAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        reconcileAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        removeAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
        nameTransactionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        reportsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        otherReportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        formToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        formsToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
        formsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        formsToolStripMenUtem = new System.Windows.Forms.ToolStripMenuItem();
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
        toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
        taskItemsToolStripButton = new System.Windows.Forms.ToolStripButton();
        _openFileDialog = new System.Windows.Forms.OpenFileDialog();
        _saveFileDialog = new System.Windows.Forms.SaveFileDialog();
        _recentPathManager = new RecentPathManager(components);
        _factory = new FormFactory(components);
        toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
        splashScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        _menuStrip.SuspendLayout();
        _toolStrip.SuspendLayout();
        SuspendLayout();
        // 
        // _menuStrip
        // 
        _menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem1, viewToolStripMenuItem, reportsToolStripMenuItem1, formToolStripMenuItem, helpToolStripMenuItem1 });
        resources.ApplyResources(_menuStrip, "_menuStrip");
        _menuStrip.Name = "_menuStrip";
        // 
        // fileToolStripMenuItem
        // 
        fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { newToolStripMenuItem, openToolStripMenuItem, importToolStripMenuItem, toolStripSeparator, saveToolStripMenuItem, saveAsToolStripMenuItem, exportToolStripMenuItem, toolStripSeparator1, editToolStripMenuItem, toolStripSeparator12, anonymizeToolStripMenuItem, toolStripSeparator8, recentPathsToolStripMenuItem, recentPathsToolStripSeparator, exitToolStripMenuItem });
        fileToolStripMenuItem.Name = "fileToolStripMenuItem";
        resources.ApplyResources(fileToolStripMenuItem, "fileToolStripMenuItem");
        // 
        // newToolStripMenuItem
        // 
        newToolStripMenuItem.Image = VisualStudioImageLibrary.AddDocument;
        resources.ApplyResources(newToolStripMenuItem, "newToolStripMenuItem");
        newToolStripMenuItem.Name = "newToolStripMenuItem";
        newToolStripMenuItem.Click += OnNewToolStripMenuItemClick;
        // 
        // openToolStripMenuItem
        // 
        openToolStripMenuItem.Image = VisualStudioImageLibrary.OpenFile;
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
        importAccountsToolStripMenuItem.Image = VisualStudioImageLibrary.Import;
        importAccountsToolStripMenuItem.Name = "importAccountsToolStripMenuItem";
        resources.ApplyResources(importAccountsToolStripMenuItem, "importAccountsToolStripMenuItem");
        importAccountsToolStripMenuItem.Click += OnImportAccountsToolStripMenuItemClick;
        // 
        // importTransactionsToolStripMenuItem
        // 
        importTransactionsToolStripMenuItem.Image = VisualStudioImageLibrary.Import;
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
        saveToolStripMenuItem.Image = VisualStudioImageLibrary.Save;
        resources.ApplyResources(saveToolStripMenuItem, "saveToolStripMenuItem");
        saveToolStripMenuItem.Name = "saveToolStripMenuItem";
        saveToolStripMenuItem.Click += OnSaveToolStripMenuItemClick;
        // 
        // saveAsToolStripMenuItem
        // 
        saveAsToolStripMenuItem.Image = VisualStudioImageLibrary.SaveAs;
        saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
        resources.ApplyResources(saveAsToolStripMenuItem, "saveAsToolStripMenuItem");
        saveAsToolStripMenuItem.Click += OnSaveAsToolStripMenuItemClick;
        // 
        // exportToolStripMenuItem
        // 
        exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { exportCompanyJsonToolStripMenuItem, exportCompanyXmlToolStripMenuItem, toolStripSeparator3, exportAccountsToolStripMenuItem, exportTransactionsToolStripMenuItem, toolStripSeparator5, exportAccountsIifToolStripMenuItem });
        exportToolStripMenuItem.Name = "exportToolStripMenuItem";
        resources.ApplyResources(exportToolStripMenuItem, "exportToolStripMenuItem");
        // 
        // exportCompanyJsonToolStripMenuItem
        // 
        exportCompanyJsonToolStripMenuItem.Image = VisualStudioImageLibrary.Export;
        exportCompanyJsonToolStripMenuItem.Name = "exportCompanyJsonToolStripMenuItem";
        resources.ApplyResources(exportCompanyJsonToolStripMenuItem, "exportCompanyJsonToolStripMenuItem");
        exportCompanyJsonToolStripMenuItem.Click += OnExportToolStripMenuItemClick;
        // 
        // exportCompanyXmlToolStripMenuItem
        // 
        exportCompanyXmlToolStripMenuItem.Image = VisualStudioImageLibrary.Export;
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
        exportAccountsToolStripMenuItem.Image = VisualStudioImageLibrary.Export;
        exportAccountsToolStripMenuItem.Name = "exportAccountsToolStripMenuItem";
        resources.ApplyResources(exportAccountsToolStripMenuItem, "exportAccountsToolStripMenuItem");
        exportAccountsToolStripMenuItem.Click += OnExportToolStripMenuItemClick;
        // 
        // exportTransactionsToolStripMenuItem
        // 
        exportTransactionsToolStripMenuItem.Image = VisualStudioImageLibrary.Export;
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
        exportAccountsIifToolStripMenuItem.Image = VisualStudioImageLibrary.Export;
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
        editToolStripMenuItem.Image = VisualStudioImageLibrary.Settings;
        editToolStripMenuItem.Name = "editToolStripMenuItem";
        resources.ApplyResources(editToolStripMenuItem, "editToolStripMenuItem");
        editToolStripMenuItem.Click += OnEditToolStripMenuItemClick;
        // 
        // toolStripSeparator12
        // 
        toolStripSeparator12.Name = "toolStripSeparator12";
        resources.ApplyResources(toolStripSeparator12, "toolStripSeparator12");
        // 
        // anonymizeToolStripMenuItem
        // 
        anonymizeToolStripMenuItem.Name = "anonymizeToolStripMenuItem";
        resources.ApplyResources(anonymizeToolStripMenuItem, "anonymizeToolStripMenuItem");
        anonymizeToolStripMenuItem.Click += OnAnonymizeToolStripMenuItemClick;
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
        editToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { cultureToolStripMenuItem, importSettingsToolStripMenuItem, toolStripSeparator7, unreconcileToolStripMenuItem, toolStripSeparator17, combinePdfDocumentsToolStripMenuItem });
        editToolStripMenuItem1.Name = "editToolStripMenuItem1";
        resources.ApplyResources(editToolStripMenuItem1, "editToolStripMenuItem1");
        // 
        // cultureToolStripMenuItem
        // 
        cultureToolStripMenuItem.Image = VisualStudioImageLibrary.SetLanguage;
        cultureToolStripMenuItem.Name = "cultureToolStripMenuItem";
        resources.ApplyResources(cultureToolStripMenuItem, "cultureToolStripMenuItem");
        cultureToolStripMenuItem.Click += OnCultureToolStripMenuItemClick;
        // 
        // importSettingsToolStripMenuItem
        // 
        importSettingsToolStripMenuItem.Image = VisualStudioImageLibrary.ImportSettings;
        importSettingsToolStripMenuItem.Name = "importSettingsToolStripMenuItem";
        resources.ApplyResources(importSettingsToolStripMenuItem, "importSettingsToolStripMenuItem");
        importSettingsToolStripMenuItem.Click += OnImportSettingsToolStripMenuItemClick;
        // 
        // toolStripSeparator7
        // 
        toolStripSeparator7.Name = "toolStripSeparator7";
        resources.ApplyResources(toolStripSeparator7, "toolStripSeparator7");
        // 
        // unreconcileToolStripMenuItem
        // 
        unreconcileToolStripMenuItem.Name = "unreconcileToolStripMenuItem";
        resources.ApplyResources(unreconcileToolStripMenuItem, "unreconcileToolStripMenuItem");
        unreconcileToolStripMenuItem.Click += OnUnreconcileToolStripMenuItemClick;
        // 
        // toolStripSeparator17
        // 
        toolStripSeparator17.Name = "toolStripSeparator17";
        resources.ApplyResources(toolStripSeparator17, "toolStripSeparator17");
        // 
        // combinePdfDocumentsToolStripMenuItem
        // 
        combinePdfDocumentsToolStripMenuItem.Name = "combinePdfDocumentsToolStripMenuItem";
        resources.ApplyResources(combinePdfDocumentsToolStripMenuItem, "combinePdfDocumentsToolStripMenuItem");
        combinePdfDocumentsToolStripMenuItem.Click += OnCombinePdfDocumentsToolStripMenuItemClick;
        // 
        // viewToolStripMenuItem
        // 
        viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { accountsToolStripMenuItem, transactionToolStripMenuItem, toolStripSeparator2, checkToolStripMenuItem, accountTransactionsToolStripMenuItem, toolStripSeparator4, reportsToolStripMenuItem, toolStripSeparator9, taskItemsToolStripMenuItem, toolStripSeparator14, otherWindowsToolStripMenuItem });
        viewToolStripMenuItem.Name = "viewToolStripMenuItem";
        resources.ApplyResources(viewToolStripMenuItem, "viewToolStripMenuItem");
        // 
        // accountsToolStripMenuItem
        // 
        accountsToolStripMenuItem.Image = VisualStudioImageLibrary.TableGroup;
        accountsToolStripMenuItem.Name = "accountsToolStripMenuItem";
        resources.ApplyResources(accountsToolStripMenuItem, "accountsToolStripMenuItem");
        accountsToolStripMenuItem.Click += OnAccountsToolStripMenuItemClick;
        // 
        // transactionToolStripMenuItem
        // 
        transactionToolStripMenuItem.Image = VisualStudioImageLibrary.JournalMessage;
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
        // accountTransactionsToolStripMenuItem
        // 
        accountTransactionsToolStripMenuItem.Image = VisualStudioImageLibrary.Log;
        accountTransactionsToolStripMenuItem.Name = "accountTransactionsToolStripMenuItem";
        resources.ApplyResources(accountTransactionsToolStripMenuItem, "accountTransactionsToolStripMenuItem");
        accountTransactionsToolStripMenuItem.Click += OnAccountTransactionsToolStripMenuItemClick;
        // 
        // toolStripSeparator4
        // 
        toolStripSeparator4.Name = "toolStripSeparator4";
        resources.ApplyResources(toolStripSeparator4, "toolStripSeparator4");
        // 
        // reportsToolStripMenuItem
        // 
        reportsToolStripMenuItem.Image = VisualStudioImageLibrary.Report;
        reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
        resources.ApplyResources(reportsToolStripMenuItem, "reportsToolStripMenuItem");
        reportsToolStripMenuItem.Click += OnReportsToolStripMenuItemClick;
        // 
        // toolStripSeparator9
        // 
        toolStripSeparator9.Name = "toolStripSeparator9";
        resources.ApplyResources(toolStripSeparator9, "toolStripSeparator9");
        // 
        // taskItemsToolStripMenuItem
        // 
        taskItemsToolStripMenuItem.Image = VisualStudioImageLibrary.TaskList;
        taskItemsToolStripMenuItem.Name = "taskItemsToolStripMenuItem";
        resources.ApplyResources(taskItemsToolStripMenuItem, "taskItemsToolStripMenuItem");
        taskItemsToolStripMenuItem.Click += OnTaskItemsToolStripMenuItemClick;
        // 
        // toolStripSeparator14
        // 
        toolStripSeparator14.Name = "toolStripSeparator14";
        resources.ApplyResources(toolStripSeparator14, "toolStripSeparator14");
        // 
        // otherWindowsToolStripMenuItem
        // 
        otherWindowsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { newAccountToolStripMenuItem, reconcileAccountToolStripMenuItem, removeAccountToolStripMenuItem, toolStripSeparator13, nameTransactionsToolStripMenuItem, toolStripSeparator18, splashScreenToolStripMenuItem });
        otherWindowsToolStripMenuItem.Name = "otherWindowsToolStripMenuItem";
        resources.ApplyResources(otherWindowsToolStripMenuItem, "otherWindowsToolStripMenuItem");
        // 
        // newAccountToolStripMenuItem
        // 
        newAccountToolStripMenuItem.Image = VisualStudioImageLibrary.AddTable;
        newAccountToolStripMenuItem.Name = "newAccountToolStripMenuItem";
        resources.ApplyResources(newAccountToolStripMenuItem, "newAccountToolStripMenuItem");
        newAccountToolStripMenuItem.Click += OnNewAccountToolStripMenuItemClick;
        // 
        // reconcileAccountToolStripMenuItem
        // 
        reconcileAccountToolStripMenuItem.Name = "reconcileAccountToolStripMenuItem";
        resources.ApplyResources(reconcileAccountToolStripMenuItem, "reconcileAccountToolStripMenuItem");
        reconcileAccountToolStripMenuItem.Click += OnReconcileAccountToolStripMenuItemClick;
        // 
        // removeAccountToolStripMenuItem
        // 
        removeAccountToolStripMenuItem.Image = VisualStudioImageLibrary.DeleteTable;
        removeAccountToolStripMenuItem.Name = "removeAccountToolStripMenuItem";
        resources.ApplyResources(removeAccountToolStripMenuItem, "removeAccountToolStripMenuItem");
        removeAccountToolStripMenuItem.Click += OnRemoveAccountToolStripMenuItemClick;
        // 
        // toolStripSeparator13
        // 
        toolStripSeparator13.Name = "toolStripSeparator13";
        resources.ApplyResources(toolStripSeparator13, "toolStripSeparator13");
        // 
        // nameTransactionsToolStripMenuItem
        // 
        nameTransactionsToolStripMenuItem.Name = "nameTransactionsToolStripMenuItem";
        resources.ApplyResources(nameTransactionsToolStripMenuItem, "nameTransactionsToolStripMenuItem");
        nameTransactionsToolStripMenuItem.Click += OnNameTransactionsToolStripMenuItemClick;
        // 
        // reportsToolStripMenuItem1
        // 
        reportsToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { otherReportsToolStripMenuItem });
        reportsToolStripMenuItem1.Name = "reportsToolStripMenuItem1";
        resources.ApplyResources(reportsToolStripMenuItem1, "reportsToolStripMenuItem1");
        // 
        // otherReportsToolStripMenuItem
        // 
        otherReportsToolStripMenuItem.Name = "otherReportsToolStripMenuItem";
        resources.ApplyResources(otherReportsToolStripMenuItem, "otherReportsToolStripMenuItem");
        // 
        // formToolStripMenuItem
        // 
        formToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { closeAllToolStripMenuItem, formsToolStripSeparator, formsToolStripMenuItem1, formsToolStripMenUtem });
        formToolStripMenuItem.Name = "formToolStripMenuItem";
        resources.ApplyResources(formToolStripMenuItem, "formToolStripMenuItem");
        // 
        // closeAllToolStripMenuItem
        // 
        closeAllToolStripMenuItem.Image = VisualStudioImageLibrary.CloseAll;
        closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
        resources.ApplyResources(closeAllToolStripMenuItem, "closeAllToolStripMenuItem");
        closeAllToolStripMenuItem.Click += OnCloseAllToolStripMenuItem_Click;
        // 
        // formsToolStripSeparator
        // 
        formsToolStripSeparator.Name = "formsToolStripSeparator";
        resources.ApplyResources(formsToolStripSeparator, "formsToolStripSeparator");
        // 
        // formsToolStripMenuItem1
        // 
        formsToolStripMenuItem1.Name = "formsToolStripMenuItem1";
        resources.ApplyResources(formsToolStripMenuItem1, "formsToolStripMenuItem1");
        // 
        // formsToolStripMenUtem
        // 
        formsToolStripMenUtem.Name = "formsToolStripMenUtem";
        resources.ApplyResources(formsToolStripMenUtem, "formsToolStripMenUtem");
        formsToolStripMenUtem.Click += OnFormsToolStripMenuItemClick;
        // 
        // helpToolStripMenuItem1
        // 
        helpToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { contentsToolStripMenuItem, indexToolStripMenuItem, searchToolStripMenuItem, toolStripSeparator16, aboutToolStripMenuItem });
        helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
        resources.ApplyResources(helpToolStripMenuItem1, "helpToolStripMenuItem1");
        // 
        // contentsToolStripMenuItem
        // 
        contentsToolStripMenuItem.Image = VisualStudioImageLibrary.HelpTableOfContents;
        contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
        resources.ApplyResources(contentsToolStripMenuItem, "contentsToolStripMenuItem");
        contentsToolStripMenuItem.Click += OnContentsToolStripMenuItemClick;
        // 
        // indexToolStripMenuItem
        // 
        indexToolStripMenuItem.Image = VisualStudioImageLibrary.HelpIndexFile;
        indexToolStripMenuItem.Name = "indexToolStripMenuItem";
        resources.ApplyResources(indexToolStripMenuItem, "indexToolStripMenuItem");
        indexToolStripMenuItem.Click += OnIndexToolStripMenuItemClick;
        // 
        // searchToolStripMenuItem
        // 
        searchToolStripMenuItem.Image = VisualStudioImageLibrary.Search;
        searchToolStripMenuItem.Name = "searchToolStripMenuItem";
        resources.ApplyResources(searchToolStripMenuItem, "searchToolStripMenuItem");
        searchToolStripMenuItem.Click += OnSearchToolStripMenuItemClick;
        // 
        // toolStripSeparator16
        // 
        toolStripSeparator16.Name = "toolStripSeparator16";
        resources.ApplyResources(toolStripSeparator16, "toolStripSeparator16");
        // 
        // aboutToolStripMenuItem
        // 
        aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
        resources.ApplyResources(aboutToolStripMenuItem, "aboutToolStripMenuItem");
        aboutToolStripMenuItem.Click += OnAboutToolStripMenuItemClick;
        // 
        // _toolStrip
        // 
        _toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { newToolStripButton, openToolStripButton, saveToolStripButton, toolStripSeparator6, accountsToolStripButton, transactionToolStripButton, toolStripSeparator10, transactionsToolStripButton, toolStripSeparator11, reportsToolStripButton, toolStripSeparator15, taskItemsToolStripButton });
        resources.ApplyResources(_toolStrip, "_toolStrip");
        _toolStrip.Name = "_toolStrip";
        // 
        // newToolStripButton
        // 
        newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        newToolStripButton.Image = VisualStudioImageLibrary.AddDocument;
        resources.ApplyResources(newToolStripButton, "newToolStripButton");
        newToolStripButton.Name = "newToolStripButton";
        newToolStripButton.Click += OnNewToolStripMenuItemClick;
        // 
        // openToolStripButton
        // 
        openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        openToolStripButton.Image = VisualStudioImageLibrary.OpenFile;
        resources.ApplyResources(openToolStripButton, "openToolStripButton");
        openToolStripButton.Name = "openToolStripButton";
        openToolStripButton.Click += OnOpenToolStripMenuItemClick;
        // 
        // saveToolStripButton
        // 
        saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        saveToolStripButton.Image = VisualStudioImageLibrary.Save;
        resources.ApplyResources(saveToolStripButton, "saveToolStripButton");
        saveToolStripButton.Name = "saveToolStripButton";
        saveToolStripButton.Click += OnSaveToolStripMenuItemClick;
        // 
        // toolStripSeparator6
        // 
        toolStripSeparator6.Name = "toolStripSeparator6";
        resources.ApplyResources(toolStripSeparator6, "toolStripSeparator6");
        // 
        // accountsToolStripButton
        // 
        accountsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        accountsToolStripButton.Image = VisualStudioImageLibrary.TableGroup;
        resources.ApplyResources(accountsToolStripButton, "accountsToolStripButton");
        accountsToolStripButton.Name = "accountsToolStripButton";
        accountsToolStripButton.Click += OnAccountsToolStripMenuItemClick;
        // 
        // transactionToolStripButton
        // 
        transactionToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        transactionToolStripButton.Image = VisualStudioImageLibrary.JournalMessage;
        resources.ApplyResources(transactionToolStripButton, "transactionToolStripButton");
        transactionToolStripButton.Name = "transactionToolStripButton";
        transactionToolStripButton.Click += OnTransactionToolStripMenuItemClick;
        // 
        // toolStripSeparator10
        // 
        toolStripSeparator10.Name = "toolStripSeparator10";
        resources.ApplyResources(toolStripSeparator10, "toolStripSeparator10");
        // 
        // transactionsToolStripButton
        // 
        transactionsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        transactionsToolStripButton.Image = VisualStudioImageLibrary.Log;
        resources.ApplyResources(transactionsToolStripButton, "transactionsToolStripButton");
        transactionsToolStripButton.Name = "transactionsToolStripButton";
        transactionsToolStripButton.Click += OnAccountTransactionsToolStripMenuItemClick;
        // 
        // toolStripSeparator11
        // 
        toolStripSeparator11.Name = "toolStripSeparator11";
        resources.ApplyResources(toolStripSeparator11, "toolStripSeparator11");
        // 
        // reportsToolStripButton
        // 
        reportsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        reportsToolStripButton.Image = VisualStudioImageLibrary.Report;
        resources.ApplyResources(reportsToolStripButton, "reportsToolStripButton");
        reportsToolStripButton.Name = "reportsToolStripButton";
        reportsToolStripButton.Click += OnReportsToolStripMenuItemClick;
        // 
        // toolStripSeparator15
        // 
        toolStripSeparator15.Name = "toolStripSeparator15";
        resources.ApplyResources(toolStripSeparator15, "toolStripSeparator15");
        // 
        // taskItemsToolStripButton
        // 
        taskItemsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
        taskItemsToolStripButton.Image = VisualStudioImageLibrary.TaskList;
        resources.ApplyResources(taskItemsToolStripButton, "taskItemsToolStripButton");
        taskItemsToolStripButton.Name = "taskItemsToolStripButton";
        taskItemsToolStripButton.Click += OnTaskItemsToolStripMenuItemClick;
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
        _recentPathManager.MaxPaths = 10;
        _recentPathManager.Invalidated += OnRecentPathManagerInvalidated;
        // 
        // _factory
        // 
        _factory.Invalidated += OnFactoryInvalidated;
        // 
        // toolStripSeparator18
        // 
        toolStripSeparator18.Name = "toolStripSeparator18";
        resources.ApplyResources(toolStripSeparator18, "toolStripSeparator18");
        // 
        // splashScreenToolStripMenuItem
        // 
        splashScreenToolStripMenuItem.Name = "splashScreenToolStripMenuItem";
        resources.ApplyResources(splashScreenToolStripMenuItem, "splashScreenToolStripMenuItem");
        splashScreenToolStripMenuItem.Click += OnSplashScreenToolStripMenuItemClick;
        // 
        // MainForm
        // 
        AllowDrop = true;
        resources.ApplyResources(this, "$this");
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
    private System.Windows.Forms.ToolStripMenuItem accountTransactionsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem formToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator formsToolStripSeparator;
    private System.Windows.Forms.ToolStripMenuItem formsToolStripMenuItem1;
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
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
    private System.Windows.Forms.ToolStripMenuItem anonymizeToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem exportCompanyJsonToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
    private System.Windows.Forms.ToolStripMenuItem nameTransactionsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem reconcileAccountToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem taskItemsToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
    private System.Windows.Forms.ToolStripMenuItem formsToolStripMenUtem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
    private System.Windows.Forms.ToolStripButton taskItemsToolStripButton;
    private System.Windows.Forms.ToolStripMenuItem unreconcileToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator17;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator18;
    private System.Windows.Forms.ToolStripMenuItem splashScreenToolStripMenuItem;
}
