using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Seivad.ConstructorSelector
{
    class ConstructorFactory
    {
        private readonly static IList<IConstructorSelector> _handler;

        static ConstructorFactory()
        {
            _handler = new List<IConstructorSelector>();

            _handler.Add(new DefaultOnlySelector());
            _handler.Add(new ParameterisedOnlySelector());

        }

        static IConstructorSelector GetConstructorHandler(IList<ConstructorInfo> constructors, IArguments arguments)
        {
            var handler = _handler.Where(h => h.CanHandle(constructors, arguments));

            if (handler.Count() == 0)
            {
                throw new NotImplementedException("No ConstructorHandler for this type has been implemented");
            }

            return handler.First();
        }

        static ConstructorInfo GetConstructor(IList<ConstructorInfo> constructors, IArguments arguments, IRegistry registry )
        {

            if (constructors == null || constructors.Count == 0) throw new ConstructorException("No matching Constructors found");

            //default constructor only
            if (constructors.All(c => c.GetParameters().Count() == 0))
            {
                if (arguments.Count > 0)
                {
                    throw new ConstructorException("No matching Constructors found");
                }
                else
                {
                    return constructors.First();    //you can only have one default constructor...right?
                }

            }

            //only parameterised constructors
            if (constructors.All(c => c.GetParameters().Count() > 0))
            {
                var sameLength = constructors.Where(c => c.GetParameters().Count() == arguments.Count);
                var containsAll = sameLength.Where(c => c.GetParameters().All(p => arguments.contains(p)));
            }

            //mixed 

        }
    }
}
