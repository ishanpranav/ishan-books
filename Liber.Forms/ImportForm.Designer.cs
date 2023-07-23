namespace Liber.Forms.Accounts
{
    partial class ImportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportForm));
            cancelButton = new System.Windows.Forms.Button();
            acceptButton = new System.Windows.Forms.Button();
            _listView = new System.Windows.Forms.ListViewEx();
            _helpProvider = new System.Windows.Forms.HelpProvider();
            SuspendLayout();
            // 
            // cancelButton
            // 
            resources.ApplyResources(cancelButton, "cancelButton");
            _helpProvider.SetHelpString(cancelButton, resources.GetString("cancelButton.HelpString"));
            cancelButton.Name = "cancelButton";
            _helpProvider.SetShowHelp(cancelButton, (bool)resources.GetObject("cancelButton.ShowHelp"));
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += OnCancelButtonClick;
            // 
            // acceptButton
            // 
            resources.ApplyResources(acceptButton, "acceptButton");
            _helpProvider.SetHelpString(acceptButton, resources.GetString("acceptButton.HelpString"));
            acceptButton.Name = "acceptButton";
            _helpProvider.SetShowHelp(acceptButton, (bool)resources.GetObject("acceptButton.ShowHelp"));
            acceptButton.UseVisualStyleBackColor = true;
            acceptButton.Click += OnAcceptButtonClick;
            // 
            // _listView
            // 
            _listView.AllowColumnReorder = true;
            resources.ApplyResources(_listView, "_listView");
            _listView.Name = "_listView";
            _listView.SortColumn = 0;
            _listView.SortOrder = System.Windows.Forms.SortOrder.None;
            _listView.UseCompatibleStateImageBehavior = false;
            _listView.View = System.Windows.Forms.View.List;
            // 
            // ImportForm
            // 
            AcceptButton = acceptButton;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            CancelButton = cancelButton;
            Controls.Add(_listView);
            Controls.Add(acceptButton);
            Controls.Add(cancelButton);
            Name = "ImportForm";
            ShowIcon = false;
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button acceptButton;
        protected System.Windows.Forms.ListViewEx _listView;
        private System.Windows.Forms.HelpProvider _helpProvider;
    }
}