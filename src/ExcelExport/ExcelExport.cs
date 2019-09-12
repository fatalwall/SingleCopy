using System;
using System.ComponentModel.Composition;
using SingleCopy.Plugin;
using System.Windows.Forms;
using System.Linq;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using OutlookStyleControls;
using System.IO;

namespace ExcelExport
{
    [Export(typeof(IButton))]
    [ExportMetadata("ToolBarGroup", "_Export")]
    [ExportMetadata("Weight", 0)]
    [ExportMetadata("DisplayStyle", ToolStripItemDisplayStyle.Image)]
    [ExportMetadata("Text", "Spreadsheet")]
    [ExportMetadata("ToolTip", "Export to Excel *.xlsx")]
    [ExportMetadata("Icon", "spreadsheet")]
    public class ExcelExport : IButton
    {
        public void OnClick(object sender, EventArgs e)
        {
            IButtonMetadata meta = PluginManager.GetMedadata();
            PluginLogger.Trace("{0} has been pressed", meta.Text);

            if (PluginManager.DataGrid.Rows.Count > 0)
            {
                SaveFileDialog saveFile = new SaveFileDialog() { FileName = "SingleCopy Export", Filter = "Excel Spreadsheet (*.xlsx)|*.xlsx" };
                if (saveFile.ShowDialog(PluginManager.Form) == DialogResult.OK)
                {
                    IWorkbook workbook = new XSSFWorkbook();
                    ISheet sheet = workbook.CreateSheet("SingleCopy Export");

                    IRow sheetRow = sheet.CreateRow(0);
                    //Output Column Headers
                    foreach (DataGridViewColumn col in PluginManager.DataGrid.Columns)
                    {
                        if (col.Visible)
                        {
                            ICell cell = sheetRow.CreateCell(sheetRow.LastCellNum >= 0 ? sheetRow.LastCellNum : 0);
                            cell.SetCellValue(col.HeaderText);
                        }
                    }
                    int LastCellNum = sheetRow.LastCellNum - 1;

                    //Output Table Content
                    foreach (OutlookGridRow row in PluginManager.DataGrid.Rows)
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
        }

    }
}
