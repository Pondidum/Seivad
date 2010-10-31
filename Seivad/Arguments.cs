using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seivad
{

    public interface IArguments
    {
        IArguments Add(string name, object value);
        IDictionary<string, object> GetArguments();
    }
    
    public sealed class Arguments
    {

        private Arguments()
        {
        }

        public static IArguments  Add(string name, Object value)
        {
            return new ArgumentsDefinition().Add(name, value);
        }
        
    }

    public sealed class ArgumentsDefinition :IArguments
    {
        private readonly IDictionary<string, object> _arguments;

        internal ArgumentsDefinition()
        {
            _arguments = new Dictionary<string, object>();
        }

        public IArguments Add(string name, object value)
        {
            _arguments.Add(name, value);
            return this;
        }

        public IDictionary<string, object> GetArguments()
        {
            return _arguments;
        }

    }

}
