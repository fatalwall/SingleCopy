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
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vshed.IO
{
    public enum FileInfoCollectionEventType
    {
        Add,
        Remove
    }

    public class FileInfoCollectionEventArgs : EventArgs
    {
        public FileInfoCollectionEventArgs(FileInfoCollectionEventType eventType, FileInfo[] items)
        {
            this.EventType = eventType;
            this.Items = items;
        }

        public FileInfoCollectionEventType EventType { get; private set; }
        public FileInfo[] Items { get; private set; }

        public override string ToString() { return string.Format("Event: {0}, Items Count: {1}", this.EventType, this?.Items.Count() ?? 0); }
    }

    public class FileInfoCollection : Collection<FileInfo>
    {
        public delegate void ItemsChangedEventHandler(object sender, FileInfoCollectionEventArgs e);
        public event ItemsChangedEventHandler ItemsChanged;
        private void CallItemsChanged(object sender, FileInfoCollectionEventArgs e) { if (null != ItemsChanged) ItemsChanged(sender, e); }

        public delegate void ItemsAddedEventHandler(object sender, FileInfoCollectionEventArgs e);
        public event ItemsAddedEventHandler ItemsAdded;
        private void CallItemsAdded(object sender, FileInfoCollectionEventArgs e)
        {
            foreach (FileInfo i in e.Items) { md5Tasks.Add(i.md5sumAsync()); }
            if (null != ItemsAdded) ItemsAdded(sender, e);
            if (null != ItemsChanged) ItemsChanged(sender, e);
        }

        public delegate void ItemsRemovedEventHandler(object sender, FileInfoCollectionEventArgs e);
        public event ItemsRemovedEventHandler ItemsRemoved;
        private void CallItemsRemoved(object sender, FileInfoCollectionEventArgs e)
        {
            if (null != ItemsRemoved) ItemsRemoved(sender, e);
            if (null != ItemsChanged) ItemsChanged(sender, e);
        }

        private List<Task> md5Tasks = new List<Task>();
        public void WaitMd5() { Task.WaitAll(md5Tasks.Where(t => !(t is null)).ToArray()); }

        public new FileInfo this[int index]
        {
            get { return base[index]; }
            set
            {
                base[index] = value;
                CallItemsChanged(this, new FileInfoCollectionEventArgs(FileInfoCollectionEventType.Add, new FileInfo[] { value }));
            }
        }

        public new void Add(FileInfo item)
        {
            base.Add(item);
            CallItemsAdded(this, new FileInfoCollectionEventArgs(FileInfoCollectionEventType.Add, new FileInfo[] { item }));
        }
        public void AddRange(IEnumerable<FileInfo> items)
        {
            foreach (FileInfo item in items)
                base.Add(item);
            CallItemsAdded(this, new FileInfoCollectionEventArgs(FileInfoCollectionEventType.Add, items.ToArray<FileInfo>()));
        }
        public new void Clear()
        {
            CallItemsRemoved(this, new FileInfoCollectionEventArgs(FileInfoCollectionEventType.Remove, this.ToArray<FileInfo>()));
            base.Clear();
        }
        public new void Insert(int index, FileInfo item)
        {
            base.Insert(index, item);
            CallItemsChanged(this, new FileInfoCollectionEventArgs(FileInfoCollectionEventType.Add, new FileInfo[] { item }));
        }
        public new void Remove(FileInfo item)
        {
            CallItemsRemoved(this, new FileInfoCollectionEventArgs(FileInfoCollectionEventType.Remove, new FileInfo[] { item }));
            base.Remove(item);
        }
        public new void RemoveAt(int index)
        {
            CallItemsRemoved(this, new FileInfoCollectionEventArgs(FileInfoCollectionEventType.Remove, new FileInfo[] { base[index] }));
            base.RemoveAt(index);
        }
    }
}
