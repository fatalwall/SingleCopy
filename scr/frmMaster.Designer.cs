namespace SingleCopy
{
    partial class frmMaster
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMaster));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusBar = new System.Windows.Forms.ToolStripProgressBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.columnsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.columns_Name = new System.Windows.Forms.ToolStripMenuItem();
            this.columns_Extention = new System.Windows.Forms.ToolStripMenuItem();
            this.columns_Directory = new System.Windows.Forms.ToolStripMenuItem();
            this.columns_FullName = new System.Windows.Forms.ToolStripMenuItem();
            this.columns_Size = new System.Windows.Forms.ToolStripMenuItem();
            this.columns_IsReadOnly = new System.Windows.Forms.ToolStripMenuItem();
            this.columns_CreationTime = new System.Windows.Forms.ToolStripMenuItem();
            this.columns_CreationTimeUTC = new System.Windows.Forms.ToolStripMenuItem();
            this.columns_LastAccessTime = new System.Windows.Forms.ToolStripMenuItem();
            this.columns_LastActionTimeUTC = new System.Windows.Forms.ToolStripMenuItem();
            this.columns_LastWriteTime = new System.Windows.Forms.ToolStripMenuItem();
            this.columns_LastWriteTimeUTC = new System.Windows.Forms.ToolStripMenuItem();
            this.columns_Attributes = new System.Windows.Forms.ToolStripMenuItem();
            this.columns_MD5 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteConfirmationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripScan = new System.Windows.Forms.ToolStripButton();
            this.toolStripSpreadsheet = new System.Windows.Forms.ToolStripButton();
            this.toolStripToggleViewer = new System.Windows.Forms.ToolStripButton();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.grdFiles = new OutlookStyleControls.OutlookGrid();
            this.viewBox_Text = new System.Windows.Forms.RichTextBox();
            this.viewBox_Picture = new System.Windows.Forms.PictureBox();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdFiles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewBox_Picture)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatus,
            this.toolStripStatusBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 13, 0);
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatus
            // 
            this.toolStripStatus.Name = "toolStripStatus";
            this.toolStripStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusBar
            // 
            this.toolStripStatusBar.MarqueeAnimationSpeed = 50;
            this.toolStripStatusBar.Name = "toolStripStatusBar";
            this.toolStripStatusBar.Size = new System.Drawing.Size(100, 18);
            this.toolStripStatusBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.toolStripStatusBar.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(108, 26);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.columnsToolStripMenuItem,
            this.deleteConfirmationToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // columnsToolStripMenuItem
            // 
            this.columnsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.columns_Name,
            this.columns_Extention,
            this.columns_Directory,
            this.columns_FullName,
            this.columns_Size,
            this.columns_IsReadOnly,
            this.columns_CreationTime,
            this.columns_CreationTimeUTC,
            this.columns_LastAccessTime,
            this.columns_LastActionTimeUTC,
            this.columns_LastWriteTime,
            this.columns_LastWriteTimeUTC,
            this.columns_Attributes,
            this.columns_MD5});
            this.columnsToolStripMenuItem.Name = "columnsToolStripMenuItem";
            this.columnsToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.columnsToolStripMenuItem.Text = "Columns";
            // 
            // columns_Name
            // 
            this.columns_Name.Checked = true;
            this.columns_Name.CheckState = System.Windows.Forms.CheckState.Checked;
            this.columns_Name.Name = "columns_Name";
            this.columns_Name.Size = new System.Drawing.Size(225, 26);
            this.columns_Name.Text = "Name";
            this.columns_Name.Click += new System.EventHandler(this.columns_Item_Click);
            // 
            // columns_Extention
            // 
            this.columns_Extention.Checked = true;
            this.columns_Extention.CheckState = System.Windows.Forms.CheckState.Checked;
            this.columns_Extention.Name = "columns_Extention";
            this.columns_Extention.Size = new System.Drawing.Size(225, 26);
            this.columns_Extention.Text = "Extention";
            this.columns_Extention.Click += new System.EventHandler(this.columns_Item_Click);
            // 
            // columns_Directory
            // 
            this.columns_Directory.Checked = true;
            this.columns_Directory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.columns_Directory.Name = "columns_Directory";
            this.columns_Directory.Size = new System.Drawing.Size(225, 26);
            this.columns_Directory.Text = "Directory";
            this.columns_Directory.Click += new System.EventHandler(this.columns_Item_Click);
            // 
            // columns_FullName
            // 
            this.columns_FullName.Name = "columns_FullName";
            this.columns_FullName.Size = new System.Drawing.Size(225, 26);
            this.columns_FullName.Text = "Full Name";
            this.columns_FullName.Click += new System.EventHandler(this.columns_Item_Click);
            // 
            // columns_Size
            // 
            this.columns_Size.Checked = true;
            this.columns_Size.CheckState = System.Windows.Forms.CheckState.Checked;
            this.columns_Size.Name = "columns_Size";
            this.columns_Size.Size = new System.Drawing.Size(225, 26);
            this.columns_Size.Text = "Size (Bytes)";
            this.columns_Size.Click += new System.EventHandler(this.columns_Item_Click);
            // 
            // columns_IsReadOnly
            // 
            this.columns_IsReadOnly.Name = "columns_IsReadOnly";
            this.columns_IsReadOnly.Size = new System.Drawing.Size(225, 26);
            this.columns_IsReadOnly.Text = "Is Read Only";
            this.columns_IsReadOnly.Click += new System.EventHandler(this.columns_Item_Click);
            // 
            // columns_CreationTime
            // 
            this.columns_CreationTime.Name = "columns_CreationTime";
            this.columns_CreationTime.Size = new System.Drawing.Size(225, 26);
            this.columns_CreationTime.Text = "Creation Time";
            this.columns_CreationTime.Click += new System.EventHandler(this.columns_Item_Click);
            // 
            // columns_CreationTimeUTC
            // 
            this.columns_CreationTimeUTC.Name = "columns_CreationTimeUTC";
            this.columns_CreationTimeUTC.Size = new System.Drawing.Size(225, 26);
            this.columns_CreationTimeUTC.Text = "Creation Time UTC";
            this.columns_CreationTimeUTC.Click += new System.EventHandler(this.columns_Item_Click);
            // 
            // columns_LastAccessTime
            // 
            this.columns_LastAccessTime.Name = "columns_LastAccessTime";
            this.columns_LastAccessTime.Size = new System.Drawing.Size(225, 26);
            this.columns_LastAccessTime.Text = "Last Access Time";
            this.columns_LastAccessTime.Click += new System.EventHandler(this.columns_Item_Click);
            // 
            // columns_LastActionTimeUTC
            // 
            this.columns_LastActionTimeUTC.Name = "columns_LastActionTimeUTC";
            this.columns_LastActionTimeUTC.Size = new System.Drawing.Size(225, 26);
            this.columns_LastActionTimeUTC.Text = "Last Access Time UTC";
            this.columns_LastActionTimeUTC.Click += new System.EventHandler(this.columns_Item_Click);
            // 
            // columns_LastWriteTime
            // 
            this.columns_LastWriteTime.Name = "columns_LastWriteTime";
            this.columns_LastWriteTime.Size = new System.Drawing.Size(225, 26);
            this.columns_LastWriteTime.Text = "Last Write Time";
            this.columns_LastWriteTime.Click += new System.EventHandler(this.columns_Item_Click);
            // 
            // columns_LastWriteTimeUTC
            // 
            this.columns_LastWriteTimeUTC.Name = "columns_LastWriteTimeUTC";
            this.columns_LastWriteTimeUTC.Size = new System.Drawing.Size(225, 26);
            this.columns_LastWriteTimeUTC.Text = "Last Write Time UTC";
            this.columns_LastWriteTimeUTC.Click += new System.EventHandler(this.columns_Item_Click);
            // 
            // columns_Attributes
            // 
            this.columns_Attributes.Name = "columns_Attributes";
            this.columns_Attributes.Size = new System.Drawing.Size(225, 26);
            this.columns_Attributes.Text = "Attributes";
            this.columns_Attributes.Click += new System.EventHandler(this.columns_Item_Click);
            // 
            // columns_MD5
            // 
            this.columns_MD5.Name = "columns_MD5";
            this.columns_MD5.Size = new System.Drawing.Size(225, 26);
            this.columns_MD5.Text = "MD5";
            this.columns_MD5.Click += new System.EventHandler(this.columns_Item_Click);
            // 
            // deleteConfirmationToolStripMenuItem
            // 
            this.deleteConfirmationToolStripMenuItem.Checked = true;
            this.deleteConfirmationToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.deleteConfirmationToolStripMenuItem.Name = "deleteConfirmationToolStripMenuItem";
            this.deleteConfirmationToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.deleteConfirmationToolStripMenuItem.Text = "Delete Confirmation";
            this.deleteConfirmationToolStripMenuItem.Click += new System.EventHandler(this.deleteConfirmationToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(62, 24);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripScan,
            this.toolStripSpreadsheet,
            this.toolStripToggleViewer});
            this.toolStrip1.Location = new System.Drawing.Point(0, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 27);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripScan
            // 
            this.toolStripScan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripScan.Image = global::SingleCopy.Properties.Resources.scan;
            this.toolStripScan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripScan.Name = "toolStripScan";
            this.toolStripScan.Size = new System.Drawing.Size(24, 24);
            this.toolStripScan.Text = "Scan";
            this.toolStripScan.Click += new System.EventHandler(this.toolStripScan_Click);
            // 
            // toolStripSpreadsheet
            // 
            this.toolStripSpreadsheet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSpreadsheet.Enabled = false;
            this.toolStripSpreadsheet.Image = global::SingleCopy.Properties.Resources.spreadsheet;
            this.toolStripSpreadsheet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSpreadsheet.Name = "toolStripSpreadsheet";
            this.toolStripSpreadsheet.Size = new System.Drawing.Size(24, 24);
            this.toolStripSpreadsheet.Text = "Export Excel";
            this.toolStripSpreadsheet.Click += new System.EventHandler(this.toolStripSpreadsheet_Click);
            // 
            // toolStripToggleViewer
            // 
            this.toolStripToggleViewer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripToggleViewer.Image = global::SingleCopy.Properties.Resources.preview_expand;
            this.toolStripToggleViewer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripToggleViewer.Name = "toolStripToggleViewer";
            this.toolStripToggleViewer.Size = new System.Drawing.Size(24, 24);
            this.toolStripToggleViewer.Text = "Toggle File Viewer";
            this.toolStripToggleViewer.Click += new System.EventHandler(this.toolStripToggleViewer_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 55);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.grdFiles);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.viewBox_Text);
            this.splitContainer.Panel2.Controls.Add(this.viewBox_Picture);
            this.splitContainer.Panel2Collapsed = true;
            this.splitContainer.Size = new System.Drawing.Size(800, 373);
            this.splitContainer.SplitterDistance = 496;
            this.splitContainer.TabIndex = 4;
            // 
            // grdFiles
            // 
            this.grdFiles.AllowUserToAddRows = false;
            this.grdFiles.AllowUserToDeleteRows = false;
            this.grdFiles.AllowUserToOrderColumns = true;
            this.grdFiles.CollapseIcon = null;
            this.grdFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdFiles.ExpandIcon = null;
            this.grdFiles.Location = new System.Drawing.Point(0, 0);
            this.grdFiles.MultiSelect = false;
            this.grdFiles.Name = "grdFiles";
            this.grdFiles.ReadOnly = true;
            this.grdFiles.RowHeadersVisible = false;
            this.grdFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdFiles.Size = new System.Drawing.Size(800, 373);
            this.grdFiles.TabIndex = 0;
            this.grdFiles.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdFiles_RowEnter);
            this.grdFiles.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grdFiles_KeyDown);
            // 
            // viewBox_Text
            // 
            this.viewBox_Text.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewBox_Text.Location = new System.Drawing.Point(0, 0);
            this.viewBox_Text.Name = "viewBox_Text";
            this.viewBox_Text.ReadOnly = true;
            this.viewBox_Text.Size = new System.Drawing.Size(96, 100);
            this.viewBox_Text.TabIndex = 1;
            this.viewBox_Text.Text = "";
            // 
            // viewBox_Picture
            // 
            this.viewBox_Picture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewBox_Picture.Location = new System.Drawing.Point(0, 0);
            this.viewBox_Picture.Name = "viewBox_Picture";
            this.viewBox_Picture.Size = new System.Drawing.Size(96, 100);
            this.viewBox_Picture.TabIndex = 0;
            this.viewBox_Picture.TabStop = false;
            // 
            // bgWorker
            // 
            this.bgWorker.WorkerReportsProgress = true;
            this.bgWorker.WorkerSupportsCancellation = true;
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorker_ProgressChanged);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // frmMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmMaster";
            this.Text = "Single Copy";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdFiles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewBox_Picture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripScan;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatus;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem columnsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem columns_Name;
        private System.Windows.Forms.ToolStripMenuItem columns_Extention;
        private System.Windows.Forms.ToolStripMenuItem columns_Directory;
        private System.Windows.Forms.ToolStripMenuItem columns_FullName;
        private System.Windows.Forms.ToolStripMenuItem columns_Size;
        private System.Windows.Forms.ToolStripMenuItem columns_IsReadOnly;
        private System.Windows.Forms.ToolStripMenuItem columns_CreationTime;
        private System.Windows.Forms.ToolStripMenuItem columns_LastAccessTime;
        private System.Windows.Forms.ToolStripMenuItem columns_LastWriteTime;
        private System.Windows.Forms.ToolStripMenuItem columns_CreationTimeUTC;
        private System.Windows.Forms.ToolStripMenuItem columns_LastActionTimeUTC;
        private System.Windows.Forms.ToolStripMenuItem columns_LastWriteTimeUTC;
        private System.Windows.Forms.ToolStripMenuItem columns_Attributes;
        private System.Windows.Forms.ToolStripMenuItem columns_MD5;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.PictureBox viewBox_Picture;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.RichTextBox viewBox_Text;
        private System.Windows.Forms.ToolStripProgressBar toolStripStatusBar;
        private System.Windows.Forms.ToolStripButton toolStripToggleViewer;
        private System.Windows.Forms.ToolStripMenuItem deleteConfirmationToolStripMenuItem;
        private OutlookStyleControls.OutlookGrid grdFiles;
        private System.Windows.Forms.ToolStripButton toolStripSpreadsheet;
    }
}

