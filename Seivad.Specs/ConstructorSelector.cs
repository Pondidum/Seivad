using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Seivad.Specs
{
    class ConstructorSelector
    {

        public static ConstructorInfo SelectBestConstructor(IList<ConstructorInfo> constructors, IArguments args)
        {
            if (constructors.Count == 0) throw new NoConstructorFoundException();

           

        }

        private interface IConstructorSelector
        {
            bool Handles(IList<ConstructorInfo> constructors, IArguments args);
            ConstructorInfo GetConstructor(IList<ConstructorInfo> constructors, IArguments args);

        }

        private class NoArgumentsSelector : IConstructorSelector
        {

            public bool Handles(IList<ConstructorInfo> constructors, IArguments args)
            {
                return args.Count == 0;
            }

            public ConstructorInfo GetConstructor(IList<ConstructorInfo> constructors, IArguments args)
            {

                var matches = constructors.Where(c => c.GetParameters().Count() == 0);

                if (matches.Any()) {
                    return matches.First();
                }

                //TODO: get a list of parameters we can resolve and see if any ctors match those
            }
        }
    }
}
