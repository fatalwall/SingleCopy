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
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace vshed.Control
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class OutlookGridAttribute : Attribute
    {
        public OutlookGridAttribute() { }

        //private bool _TreatAsProperty = false;
        [DefaultValue(false)]
        public bool TreatAsProperty { get; set; } = false;

        public static MethodInfo[] GetMethods(Type type)
        {
            return Assembly.GetCallingAssembly().GetTypes()
                            .Where(t => t.IsSealed && !t.IsGenericType && !t.IsNested)
                            .SelectMany(t => t.GetMethods(BindingFlags.Static | BindingFlags.Public)
                                .Where(m => m.IsDefined(typeof(OutlookGridAttribute), true))
                            ).Where(m => m.GetCustomAttribute<OutlookGridAttribute>().TreatAsProperty == true)
                            .ToArray();
        }
    }
}
