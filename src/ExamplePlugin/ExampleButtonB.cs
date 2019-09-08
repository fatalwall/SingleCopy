using System;
using System.ComponentModel.Composition;
using SingleCopy.Plugin;
using System.Windows.Forms;
using System.Linq;

namespace ExamplePlugin
{
    [Export(typeof(IButton))]
    [ExportMetadata("Name", "Example Plugin")]
    [ExportMetadata("ToolBarGroup", "Example Buttons")]
    [ExportMetadata("DisplayStyle", ToolStripItemDisplayStyle.ImageAndText)]
    [ExportMetadata("Text", "Button B")]
    [ExportMetadata("ToolTip", "Example Button B")]
    [ExportMetadata("Icon", "ButtonB")]
    public class ExampleButtonB : IButton
    {
        public void OnClick(object sender, EventArgs e)
        {
            MessageBox.Show(string.Format("{0} has been pressed", (string)MetadataAttribute("Text")), (string)MetadataAttribute("Name"), MessageBoxButtons.OK);
        }

        protected object MetadataAttribute(string Name)
        {
            foreach (var a in this.GetType().GetCustomAttributes(typeof(ExportMetadataAttribute), true).OfType<ExportMetadataAttribute>().Where(a => a.Name == Name))
            {
                return a.Value;
            }
            return null;
        }
    }
}
