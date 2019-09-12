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

namespace SingleCopy.Plugin
{
    public interface IButton
    {
        void OnClick(object sender, EventArgs e);
    }

    public interface IButtonMetadata
    {
        string ToolBarGroup { get; }
        int Weight { get; }
        System.Windows.Forms.ToolStripItemDisplayStyle DisplayStyle { get; }
        string Text { get; }
        string ToolTip { get; }
        string Icon { get; }      
    }
}
