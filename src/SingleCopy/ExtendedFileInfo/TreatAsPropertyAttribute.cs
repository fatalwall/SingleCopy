using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vshed.IO
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class TreatAsProperty : Attribute
    {
        public TreatAsProperty(bool value)
        {
            this.Value = value;
        }
        public virtual bool Value { get; private set; }
    }
}
