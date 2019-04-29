using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
