using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Seivad
{
    public class NoConstructorFoundException : Exception
    {
    
        public NoConstructorFoundException() : base() { }
        public NoConstructorFoundException(string message) : base(message) { }
        public NoConstructorFoundException(string message, Exception innerException) : base(message, innerException) { }
        public NoConstructorFoundException(SerializationInfo info, StreamingContext context) : base(info,context) { }

    }
}
