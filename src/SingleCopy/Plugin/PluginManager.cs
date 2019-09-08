/* 
 *Copyright (C) 2019 Peter Varney - All Rights Reserved
 * You may use, distribute and modify this code under the
 * terms of the MIT license, 
 *
 * You should have received a copy of the MIT license with
 * this file. If not, visit : https://github.com/fatalwall/SingleCopy
 */
using OutlookStyleControls;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SingleCopy.Plugin
{
    public sealed class PluginManager
    {
        private static readonly PluginManager manager = new PluginManager();
        public static PluginManager GetManager() => manager;


        private static frmMaster Form => Application.OpenForms.OfType<frmMaster>().Single();

        public static OutlookGrid DataGrid => Form.Controls.OfType<SplitContainer>().Single().Panel1.Controls.OfType<OutlookGrid>().Single();
        public static void StartScan(string Path) { Form.StartScan(Path); }
        public static ToolStrip toolStrip => Form.Controls.OfType<ToolStrip>().Where(t => t.Name=="toolStrip1").Single();

        #region "Fetch Metadata"
        public static dynamic GetMedadata()
        {
            return GetMedadata((new StackTrace()).GetFrame(1).GetMethod().ReflectedType);
        }
        public static dynamic GetMedadata(Type type)
        {
            if (type.GetInterfaces().Contains(typeof(IButton)))
                return GetManager().Buttons.Where(p => p.Value.GetType() == type).Single().Metadata;
            if (type.GetInterfaces().Contains(typeof(IMenu)))
                return GetManager().Menus.Where(p => p.Value.GetType() == type).Single().Metadata;
            if (type.GetInterfaces().Contains(typeof(IPreScanAction)))
                return GetManager().PreScanActions.Where(p => p.Value.GetType() == type).Single().Metadata;
            if (type.GetInterfaces().Contains(typeof(IPostScanAction)))
                return GetManager().PostScanActions.Where(p => p.Value.GetType() == type).Single().Metadata;
            return null;//If not one of the above types
        }
        #endregion

        #region "Plugin Module Types"
        [ImportMany]
        private IEnumerable<Lazy<IButton, IButtonMetadata>> Buttons;

        private void InitilizeButtons()
        {
            foreach(var button in Buttons)
            {
                //Create Seperator if needed for grouping
                if (!toolStrip.Items.ContainsKey("ToolStripSeparator" + button.Metadata.ToolBarGroup))
                    toolStrip.Items.Add(new ToolStripSeparator() { Name = "ToolStripSeparator" + button.Metadata.ToolBarGroup });

                //Load Resource Manager for Icons
                var resourceManager = new System.Resources.ResourceManager(button.Value.GetType().Assembly.GetName().Name + ".Properties.Resources"
                    , button.Value.GetType().Assembly);

                //Create button
                toolStrip.Items.Insert(toolStrip.Items.IndexOfKey("ToolStripSeparator" + button.Metadata.ToolBarGroup)
                    , new ToolStripButton(button.Metadata.Text, (Bitmap)resourceManager.GetObject(button.Metadata.Icon), button.Value.OnClick)
                        { ToolTipText = button.Metadata.ToolTip, DisplayStyle = button.Metadata.DisplayStyle});
 
            }
        }

        [ImportMany]
        private IEnumerable<Lazy<IMenu, IMenuMetadata>> Menus;

        [ImportMany]
        private IEnumerable<Lazy<IPostScanAction, IPostScanActionMetadata>> PostScanActions;

        [ImportMany]
        private IEnumerable<Lazy<IPreScanAction, IPreScanActionMetadata>> PreScanActions;
        #endregion

        public PluginManager()
        {
            AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolver;
            DoImport();
            InitilizeButtons();
        }

        private void DoImport()
        {
            var catalog = new AggregateCatalog();

            //Load from executing assemblies plugin subdirectory
            try
            {
                //Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)   
                catalog.Catalogs.Add(new DirectoryCatalog(@".\Plugins"));
                foreach (var path in System.IO.Directory.EnumerateDirectories(@".\Plugins", "*", System.IO.SearchOption.TopDirectoryOnly))
                {
                    catalog.Catalogs.Add(new DirectoryCatalog(path));
                }
            }
            catch
            {
                catalog.Catalogs.Add(new DirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory));
                foreach (var path in System.IO.Directory.EnumerateDirectories(AppDomain.CurrentDomain.BaseDirectory, "*", System.IO.SearchOption.TopDirectoryOnly))
                {
                    catalog.Catalogs.Add(new DirectoryCatalog(path));
                }
            }
            CompositionContainer container = new CompositionContainer(catalog);

            try { container.ComposeParts(this); }
            catch (System.Reflection.ReflectionTypeLoadException) { }//if no modules load oh well
        }

        #region "Assembly Resolution"
        private static System.Reflection.Assembly AssemblyResolver(object sender, ResolveEventArgs args)
        {
            string exePath = System.IO.Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase).LocalPath);

            string[] DirectoryList =
                {
                    exePath, //App Folder
                    System.IO.Path.Combine(exePath, "Plugins"), //App/Plugins Folder
                    System.IO.Path.Combine(exePath, "Plugins", args.Name), //App/Plugins/PluginName Folder
                    Environment.SystemDirectory, //System32 Folder
                    Environment.GetFolderPath(Environment.SpecialFolder.Windows) //Windows Folder
                };

            System.Reflection.Assembly assembly = null;
            foreach (string path in DirectoryList)
            {
                if (TryGetAssembly(path, args.Name, out assembly)) return assembly;
            }

            PluginLogger.Error("{0} could not be found in any of the following locations:\r\n\t{1}\r\n\t{2}\r\n\t{3}\r\n\t{4}\r\n\t{5}\r\n", args.Name, DirectoryList);
            return null;
        }

        private static Assembly GetAssembly(string directory, string Name)
        {
            string path = Path.Combine(directory, string.Format("{0}.dll", Name));
            try { return File.Exists(path) ? Assembly.LoadFile(path) : null; }
            catch (Exception ex)
            {
                throw new ArgumentException(string.Format("Assembly could not be found at {0}",path), ex);
            }
        }

        private static bool TryGetAssembly(string directory, string Name, out Assembly assembly)
        {
            string path = Path.Combine(directory, string.Format("{0}.dll", Name));
            try
            {
                if (File.Exists(path)) assembly = Assembly.LoadFile(path);
                else assembly = null;
            }
            catch (Exception) { assembly = null; }

            return assembly is null ? false : true;
        }
        #endregion
    }
}
