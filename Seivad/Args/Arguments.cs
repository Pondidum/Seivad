using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Seivad.Args
{
    public class Arguments : List<Argument>
    {
        public Arguments Add(string name, object value)
        {
            base.Add(new Argument(name, value));
            return this;
        }

        /// <summary>Checks if a given arg name is in the collection</summary>
        /// <param name="name">Case sensitive</param>
        public bool Contains(string name)
        {
            return this.Any(a => a.Name == name);
        }

        public bool Contains(ParameterInfo param)
        {
            var paramType = param.ParameterType;
            return this.Any(a => a.Name == param.Name && a.Value.GetType() == paramType);
        }

    }
}
