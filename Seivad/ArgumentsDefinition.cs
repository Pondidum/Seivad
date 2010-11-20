using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace Seivad
{
    internal sealed class ArgumentsDefinition : IArguments
    {
        private readonly List<Tuple<string, object>> _arguments;

        internal ArgumentsDefinition()
        {
            _arguments = new List<Tuple<string, object>>();
        }

        public IArguments Add(string name, object value)
        {
            if (_arguments.Select(x => x.Item1).Contains(name))
            {
                throw new ArgumentException(string.Format("The argument '{0}' has been specified already", name));
            }

            _arguments.Add(Tuple.Create(name, value));

            return this;
        }

        public IDictionary<string, object> ToDictionary()
        {
            var result = new OrderedDictionary();

            _arguments.ForEach(a => result.Add(a.Item1, a.Item2));

            return result;
        }

        public int Count { get { return _arguments.Count; } }

    }
}
