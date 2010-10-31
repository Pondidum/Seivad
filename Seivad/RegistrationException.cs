using System;
using System.Runtime.Serialization;


namespace Seivad
{
    class RegistrationException : Exception
    {
        public RegistrationException(string message)
            : base(message)
        { }

        public RegistrationException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public RegistrationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

    }
}
