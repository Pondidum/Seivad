using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seivad.Args
{
    public sealed class Arguments : List<Argument>
    {

        public Arguments Add(string name, object value)
        {
            base.Add(new Argument(name, value));
            return this;
        }
    }
}
