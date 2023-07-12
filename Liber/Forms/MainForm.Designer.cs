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
        _menuStrip = new System.Windows.Forms.MenuStrip();
        _saveFileDialog = new System.Windows.Forms.SaveFileDialog();
        _openFileDialog = new System.Windows.Forms.OpenFileDialog();
        _toolStrip = new System.Windows.Forms.ToolStrip();
        SuspendLayout();
        // 
        // _menuStrip
        // 
        resources.ApplyResources(_menuStrip, "_menuStrip");
        _menuStrip.Name = "_menuStrip";
        // 
        // _saveFileDialog
        // 
        _saveFileDialog.DefaultExt = "json";
        resources.ApplyResources(_saveFileDialog, "_saveFileDialog");
        _saveFileDialog.FilterIndex = 2;
        _saveFileDialog.RestoreDirectory = true;
        // 
        // _openFileDialog
        // 
        _openFileDialog.DefaultExt = "json";
        resources.ApplyResources(_openFileDialog, "_openFileDialog");
        _openFileDialog.FilterIndex = 2;
        // 
        // _toolStrip
        // 
        resources.ApplyResources(_toolStrip, "_toolStrip");
        _toolStrip.Name = "_toolStrip";
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
        FormClosing += OnFormClosing;
        Load += OnLoad;
        DragDrop += OnDragDrop;
        DragOver += OnDragOver;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private System.Windows.Forms.MenuStrip _menuStrip;
    private System.Windows.Forms.SaveFileDialog _saveFileDialog;
    private System.Windows.Forms.OpenFileDialog _openFileDialog;
    private System.Windows.Forms.ToolStrip _toolStrip;
}