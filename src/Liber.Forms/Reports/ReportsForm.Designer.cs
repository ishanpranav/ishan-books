using Liber.Forms.RecentPaths;

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
            _listView = new ReportListView();
            _propertyGrid = new System.Windows.Forms.PropertyGrid();
            _webView = new ReportWebView();
            _timer = new System.Windows.Forms.Timer(components);
            _recentPathManager = new RecentPathManager(components);
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
            _listView.FullRowSelect = true;
            _listView.GridLines = true;
            _listView.MultiSelect = false;
            _listView.Name = "_listView";
            _listView.SortColumn = 0;
            _listView.UseCompatibleStateImageBehavior = false;
            _listView.ItemActivate += OnListViewItemActivate;
            // 
            // _propertyGrid
            // 
            _propertyGrid.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(_propertyGrid, "_propertyGrid");
            _propertyGrid.Name = "_propertyGrid";
            _propertyGrid.PropertyValueChanged += OnPropertyGridPropertyValueChanged;
            // 
            // _webView
            // 
            _webView.AllowExternalDrop = false;
            _webView.CreationProperties = null;
            _webView.DefaultBackgroundColor = System.Drawing.Color.White;
            resources.ApplyResources(_webView, "_webView");
            _webView.Name = "_webView";
            _webView.ZoomFactor = 1D;
            // 
            // _timer
            // 
            _timer.Interval = 30000;
            _timer.Tick += OnTimerTick;
            // 
            // _recentPathManager
            // 
            _recentPathManager.MaxPaths = 1;
            // 
            // ReportsForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(splitContainer1);
            Name = "ReportsForm";
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
        private ReportListView _listView;
        private ReportWebView _webView;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.PropertyGrid _propertyGrid;
        private System.Windows.Forms.Timer _timer;
        private RecentPathManager _recentPathManager;
    }
}
