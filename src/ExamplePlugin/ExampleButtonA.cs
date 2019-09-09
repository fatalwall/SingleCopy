using System;
using System.ComponentModel.Composition;
using SingleCopy.Plugin;
using System.Windows.Forms;
using System.Linq;

namespace ExamplePlugin
{
    [Export(typeof(IButton))]
    [ExportMetadata("ToolBarGroup", "Example Buttons")]
    [ExportMetadata("DisplayStyle", ToolStripItemDisplayStyle.Image)]
    [ExportMetadata("Text", "Button A")]
    [ExportMetadata("ToolTip", "Example Button A")]
    [ExportMetadata("Icon", "ButtonA")]
    public class ExampleButtonA : IButton
    {
        public void OnClick(object sender, EventArgs e)
        {
            IButtonMetadata meta = PluginManager.GetMedadata();
            
            MessageBox.Show(string.Format("{0} has been pressed\r\n\r\nDataGrid contains {1} rows", meta.Text, PluginManager.DataGrid.Rows.Count), this.GetType().Assembly.GetName().Name, MessageBoxButtons.OK);
            PluginLogger.Debug("{0} has been pressed", meta.Text);
        }
    }
}
