// Copyright 2006 Herre Kuijpers - <herre@xs4all.nl>
//
// This source file(s) may be redistributed, altered and customized
// by any means PROVIDING the authors name and all copyright
// notices remain intact.
// THIS SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED. USE IT AT YOUR OWN RISK. THE AUTHOR ACCEPTS NO
// LIABILITY FOR ANY DATA DAMAGE/LOSS THAT THIS PRODUCT MAY CAUSE.
//-----------------------------------------------------------------------
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.IO;
using vshed.IO;

namespace OutlookStyleControls
{
    public class FileInfoComparer : IComparer
    {
        ListSortDirection direction;
        int columnIndex;

        public FileInfoComparer(int columnIndex, ListSortDirection direction)
        {
            this.columnIndex = columnIndex;
            this.direction = direction;
        }

        #region IComparer Members

        public int Compare(object x, object y)
        {
            FileInfo obj1 = (FileInfo)x;
            FileInfo obj2 = (FileInfo)y;
            return string.Compare(obj1.md5sum()??"", obj2.md5sum()??"") * (direction == ListSortDirection.Ascending ? 1 : -1);
            //return string.Compare(obj1[columnIndex].ToString(), obj2[columnIndex].ToString()) * (direction == ListSortDirection.Ascending ? 1 : -1);
        }
        #endregion
    }
}
