/* 
 *Copyright (C) 2019 Peter Varney - All Rights Reserved
 * You may use, distribute and modify this code under the
 * terms of the MIT license, 
 *
 * You should have received a copy of the MIT license with
 * this file. If not, visit : https://github.com/fatalwall/SingleCopy
 */
using System;

namespace SingleCopy
{
    public class ElementChangeEventArgs : EventArgs
    {
        public ElementChangeEventArgs(ElementChangeActionType ActionType, ElementChangeAction Action)
        {
            this.ActionType = ActionType;
            this.Action = Action;
        }
        public ElementChangeActionType ActionType { get; private set; }
        public ElementChangeAction Action { get; private set; }

        public override string ToString() { return string.Format("ActionType: {0}, Action: {1}", this.ActionType, this.Action); }
    }
}
