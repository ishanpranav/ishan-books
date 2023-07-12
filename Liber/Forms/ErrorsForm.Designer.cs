namespace Liber.Forms
{
    partial class ErrorsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorsForm));
            _listView = new System.Windows.Forms.ListViewEx();
            createdColumn = new System.Windows.Forms.ColumnHeader();
            descriptionColumn = new System.Windows.Forms.ColumnHeader();
            rawStringColumn = new System.Windows.Forms.ColumnHeader();
            panel1 = new System.Windows.Forms.Panel();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            _toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            refreshButton = new System.Windows.Forms.Button();
            ignoreButton = new System.Windows.Forms.Button();
            ignoreAllButton = new System.Windows.Forms.Button();
            cancelButton = new System.Windows.Forms.Button();
            panel1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // _listView
            // 
            _listView.CheckBoxes = true;
            _listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { createdColumn, descriptionColumn, rawStringColumn });
            resources.ApplyResources(_listView, "_listView");
            _listView.Name = "_listView";
            _listView.SortColumn = 0;
            _listView.SortOrder = System.Windows.Forms.SortOrder.None;
            _listView.UseCompatibleStateImageBehavior = false;
            _listView.View = System.Windows.Forms.View.Details;
            _listView.ItemChecked += OnListViewItemChecked;
            // 
            // createdColumn
            // 
            resources.ApplyResources(createdColumn, "createdColumn");
            // 
            // descriptionColumn
            // 
            resources.ApplyResources(descriptionColumn, "descriptionColumn");
            // 
            // rawStringColumn
            // 
            resources.ApplyResources(rawStringColumn, "rawStringColumn");
            // 
            // panel1
            // 
            panel1.Controls.Add(statusStrip1);
            panel1.Controls.Add(refreshButton);
            panel1.Controls.Add(ignoreButton);
            panel1.Controls.Add(ignoreAllButton);
            panel1.Controls.Add(cancelButton);
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { _toolStripStatusLabel });
            resources.ApplyResources(statusStrip1, "statusStrip1");
            statusStrip1.Name = "statusStrip1";
            // 
            // _toolStripStatusLabel
            // 
            _toolStripStatusLabel.Name = "_toolStripStatusLabel";
            resources.ApplyResources(_toolStripStatusLabel, "_toolStripStatusLabel");
            // 
            // refreshButton
            // 
            resources.ApplyResources(refreshButton, "refreshButton");
            refreshButton.Name = "refreshButton";
            refreshButton.UseVisualStyleBackColor = true;
            refreshButton.Click += OnRefreshButtonClick;
            // 
            // ignoreButton
            // 
            resources.ApplyResources(ignoreButton, "ignoreButton");
            ignoreButton.Name = "ignoreButton";
            ignoreButton.UseVisualStyleBackColor = true;
            ignoreButton.Click += OnIgnoreButtonClick;
            // 
            // ignoreAllButton
            // 
            resources.ApplyResources(ignoreAllButton, "ignoreAllButton");
            ignoreAllButton.Name = "ignoreAllButton";
            ignoreAllButton.UseVisualStyleBackColor = true;
            ignoreAllButton.Click += OnIgnoreAllButtonClick;
            // 
            // cancelButton
            // 
            resources.ApplyResources(cancelButton, "cancelButton");
            cancelButton.Name = "cancelButton";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += OnCloseClick;
            // 
            // ErrorsForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = cancelButton;
            Controls.Add(_listView);
            Controls.Add(panel1);
            Name = "ErrorsForm";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ListViewEx _listView;
        private System.Windows.Forms.ColumnHeader createdColumn;
        private System.Windows.Forms.ColumnHeader descriptionColumn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button ignoreButton;
        private System.Windows.Forms.Button ignoreAllButton;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.ColumnHeader rawStringColumn;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel _toolStripStatusLabel;
    }
}