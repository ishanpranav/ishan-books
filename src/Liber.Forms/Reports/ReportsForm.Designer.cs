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
            splitContainer2 = new System.Windows.Forms.SplitContainer();
            _listView = new System.Windows.Forms.ListViewEx();
            _imageList = new System.Windows.Forms.ImageList(components);
            _propertyGrid = new System.Windows.Forms.PropertyGrid();
            _webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            _backgroundWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_webView).BeginInit();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(splitContainer1, "splitContainer1");
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(_webView);
            // 
            // splitContainer2
            // 
            resources.ApplyResources(splitContainer2, "splitContainer2");
            splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(_listView);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(_propertyGrid);
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
            // _propertyGrid
            // 
            resources.ApplyResources(_propertyGrid, "_propertyGrid");
            _propertyGrid.Name = "_propertyGrid";
            _propertyGrid.PropertyValueChanged += OnPropertyGridPropertyValueChanged;
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
            // _backgroundWorker
            // 
            _backgroundWorker.WorkerSupportsCancellation = true;
            _backgroundWorker.DoWork += OnBackgroundWorkerDoWork;
            _backgroundWorker.RunWorkerCompleted += OnBackgroundWorkerRunWorkerCompleted;
            // 
            // ReportsForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(splitContainer1);
            Name = "ReportsForm";
            ShowIcon = false;
            WindowState = System.Windows.Forms.FormWindowState.Maximized;
            Load += OnLoad;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_webView).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListViewEx _listView;
        private Microsoft.Web.WebView2.WinForms.WebView2 _webView;
        private System.Windows.Forms.ImageList _imageList;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.PropertyGrid _propertyGrid;
        private System.ComponentModel.BackgroundWorker _backgroundWorker;
    }
}
