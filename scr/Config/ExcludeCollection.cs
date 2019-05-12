/* 
 *Copyright (C) 2019 Peter Varney - All Rights Reserved
 * You may use, distribute and modify this code under the
 * terms of the MIT license, 
 *
 * You should have received a copy of the MIT license with
 * this file. If not, visit : https://github.com/fatalwall/SingleCopy
 */
using System;
using System.Collections.Generic;
using System.Configuration;

namespace SingleCopy.Config
{
    public class ExcludeCollection : ConfigurationElementCollection
    {
        public int IndexOf(String Folder)
        {
            for (int idx = 0; idx < base.Count; idx++)
            {
                if (this[idx].Folder.ToUpper() == Folder.ToUpper())
                    return idx;
            }
            return -1;
        }

        public ExcludeElement this[int index]
        {
            get { return (ExcludeElement)BaseGet(index); }
        }

        public new ExcludeElement this[String Folder]
        {
            get
            {
                if (IndexOf(Folder) < 0) return null;
                return (ExcludeElement)BaseGet(Folder);
            }
        }

        public void Add(ExcludeElement c) { BaseAdd(c); }

        public void Remove(ExcludeElement c) { if (BaseIndexOf(c) >= 0) BaseRemove(c.Folder); }

        public void RemoveAt(int index) { BaseRemoveAt(index); }

        public void Remove(String Folder) { BaseRemove(Folder); }

        public void Clear() { BaseClear(); }

        public new int Count() { return base.Count; }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ExcludeElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ExcludeElement)element).Folder;
        }

        protected override string ElementName
        {
            get
            {
                return "Exclude";
            }
        }

        public bool Match(string Folder)
        {
            foreach (ExcludeElement e in this)
            {
                if (e.Match(Folder)) return true;
            }
            return false;
        }

        public new IEnumerator<ExcludeElement> GetEnumerator() { foreach (ExcludeElement e in this) { yield return e; } }
    }
}