namespace Liber.Forms
{
    partial class ReportsForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportsForm));
            saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            printToolStripButton = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            cutToolStripButton = new System.Windows.Forms.ToolStripButton();
            copyToolStripButton = new System.Windows.Forms.ToolStripButton();
            pasteToolStripButton = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            helpToolStripButton = new System.Windows.Forms.ToolStripButton();
            toolStrip1 = new System.Windows.Forms.ToolStrip();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            _listView = new System.Windows.Forms.ListViewEx();
            _imageList = new System.Windows.Forms.ImageList(components);
            _webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_webView).BeginInit();
            SuspendLayout();
            // 
            // saveToolStripButton
            // 
            saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(saveToolStripButton, "saveToolStripButton");
            saveToolStripButton.Name = "saveToolStripButton";
            // 
            // printToolStripButton
            // 
            printToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(printToolStripButton, "printToolStripButton");
            printToolStripButton.Name = "printToolStripButton";
            // 
            // toolStripSeparator
            // 
            toolStripSeparator.Name = "toolStripSeparator";
            resources.ApplyResources(toolStripSeparator, "toolStripSeparator");
            // 
            // cutToolStripButton
            // 
            cutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(cutToolStripButton, "cutToolStripButton");
            cutToolStripButton.Name = "cutToolStripButton";
            // 
            // copyToolStripButton
            // 
            copyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(copyToolStripButton, "copyToolStripButton");
            copyToolStripButton.Name = "copyToolStripButton";
            // 
            // pasteToolStripButton
            // 
            pasteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(pasteToolStripButton, "pasteToolStripButton");
            pasteToolStripButton.Name = "pasteToolStripButton";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(toolStripSeparator1, "toolStripSeparator1");
            // 
            // helpToolStripButton
            // 
            helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(helpToolStripButton, "helpToolStripButton");
            helpToolStripButton.Name = "helpToolStripButton";
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { saveToolStripButton, printToolStripButton, toolStripSeparator, cutToolStripButton, copyToolStripButton, pasteToolStripButton, toolStripSeparator1, helpToolStripButton });
            resources.ApplyResources(toolStrip1, "toolStrip1");
            toolStrip1.Name = "toolStrip1";
            // 
            // splitContainer1
            // 
            resources.ApplyResources(splitContainer1, "splitContainer1");
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(_listView);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(_webView);
            // 
            // _listView
            // 
            _listView.AllowColumnReorder = true;
            resources.ApplyResources(_listView, "_listView");
            _listView.LargeImageList = _imageList;
            _listView.MultiSelect = false;
            _listView.Name = "_listView";
            _listView.SortColumn = 0;
            _listView.SortOrder = System.Windows.Forms.SortOrder.None;
            _listView.UseCompatibleStateImageBehavior = false;
            _listView.ItemActivate += OnListViewItemActivate;
            // 
            // _imageList
            // 
            _imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            resources.ApplyResources(_imageList, "_imageList");
            _imageList.TransparentColor = System.Drawing.Color.Transparent;
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
            // ReportsForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(splitContainer1);
            Controls.Add(toolStrip1);
            Name = "ReportsForm";
            ShowIcon = false;
            Load += OnLoad;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_webView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripButton printToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton cutToolStripButton;
        private System.Windows.Forms.ToolStripButton copyToolStripButton;
        private System.Windows.Forms.ToolStripButton pasteToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton helpToolStripButton;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListViewEx _listView;
        private Microsoft.Web.WebView2.WinForms.WebView2 _webView;
        private System.Windows.Forms.ImageList _imageList;
    }
}