using System;
using SingleCopy.Plugin;
using System.Windows.Forms;
using System.ComponentModel.Composition;

namespace ExamplePlugin
{
    [Export(typeof(IMenu))]
    [ExportMetadata("Text", "Menu C")]
    [ExportMetadata("ToolTip", "Example Menu C")]
    [ExportMetadata("Icon", "MenuC")]
    public class ExampleMenuC : IMenu
    {
        public ToolStripMenuItem[] DropDownItems => throw new NotImplementedException();

        public void OnClick(object sender, EventArgs e)
        {
            IMenuMetadata meta = PluginManager.GetMedadata();

            MessageBox.Show(string.Format("{0} has been pressed\r\n\r\nDataGrid contains {1} rows", meta.Text, PluginManager.DataGrid.Rows.Count), this.GetType().Assembly.GetName().Name, MessageBoxButtons.OK);
            PluginLogger.Debug("{0} has been pressed", meta.Text);
        }
    }
}
