namespace Liber.Forms.Help;

partial class UriForm
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UriForm));
        _webView = new Microsoft.Web.WebView2.WinForms.WebView2();
        ((System.ComponentModel.ISupportInitialize)_webView).BeginInit();
        SuspendLayout();
        // 
        // _webView
        // 
        _webView.AllowExternalDrop = true;
        _webView.CreationProperties = null;
        _webView.DefaultBackgroundColor = System.Drawing.Color.White;
        resources.ApplyResources(_webView, "_webView");
        _webView.Name = "_webView";
        _webView.ZoomFactor = 1D;
        // 
        // UriForm
        // 
        resources.ApplyResources(this, "$this");
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        Controls.Add(_webView);
        Name = "UriForm";
        WindowState = System.Windows.Forms.FormWindowState.Maximized;
        Load += OnLoad;
        ((System.ComponentModel.ISupportInitialize)_webView).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Microsoft.Web.WebView2.WinForms.WebView2 _webView;
}
