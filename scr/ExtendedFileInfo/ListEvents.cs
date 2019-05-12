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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleCopy
{
    public class ListEvents<T> : List<T>
    {
        public delegate void ElementChangeEventHandler(object sender, ElementChangeEventArgs e);
        public event ElementChangeEventHandler ElementChange;

        public new void Add(T item)
        {
            base.Add(item);
            if (null != ElementChange) ElementChange(this, new ElementChangeEventArgs(ElementChangeActionType.Add, ElementChangeAction.Add));
        }
        public new void AddRange(IEnumerable<T> collection)
        {
            base.AddRange(collection);
            if (null != ElementChange) ElementChange(this, new ElementChangeEventArgs(ElementChangeActionType.Add, ElementChangeAction.AddRange));
        }
        public new void Insert(int index, T item)
        {
            base.Insert(index, item);
            if (null != ElementChange) ElementChange(this, new ElementChangeEventArgs(ElementChangeActionType.Add, ElementChangeAction.Insert));
        }
        public new void InsertRange(int index, IEnumerable<T> collection)
        {
            base.InsertRange(index, collection);
            if (null != ElementChange) ElementChange(this, new ElementChangeEventArgs(ElementChangeActionType.Add, ElementChangeAction.InsertRange));
        }

        public new void Remove(T item)
        {
            base.Remove(item);
            if (null != ElementChange) ElementChange(this, new ElementChangeEventArgs(ElementChangeActionType.Remove, ElementChangeAction.Remove));
        }
        public new void RemoveAt(int index)
        {
            base.RemoveAt(index);
            if (null != ElementChange) ElementChange(this, new ElementChangeEventArgs(ElementChangeActionType.Remove, ElementChangeAction.RemoveAt));
        }
        public new void RemoveAll(Predicate<T> match)
        {
            base.RemoveAll(match);
            if (null != ElementChange) ElementChange(this, new ElementChangeEventArgs(ElementChangeActionType.Remove, ElementChangeAction.RemoveAll));
        }
        public new void Clear()
        {
            base.Clear();
            if (null != ElementChange) ElementChange(this, new ElementChangeEventArgs(ElementChangeActionType.Remove, ElementChangeAction.Clear));
        }
    }
}
