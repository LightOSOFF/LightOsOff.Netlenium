using System;
using System.Runtime.Serialization;

namespace Intellivoid.Netlenium
{
    [Serializable]
    public class SessionErrorException : Exception
    {
        public SessionErrorException()
        {
        }

        public SessionErrorException(string message) : base(message)
        {
        }

        public SessionErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SessionErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}