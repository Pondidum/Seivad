using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seivad
{
    internal class ObjectCreator
    {
        public Type ReturnType { get; set; }
        public bool IsSingleton { get; set; }

        internal ObjectCreator()
        {
            ReturnType = null;
            IsSingleton = false;
        }

        public Object GetInstance()
        { 
            
        }
    }
}
