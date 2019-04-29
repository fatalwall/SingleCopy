/* 
 *Copyright (C) 2019 Peter Varney - All Rights Reserved
 * You may use, distribute and modify this code under the
 * terms of the MIT license, 
 *
 * You should have received a copy of the MIT license with
 * this file. If not, visit : https://github.com/fatalwall/SingleCopy
 */
using System;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Data;

namespace SingleCopy
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMaster());
        }

        public static ListEvents<FileInfo> files = new ListEvents<FileInfo>();
        public static DataTable Table = null;

        public static void getFiles(String Path, String[] Exclude = null)
        {
            if (Exclude != null && Exclude.Contains(Path)) return;
            DirectoryInfo dir = new DirectoryInfo(Path);
            if (dir.GetFiles().Count() > 0)
                files.AddRange(dir.GetFiles());
            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                try
                {
                    if (d.FullName.ToLower().IndexOf("$recycle.bin") == -1)
                        getFiles(d.FullName, Exclude);
                }
                catch (Exception ex)
                { Console.WriteLine(ex.GetType().ToString() + " - " + d.FullName); }
            }
        }
    }
}
