using System;
using System.Runtime.Serialization;

namespace Intellivoid.Netlenium
{
    [Serializable]
    public class InvalidProxySchemeException : Exception
    {
        public InvalidProxySchemeException()
        {
        }

        public InvalidProxySchemeException(string message) : base(message)
        {
        }

        public InvalidProxySchemeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidProxySchemeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}