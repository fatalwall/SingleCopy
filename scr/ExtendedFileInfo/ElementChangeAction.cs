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
    public enum ElementChangeAction
    {
        Add,
        AddRange,
        Insert,
        InsertRange,
        Remove,
        RemoveAt,
        RemoveAll,
        Clear
    }
}
