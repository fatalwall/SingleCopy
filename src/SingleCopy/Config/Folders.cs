/* 
 *Copyright (C) 2019 Peter Varney - All Rights Reserved
 * You may use, distribute and modify this code under the
 * terms of the MIT license, 
 *
 * You should have received a copy of the MIT license with
 * this file. If not, visit : https://github.com/fatalwall/SingleCopy
 */
using System.Configuration;
using System.Reflection;
using System.Linq;

namespace SingleCopy.Config
{
    public class Folders : ConfigurationSection
    {
        private static Folders _instance;
        public static Folders getCurrentInstance()
        {
            if (_instance is null)
            {
                _instance = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).Sections.OfType<Folders>().FirstOrDefault() as Folders
                    ?? ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location).Sections.OfType<Folders>().FirstOrDefault() as Folders;
            }
            return _instance;
        }

        [ConfigurationProperty("", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(ExcludeCollection),
            AddItemName = "add",
            ClearItemsName = "Clear",
            RemoveItemName = "Remove")]
        public ExcludeCollection Excludes
        {
            get { return ((ExcludeCollection)base[""]) ?? new ExcludeCollection(); }
        }
    }
}