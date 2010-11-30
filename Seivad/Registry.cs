using System;
using System.Collections.Generic;
using Seivad.Args;

namespace Seivad
{
    internal  class Registry : Dictionary<Type, ObjectCreator>, IRegistry
    {

        public bool ContainsAll(IEnumerable<Type> types)
        {
            return false;
        }

        public T Get<T>(Arguments args)
        {
            var type = typeof(T);

            if (!ContainsKey(type))
                throw new RegistrationException(string.Format("The type {0} has not been registered", type.Name));

            return (T)this[type].GetInstance(args);
        }
    }
}
