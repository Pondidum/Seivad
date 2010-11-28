using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Seivad.Args
{
    public class Arguments : List<Argument>
    {

        internal  Arguments()
        {
            
        }

        internal Arguments(IEnumerable<Argument> args)
        {
            foreach (var argument in args)
            {
                Add(argument);
            }
        }


        public Arguments Add(string name, object value)
        {
            Add(new Argument(name, value));
            return this;
        }

        /// <summary>Checks if a given arg name is in the collection</summary>
        /// <param name="name">Case sensitive</param>
        public bool Contains(string name)
        {
            return this.Any(a => a.Name == name);
        }

        public bool Contains(ParameterInfo param)
        {
            var paramType = param.ParameterType;
            return this.Any(a => a.Name == param.Name && a.Value.GetType() == paramType);
        }

        internal Arguments FilteredFor(ConstructorInfo constructor)
        {

            var paramNames = constructor.GetParameters().Select(p => p.Name);

            var args = this.Where(a => paramNames.Contains(a.Name));

            return new Arguments(args);
        }

    }

}
