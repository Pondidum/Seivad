using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seivad.ConstructorSelector
{
    class DefaultOnlySelector : IConstructorSelector
    {
        public bool CanHandle(IList<ConstructorInfo> constructors, IArguments arguments)
        {
            return _constructors.All(c => c.GetParameters().Count() == 0) && arguments.Count == 0;
        }

        public ConstructorInfo GetConstructor(IList<ConstructorInfo> constructors, IArguments arguments)
        {
            if (!CanHandle(_constructors, arguments))
            {
                throw new ConstructorException("No matching constructor found");
            }

            //is it even possible to have a type with more than one parameterless constructor?
            return _constructors.First();
        }
    }
}
