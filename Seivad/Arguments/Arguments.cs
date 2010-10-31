using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seivad.Arguments
{
    public abstract class Arguments
    {
        public static Argument Add(string name, Object value)
        {
            return new Argument(name, value);
        }
    }
}
