using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seivad
{
    
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

   

}
