using System;
using System.ComponentModel.Composition;
using SingleCopy.Plugin;
using System.Windows.Forms;
using System.Linq;

namespace ExamplePlugin
{
    [Export(typeof(IButton))]
    [ExportMetadata("ToolBarGroup", "Example Buttons")]
    [ExportMetadata("Weight", -1)]
    [ExportMetadata("DisplayStyle", ToolStripItemDisplayStyle.ImageAndText)]
    [ExportMetadata("Text", "Button B")]
    [ExportMetadata("ToolTip", "Example Button B")]
    [ExportMetadata("Icon", "ButtonB")]
    public class ExampleButtonB : IButton
    {
        public void OnClick(object sender, EventArgs e)
        {
            IButtonMetadata meta = PluginManager.GetMedadata(this.GetType());

            MessageBox.Show(string.Format("{0} has been pressed", meta.Text), this.GetType().Assembly.GetName().Name, MessageBoxButtons.OK);
            PluginLogger.Debug("{0} has been pressed", meta.Text);
        }
    }
}
