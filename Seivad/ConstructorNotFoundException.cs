using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seivad
{
    public class ConstructorNotFoundException : Exception
    {
    
        public ConstructorNotFoundException() : base() { }
        public ConstructorNotFoundException(string message) : base(message) { }
        public ConstructorNotFoundException(string message, Exception innerException) : base(message, innerException) { }
        public ConstructorNotFoundException(SerializationInfo info, StreamingContext context) : base(info,context) { }

    }
}
