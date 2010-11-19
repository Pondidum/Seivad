using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seivad.ConstructorSelector
{
    class ConstructorFactory
    {
        private readonly static IList<IConstructorSelector> _handler;

        static ConstructorFactory()
        {
            _handler = new List<IConstructorSelector>();

            _handler.Add(new DefaultOnlySelector());
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
    }
}
