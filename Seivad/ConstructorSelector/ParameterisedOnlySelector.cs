using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Seivad.ConstructorSelector
{
    class ParameterisedOnlySelector : IConstructorSelector
    {
        public bool CanHandle(IList<ConstructorInfo> constructors, IArguments arguments)
        {
            return constructors.All(c => c.GetParameters().Count() > 0) && arguments.Count > 0;
        }
        
        public ConstructorInfo GetConstructor(IList<ConstructorInfo> constructors, IArguments arguments)
        {
            if (!CanHandle(constructors, arguments))
            {
                throw new ConstructorException("No matching constructor found");
            }

            var matches = ConstructorsWithAllArguments(constructors, arguments);

            if (matches.Count() == 0)
            {
                throw new ConstructorException("No matching constructor found");
            }

            if (matches.Count() == 1)
            {
                return matches.First();
            }




        }

        private List<ConstructorInfo> ConstructorsWithAllArguments(IEnumerable<ConstructorInfo> constructors, IArguments args)
        {
            //get constructors that contain all of the args
            return constructors.Where(c => args.ToDictionary().Select(d => d.Key).All(name => c.GetParameters().Select(p => p.Name).Contains(name)))
                               .OrderBy(c => c.GetParameters().Count())
                               .ToList();
        }

        private ConstructorInfo ConstructorWithBestParameterOrder(IEnumerable<ConstructorInfo> constructors, IArguments args)
        {
            var matches = new List<ConstructorInfo>();
            matches.AddRange(constructors.Where(c => c.GetParameters().Count() == args.Count));

            if (matches.Count() == 0)
            {
                throw new ConstructorException("No matching constructor found");
            }

            if (matches.Count() == 1)
            {
                return matches.First();
            }

            var ctors = new List<List<ConstructorInfo>>();

            for (var i = 0; i < args.Count; i++)
            {
                ctors.Add(constructors.Where(c => c.GetParameters[i].Name == args.ToDictionary[i].Key));
            }

           //bestmatch = an item in all lists

        }

    }
}
