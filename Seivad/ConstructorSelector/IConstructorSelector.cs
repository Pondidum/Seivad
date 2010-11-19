using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seivad.ConstructorSelector
{
    interface IConstructorSelector
    {
        bool CanHandle(IList<ConstructorInfo> constructors, IArguments arguments);
        ConstructorInfo GetConstructor(IList<ConstructorInfo> constructors, IArguments arguments);
    }
}
