using System;
using System.Collections.Generic;
using Seivad.Args;

namespace Seivad
{
    public interface IRegistry
    {
        bool ContainsAll(IEnumerable<Type> types);
        T Get<T>(Arguments args);
    }
}
