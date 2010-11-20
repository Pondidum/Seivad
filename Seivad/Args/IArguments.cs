using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seivad.Args
{
    public interface IArguments : IList<Argument>
    {
        IArguments Add(string name, Type type);
    }

}
