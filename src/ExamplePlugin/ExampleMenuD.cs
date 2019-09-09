using System;
using SingleCopy.Plugin;
using System.Windows.Forms;
using System.ComponentModel.Composition;

namespace ExamplePlugin
{
    [Export(typeof(IMenu))]
    [ExportMetadata("Text", "Menu D")]
    [ExportMetadata("ToolTip", "Example Menu D")]
    [ExportMetadata("Icon", "MenuD")]
    public class ExampleMenuD : IMenu
    {
        public ToolStripMenuItem[] DropDownItems => new ToolStripMenuItem[] 
            {
                new ToolStripMenuItem("SubMenuA",null,SubMenuOnClick),
                new ToolStripMenuItem("SubMenuB",null,SubMenuOnClick)
            }; 
        

        public void OnClick(object sender, EventArgs e)
        {
            IMenuMetadata meta = PluginManager.GetMedadata();

            MessageBox.Show(string.Format("{0} has been pressed", meta.Text), this.GetType().Assembly.GetName().Name, MessageBoxButtons.OK);
            PluginLogger.Debug("{0} has been pressed", meta.Text);
        }

        public void SubMenuOnClick(object sender, EventArgs e)
        {
            MessageBox.Show(string.Format("{0} has been pressed", ((ToolStripMenuItem)sender).Text), this.GetType().Assembly.GetName().Name, MessageBoxButtons.OK);
            PluginLogger.Debug("{0} has been pressed", ((ToolStripMenuItem)sender).Text);
        }


    }
}
