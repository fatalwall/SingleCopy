/* 
 *Copyright (C) 2019 Peter Varney - All Rights Reserved
 * You may use, distribute and modify this code under the
 * terms of the MIT license, 
 *
 * You should have received a copy of the MIT license with
 * this file. If not, visit : https://github.com/fatalwall/SingleCopy
 */
 using System;
using System.ComponentModel;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using vshed.IO;

namespace SingleCopy
{
    public partial class frmMaster : Form
    {
        public frmMaster()
        {
            InitializeComponent();
            Program.files.ElementChange += fileListchange;
        }

        private void fileListchange(object sender, ElementChangeEventArgs e)
        {
            try { bgWorker.ReportProgress(((ListEvents<FileInfo>)sender).Count()); }
            catch (Exception) {}//Prevent issue with threading
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) { Application.Exit(); }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) { (new frmAbout()).ShowDialog(); }

        private void toolStripScan_Click(object sender, EventArgs e)
        {
            toolStripStatus.Text = "Scanning Files";
            toolStripScan.Enabled = false;
            bgWorker.RunWorkerAsync();
        }

        private void DataGridView_UpdateColumns()
        {
            //Always hide
            grdFiles.Columns["DirectoryName"].Visible = false;
            grdFiles.Columns["Exists"].Visible = false;

            //Friendly Headers
            grdFiles.Columns["Length"].HeaderText = "Size (Bytes)";
            grdFiles.Columns["Length"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grdFiles.Columns["md5sum"].HeaderText = "MD5";
            grdFiles.Columns["md5sum"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grdFiles.Columns["IsReadOnly"].HeaderText = "Is Read Only";
            grdFiles.Columns["FullName"].HeaderText = "Full Name";
            grdFiles.Columns["CreationTime"].HeaderText = "Creation Time";
            grdFiles.Columns["CreationTime"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grdFiles.Columns["CreationTimeUtc"].HeaderText = "Creation Time UTC";
            grdFiles.Columns["CreationTimeUtc"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grdFiles.Columns["LastAccessTime"].HeaderText = "Last Access Time";
            grdFiles.Columns["LastAccessTime"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grdFiles.Columns["LastAccessTimeUtc"].HeaderText = "Last Access Time UTC";
            grdFiles.Columns["LastAccessTimeUtc"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grdFiles.Columns["LastWriteTime"].HeaderText = "Last Write Time";
            grdFiles.Columns["LastWriteTime"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grdFiles.Columns["LastWriteTimeUtc"].HeaderText = "Last Write Time UTC";
            grdFiles.Columns["LastWriteTimeUtc"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            //Set Visable Columns based on Options
            foreach (ToolStripMenuItem item in columnsToolStripMenuItem.DropDownItems)
            {
                foreach (DataGridViewColumn column in grdFiles.Columns)
                {
                    if (item.Text == column.HeaderText) column.Visible = item.Checked;
                }
            }
        }

        private void columns_Item_Click(object sender, EventArgs e)
        {
            ((ToolStripMenuItem)sender).Checked = !((ToolStripMenuItem)sender).Checked;
            DataGridView_UpdateColumns();
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Program.getFiles(@"C:\", new string[] { @"C:\Users\Default", @"C:\Users\All Users\Microsoft", @"C:\ProgramData\Microsoft", @"C:\Windows", @"C:\Program Files (x86)", @"C:\Program File" });
            bgWorker.ReportProgress(-1);
            Program.Table = Program.files.ToDataTable();
        }
        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage != -1) { toolStripStatus.Text = string.Format("{0:n0} Files scanned", e.ProgressPercentage); }
            else
            {
                toolStripStatusBar.Visible = true;
                toolStripStatus.Text += " - Generating MD5 Hashs";
            }
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            grdFiles.DataSource = Program.Table;
            DataGridView_UpdateColumns();
            grdFiles.Sort(grdFiles.Columns["md5sum"], ListSortDirection.Ascending);
            toolStripStatus.Text = string.Format("{0:n0} Files scanned", Program.files.Count());
            toolStripStatusBar.Visible = false;
            toolStripScan.Enabled = true;
            Program.files.Clear();
        }

        private void grdFiles_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            ViewFile(e.RowIndex);
        }

        private void toolStripToggleViewer_Click(object sender, EventArgs e)
        {
            splitContainer.Panel2Collapsed = !splitContainer.Panel2Collapsed;
            if(!splitContainer.Panel2Collapsed)
            {
                foreach(DataGridViewRow r in grdFiles.SelectedRows) { ViewFile(r.Index); }
            }
        }

        private void ViewFile(int RowIndex)
        {
            if (RowIndex >= 0 && !splitContainer.Panel2Collapsed)
            {
                switch (grdFiles.Rows[RowIndex].Cells["Extension"].Value.ToString().ToUpper())
                {
                    case ".JPG":
                    case ".JPEG":
                    case ".GIF":
                    case ".PNG":
                    case ".TIFF":
                    case ".TIF":
                    case ".ICO":
                    case ".SVG":
                        viewBox_Picture.Visible = true;
                        viewBox_Text.Text = "";
                        viewBox_Text.Visible = false;
                        viewBox_Picture.LoadAsync(grdFiles.Rows[RowIndex].Cells["FullName"].Value.ToString());
                        viewBox_Picture.SizeMode = PictureBoxSizeMode.StretchImage;
                        break;
                    //Documents
                    case ".PDF":
                    //Word
                    case ".DOC":
                    case ".DOT":
                    case ".DOCX":
                    case ".DOCM":
                    case ".DOTX":
                    case ".DOTM":
                    case ".DOCB":
                    //Excel
                    case ".XLS":
                    case ".XLT":
                    case ".XLM":
                    case ".XLSX":
                    case ".XLSM":
                    case ".XLTX":
                    case ".XLTM":
                    case ".XLSB":
                    //PowerPoint
                    case ".ppt":
                    case ".pot":
                    case ".pps":
                    case ".PPTX":
                    case ".PPTM":
                    case ".POTX":
                    case ".POTM":
                    case ".PPSX":
                    //Access
                    case ".ACCDB":
                    case ".ACCDE":
                    case ".ACCDT":
                    case ".ACCDR":
                    //Random Binary Files
                    case ".SYS":
                    case ".SUO":
                    case ".RESOURCES":
                    case ".TLB":
                    case ".ITHMB":
                    case ".IPA":
                    //Audio
                    case ".MP3":
                    case ".WAV":
                    case ".M4A":
                    //Binary
                    case ".EXE":
                    case ".DLL":
                    //Packages
                    case ".TAR":
                    case ".CAB":
                    case ".7Z":
                    case ".MSI":
                    case ".ZIP":
                    case ".GZ":
                        viewBox_Picture.Image = null;
                        viewBox_Picture.Visible = false;
                        viewBox_Text.Text = "";
                        viewBox_Text.Visible = false;
                        break;
                    default:
                        viewBox_Picture.Image = null;
                        viewBox_Picture.Visible = false;
                        viewBox_Text.Visible = true;
                        viewBox_Text.Text = File.ReadAllText(grdFiles.Rows[RowIndex].Cells["FullName"].Value.ToString());
                        break;
                }
            }

        }



        private void grdFiles_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                foreach (DataGridViewRow r in grdFiles.SelectedRows)
                {
                    if (!(deleteConfirmationToolStripMenuItem.Checked && MessageBox.Show(String.Format("Are you sure you want to delete '{0}'?", grdFiles.Rows[r.Index].Cells["FullName"].Value.ToString()), "Delete Confirmation", MessageBoxButtons.YesNo) == DialogResult.No))
                    {
                        File.Delete(grdFiles.Rows[r.Index].Cells["FullName"].Value.ToString());
                        grdFiles.Rows.RemoveAt(r.Index);
                    }
                }
            }

        }

        private void deleteConfirmationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((ToolStripMenuItem)sender).Checked = !((ToolStripMenuItem)sender).Checked;
        }
    }
}
