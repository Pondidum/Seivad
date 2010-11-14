using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seivad
{
    public class ConstructorException : Exception
    {
    
        public ConstructorException() : base() { }
        public ConstructorException(string message) : base(message) { }
        public ConstructorException(string message, Exception innerException) : base(message, innerException) { }
        public ConstructorException(SerializationInfo info, StreamingContext context) : base(info,context) { }

    }
}
