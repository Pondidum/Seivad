using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seivad
{
    interface IRegistry
    {
        bool Contains(Type requestedType);
    }
}
