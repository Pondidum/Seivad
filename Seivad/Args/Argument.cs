using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seivad.Args
{
    public sealed class Argument
    {

        public readonly static Argument Empty = new Argument();

        public string Name { get; private set; }
        public object Value { get; private set; }
        
        private Argument()
        {
            Value = null;
            Name = string.Empty;
        }

        public Argument(string name, Object value)
        {
            Name = name;
            Value = value;
        }

    }
}
