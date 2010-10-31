using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seivad.Arguments
{
    public sealed class Argument
    {
        public string Name { get; private set; }
        public Object Value { get; private set; }

        public Argument(string name, Object value)
        {
            Name = name;
            Value = value;
        }
    }
}
