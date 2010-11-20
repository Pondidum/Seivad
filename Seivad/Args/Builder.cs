using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seivad.Args
{
    public abstract class Builder
    {
        public static Arguments Add(string name, Object value)
        {
            return new Arguments().Add(name, value);
        }
    }
}
