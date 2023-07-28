namespace Liber.Forms.Reports
{
    partial class ReportsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportsForm));
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            _listView = new System.Windows.Forms.ListViewEx();
            _imageList = new System.Windows.Forms.ImageList(components);
            _webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            panel1 = new System.Windows.Forms.Panel();
            label2 = new System.Windows.Forms.Label();
            postedDateTimePicker = new System.Windows.Forms.DateTimePicker();
            label1 = new System.Windows.Forms.Label();
            startedDateTimePicker = new System.Windows.Forms.DateTimePicker();
            saveAsToolStripButton = new System.Windows.Forms.ToolStripButton();
            printToolStripButton = new System.Windows.Forms.ToolStripButton();
            _toolStrip = new System.Windows.Forms.ToolStrip();
            printPreviewToolStripButton = new System.Windows.Forms.ToolStripButton();
            _saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            _contextMenu = new System.Windows.Forms.ContextMenuStrip(components);
            saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            _helpProvider = new System.Windows.Forms.HelpProvider();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_webView).BeginInit();
            panel1.SuspendLayout();
            _toolStrip.SuspendLayout();
            _contextMenu.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(splitContainer1, "splitContainer1");
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(_listView);
            _helpProvider.SetShowHelp(splitContainer1.Panel1, (bool)resources.GetObject("splitContainer1.Panel1.ShowHelp"));
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(_webView);
            splitContainer1.Panel2.Controls.Add(panel1);
            _helpProvider.SetShowHelp(splitContainer1.Panel2, (bool)resources.GetObject("splitContainer1.Panel2.ShowHelp"));
            _helpProvider.SetShowHelp(splitContainer1, (bool)resources.GetObject("splitContainer1.ShowHelp"));
            // 
            // _listView
            // 
            _listView.AllowColumnReorder = true;
            resources.ApplyResources(_listView, "_listView");
            _listView.LargeImageList = _imageList;
            _listView.MultiSelect = false;
            _listView.Name = "_listView";
            _helpProvider.SetShowHelp(_listView, (bool)resources.GetObject("_listView.ShowHelp"));
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
            _helpProvider.SetShowHelp(_webView, (bool)resources.GetObject("_webView.ShowHelp"));
            _webView.ZoomFactor = 1D;
            // 
            // panel1
            // 
            panel1.Controls.Add(label2);
            panel1.Controls.Add(postedDateTimePicker);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(startedDateTimePicker);
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            _helpProvider.SetShowHelp(panel1, (bool)resources.GetObject("panel1.ShowHelp"));
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            _helpProvider.SetShowHelp(label2, (bool)resources.GetObject("label2.ShowHelp"));
            // 
            // postedDateTimePicker
            // 
            resources.ApplyResources(postedDateTimePicker, "postedDateTimePicker");
            postedDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            _helpProvider.SetHelpString(postedDateTimePicker, resources.GetString("postedDateTimePicker.HelpString"));
            postedDateTimePicker.Name = "postedDateTimePicker";
            _helpProvider.SetShowHelp(postedDateTimePicker, (bool)resources.GetObject("postedDateTimePicker.ShowHelp"));
            postedDateTimePicker.ValueChanged += OnPostedDateTimePickerValueChanged;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            _helpProvider.SetShowHelp(label1, (bool)resources.GetObject("label1.ShowHelp"));
            // 
            // startedDateTimePicker
            // 
            resources.ApplyResources(startedDateTimePicker, "startedDateTimePicker");
            startedDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            _helpProvider.SetHelpString(startedDateTimePicker, resources.GetString("startedDateTimePicker.HelpString"));
            startedDateTimePicker.Name = "startedDateTimePicker";
            _helpProvider.SetShowHelp(startedDateTimePicker, (bool)resources.GetObject("startedDateTimePicker.ShowHelp"));
            startedDateTimePicker.ValueChanged += OnStartedDateTimePickerValueChanged;
            // 
            // saveAsToolStripButton
            // 
            saveAsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(saveAsToolStripButton, "saveAsToolStripButton");
            saveAsToolStripButton.Name = "saveAsToolStripButton";
            saveAsToolStripButton.Click += OnSaveAsToolStripButtonClick;
            // 
            // printToolStripButton
            // 
            printToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(printToolStripButton, "printToolStripButton");
            printToolStripButton.Name = "printToolStripButton";
            printToolStripButton.Click += OnPrintPreviewToolStripButtonClick;
            // 
            // _toolStrip
            // 
            _toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            _toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { saveAsToolStripButton, printPreviewToolStripButton, printToolStripButton });
            resources.ApplyResources(_toolStrip, "_toolStrip");
            _toolStrip.Name = "_toolStrip";
            _helpProvider.SetShowHelp(_toolStrip, (bool)resources.GetObject("_toolStrip.ShowHelp"));
            // 
            // printPreviewToolStripButton
            // 
            printPreviewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(printPreviewToolStripButton, "printPreviewToolStripButton");
            printPreviewToolStripButton.Name = "printPreviewToolStripButton";
            printPreviewToolStripButton.Click += OnPrintPreviewToolStripButtonClick;
            // 
            // _saveFileDialog
            // 
            resources.ApplyResources(_saveFileDialog, "_saveFileDialog");
            // 
            // _contextMenu
            // 
            _contextMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            _contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { saveAsToolStripMenuItem, toolStripSeparator1, printToolStripMenuItem, printPreviewToolStripMenuItem });
            _contextMenu.Name = "contextMenuStrip1";
            _helpProvider.SetShowHelp(_contextMenu, (bool)resources.GetObject("_contextMenu.ShowHelp"));
            resources.ApplyResources(_contextMenu, "_contextMenu");
            // 
            // saveAsToolStripMenuItem
            // 
            resources.ApplyResources(saveAsToolStripMenuItem, "saveAsToolStripMenuItem");
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.Click += OnSaveAsToolStripButtonClick;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(toolStripSeparator1, "toolStripSeparator1");
            // 
            // printToolStripMenuItem
            // 
            resources.ApplyResources(printToolStripMenuItem, "printToolStripMenuItem");
            printToolStripMenuItem.Name = "printToolStripMenuItem";
            printToolStripMenuItem.Click += OnPrintPreviewToolStripButtonClick;
            // 
            // printPreviewToolStripMenuItem
            // 
            resources.ApplyResources(printPreviewToolStripMenuItem, "printPreviewToolStripMenuItem");
            printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
            // 
            // ReportsForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(splitContainer1);
            Controls.Add(_toolStrip);
            Name = "ReportsForm";
            _helpProvider.SetShowHelp(this, (bool)resources.GetObject("$this.ShowHelp"));
            ShowIcon = false;
            WindowState = System.Windows.Forms.FormWindowState.Maximized;
            Load += OnLoad;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_webView).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            _toolStrip.ResumeLayout(false);
            _toolStrip.PerformLayout();
            _contextMenu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ToolStripButton saveAsToolStripButton;
        private System.Windows.Forms.ToolStripButton printToolStripButton;
        private System.Windows.Forms.ToolStrip _toolStrip;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListViewEx _listView;
        private Microsoft.Web.WebView2.WinForms.WebView2 _webView;
        private System.Windows.Forms.ImageList _imageList;
        private System.Windows.Forms.SaveFileDialog _saveFileDialog;
        private System.Windows.Forms.ContextMenuStrip _contextMenu;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton printPreviewToolStripButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker postedDateTimePicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker startedDateTimePicker;
        private System.Windows.Forms.HelpProvider _helpProvider;
    }
}
