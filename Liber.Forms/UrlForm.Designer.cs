namespace Liber.Forms
{
    partial class UrlForm
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
            _webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)_webView).BeginInit();
            SuspendLayout();
            // 
            // _webView
            // 
            _webView.AllowExternalDrop = true;
            _webView.CreationProperties = null;
            _webView.DefaultBackgroundColor = System.Drawing.Color.White;
            _webView.Dock = System.Windows.Forms.DockStyle.Fill;
            _webView.Location = new System.Drawing.Point(0, 0);
            _webView.Name = "_webView";
            _webView.Size = new System.Drawing.Size(546, 325);
            _webView.TabIndex = 0;
            _webView.ZoomFactor = 1D;
            // 
            // BrowserForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(546, 325);
            Controls.Add(_webView);
            Name = "BrowserForm";
            Load += OnLoad;
            ((System.ComponentModel.ISupportInitialize)_webView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 _webView;
    }
}