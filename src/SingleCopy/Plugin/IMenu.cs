/* 
 *Copyright (C) 2019 Peter Varney - All Rights Reserved
 * You may use, distribute and modify this code under the
 * terms of the MIT license, 
 *
 * You should have received a copy of the MIT license with
 * this file. If not, visit : https://github.com/fatalwall/SingleCopy
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SingleCopy.Plugin
{
    public interface IMenu
    {
        ToolStripMenuItem[] DropDownItems { get; }
        void OnClick(object sender, EventArgs e);
    }

    public interface IMenuMetadata
    {
        string Text { get; }
        string ToolTip { get; }
        string Icon { get; }
    }
}
