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
using OutlookStyleControls;
using System.Data;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

namespace SingleCopy
{
    public partial class frmMaster : Form
    {
        public frmMaster()
        {
            InitializeComponent();
            Program.files.ItemsChanged += fileListchange;
        }

        private void fileListchange(object sender, FileInfoCollectionEventArgs e)
        {
            try { bgWorker.ReportProgress(((FileInfoCollection)sender).Count()); }
            catch (Exception) {}//Prevent issue with threading
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) { Application.Exit(); }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) { (new frmAbout()).ShowDialog(); }

        private DateTime startTime;
        private void toolStripScan_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dir = new FolderBrowserDialog() { ShowNewFolderButton = false, RootFolder = Environment.SpecialFolder.MyComputer };

            if (dir.ShowDialog() == DialogResult.OK)
            {
                StartScan(dir.SelectedPath);
            }
        }
        public void StartScan(string Path)
        {
            if (string.IsNullOrWhiteSpace(Path)) throw new ArgumentNullException(Path);
            if (!System.IO.Directory.Exists(Path)) throw new ArgumentException("Value is not valid or your account does not have permissions to access the folder", "Path", new DirectoryNotFoundException(string.Format("'{0}' could not be found", Path)));
            
            startTime = DateTime.Now;
            toolStripStatus.Text = "Scanning Files";
            toolStripSpreadsheet.Enabled = false;
            toolStripScan.Enabled = false;
            bgWorker.RunWorkerAsync(Path);
        }

        private void DataGridView_UpdateColumns()
        {
            if (grdFiles.Columns.Count > 0)
            {
                //Always hide
                grdFiles.Columns["DirectoryName"].Visible = false;
                grdFiles.Columns["Exists"].Visible = false;

                //Friendly Headers
                grdFiles.Columns["Length"].HeaderText = "Size (Bytes)";
                grdFiles.Columns["Length"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                //grdFiles.Columns.Add("md5sum","MD5");
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
                grdFiles.GroupTemplate.Column = grdFiles.Columns["md5sum"];
                //grdFiles.Sort(new FileInfoComparer(grdFiles.Columns["md5sum"].Index, ListSortDirection.Ascending)); //used if bidning FileInfo directly
                grdFiles.Sort(new OutlookStyleControls.DataRowComparer(grdFiles.Columns["md5sum"].Index, ListSortDirection.Ascending));//use when binding data set
            }
        }

        private void columns_Item_Click(object sender, EventArgs e)
        {
            ((ToolStripMenuItem)sender).Checked = !((ToolStripMenuItem)sender).Checked;
            DataGridView_UpdateColumns();
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Program.DS.Tables.Clear();
            Program.getFiles((string)e.Argument, (from Config.ExcludeElement ee in Config.Folders.getCurrentInstance().Excludes select ee.Folder).ToArray<string>());
            bgWorker.ReportProgress(-1);
            Program.files.WaitMd5();
            
            Program.DS.Tables.Add(Program.files.ToDataTable());
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
            grdFiles.BindData(Program.DS, "Files");
            //grdFiles.BindData(Program.files,null); //This method taxs the cpu when the grid is updated. Best to convert to a dataset first
            DataGridView_UpdateColumns();
            TimeSpan elapse = DateTime.Now.Subtract(startTime);
            toolStripStatus.Text = string.Format("{0:n0} Files scanned in {1} minutes", Program.files.Count(),elapse.TotalMinutes);
            toolStripStatusBar.Visible = false;
            toolStripScan.Enabled = true;
            toolStripSpreadsheet.Enabled = true;
            Program.files.Clear();
        }

        private void grdFiles_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            ViewFile(e.RowIndex);
        }

        private void toolStripToggleViewer_Click(object sender, EventArgs e)
        {
            //Toggle the preview panel open or closed
            splitContainer.Panel2Collapsed = !splitContainer.Panel2Collapsed;
            //change button display and tool tip
            if (splitContainer.Panel2Collapsed) { toolStripToggleViewer.Image = Properties.Resources.preview_expand; toolStripToggleViewer.Text = "Show Preview Panel"; }
            else { toolStripToggleViewer.Image = Properties.Resources.preview_collapse; toolStripToggleViewer.Text = "Show Preview Panel"; }
            //If panel is showing set whats displayed
            if (!splitContainer.Panel2Collapsed) { foreach(DataGridViewRow r in grdFiles.SelectedRows) { ViewFile(r.Index); } }
        }

        private void ViewFile(int RowIndex)
        {
            if (RowIndex >= 0 && !splitContainer.Panel2Collapsed && ((OutlookGridRow)grdFiles.Rows[RowIndex]).IsGroupRow == false)
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
                        try { viewBox_Text.Text = File.ReadAllText(grdFiles.Rows[RowIndex].Cells["FullName"].Value.ToString()); } catch { }
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
                    if (((OutlookGridRow)grdFiles.Rows[r.Index]).IsGroupRow == false)
                    {
                        if (!(deleteConfirmationToolStripMenuItem.Checked && MessageBox.Show(String.Format("Are you sure you want to delete '{0}'?", grdFiles.Rows[r.Index].Cells["FullName"].Value.ToString()), "Delete Confirmation", MessageBoxButtons.YesNo) == DialogResult.No))
                        {
                            try { File.Delete(grdFiles.Rows[r.Index].Cells["FullName"].Value.ToString()); }
                            catch (IOException) { }
                            finally { grdFiles.Rows.RemoveAt(r.Index); }
                        }
                    }
                }
            }

        }

        private void deleteConfirmationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((ToolStripMenuItem)sender).Checked = !((ToolStripMenuItem)sender).Checked;
        }


        private void toolStripSpreadsheet_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog() { FileName = "SingleCopy Export", Filter = "Excel Spreadsheet (*.xlsx)|*.xlsx" };
            if (saveFile.ShowDialog(this) == DialogResult.OK)
            {
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("SingleCopy Export");

                IRow sheetRow = sheet.CreateRow(0);
                //Output Column Headers
                foreach (DataGridViewColumn col in grdFiles.Columns)
                {
                    if (col.Visible)
                    {
                        ICell cell = sheetRow.CreateCell(sheetRow.LastCellNum >= 0 ? sheetRow.LastCellNum : 0);
                        cell.SetCellValue(col.HeaderText);
                    }
                }
                int LastCellNum = sheetRow.LastCellNum - 1;

                //Output Table Content
                foreach (OutlookGridRow row in grdFiles.Rows)
                {
                    if (!row.IsGroupRow)
                    {
                        sheetRow = sheet.CreateRow(sheet.LastRowNum + 1);

                        foreach (DataGridViewCell col in row.Cells)
                        {
                            if (col.Visible)
                            {
                                ICell cell = sheetRow.CreateCell(sheetRow.LastCellNum >= 0 ? sheetRow.LastCellNum : 0);
                                cell.SetCellValue(col.Value?.ToString() ?? "");
                            }
                        }
                    }
                }

                //Formating
                sheet.SetAutoFilter(new NPOI.SS.Util.CellRangeAddress(0, sheet.LastRowNum, 0, LastCellNum));

                //Save
                using (FileStream s = new FileStream(saveFile.FileName, FileMode.Create))
                {
                    workbook.Write(s);
                    s.Close();
                }
            }
        }

        private void frmMaster_Load(object sender, EventArgs e)
        {
            Plugin.PluginManager.GetManager();
        }
    }
}
